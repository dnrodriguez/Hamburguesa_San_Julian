using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class RolDAL
    {
        public static async Task<int> Crear(Rol pRol)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                bdContext.Add(pRol);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(Rol pRol)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var rol = await bdContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                bdContext.Update(rol);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Eliminar(Rol pRol)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var rol = await bdContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
                bdContext.Remove(rol);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorId(Rol pRol)
        {
            var rol = new Rol();
            using (var bdContext = new DBContexto())
            {
                rol = await bdContext.Rol.FirstOrDefaultAsync(r => r.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodos()
        {
            var roles = new List<Rol>();
            using (var bdContext = new DBContexto())
            {
                roles = await bdContext.Rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id > 0) //si se busca por Id
                pQuery = pQuery.Where(r => r.Id == pRol.Id);

            if (!string.IsNullOrWhiteSpace(pRol.Nombre)) //si se busca por Nombre
                pQuery = pQuery.Where(r => r.Nombre.Contains(pRol.Nombre));
            pQuery = pQuery.OrderByDescending(r => r.Id).AsQueryable();

            if (pRol.Top_Aux > 0) //si se quiere un número específico de registros
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Rol>> Buscar(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var bdContext = new DBContexto())
            {
                var select = bdContext.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
}
    