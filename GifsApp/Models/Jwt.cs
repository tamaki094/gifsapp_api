using System.Security.Claims;

namespace GifsApp.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic ValidarToken(ClaimsIdentity claim)
        {
            try
            {
                if(claim.Claims.Count() == 0)
                {
                    return new Response
                    {
                        success = false,
                        message = "Verificar si estas enviando un token valido",
                        result = ""
                    };
                }

                int id = Int32.Parse(claim.Claims.FirstOrDefault(x => x.Type == "id").Value.ToString());
                
                using (AppDbContext context = new AppDbContext()) 
                {
                    Usuario usuario = context.Usuarios.Where(w => w.Id == id).FirstOrDefault();

                    return new Response
                    {
                        success = true,
                        message = "Exito!",
                        result = usuario
                    };
                }
               
            }
            catch (Exception ex)
            {
                return new Response
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };
            }
        }

    }
}
