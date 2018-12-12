using GerenciadorProjetos.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.ViewModels
{
    public class ColaboracaoViewModel : Colaboracao
    {
        public string NomeColaborador { get; set; }
        public string NomeProjeto { get; set; }
    }
}
