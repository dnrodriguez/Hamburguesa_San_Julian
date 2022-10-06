using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.LogicaDeNegocios;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Hamber.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {
        private MarcaBL marcaBL = new MarcaBL();

        [HttpGet]
        public async Task<IEnumerable<Marca>> Get()
        {
            return await marcaBL.ObtenerTodos();
        }

        [HttpGet("{id}")]
        public async Task<Marca> Get(int id)
        {
            Marca marca = new Marca();
            marca.Id = id;
            return await marcaBL.ObtenerPorId(marca);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Marca marca)
        {
            try
            {
                await marcaBL.Crear(marca);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Marca marca)
        {

            if (marca.Id == id)
            {
                await marcaBL.Modificar(marca);
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
                Marca marca = new Marca();
                marca.Id = id;
                await marcaBL.Eliminar(marca);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Marca>> Buscar([FromBody] object pMarca)
        {

            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strMarca = JsonSerializer.Serialize(pMarca);
            Marca marca = JsonSerializer.Deserialize<Marca>(strMarca, option);
            return await marcaBL.Buscar(marca);

        }
    }
}
