using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.LogicaDeNegocios;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Hamber.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private ProductoBL productoBL = new ProductoBL();

        [HttpGet]

        [AllowAnonymous]
        public async Task<IEnumerable<Producto>> Get()
        {
            return await productoBL.ObtenerTodos();
        }

        [HttpGet("{id}")]
        public async Task<Producto> Get(int id)
        {
            Producto producto = new Producto();
            producto.Id = id;
            return await productoBL.ObtenerPorId(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Get(Producto producto)
        {
            try
            {
                await productoBL.Crear(producto);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        } 

        [HttpPut("{id}")]
        public async Task<ActionResult>Put(int id,[FromBody]Producto producto)
        {
            if (producto.Id == id)
            {
                await productoBL.Modificar(producto);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult>Delete(int id)
        {
            try
            {
                Producto producto = new Producto();
                producto.Id = id;
                await productoBL.Eliminar(producto);
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Buscar")]
        public async Task<List<Producto>>Buscar([FromBody]object pProducto)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strProducto = JsonSerializer.Serialize(pProducto);
            Producto producto = JsonSerializer.Deserialize<Producto>(strProducto, option);
            var productos = await productoBL.BuscarIncluirMarca_Categoria(producto);
            productos.ForEach(p => p.Categoria.Producto = null);
            productos.ForEach(p => p.Marca.Producto = null);
            return productos;
        }
    }
}
