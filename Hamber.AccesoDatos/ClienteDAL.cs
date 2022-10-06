using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class ClienteDAL

    {
        public static async Task<int> Crear(Cliente pCliente)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                pCliente.Fecha = DateTime.Now;
                bdContext.Add(pCliente);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Modificar(Cliente pCliente)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var cliente = await bdContext.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
                cliente.Nombre = pCliente.Nombre;
                cliente.Apellido = pCliente.Apellido;
                cliente.Email = pCliente.Email;
                cliente.Estatus = pCliente.Estatus;
                bdContext.Update(cliente);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> Eliminar(Cliente pCliente)
        {
            int result = 0;
            using (var bdContext = new DBContexto())
            {
                var cliente = await bdContext.Cliente.FirstOrDefaultAsync(c => c .Id == pCliente.Id);
                bdContext.Remove(cliente);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Cliente> ObtenerPorId(Cliente pCliente)
        {
            var cliente = new Cliente();
            using (var bdContext = new DBContexto())
            {
                cliente = await bdContext.Cliente.FirstOrDefaultAsync(c => c.Id == pCliente.Id);
            }
            return cliente;
        }

        public static async Task<List<Cliente>> ObtenerTodos()
        {
            var clientes = new List<Cliente>();
            using (var bdContext = new DBContexto())
            {
                clientes = await bdContext.Cliente.ToListAsync();
            }
            return clientes;
        }
    }
}
