using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InicioController : Controller
    {
        
        [HttpGet("Nombre")]
        public string TraerNombre()
        {
            return "Fiambreria Don Juan";
        }
    }
}
