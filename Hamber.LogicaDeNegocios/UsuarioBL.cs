using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using Hamber.AccesoDatos;

namespace Hamber.LogicaDeNegocios
{
    public class UsuarioBL
    {
        #region CRUD

        public async Task<int> Crear(Usuario pUsuario)
        {
            return await UsuarioDAL.Crear(pUsuario);
        }

        public async Task<int> Modificar(Usuario pUsuario)
        {
            return await UsuarioDAL.Modificar(pUsuario);
        }

        public async Task<int> Eliminar(Usuario pUsuario)
        {
            return await UsuarioDAL.Eliminar(pUsuario);
        }

        public async Task<Usuario> ObtenerPorId(Usuario pUsuario)
        {
            return await UsuarioDAL.ObtenerPorId(pUsuario);
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await UsuarioDAL.ObtenerTodos();
        }

        public async Task<List<Usuario>> Buscar(Usuario pUsuario)
        {
            return await UsuarioDAL.Buscar(pUsuario);
        }
        #endregion

        public async Task<Usuario> Login(Usuario pUsuario)
        {
            return await UsuarioDAL.Login(pUsuario);
        }

        public async Task<int> CambiarPasswordAsync(Usuario pUsuario, string pPasswordAnt)
        {
            return await UsuarioDAL.CambiarPasswordAsync(pUsuario, pPasswordAnt);
        }

        public async Task<List<Usuario>> BuscarIncluirRoles(Usuario pUsuario)
        {
            return await UsuarioDAL.BuscarIncluirRoles(pUsuario);
        }

       
    }
}