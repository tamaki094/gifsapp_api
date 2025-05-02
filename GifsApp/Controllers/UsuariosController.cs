using GifsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

                //var usuariosConRoles = context.Usuarios
                //.Include(u => u.RolNavigation) // Carga la relación con `Roles`
                //.Select(u => new
                //{
                //    u.Id,
                //    u.Nombre,
                //    u.Contrasena,
                //    Rol = new
                //    {
                //        u.RolNavigation.Id,  // ID del rol
                //        u.RolNavigation.Rol  // Nombre del rol
                //    }
                //})
                //.ToList();

                var usuariosConRoles = context.Usuarios
                    .Select(u => new
                    {
                        u.Id,
                        u.Nombre,
                        RolDescripcion = u.RolNavigation.Rol // Personaliza la salida con solo la descripción del rol
                    })
                    .ToList();


                return usuariosConRoles;
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
