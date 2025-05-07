using GifsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GifsApp.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private IConfiguration _configuration;

        public UsuariosController(IConfiguration conf)
        {
            this._configuration = conf;
        }

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
        /// <summary>
        // 1️- SymmetricSecurityKey → Creas la clave simétrica a partir de una cadena de texto.
        // Se convierte a bytes con Encoding.UTF8.GetBytes("clave_segura").
        // Se usa para firmar y validar el JWT porque es una clave compartida.
        //
        // 2- SigningCredentials → Firmas el JWT usando la clave y el algoritmo HmacSha256.
        // Esta firma garantiza que el token no ha sido modificado después de su generación.
        // Al validar el token, el mismo SecurityKey se usa para comprobar que la firma es legítima.
        //
        // Importante:
        // No estás "encriptando" el JWT, solo lo estás firmando para validar su autenticidad.
        // Si usas criptografía asimétrica (RSA) en lugar de HMAC, entonces usarías una clave privada para firmar y una pública para validar
        //
        /// </summary>
        /// <param name="optData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public Response Login([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string user = data.usuario.ToString();
            string password = data.password.ToString();

            using (AppDbContext context = new AppDbContext())
            {
                Usuario usr = context.Usuarios.Where(w => w.Nombre == user && w.Contrasena == password).FirstOrDefault();

                if (usr == null)
                {
                    return new Response
                    {
                        success = false,
                        message = "Credenciales incorrectas",
                        result = ""
                    };
                }
                else
                {
                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();


                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                        new Claim("id", usr.Id.ToString()),
                        new Claim("usuario", usr.Nombre.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: singIn
                    );

                    return new Response
                    {
                        success = true,
                        message = "exito",
                        result = new JwtSecurityTokenHandler().WriteToken(token)
                    };
                }

            }

        }

    }
}
