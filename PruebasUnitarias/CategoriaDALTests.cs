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
    public class CategoriaDALTests
    {
        private static Categoria categoriaInicial = new Categoria { Id = 2 };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var categoria = new Categoria();
            categoria.Nombre = "bebidas";
            int result = await CategoriaDAL.Crear(categoria);
            Assert.AreNotEqual(0,result);
            categoriaInicial.Id = categoria.Id;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var categoria = new Categoria();
            categoria.Id= categoriaInicial.Id;
            categoria.Nombre = "bebida";
            int result = await CategoriaDAL.Modificar(categoria);
            Assert.AreNotEqual(0,result);
        }

        

        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var categoria = new Categoria();
            categoria.Id = categoriaInicial.Id;
            var resultCategoria = await CategoriaDAL.ObtenerPorId(categoria);
            Assert.AreEqual(categoria.Id ,resultCategoria.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultCategorias = await CategoriaDAL.ObtenerTodos();
            Assert.AreNotEqual(0,resultCategorias.Count);
        }
        [TestMethod()]
        public async Task T5EliminarTest()
        {
            var categoria = new Categoria();
            categoria.Id = categoriaInicial.Id;
            int result = await CategoriaDAL.Eliminar(categoria);
            Assert.AreNotEqual(0,result);
        }
        [TestMethod()]
        public async Task T5BuscarTest()
        {
            var categoria = new Categoria();
            categoria.Nombre = "e";
            categoria.Top_Aux = 10;
            var resultCategorias = await CategoriaDAL.Buscar(categoria);
            Assert.AreNotEqual(0, resultCategorias.Count);
        }
    }
}