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
    public class ProductoDALTests
    {
        private static Producto productoInicial = new Producto { Id = 3, IdCategoria = 2, IdMarca = 2 };

        [TestMethod()]
        public async Task T1CrearTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdMarca = productoInicial.IdMarca;
            producto.Nombre = "Coca cola";
            producto.Descripcion = "bebida vitaminada";
            producto.Imagen = "https://www.cocacola.es/content/dam/one/es/es2/coca-cola/products/productos/dic-2021/CC_Origal.jpg";
            producto.Precio = "5.35";
            int result = await ProductoDAL.Crear(producto);
            Assert.AreNotEqual(0, result);
            productoInicial.Id=producto.Id;
        }

        [TestMethod()]
        public async Task T2ModificarTest()
        {
            var producto = new Producto();
            producto.Id = productoInicial.Id;
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdMarca = productoInicial.IdMarca;
            producto.Nombre = "Hamburguer";
            producto.Descripcion = "Grasas";
            producto.Imagen = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQbS0RCu9tiq4_0uS8z7jvcBkZ72C5RyIEVqw&usqp=CAU";
            producto.Precio = "23.3";
            int result = await ProductoDAL.Modificar(producto);
            Assert.AreNotEqual(0, result);
        }



        [TestMethod()]
        public async Task T3ObtenerPorIdTest()
        {
            var producto = new Producto();
            producto.Id = productoInicial.Id;
            var resultProducto = await ProductoDAL.ObtenerPorId(producto);
            Assert.AreEqual(producto.Id, resultProducto.Id);
        }

        [TestMethod()]
        public async Task T4ObtenerTodosTest()
        {
            var resultProductos = await ProductoDAL.ObtenerTodos();
            Assert.AreNotEqual(0, resultProductos.Count);
        }

        [TestMethod()]
        public async Task T5BuscarTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdMarca = productoInicial.IdMarca;
            producto.Nombre = "a";
            producto.Descripcion = "a";
            producto.Precio = "23.3";
            producto.Top_Aux = 10;
            var resultProductos = await ProductoDAL.Buscar(producto);
            Assert.AreNotEqual(0, resultProductos.Count);
        }

        [TestMethod()]
        public async Task T6BuscarIncluirMarcasTest()
        {
            var producto = new Producto();
            producto.IdCategoria = productoInicial.IdCategoria;
            producto.IdMarca = productoInicial.IdMarca;
            producto.Nombre = "H";
            producto.Descripcion = "a";
            producto.Precio = "23.3";
            producto.Top_Aux = 10;
            var resultProductos = await ProductoDAL.BuscarIncluirMarca_Categoria(producto);
            Assert.AreNotEqual(0,resultProductos.Count);
            var ultimoProducto = resultProductos.FirstOrDefault();
            Assert.IsTrue(ultimoProducto.Marca != null && producto.IdMarca == ultimoProducto.Marca.Id);
            Assert.IsTrue(ultimoProducto.Categoria != null && producto.IdCategoria == ultimoProducto.Categoria.Id);
        }

        //[TestMethod()]
        //public async Task T7BuscarIncluirCategoriasTest()
        //{
        //    var producto = new Producto();
        //    producto.IdCategoria = productoInicial.IdCategoria;
        //    producto.IdMarca = productoInicial.IdMarca;
        //    producto.Nombre = "H";
        //    producto.Descripcion = "a";
        //    producto.Precio = 13.3;
        //    producto.Top_Aux = 10;
        //    Assert.Fail();
        //}

       

        [TestMethod()]
        public async Task T7EliminarTest()
        {
            var producto = new Producto();
            producto.Id = productoInicial.Id;
            int result = await ProductoDAL.Eliminar(producto);
            Assert.AreNotEqual(0,result);
        }
    }
}