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
    public class ContribuicaoController : PrincipalController
    {
        public IActionResult Index()
        {
            List<ContribuicaoViewModel> contribuicoes = ContribuicaoRepository.Listar();
            return View(contribuicoes);
        }

        public IActionResult Listar()
        {
            List<ContribuicaoViewModel> contribuicoes = ContribuicaoRepository.Listar();
            return PartialView("_Listar", contribuicoes);
        }

        public IActionResult Criar()
        {
            ViewBag.Criando = true;
            return PartialView("_CriarEditar");
        }

        public IActionResult Editar(int codigo)
        {
            Contribuicao contribuicao = ContribuicaoRepository.Obter(codigo);
            return PartialView("_CriarEditar", contribuicao);
        }

        [HttpPost]
        public IActionResult Gravar([FromForm] Contribuicao contribuicao)
        {
            string mensagem = null;
            if (contribuicao == null)
            {
                mensagem = "Dados inválidos!";
            }
            else
            {
                mensagem = contribuicao.Validar();
            }

            if (mensagem != null)
            {
                return Erro(mensagem);
            }

            if (contribuicao.Codigo == 0)
            {
                ContribuicaoRepository.Criar(contribuicao);
            }
            else
            {
                ContribuicaoRepository.Alterar(contribuicao);
            }

            return Ok();
        }

        public IActionResult Excluir(int codigo)
        {
            ContribuicaoRepository.Excluir(codigo);
            return Ok();
        }
    }
}
