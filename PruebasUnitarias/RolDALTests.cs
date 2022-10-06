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
    public class RolDALTests
    {
        private static Rol rolInicial = new Rol { Id = 2 };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var rol = new Rol();
            rol.Nombre = "Administrador";
            int result = await RolDAL.Crear(rol);
            Assert.AreNotEqual(0,result);
            rolInicial.Id = rol.Id;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            rol.Nombre = "Admin";
            int result = await RolDAL.Modificar(rol);
            Assert.AreNotEqual(0,result);
        }
  

        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            var resultRol = await RolDAL.ObtenerPorId(rol);
            Assert.AreEqual(rol.Id, resultRol.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultRoles = await RolDAL.ObtenerTodos();
            Assert.AreNotEqual(0, resultRoles.Count);
        }

        [TestMethod()]
        public async Task T5BuscarTest()
        {
            var rol = new Rol();
            rol.Nombre = "a";
            rol.Top_Aux = 10;
            var resultRoles = await RolDAL.Buscar(rol);
            Assert.AreNotEqual(0,resultRoles.Count);
        }
        [TestMethod()]
        public async Task T6EliminarTest()
        {
            var rol = new Rol();
            rol.Id = rolInicial.Id;
            int result = await RolDAL.Eliminar(rol);
            Assert.AreNotEqual(0, result);
        }
    }
}