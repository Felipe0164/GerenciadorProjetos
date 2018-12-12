using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GerenciadorProjetos.Models;

namespace GerenciadorProjetos.Controllers
{
    public class HomeController : PrincipalController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult EfetuarLogin(string login, string senha)
        {
            string cookie = Usuario.EfetuarLogin(login, senha);

            if (cookie == null)
            {
                return Erro("Usuário/senha inválidos");
            }

            Response.Cookies.Append("usuario", cookie, new Microsoft.AspNetCore.Http.CookieOptions()
            {
                MaxAge = TimeSpan.FromDays(365)
            });

            return Json(1);
        }

        public IActionResult EfetuarLogout()
        {
            Usuario.EfetuarLogout(CodigoUsuario);

            Response.Cookies.Append("usuario", "", new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Expires = DateTime.Now.AddYears(-2)
            });

            return new RedirectToActionResult("Login", "Home", null, false);
        }

        public IActionResult Exemplo()
        {
            return View();
        }

        public IActionResult Erro()
        {
            return View();
        }
    }
}
