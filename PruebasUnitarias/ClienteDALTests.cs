using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hamber.AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;

namespace Hamber.AccesoDatos.Tests
{
    [TestClass()]
    public class ClienteDALTests
    {
        private static Cliente clienteInicial = new Cliente { Id = 2 };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var cliente = new Cliente();
            cliente.Nombre = "Maria";
            cliente.Apellido = "Rodriguez";
            cliente.Email = "Mariaaaa";
            cliente.Estatus = (byte)Estatus_Cliente.INACTIVO;
            int result = await ClienteDAL.Crear(cliente);
            Assert.AreNotEqual(0,result);
            clienteInicial.Id = cliente.Id;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var cliente = new Cliente();
            cliente.Id = clienteInicial.Id;
            cliente.Nombre = "Juana";
            cliente.Apellido = "Rodrigue";
            cliente.Email = "Juanaaa";
            cliente.Estatus = (byte)Estatus_Cliente.ACTIVO;
            int result = await ClienteDAL.Modificar(cliente);
            Assert.AreNotEqual(0,result);
        }

   

        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var cliente = new Cliente();
            cliente.Id = clienteInicial.Id;
            var resultCliente = await ClienteDAL.ObtenerPorId(cliente);
            Assert.AreEqual(cliente.Id ,clienteInicial.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultClientes = await ClienteDAL.ObtenerTodos();
            Assert.AreNotEqual(0,resultClientes.Count);
        }
        [TestMethod()]
        public async Task T5EliminarTest()
        {
            var cliente = new Cliente();
            cliente.Id = clienteInicial.Id;
            int result = await ClienteDAL.Eliminar(cliente);
            Assert.AreNotEqual(0,result);
        }
    }
}