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
    public class MarcaDALTests
    {
        private static Marca marcaInicial = new Marca { Id = 2 };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var marca = new Marca();
            marca.Nombre = "buger";
            int result = await MarcaDAL.Crear(marca);
            Assert.AreNotEqual(0,result);
            marcaInicial.Id = marca.Id;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var marca = new Marca();
            marca.Id= marcaInicial.Id;
            marca.Nombre = "bugger";
            int result = await MarcaDAL.Modificar(marca);
            Assert.AreNotEqual(0,result);
        }

        

        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var marca = new Marca();
            marca.Id = marcaInicial.Id;
            var resultMarca = await MarcaDAL.ObtenerPorId(marca);
            Assert.AreEqual(marca.Id, resultMarca.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultMarcas = await MarcaDAL.ObtenerTodos();
            Assert.AreNotEqual(0,resultMarcas.Count);
        }

        [TestMethod()]
        public async Task T5BuscarTest()
        {
            var marca = new Marca();
            marca.Nombre = "u";
            marca.Top_Aux = 10;
            var resultMarcas = await MarcaDAL.Buscar(marca);
            Assert.AreNotEqual(0,resultMarcas.Count);
        }
        [TestMethod()]
        public async Task T6EliminarTest()
        {
            var marca = new Marca();
            marca.Id = marcaInicial.Id;
            int result = await MarcaDAL.Eliminar(marca);
            Assert.AreNotEqual(0,result);
        }
    }
}