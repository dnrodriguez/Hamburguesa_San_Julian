using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class ProductoDAL
    {
        public static async Task<int> Crear(Producto pProducto)
        {
            int result = 0;
            using (var dbContext = new DBContexto())
            {
                pProducto.Fecha = DateTime.Now;
                dbContext.Add(pProducto);
                result = await dbContext.SaveChangesAsync();

            }
            return result;
        }

        public static async Task<int> Modificar(Producto pProducto)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {

                var producto = await DbContext.Producto
                    .FirstOrDefaultAsync(p => p.Id == pProducto.Id);

               
                producto.IdCategoria = pProducto.IdCategoria;
                producto.IdMarca = pProducto.IdMarca;
                producto.Nombre = pProducto.Nombre;
                producto.Descripcion = pProducto.Descripcion;
                producto.Imagen = pProducto.Imagen;
                producto.Precio = pProducto.Precio;

                DbContext.Update(producto);
                result = await DbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Eliminar(Producto pProducto)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                var producto = await DbContext.Producto
                    .FirstOrDefaultAsync(p => p.Id == pProducto.Id);
                DbContext.Remove(producto);
                result = await DbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Producto> ObtenerPorId(Producto pProducto)
        {
            var producto = new Producto();
            using (var DbContext = new DBContexto())
            {
                producto = await DbContext.Producto
                    .FirstOrDefaultAsync(p => p.Id == pProducto.Id);
            }
            return producto;
        }
        public static async Task<List<Producto>> ObtenerTodos()
        {
            var producto = new List<Producto>();
            using (var dbContext = new DBContexto())
            {
                producto = await dbContext.Producto.ToListAsync();
            }
            return producto;
        }
        internal static IQueryable<Producto> QuerySelect(IQueryable<Producto> pQuery,
             Producto pProducto)
        {
            if (pProducto.Id > 0)
                pQuery = pQuery.Where(p => p.Id == pProducto.Id);

            if (pProducto.IdCategoria > 0)
                pQuery = pQuery.Where(p => p.IdCategoria == pProducto.IdCategoria);


            if (pProducto.IdMarca > 0)
                pQuery = pQuery.Where(p => p.IdMarca == pProducto.IdMarca);


            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
                pQuery = pQuery.Where(p => p.Nombre.Contains(pProducto.Nombre));

            if (!string.IsNullOrWhiteSpace(pProducto.Descripcion))
                pQuery = pQuery.Where(p => p.Descripcion.Contains(pProducto.Descripcion));

            if (!string.IsNullOrWhiteSpace(pProducto.Imagen))
                pQuery = pQuery.Where(p => p.Imagen.Contains(pProducto.Imagen));

            if (!string.IsNullOrWhiteSpace(pProducto.Precio))
                pQuery = pQuery.Where(p => p.Precio.Contains(pProducto.Precio));

            if (pProducto.Fecha.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pProducto.Fecha.Year,
                    pProducto.Fecha.Month, pProducto.Fecha.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(p => p.Fecha >= fechaInicial &&
                    p.Fecha <= fechaFinal);
            }

            pQuery = pQuery.OrderByDescending(p => p.Id).AsQueryable();
            if (pProducto.Top_Aux > 0)
                pQuery = pQuery.Take(pProducto.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Producto>> Buscar(Producto pProducto)
        {
            var productos = new List<Producto>();
            using (var DbContext = new DBContexto())
            {
                var select = DbContext.Producto.AsQueryable();
                select = QuerySelect(select, pProducto);
                productos = await select.ToListAsync();
            }
            return productos;
        }

        public static async Task<List<Producto>> BuscarIncluirMarca_Categoria(Producto pProducto)
        {
            var productos = new List<Producto>();
            using (var DbContext = new DBContexto())
            {
                var select = DbContext.Producto.AsQueryable();
                select = QuerySelect(select, pProducto).Include(p => p.Marca).AsQueryable();
                select = QuerySelect(select, pProducto).Include(p => p.Categoria).AsQueryable();
                productos = await select.ToListAsync();
            }
            return productos;
        }
        //public static async Task<List<Producto>> BuscarIncluirCategorias(Producto pProducto)
        //{
        //    var productos = new List<Producto>();
        //    using (var DbContext = new DBContexto())
        //    {
        //        var select = DbContext.Producto.AsQueryable();
        //        select = QuerySelect(select, pProducto).Include(c => c.Categoria).AsQueryable();
        //        productos = await select.ToListAsync();
        //    }
        //    return productos;
        //}

      

    }
}
