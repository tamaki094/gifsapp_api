using GifsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GifsApp.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {


        [HttpGet]
        [Route("listar")]
        public dynamic ListarUsuarios()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Roles.ToList();
            }

        }

        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarUsuario(Usuario usuario)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Add(usuario);
                context.SaveChanges();
                return context.Usuarios.ToList();

            }
        }

    }
}
