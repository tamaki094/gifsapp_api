using GifsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GifsApp.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatController : ControllerBase
    {


        [HttpPost]
        [Route("mensajes")]
        [Authorize]
        public dynamic ListarMensajes()
        {
            var cabecera = HttpContext.Request.Headers;
            Console.WriteLine($"Token recibido: {HttpContext.Request.Headers["Authorization"]}");
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = identity?.Claims.Select(c => new { c.Type, c.Value });
           

            var rToken = Jwt.ValidarToken(identity);

            if (!rToken.success) return rToken;

            Usuario usuario = rToken.result;

            if (usuario.Rol != 1)
            {
                return new Response
                {
                    success = false,
                    message = "Error de permisos",
                    result = ""
                };
            }

            using (AppDbContext context = new AppDbContext())
            {
                return new Response
                {
                    success = true,
                    message = "Mostrando Mensajes",
                    result = context.Chats.ToList()
                };
            }
        }

        [HttpPost]
        [Route("enviar")]
        [Authorize]
        public dynamic EnviarMensaje(Chat mensaje)
        {

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity?.Claims.Select(c => new { c.Type, c.Value });
                var rToken = Jwt.ValidarToken(identity);

                if (!rToken.success) return rToken;

                Usuario usuario = rToken.result;

                if (usuario.Rol != 1)
                {
                    return new Response
                    {
                        success = false,
                        message = "Error de permisos",
                        result = ""
                    };
                }

                using (AppDbContext context = new AppDbContext())
                {
                    context.Chats.Add(mensaje);
                    int i = context.SaveChanges();

                    return new Response
                    {
                        success = true,
                        message = "Enviado",
                        result = i
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    success = false,
                    message = ex.Message,
                    result = ex.InnerException
                };
            }
            
        }
    }
}
