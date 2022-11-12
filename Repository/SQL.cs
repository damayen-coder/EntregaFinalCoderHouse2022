using  System.Data.SqlClient;

namespace ProyectoFinalJoseArmando.Repository
{
    public class SQL
    {
        public static string ConnectionString()

        {
            SqlConnectionStringBuilder conecctionbuilder = new SqlConnectionStringBuilder();
            conecctionbuilder.DataSource = "DESKTOP-VRSGEG4";
            conecctionbuilder.InitialCatalog = "SistemaGestion";
            conecctionbuilder.IntegratedSecurity = true;
            var cs = conecctionbuilder.ConnectionString;
            return (cs);

        }
    }
}
