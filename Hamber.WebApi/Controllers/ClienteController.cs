using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.LogicaDeNegocios;
using Microsoft.AspNetCore.Authorization;

namespace Hamber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ClienteController : ControllerBase
    {
        private ClienteBL clienteBL = new ClienteBL();

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<Cliente> Get(int id)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            return await clienteBL.ObtenerPorId(cliente);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cliente cliente)
        {
            try
            {
                await clienteBL.Crear(cliente);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Cliente cliente)
        {

            if (cliente.Id == id)
            {
                await clienteBL.Modificar(cliente);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Id = id;
                await clienteBL.Eliminar(cliente);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
