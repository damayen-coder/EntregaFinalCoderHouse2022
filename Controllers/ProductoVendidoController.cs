using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductoVendido(int idUsuario)
        {
            return ProductoVendidoHandler.TraerProductosVendidos(idUsuario);
        }
    }
}
