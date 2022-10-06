using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.AccesoDatos;
using Hamber.EntidadesDeNegocio;

namespace Hamber.LogicaDeNegocios
{
    public class MarcaBL
    {
        public async Task<int> Crear(Marca pMarca)
        {
            return await MarcaDAL.Crear(pMarca);
        }

        public async Task<int> Modificar(Marca pMarca)
        {
            return await MarcaDAL.Modificar(pMarca);
        }

        public async Task<int> Eliminar(Marca pMarca)
        {
            return await MarcaDAL.Eliminar(pMarca);
        }

        public async Task<Marca> ObtenerPorId(Marca pMarca)
        {
            return await MarcaDAL.ObtenerPorId(pMarca);
        }

        public async Task<List<Marca>> ObtenerTodos()
        {
            return await MarcaDAL.ObtenerTodos();
        }

        public async Task<List<Marca>> Buscar(Marca pMarca)
        {
            return await MarcaDAL.Buscar(pMarca);
        }
    }
}
