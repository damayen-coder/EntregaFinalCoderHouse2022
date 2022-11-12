using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        
        [HttpGet("{idUsuario}")]
        public List<Venta> TraearVenta(int idUsuario)
        {
            return VentaHandler.TraerVentas(idUsuario);
            
        }

        [HttpPost("cargarVenta")]
        public bool CargarVenta([FromBody] List<ProductoVendido> listaProd, int userId, string comentario)
        {
            return VentaHandler.CargarVenta(listaProd, userId, comentario);
            
        }

        [HttpDelete("{idVenta}")]
        public bool EliminarVenta(int idVenta)
        {
            return VentaHandler.EliminarVenta(idVenta);
            
        }
    }
}
