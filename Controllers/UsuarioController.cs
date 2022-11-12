using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalJoseArmando.Modulos;
using ProyectoFinalJoseArmando.Repository;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalJoseArmando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        [HttpGet("{nombreUsuario}")]
        public Usuarios Consultar([Required] string nombreUsuario)
        {
            return UsuarioHandler.TraerUsuario(nombreUsuario);
        }

        
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuarios InicioSesion(string nombreUsuario, string contraseña)

        {
            return UsuarioHandler.IniciarSesion(nombreUsuario, contraseña);
        }

        
        [HttpPut]
        public bool Modificar([FromBody] Usuarios usuario)

        {
            return UsuarioHandler.ModificarUsuario(usuario);
        }

        
        [HttpPost]
        public bool CrearUsuario([FromBody] Usuarios u)
        {
            return UsuarioHandler.CrearUsuario(u);
        }

        
        [HttpDelete("EliminarUsuario")]
        public bool EliminarUsuario([FromBody] int id)
        {
            return UsuarioHandler.EliminarUsuario(id);
        }

    }
}
