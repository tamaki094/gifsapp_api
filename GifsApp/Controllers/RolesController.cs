using GifsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GifsApp.Controllers
{
    [ApiController]
    [Route("roles")]
    public class RolesController : ControllerBase
    {
       

        [HttpGet]
        [Route("listar")]
        public dynamic ListarRoles()
        {
            using (AppDbContext context = new AppDbContext())
            {
                return context.Roles.ToList();
            }
            
        }

    }
}
