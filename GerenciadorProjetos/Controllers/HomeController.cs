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
