using System.Data;
using System.Data.SqlClient;
using static ProyectoFinalJoseArmando.Controllers.UsuarioController;
using ProyectoFinalJoseArmando.Modulos;
using System.Net.Mail;

namespace ProyectoFinalJoseArmando.Repository
{
   
    public class UsuarioHandler
    {

        public static List<Usuarios> TraerUsuario()

        //Se ingresa un nombre de usuario  y devuelve la informacion del usuario

        {
            var UserList = new List<Usuarios>();
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuario", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var user = new Usuarios();
                            user.id = (int)dr.GetInt64(0);
                            user.Nombre = dr.GetString(1);
                            user.Apellido = dr.GetString(2);
                            user.NombreUsuario = dr.GetString(3);
                            user.Contraseña = dr.GetString(4);
                            user.Mail = dr.GetString(5);
                            UserList.Add(user);

                        }
                    }
                }
                cnn.Close();
                return UserList;
            }
        }


        public static Usuarios TraerUsuario(string userName)
        //Ingresamos un Nombre de usuario y devolvemos el Usuario
        {
            var User = new Usuarios();
            var UserList = new List<Usuarios>();
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("Select * FROM Usuario WHERE nombreUsuario='" + userName + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            User.id = (int)dr.GetInt64(0);
                            User.Nombre = dr.GetString(1);
                            User.Apellido = dr.GetString(2);
                            User.NombreUsuario = dr.GetString(3);
                            User.Contraseña = dr.GetString(4);
                            User.Mail = dr.GetString(5);
                        }
                    }
                }
                cnn.Close();
                return User;
            }
        }

        //Modificar la informacion de un usuario en la base de datos
        public static bool ModificarUsuario(Usuarios User)
        {

            string responder = string.Empty;
            bool validEmail;

            //Comprobamos que el email tenga un formato valido

            validEmail = IsMailValid(User.Mail);

            if (validEmail != true)
            {
                return false;
            }

            bool SePuede = false;

            //Comprobamos que el ID del usuario a modificar exista en la base de datos.
            var usuarios = new Usuarios();
            var UserList = new List<Usuarios>();

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuario WHERE id=" + User.id, cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        SePuede = true;
                    }
                }
                cnn.Close();
            }

            if (SePuede)
            {
                var query = "UPDATE Usuario SET Nombre=@nombre, Apellido=@apellido, " +
                                " nombreUsuario=@nombreUsuario, Contraseña=@password, Mail=@mail WHERE id=@id";

                ModificarCrearUsuario(User, query);
                return true;
            }
            else
            {
                return false;
            }

        }


        public static Usuarios IniciarSesion(string userName, string password)
        {
            var usuarios = new Usuarios();
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario='" + userName + "' and Contraseña='" + password + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            long id = dr.GetInt64(0);
                            usuarios = new Usuarios(dr.GetInt64(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                        }
                    }
                    else
                    {
                        usuarios = new Usuarios();
                    }
                }
                cnn.Close();
                return usuarios;
            }
        }


        
        public static bool EliminarUsuario(int id)
        //Eliminar un usuario de la base de datos.
        {
            bool Sepuede = false;
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuario WHERE id=" + id, cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        Sepuede = true;
                    }
                }
                cnn.Close();
                if (Sepuede)
                {
                    var query = "DELETE Usuario WHERE id=" + id;

                    using (SqlConnection cnnDel = new SqlConnection(SQL.ConnectionString()))
                    {
                        cnnDel.Open();
                        using (SqlCommand cmdUp = new SqlCommand(query, cnnDel))
                        {
                            cmdUp.ExecuteNonQuery();
                        }
                        cnnDel.Close();
                    }
                }
            }
            return Sepuede;
        }

        
        public static bool CrearUsuario(Usuarios user)
        //Crear Uusario en la base de datos
        {
            bool valido = false;
            Usuarios prueba = IniciarSesion(user.NombreUsuario, user.Contraseña);
            if (prueba.id == 0)
            {
                valido = IsMailValid(user.Mail);
            }
            else
            {
                return false;
            }
            var UserTraer = TraerUsuario(user.NombreUsuario);

            if (UserTraer.id != 0)
            {
                return false;
            }
            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {
                cnn.Open();
                var cmd = new SqlCommand("SELECT * FROM Usuario WHERE = mail'" + user.Mail + "'", cnn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        valido = false;
                    }
                }
                cnn.Close();
            }
            if (valido)
            {
                if (user.Nombre.Length > 0 && user.NombreUsuario.Length > 0 && user.Apellido.Length > 0 && user.Contraseña.Length > 0)
                {
                    var query = "INSERT INTO Usuario VALUES(@nombre, @apellido,@nombreUsuario, @password, @mail )";
                    ModificarCrearUsuario(user, query);
                    return true;

                }
                else
                { 
                    return false; 
                }

            }
            else
            { 
                return false; 
            }
        }

        
        private static void ModificarCrearUsuario(Usuarios User, string query)
        //Modificar usuariop de la base de datos.
        {

            using (SqlConnection cnn = new SqlConnection(SQL.ConnectionString()))
            {

                var paramId = new SqlParameter();
                paramId.ParameterName = "id";
                paramId.SqlDbType = System.Data.SqlDbType.Int;
                paramId.Value = User.id;

                var paramNombre = new SqlParameter();
                paramNombre.ParameterName = "nombre";
                paramNombre.SqlDbType = System.Data.SqlDbType.VarChar;
                paramNombre.Value = User.Nombre;

                var ParamApellido = new SqlParameter();
                ParamApellido.ParameterName = "apellido";
                ParamApellido.SqlDbType = System.Data.SqlDbType.VarChar;
                ParamApellido.Value = User.Apellido;

                var paramUserName = new SqlParameter();
                paramUserName.ParameterName = "nombreUsuario";
                paramUserName.SqlDbType = System.Data.SqlDbType.VarChar;
                paramUserName.Value = User.NombreUsuario;

                var paramPass = new SqlParameter();
                paramPass.ParameterName = "password";
                paramPass.SqlDbType = System.Data.SqlDbType.VarChar;
                paramPass.Value = User.Contraseña;

                var paramMail = new SqlParameter();
                paramMail.ParameterName = "mail";
                paramMail.SqlDbType = System.Data.SqlDbType.VarChar;
                paramMail.Value = User.Mail;

                cnn.Open();
                using (SqlCommand cmdUp = new SqlCommand(query, cnn))
                {
                    cmdUp.Parameters.Add(paramId);
                    cmdUp.Parameters.Add(paramNombre);
                    cmdUp.Parameters.Add(ParamApellido);
                    cmdUp.Parameters.Add(paramUserName);
                    cmdUp.Parameters.Add(paramPass);
                    cmdUp.Parameters.Add(paramMail);
                    cmdUp.ExecuteNonQuery();
                }
                cnn.Close();
            }
        }





        //validar email
        private static bool IsMailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;

            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
