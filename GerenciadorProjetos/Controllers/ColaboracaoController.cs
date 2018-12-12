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
    public class ColaboracaoController : PrincipalController
    {
        public IActionResult Index()
        {
            List<ColaboracaoViewModel> colaboracoes = ColaboracaoRepository.Listar();
            return View(colaboracoes);
        }

        public IActionResult Listar()
        {
            List<ColaboracaoViewModel> colaboracoes = ColaboracaoRepository.Listar();
            return PartialView("_Listar", colaboracoes);
        }

        public IActionResult Criar()
        {
            ViewBag.Criando = true;
            return PartialView("_CriarEditar");
        }

        public IActionResult Editar(int codigo)
        {
            Colaboracao colaboracao = ColaboracaoRepository.Obter(codigo);
            return PartialView("_CriarEditar", colaboracao);
        }

        [HttpPost]
        public IActionResult Gravar([FromForm] Colaboracao colaboracao)
        {
            string mensagem = null;
            if (colaboracao == null)
            {
                mensagem = "Dados inválidos!";
            }
            else
            {
                mensagem = colaboracao.Validar();
            }

            if (mensagem != null)
            {
                return Erro(mensagem);
            }

            if (colaboracao.Codigo == 0)
            {
                ColaboracaoRepository.Criar(colaboracao);
            }
            else
            {
                ColaboracaoRepository.Alterar(colaboracao);
            }

            return Ok();
        }

        public IActionResult Excluir(int codigo)
        {
            ColaboracaoRepository.Excluir(codigo);
            return Ok();
        }
    }
}
