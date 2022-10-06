using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class CategoriaDAL
    {
        public static async Task<int> Crear(Categoria pCategoria)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                DbContext.Add(pCategoria);
                result = await DbContext.SaveChangesAsync();
            }
            return result;

        }
        public static async Task<int> Modificar(Categoria pCategoria)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                var categoria = await DbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                categoria.Nombre = pCategoria.Nombre;
                DbContext.Update(categoria);
                result = await DbContext.SaveChangesAsync();
            }
            return result;

        }
        public static async Task<int> Eliminar(Categoria pCategoria)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                var categoria = await DbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
                DbContext.Remove(categoria);
                result = await DbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Categoria> ObtenerPorId(Categoria pCategoria)
        {
            var categoria = new Categoria();
            using (var DbContext = new DBContexto())
            {
                categoria = await DbContext.Categoria.FirstOrDefaultAsync(c => c.Id == pCategoria.Id);
            }
            return categoria;
        }

        public static async Task<List<Categoria>> ObtenerTodos()
        {
            var categoria = new List<Categoria>();
            using (var DbContext = new DBContexto())
            {
                categoria = await DbContext.Categoria.ToListAsync();
            }
            return categoria;
        }

        internal static IQueryable<Categoria> QuerySelect(IQueryable<Categoria> pQuery, Categoria pCategoria)
        {
            if (pCategoria.Id > 0) //si se busca por Id
                pQuery = pQuery.Where(c => c.Id == pCategoria.Id);

            if (!string.IsNullOrWhiteSpace(pCategoria.Nombre)) //si se busca por Nombre
                pQuery = pQuery.Where(c => c.Nombre.Contains(pCategoria.Nombre));
            pQuery = pQuery.OrderByDescending(c => c.Id).AsQueryable();

            if (pCategoria.Top_Aux > 0) //si se quiere un número específico de registros
                pQuery = pQuery.Take(pCategoria.Top_Aux).AsQueryable();

            return pQuery;
        }
        public static async Task<List<Categoria>> Buscar(Categoria pCategoria)
        {
            var categorias = new List<Categoria>();
            using (var bdContext = new DBContexto())
            {
                var select = bdContext.Categoria.AsQueryable();
                select = QuerySelect(select, pCategoria);
                categorias = await select.ToListAsync();
            }
            return categorias;
        }
    }
}
