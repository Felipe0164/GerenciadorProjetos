using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using GerenciadorProjetos.Repositories;

namespace GerenciadorProjetos.Controllers
{
    public class UsuarioController : PrincipalController
    {
        public IActionResult Index()
        {
            List<UsuarioViewModel> usuarios = UsuarioRepository.Listar();
            return View(usuarios);
        }

        public IActionResult Listar()
        {
            List<UsuarioViewModel> usuarios = UsuarioRepository.Listar();
            return PartialView("_Listar", usuarios);
        }

        public IActionResult Criar()
        {
            ViewBag.Criando = true;
            return PartialView("_CriarEditar");
        }

        public IActionResult Editar(int codigo)
        {
            Usuario usuario = UsuarioRepository.Obter(codigo);
            return PartialView("_CriarEditar", usuario);
        }

        [HttpPost]
        public IActionResult Gravar([FromForm] Usuario usuario)
        {
            string mensagem = null;
            if (usuario == null)
            {
                mensagem = "Dados inválidos!";
            }
            else
            {
                mensagem = usuario.Validar();
            }

            if (mensagem != null)
            {
                return Erro(mensagem);
            }

            if (usuario.Codigo == 0)
            {
                UsuarioRepository.Criar(usuario);
            }
            else
            {
                UsuarioRepository.Alterar(usuario);
            }

            return Ok();
        }

        public IActionResult Excluir(int codigo)
        {
            UsuarioRepository.Excluir(codigo);
            return Ok();
        }
    }
}
