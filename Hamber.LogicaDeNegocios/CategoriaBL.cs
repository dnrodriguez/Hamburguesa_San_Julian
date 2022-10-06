﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.AccesoDatos;
using Hamber.EntidadesDeNegocio;

namespace Hamber.LogicaDeNegocios
{
    public class CategoriaBL
    {
        public async Task<int> Crear(Categoria pCategoria)
        {
            return await CategoriaDAL.Crear(pCategoria);
        }

        public async Task<int> Modificar(Categoria pCategoria)
        {
            return await CategoriaDAL.Modificar(pCategoria);
        }

        public async Task<int> Eliminar(Categoria pCategoria)
        {
            return await CategoriaDAL.Eliminar(pCategoria);
        }

        public async Task<Categoria> ObtenerPorId(Categoria pCategoria)
        {
            return await CategoriaDAL.ObtenerPorId(pCategoria);
        }

        public async Task<List<Categoria>> ObtenerTodos()
        {
            return await  CategoriaDAL.ObtenerTodos();
        }
        public async Task<List<Categoria>> Buscar(Categoria pCategoria)
        {
            return await CategoriaDAL.Buscar(pCategoria);
        }
    }
}
