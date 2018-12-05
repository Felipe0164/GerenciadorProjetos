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
    public class ProjetoController : PrincipalController
    {
        public IActionResult Index()
        {
            List<ProjetoViewModel> projetos = ProjetoRepository.Listar();
            return View(projetos);
        }

        public IActionResult Listar()
        {
            List<ProjetoViewModel> projetos = ProjetoRepository.Listar();
            return PartialView("_Listar", projetos);
        }

        public IActionResult Criar()
        {
            ViewBag.Criando = true;
            return PartialView("_CriarEditar");
        }

        public IActionResult Editar(int codigo)
        {
            Projeto projeto = ProjetoRepository.Obter(codigo);
            return PartialView("_CriarEditar", projeto);
        }

        [HttpPost]
        public IActionResult Gravar([FromForm] Projeto projeto)
        {
            string mensagem = null;
            if (projeto == null)
            {
                mensagem = "Dados inválidos!";
            }
            else
            {
                mensagem = projeto.Validar();
            }

            if (mensagem != null)
            {
                return Erro(mensagem);
            }

            if (projeto.Codigo == 0)
            {
                ProjetoRepository.Criar(projeto);
            }
            else
            {
                ProjetoRepository.Alterar(projeto);
            }

            return Ok();
        }

        public IActionResult Excluir(int codigo)
        {
            ProjetoRepository.Excluir(codigo);
            return Ok();
        }
    }
}
