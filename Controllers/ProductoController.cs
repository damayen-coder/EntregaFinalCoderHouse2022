using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;


namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductoByUserID(int idUsuario)
        {
            return ProductoHandler.TraerProductoByUserID(idUsuario);
        }

        
        [HttpPost]
        public bool CrearProducto([FromBody] Producto producto)

        {
            return ProductoHandler.CrearProducto(producto);
        }

        
        [HttpPut]
        public bool ModificarProducto([FromBody] Producto producto)

        {
            return ProductoHandler.ModificarProducto(producto);
        }

        
        [HttpDelete("{idProducto}")]
        public bool EliminarProducto(int idProducto)

        {
            return ProductoHandler.EliminarProducto(idProducto);
        }


    }
}
