using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class MarcaDAL
    {
        public static async Task<int> Crear(Marca pMarca)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                bdContext.Add(pMarca);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(Marca pMarca)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var marca = await bdContext.Marca.FirstOrDefaultAsync(m => m.Id == pMarca.Id);
                marca.Nombre = pMarca.Nombre;
                bdContext.Update(marca);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Eliminar(Marca pMarca)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var marca = await bdContext.Marca.FirstOrDefaultAsync(m => m.Id == pMarca.Id);
                bdContext.Remove(marca);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Marca> ObtenerPorId(Marca pMarca)
        {
            var marca = new Marca();
            using (var bdContext = new DBContexto())
            {
                marca = await bdContext.Marca.FirstOrDefaultAsync(m => m.Id == pMarca.Id);
            }
            return marca;
        }

        public static async Task<List<Marca>> ObtenerTodos()
        {
            var marcas = new List<Marca>();
            using (var bdContext = new DBContexto())
            {
                marcas = await bdContext.Marca.ToListAsync();
            }
            return marcas;
        }

        internal static IQueryable<Marca> QuerySelect(IQueryable<Marca> pQuery, Marca pMarca)
        {
            if (pMarca.Id > 0) //si se busca por Id
                pQuery = pQuery.Where(m => m.Id == pMarca.Id);

            if (!string.IsNullOrWhiteSpace(pMarca.Nombre)) //si se busca por Nombre
                pQuery = pQuery.Where(m=> m.Nombre.Contains(pMarca.Nombre));
            pQuery = pQuery.OrderByDescending(m => m.Id).AsQueryable();

            if (pMarca.Top_Aux > 0) //si se quiere un número específico de registros
                pQuery = pQuery.Take(pMarca.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Marca>> Buscar(Marca pMarca)
        {
            var marcas = new List<Marca>();
            using (var bdContext = new DBContexto())
            {
                var select = bdContext.Marca.AsQueryable();
                select = QuerySelect(select, pMarca);
                marcas = await select.ToListAsync();
            }
            return marcas;
        }
    }
}
