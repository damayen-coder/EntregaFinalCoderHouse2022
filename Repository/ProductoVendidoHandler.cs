using System.Data;
using System.Data.SqlClient;
using ProyectoFinalJoseArmando.Modulos;

namespace ProyectoFinalJoseArmando.Repository
{
    public class ProductoVendidoHandler
    {

        public static List<ProductoVendido> TraerProductosVendidosPorIdVenta(int idVenta)
        //Recive una ID de una venta y devuelve una lista de productos vendidos.
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido> { };


            
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM ProductoVendido  WHERE idVenta = '" + idVenta + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ProductoVendido prod = new ProductoVendido((int)dr.GetInt64(0), dr.GetInt32(1), Convert.ToInt32(dr.GetValue(3)), (int)dr.GetInt64(2));
                            listaProductosVendidos.Add(prod);
                        }
                    }
                }
                cnn.Close();
            }

            return listaProductosVendidos;
        }



        public static List<Producto> TraerProductosVendidos(int idUsuario)
        //Recivo un UserID y devuelve una lista de productos vendidos
        {
            string query = "";
            bool ingresado = false;
            List<Producto> productos = new List<Producto> { };
            var listaProductos = ProductoHandler.TraerProductoByUserID(idUsuario);
            var listaProductosVendidos = productos;
            foreach (Producto prod in listaProductos)
            {
                if (idUsuario == prod.IdUsuario)
                {
                    if (ingresado)
                    {
                        query = query + "," + prod.Id;
                    }
                    else
                    {
                        ingresado = true;
                        query = prod.Id.ToString();
                    }
                }
            }

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM ProductoVendido INNER JOIN Producto ON producto.id = productoVendido.IdProducto"
                    + " WHERE idProducto in (" + query + ")", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Producto prod = new Producto((int)dr.GetInt64(0), dr.GetString(5), Convert.ToDouble(dr.GetDecimal(7)), Convert.ToDouble(dr.GetDecimal(6)), Convert.ToInt32(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(8)));
                            listaProductosVendidos.Add(prod);
                        }
                    }
                }
                cnn.Close();
            }

            return listaProductosVendidos;
        }

        public static bool EliminarProductoVendido(int id)
        {
            ProductoVendido prodven = TraerProductoVendido(id);
            Producto prod = ProductoHandler.TraerProducto(prodven.IdProducto);
            prod.Stock = prodven.CantidadVendida + prod.Stock;
            ProductoHandler.ModificarProducto(prod);

            
            string query = "DELETE FROM ProductoVendido WHERE idProducto=" + id;
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                using (SqlCommand cmdCreate = new SqlCommand(query, cnn))
                {
                    cmdCreate.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return true;

        }

        public static bool ModificarProductoVendido(ProductoVendido p)
        {

            //Modificar un producto Vendido. Unicamente se le  puede modificar la cantidad vendida del producto.
            try
            {
                
                string query = "UPDATE ProductoVendido Set Stock = " + p.CantidadVendida + ",idProducto=" + p.IdProducto +
                    " WHERE id=" + p.Id;
                using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand cmdCreate = new SqlCommand(query, cnn))
                    {
                        cmdCreate.ExecuteNonQuery();
                    }
                    cnn.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        //Se le pasa un ID de producto devuelve el objeto ProductoVendido con la informacion del mismo.
        public static ProductoVendido TraerProductoVendido(int idProducto)
        {
            ProductoVendido prod = new ProductoVendido();

            using (SqlConnection connection = new SqlConnection(SQL.ConnectionString()))
            {
                connection.Open();
                var cmd = new SqlCommand("Select * from ProductoVendido Where producto.id =" + idProducto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            prod = new ProductoVendido((int)dr.GetInt64(0), dr.GetInt32(1), Convert.ToInt32(dr.GetValue(3)), (int)dr.GetInt64(2));
                        }
                    }
                }
                connection.Close();
            }
            return prod;
        }

        //Cargar un producto vendido a la base de datos del sistema
        public static bool CrearProductoVendido(ProductoVendido prodven)
        {
            string query = "INSERT INTO ProductoVendido (stock, IdProducto, IdVenta) VALUES (" + prodven.CantidadVendida + "," + prodven.IdProducto + "," + prodven.IdVenta + ")";
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                using (SqlCommand cmdUp = new SqlCommand(query, cnn))
                {
                    cmdUp.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return true;
        }

    }
}
