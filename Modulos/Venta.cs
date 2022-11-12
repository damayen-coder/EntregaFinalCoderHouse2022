namespace ProyectoFinalJoseArmando.Modulos
{
    public class Venta
    {
        private string _comentario;
        private int _idVenta;
        private int _idUsuario;
        private List<ProductoVendido> _listaProductosVendidos = new List<ProductoVendido>();


        public string Comentario { get { return _comentario; } set { _comentario = value; } }
        public int Id { get { return _idVenta; } set { _idVenta = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public List<ProductoVendido> ListaProductosVendidos { get { return _listaProductosVendidos; } set { _listaProductosVendidos = value; } }

        public Venta()
        {
            _comentario = String.Empty;
            _idVenta = 0;
            _idUsuario = 0;
            _listaProductosVendidos = new List<ProductoVendido>();
        }



        public Venta(int idUsuario, int idVenta, string comentario, List<ProductoVendido> listaProductosVendidos)
        {

            _idUsuario = idUsuario;
            _comentario = comentario;
            _idVenta = idVenta;
            _listaProductosVendidos = listaProductosVendidos;

        }
    }
}
