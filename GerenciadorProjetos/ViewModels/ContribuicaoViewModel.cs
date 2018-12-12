using GerenciadorProjetos.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.ViewModels
{
    public class ContribuicaoViewModel : Contribuicao
    {
        public string NomeContribuidor { get; set; }
        public string NomeTarefa { get; set; }
    }
}
