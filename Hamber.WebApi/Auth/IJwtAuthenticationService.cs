using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamber.EntidadesDeNegocio;

namespace Hamber.WebApi.Auth
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(Usuario pUsuario);
    }
}
