using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalJoseArmando.Repository
{
    public class ProductoHandler
    {

        public static Producto TraerProducto(int id)
        //Recibe un ID y retorna el producto con ese ID
        {

            var prod = new Producto();
            
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();

                var cmd = new SqlCommand("Select * FROM Producto WHERE id ='" + id + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            
                            prod = new Producto((int)dr.GetInt64(0), dr.GetString(1), Convert.ToDouble(dr.GetDecimal(2)), Convert.ToDouble(dr.GetDecimal(3)), Convert.ToInt32(dr.GetValue(4)), Convert.ToInt32(dr.GetValue(5)));

                        }
                    }
                }
                cnn.Close();
                return prod;
            }
        }

        public static List<Producto> TraerProductoByUserID(int idUsuario)
        //Recibe un UserID y retorna una lista de productos asignados a ese usuario
        {
            List<Producto> productos = new List<Producto> { };
            var listaProductos = productos;

            
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();

                var cmd = new SqlCommand("SELECT * FROM Producto WHERE idUsuario ='" + idUsuario + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Producto prod = new Producto((int)dr.GetInt64(0), dr.GetString(1), Convert.ToDouble(dr.GetDecimal(2)), Convert.ToDouble(dr.GetDecimal(3)), Convert.ToInt32(dr.GetValue(4)), Convert.ToInt32(dr.GetValue(5)));
                            listaProductos.Add(prod);
                        }
                    }
                }
                cnn.Close();
                return listaProductos;
            }
        }

        public static bool CrearProducto(Producto prod)
        //Crear un producto nuevo
        {
            
            string error = ValidarDatos(prod);

            if (string.IsNullOrEmpty(error))
            {
               
                var query = "INSERT into Producto values (@desc, @costo, @venta, @stock, @idUsuario)";
                ModificarCrearProducto(prod, query);

                using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
                {
                    cnn.Open();

                    var cmd = new SqlCommand("SELECT IDENT_CURRENT ('Producto')", cnn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                error = "ID del nuevo producto = " + dr.GetValue(0);
                            }
                        }
                    }
                    cnn.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ModificarProducto(Producto prod)
        //Modificar un producto

        {
            string error = ValidarDatos(prod);
            if (string.IsNullOrEmpty(error))
            {
                var query = "UPDATE Producto Set Descripciones = @desc, " +
                "Costo=@costo, PrecioVenta=@venta, Stock=@stock, idUSuario=@idUsuario " +
                "WHERE id=" + prod.Id;
                ModificarCrearProducto(prod, query);
                return true;
            }
            else
            {
                return false;
            }


        }
        public static bool EliminarProducto(int id)

        // Eliminar un producto segun su ID

        {
            bool eliminado = true;

            
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();

                var cmd = new SqlCommand("SELECT * FROM Producto WHERE id =" + id, cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (!dr.HasRows)
                    {
                        eliminado = false;

                    }
                }
                cnn.Close();
                if (eliminado == false)
                {
                    return eliminado;
                }
            }
            ProductoVendidoHandler.EliminarProductoVendido(id);

            string query = "DELETE FROM Producto WHERE id=" + id;
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return eliminado;
        }


        public static string ValidarDatos(Producto prod)
        //validar si la infomacion de un producto es correcta.

        {
            string error = string.Empty;
            if (prod.Descripciones == "" || prod.Descripciones == String.Empty)
            {
                error = "Descripcion vacio";
            }
            if (prod.Stock <= 0)
            {
                error = "Stock no puede ser menor a 1";
            }
            if (prod.IdUsuario <= 0)
            {
                error = "El ID del Usuario asignado no puede ser menor a 1";
            }
            else
            {
                
                using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
                {
                    cnn.Open();

                    var cmd = new SqlCommand("SELECT * FROM Usuario WHERE id =" + prod.IdUsuario, cnn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            error = "El ID del Usuario no existe";
                        }
                    }
                    cnn.Close();
                }
            }
            if (prod.PrecioVenta <= prod.Costo)
            {
                error = "El precio de venta debe ser mayor al costo del producto";
            }
            if (prod.Costo <= 0)
            {
                error = "EL costo del producto no puede ser menor o igual a 0";
            }
            return error;
        }


        private static void ModificarCrearProducto(Producto prod, string query)
        //Metodo interno usado para no repetir codigo al Modificar o crear un producto

        {
            

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                var paramDesc = new SqlParameter();
                paramDesc.ParameterName = "desc";
                paramDesc.SqlDbType = System.Data.SqlDbType.Char;
                paramDesc.Value = prod.Descripciones;

                var paramCosto = new SqlParameter();
                paramCosto.ParameterName = "costo";
                paramCosto.SqlDbType = System.Data.SqlDbType.Decimal;
                paramCosto.Value = prod.Costo;

                var paramVenta = new SqlParameter();
                paramVenta.ParameterName = "venta";
                paramVenta.SqlDbType = System.Data.SqlDbType.Decimal;
                paramVenta.Value = prod.PrecioVenta;

                var paramStock = new SqlParameter();
                paramStock.ParameterName = "stock";
                paramStock.SqlDbType = System.Data.SqlDbType.Int;
                paramStock.Value = prod.Stock;

                var paramIdUser = new SqlParameter();
                paramIdUser.ParameterName = "idUsuario";
                paramIdUser.SqlDbType = System.Data.SqlDbType.Int;
                paramIdUser.Value = prod.IdUsuario;

                cnn.Open();
                using (SqlCommand cmdCreate = new SqlCommand(query, cnn))
                {
                    cmdCreate.Parameters.Add(paramDesc);
                    cmdCreate.Parameters.Add(paramCosto);
                    cmdCreate.Parameters.Add(paramVenta);
                    cmdCreate.Parameters.Add(paramStock);
                    cmdCreate.Parameters.Add(paramIdUser);
                    cmdCreate.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }


    }

}
