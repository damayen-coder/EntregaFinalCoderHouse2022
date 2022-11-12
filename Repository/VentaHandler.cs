using ProyectoFinalJoseArmando.Modulos;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoFinalJoseArmando.Repository
{
    public class VentaHandler
    {

        public static bool CargarVenta(List<ProductoVendido> ListProd, int userId, string comentario)
        {
            int idVenta = 0;
            bool Stock = true;

            foreach (ProductoVendido prodven in ListProd)
            {
                using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
                {
                    cnn.Open();
                    var comando = new SqlCommand("SELECT Stock FROM Producto WHERE Id= " + prodven.IdProducto, cnn);
                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var prodStock = dr.GetInt32(0);
                                if (prodStock < prodven.CantidadVendida)
                                {
                                    Stock = false;
                                }
                            }
                        }
                    }
                    cnn.Close();
                }
            }
            if (Stock == false)
            {
                return false;
            }

            string query = "INSERT INTO VENTA (Comentarios, IdUsuario) VALUES ('" + comentario + "'," + userId + ")";
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                using (SqlCommand cmdUp = new SqlCommand(query, cnn))
                {
                    cmdUp.ExecuteNonQuery();
                }
                cnn.Close();
            }


            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var CMD = new SqlCommand("SELECT IDENT_CURRENT ('Venta')", cnn);
                using (SqlDataReader dr = CMD.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            idVenta = (int)dr.GetDecimal(0);
                        }
                    }
                }
                cnn.Close();
            }


            foreach (ProductoVendido prodven in ListProd)
            {
                prodven.IdVenta = idVenta;
                ProductoVendidoHandler.CrearProductoVendido(prodven);


                query = "DECLARE @stock int " +
                        "SET @stock = (Select Stock from Producto Where id=" + prodven.IdProducto + ") " +
                        "UPDATE Producto SET stock=(@stock-" + prodven.CantidadVendida + ") WHERE id=" + prodven.IdProducto;
                using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
                {
                    cnn.Open();
                    using (SqlCommand comandoUpdate = new SqlCommand(query, cnn))
                    {
                        comandoUpdate.ExecuteNonQuery();
                    }
                    cnn.Close();
                }
            }
            return true;

        }




        static public List<Venta> TraerVentas(int userId)
        {
            List<Venta> ventas = new List<Venta> { };
            var traerVentas = ventas;

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Venta WHERE IdUsuario = " + userId, cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Venta prod = new Venta((int)Convert.ToInt64(dr.GetValue(2)), (int)Convert.ToInt64(dr.GetValue(0)), dr.GetString(1), ProductoVendidoHandler.TraerProductosVendidosPorIdVenta((int)Convert.ToInt64(dr.GetValue(0))));
                            traerVentas.Add(prod);
                        }
                    }
                }
                cnn.Close();
            }
            return traerVentas;
        }



        public static bool EliminarVenta(int id)
        {
            bool ventas = false;

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var comando = new SqlCommand("SELECT * FROM Venta WHERE id = " + id, cnn);
                using (SqlDataReader dr = comando.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        ventas = true;
                    }
                }
                cnn.Close();

            }

            if (!ventas)
            {
                return false;
            }

            List<ProductoVendido> listaProductos = new List<ProductoVendido>();
            listaProductos = ProductoVendidoHandler.TraerProductosVendidosPorIdVenta(id);

            foreach (ProductoVendido prodven in listaProductos)
            {
                ProductoVendidoHandler.EliminarProductoVendido(prodven.Id);
            }

            string query = "DELETE Venta WHERE id=" + id;
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
