using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inova.Models;

namespace Inova.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
        UsuarioModel BuscarSessaoDoUsuario();
    }
}