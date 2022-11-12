namespace ProyectoFinalJoseArmando.Modulos
{
    public class Producto
    {
        private int _id;
        private string _descripciones;
        private int _idUsuario;
        private double _precioDeCompra;
        private double _precioDeVenta;
        private int _stock;

        

        public int Id { get { return _id; } set { _id = value; } }
        public string Descripciones { get { return _descripciones; } set { _descripciones = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public double Costo { get { return _precioDeCompra; } set { _precioDeCompra = value; } }
        public double PrecioVenta { get { return _precioDeVenta; } set { _precioDeVenta = value; } }
        public int Stock { get { return _stock; } set { _stock = value; } }


        public Producto()
        {
            _id = 0;
            _descripciones = string.Empty;
            _precioDeCompra = 0;
            _precioDeVenta = 0;
            _idUsuario = 0;
            _stock = 0;


        }
        public Producto(int idProducto, string descripciones, double precioDeVenta, double precioDeCompra, int stock, int idUsuario)
        {
            _id = idProducto;
            _descripciones = descripciones;
            _precioDeCompra = precioDeCompra;
            _precioDeVenta = precioDeVenta;
            _idUsuario = idUsuario;
            _stock = stock;
        }
    }
}
