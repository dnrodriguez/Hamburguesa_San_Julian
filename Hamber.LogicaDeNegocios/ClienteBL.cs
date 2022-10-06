using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.AccesoDatos;

namespace Hamber.LogicaDeNegocios
{
    public class ClienteBL
    {
        public async Task<int> Crear(Cliente pCliente)
        {
            return await ClienteDAL.Crear(pCliente);
        }

        public async Task<int> Modificar(Cliente pCliente)
        {
            return await ClienteDAL.Modificar(pCliente);
        }

        public async Task<int> Eliminar(Cliente pCliente)
        {
            return await ClienteDAL.Eliminar(pCliente);
        }

        public async Task<Cliente> ObtenerPorId(Cliente pCliente)
        {
            return await ClienteDAL.ObtenerPorId(pCliente);
        }

      
    }
}
