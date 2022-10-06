using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamber.LogicaDeNegocios;
using Hamber.EntidadesDeNegocio;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Hamber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private CategoriaBL categoriaBL = new CategoriaBL();
      
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Categoria>> Get()
        {
            return await categoriaBL.ObtenerTodos();
        }

        [HttpGet("{id}")]
        public async Task<Categoria>Get(int id)
        {
            Categoria categoria = new Categoria();
            categoria.Id = id;
            return await categoriaBL.ObtenerPorId(categoria);
        }

        [HttpPost]
        public async Task<ActionResult>Post([FromBody] Categoria categoria)
        {
            try
            {
                await categoriaBL.Crear(categoria);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }

            

        }

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id, [FromBody]Categoria categoria)
        {
            if (categoria.Id == id)
            {
                await categoriaBL.Modificar(categoria);
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
                Categoria categoria = new Categoria();
                categoria.Id = id;
                await categoriaBL.Eliminar(categoria);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
            [HttpPost("Buscar")]
            public async Task<List<Categoria>> Buscar([FromBody] object pCategoria)
            {

                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string strCategoria = JsonSerializer.Serialize(pCategoria);
                Categoria categoria = JsonSerializer.Deserialize<Categoria>(strCategoria, option);
                return await categoriaBL.Buscar(categoria);

            }
        
    }
}
