using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Inova.Models;
using Inova.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Inova.Controllers
{    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    // if (loginModel.Login == "adm" && loginModel.Senha == "171007")
                    // {
                    //     return RedirectToAction("Index", "Home");
                    // }
                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = "Senha ou usuário inválida, tente novamente.";
                    }
                    TempData["MensagemErro"] = "Usuário ou senha inválidos.";
                }
                return RedirectToAction("Login");
            }
            catch (System.Exception)
            {
                TempData["Mensagem"] = "Ocorreu um erro ao tentar realizar o login.";
                return RedirectToAction("Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}