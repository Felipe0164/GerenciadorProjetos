using GerenciadorProjetos.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.ViewModels
{
    public class TarefaViewModel : Tarefa
    {
        public string NomeAtribuidorTarefa { get; set; }
        public string DescricaoStatusTarefa { get; set; }
        public string Colaborador { get; set; }
    }
}
