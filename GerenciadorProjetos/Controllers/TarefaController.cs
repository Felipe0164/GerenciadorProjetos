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
    public class TarefaController : PrincipalController
    {
        public IActionResult Index()
        {
            List<TarefaViewModel> tarefas = TarefaRepository.Listar();
            return View(tarefas);
        }

        public IActionResult Listar()
        {
            List<TarefaViewModel> tarefas = TarefaRepository.Listar();
            return PartialView("_Listar", tarefas);
        }

        public IActionResult Criar()
        {
            ViewBag.Criando = true;
            return PartialView("_CriarEditar");
        }

        public IActionResult Editar(int codigo)
        {
            Tarefa tarefa = TarefaRepository.Obter(codigo);
            return PartialView("_CriarEditar", tarefa);
        }

        [HttpPost]
        public IActionResult Gravar([FromForm] Tarefa tarefa)
        {
            string mensagem = null;
            if (tarefa == null)
            {
                mensagem = "Dados inválidos!";
            }
            else
            {
                mensagem = tarefa.Validar();
            }

            if (mensagem != null)
            {
                return Erro(mensagem);
            }

            if (tarefa.Codigo == 0)
            {
                TarefaRepository.Criar(tarefa);
            }
            else
            {
                TarefaRepository.Editar(tarefa);
            }

            return Ok();
        }

        public IActionResult Excluir(int codigo)
        {
            TarefaRepository.Excluir(codigo);
            return Ok();
        }
    }
}
