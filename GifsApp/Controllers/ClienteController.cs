using GifsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GifsApp.Controllers
{

    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        List<Cliente> clientes = new List<Cliente>
    {
        new Cliente
        {
            id = "1",
            nombre = "Jesus",
            correo = "jesus@correo.com",
            edad = "15"
        },
        new Cliente
        {
            id = "2",
            nombre = "Carlos",
            correo = "carlos@correo.com",
            edad = "25"
        },
        new Cliente
        {
            id = "3",
            nombre = "Sara",
            correo = "sara@correo.com",
            edad = "22"
        }
    };

        [HttpGet]
        [Route("listar")]
        public dynamic ListarCliente()
        {
            return clientes;
        }

        [HttpGet]
        [Route("clientebyid")]
        public dynamic ClienteById(string id)
        {
            return clientes.Where(w => w.id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarCliente(Cliente cliente)
        {

            string id_actual = clientes.OrderByDescending(o => o.id).FirstOrDefault().id;
            int nuevo_id = 0;

            int.TryParse(id_actual, out nuevo_id);

            cliente.id = (nuevo_id + 1).ToString();
            clientes.Add(cliente);

            return new
            {
                success = true,
                message = "cliente registrado",
                result = cliente
            };
        }

        [HttpDelete]
        [Route("borrar")]
        public dynamic BoirrarCliente(Cliente cliente)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;


            if(token == "token")
            {
                return new
                {
                    success = true,
                    message = "cliente eliminado",
                    result = cliente
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "token incorrecto"
                };
            } 
                
                clientes.Remove(cliente);

            
        }
    }
}
