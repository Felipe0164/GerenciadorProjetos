using GerenciadorProjetos.Models;
using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.ViewModels
{
    public class ProjetoViewModel : Projeto
    {
        public string NomeCriadorProjeto { get; set; }
        public string DescricaoTipoProjeto { get; set; }
    }
}
