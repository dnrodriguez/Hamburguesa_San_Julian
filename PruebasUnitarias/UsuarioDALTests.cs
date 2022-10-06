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
    public class UsuarioDALTests
    {
        private static Usuario usuarioInicial = new Usuario { Id = 2, IdRol = 1, Login = "OscarUser", Password = "12345" };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "Ale";
            usuario.Apellido = "Ramirez";
            usuario.Login = "OscarUser";
            string password = "12345";
            usuario.Password = password;
            usuario.Status = (byte)Status_Usuario.INACTIVO;
            int result = await UsuarioDAL.Crear(usuario);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Id = usuario.Id;
            usuarioInicial.Password = password;
            usuarioInicial.Login = usuario.Login;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "Liss";
            usuario.Apellido = "Ramirez";
            usuario.Login = "OscarUser1";
            usuario.Status = (byte)Status_Usuario.ACTIVO;
            int result = await UsuarioDAL.Modificar(usuario);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Login = usuario.Login;
        }

        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            var resultUsuario = await UsuarioDAL.ObtenerPorId(usuario);
            Assert.AreEqual(usuario.Id, resultUsuario.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultUsuarios = await UsuarioDAL.ObtenerTodos();
            Assert.AreNotEqual(0, resultUsuarios.Count);
        }

        [TestMethod()]
        public async Task T5BuscarTest()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "A";
            usuario.Apellido = "a";
            usuario.Login = "A";
            usuario.Status = (byte)Status_Usuario.ACTIVO;
            usuario.Top_Aux = 10;
            var resultUsuarios = await UsuarioDAL.Buscar(usuario);
            Assert.AreNotEqual(0, resultUsuarios.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirRolesTest()
        {
            var usuario = new Usuario();
            usuario.IdRol = usuarioInicial.IdRol;
            usuario.Nombre = "A";
            usuario.Apellido = "a";
            usuario.Login = "A";
            usuario.Status = (byte)Status_Usuario.ACTIVO;
            usuario.Top_Aux = 10;
            var resultUsuarios = await UsuarioDAL.BuscarIncluirRoles(usuario);
            Assert.AreNotEqual(0, resultUsuarios.Count);
            var ultimoUsuario = resultUsuarios.FirstOrDefault();
            Assert.IsTrue(ultimoUsuario.Rol != null && usuario.IdRol == ultimoUsuario.Rol.Id);
        }

        [TestMethod()]
        public async Task T7CambiarPasswordTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            string passwordNuevo = "1234567";
            usuario.Password = passwordNuevo;
            var result = await UsuarioDAL.CambiarPasswordAsync(usuario, usuarioInicial.Password);
            Assert.AreNotEqual(0, result);
            usuarioInicial.Password = passwordNuevo;
        }

        [TestMethod()]
        public async Task T8LoginTest()
        {
            var usuario = new Usuario();
            usuario.Login = usuarioInicial.Login;
            usuario.Password = usuarioInicial.Password;
            var resultUsuario = await UsuarioDAL.Login(usuario);
            Assert.AreEqual(usuario.Login, resultUsuario.Login);
        }

        [TestMethod()]
        public async Task T9EliminarTest()
        {
            var usuario = new Usuario();
            usuario.Id = usuarioInicial.Id;
            int result = await UsuarioDAL.Eliminar(usuario);
            Assert.AreNotEqual(0, result);
        }
    }
}