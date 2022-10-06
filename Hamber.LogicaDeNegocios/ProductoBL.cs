using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.AccesoDatos;

namespace Hamber.LogicaDeNegocios
{
    public class ProductoBL
    {
        #region CRUD
        public async Task<int> Crear(Producto pProducto)
        {
            return await ProductoDAL.Crear(pProducto);
        }

        public async Task<int> Modificar(Producto pProducto)
        {
            return await ProductoDAL.Modificar(pProducto);
        }

        public async Task<int> Eliminar(Producto pProducto)
        {
            return await ProductoDAL.Eliminar(pProducto);
        }

        public async Task<Producto> ObtenerPorId(Producto pProducto)
        {
            return await ProductoDAL.ObtenerPorId(pProducto);
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            return await ProductoDAL.ObtenerTodos();
        }
        public async Task<List<Producto>> BuscarIncluirMarca_Categoria(Producto pProducto)
        {
            return await ProductoDAL.BuscarIncluirMarca_Categoria(pProducto);
        }
        //public async Task<List<Producto>> BuscarIncluirCategorias(Producto pProducto)
        //{
        //    return await ProductoDAL.BuscarIncluirCategorias(pProducto);
        //}

        public async Task<List<Producto>>Buscar(Producto pProducto)
        {
            return await ProductoDAL.Buscar(pProducto);
        }
        #endregion
    }
}

