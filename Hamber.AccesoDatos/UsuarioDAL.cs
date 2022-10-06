using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Hamber.AccesoDatos
{
    public class UsuarioDAL
    {
        private static void EncriptarMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUsuario.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                {
                    strEncriptar += result[i].ToString("x2").ToLower();
                }
                pUsuario.Password = strEncriptar;
            }
        }

        private static async Task<bool> ExisteLogin(Usuario pUsuario, DBContexto pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.Usuario
                .FirstOrDefaultAsync(u => u.Login == pUsuario.Login &&
                u.Id != pUsuario.Id);

            if (loginUsuarioExiste != null && loginUsuarioExiste.Id > 0 &&
                loginUsuarioExiste.Login == pUsuario.Login)
                result = true;

            return result;
        }

        #region CRUD
        public static async Task<int> Crear(Usuario pUsuario)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, DbContext);
                if (existeLogin == false)
                {
                    pUsuario.FechaRegistro = DateTime.Now;
                    EncriptarMD5(pUsuario);
                    DbContext.Add(pUsuario);
                    result = await DbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> Modificar(Usuario pUsuario)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                bool existeLogin = await ExisteLogin(pUsuario, DbContext);
                if (existeLogin == false)
                {
                    var usuario = await DbContext.Usuario
                        .FirstOrDefaultAsync(u => u.Id == pUsuario.Id);

                    usuario.IdRol = pUsuario.IdRol;
                    usuario.Nombre = pUsuario.Nombre;
                    usuario.Apellido = pUsuario.Apellido;
                    usuario.Login = pUsuario.Login;
                    usuario.Status = pUsuario.Status;

                    DbContext.Update(usuario);
                    result = await DbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }

        public static async Task<int> Eliminar(Usuario pUsuario)
        {
            int result = 0;
            using (var DbContext = new DBContexto())
            {
                var usuario = await DbContext.Usuario
                    .FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
                DbContext.Remove(usuario);
                result = await DbContext.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Usuario> ObtenerPorId(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var DbContext = new DBContexto())
            {
                usuario = await DbContext.Usuario
                    .FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
            }
            return usuario;
        }

        public static async Task<List<Usuario>> ObtenerTodos()
        {
            var usuarios = new List<Usuario>();
            using (var DbContext = new DBContexto())
            {
                usuarios = await DbContext.Usuario.ToListAsync();
            }
            return usuarios;
        }

        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery,
            Usuario pUsuario)
        {
            if (pUsuario.Id > 0)
                pQuery = pQuery.Where(u => u.Id == pUsuario.Id);

            if (pUsuario.IdRol > 0)
                pQuery = pQuery.Where(u => u.IdRol == pUsuario.IdRol);

            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(u => u.Nombre.Contains(pUsuario.Nombre));

            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(u => u.Apellido.Contains(pUsuario.Apellido));

            if (!string.IsNullOrWhiteSpace(pUsuario.Login))
                pQuery = pQuery.Where(u => u.Login.Contains(pUsuario.Login));

            if (pUsuario.Status > 0)
                pQuery = pQuery.Where(u => u.Status == pUsuario.Status);

            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year,
                    pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(u => u.FechaRegistro >= fechaInicial &&
                    u.FechaRegistro <= fechaFinal);
            }

            pQuery = pQuery.OrderByDescending(u => u.Id).AsQueryable();
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();

            return pQuery;
        }

        public static async Task<List<Usuario>> Buscar(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var DbContext = new DBContexto())
            {
                var select = DbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
        #endregion

        public static async Task<List<Usuario>> BuscarIncluirRoles(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var DbContext = new DBContexto())
            {
                var select = DbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(u => u.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }

        public static async Task<Usuario> Login(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var DbContext = new DBContexto())
            {
                EncriptarMD5(pUsuario);
                usuario = await DbContext.Usuario
                    .FirstOrDefaultAsync(u => u.Login == pUsuario.Login &&
                    u.Password == pUsuario.Password &&
                    u.Status == (byte)Status_Usuario.ACTIVO);
            }
            return usuario;
        }

        public static async Task<int> CambiarPasswordAsync(Usuario pUsuario,
            string pPassAnt)
        {
            int result = 0;
            var usuarioPassAnt = new Usuario { Password = pPassAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var DbContext = new DBContexto())
            {
                var usuario = await DbContext.Usuario
                    .FirstOrDefaultAsync(u => u.Id == pUsuario.Id);
                if (usuarioPassAnt.Password == usuario.Password)
                {
                    EncriptarMD5(pUsuario);
                    usuario.Password = pUsuario.Password;
                    DbContext.Update(usuario);
                    result = await DbContext.SaveChangesAsync();
                }
                else
                    throw new Exception("La contraseña actual no es válida");
            }
            return result;
        }
    }
}
