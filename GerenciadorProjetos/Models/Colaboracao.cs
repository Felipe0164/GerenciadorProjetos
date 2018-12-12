using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Models
{
    public class Colaboracao
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoProjeto { get; set; }
        public string Funcao { get; set; }

        public string Validar()
        {
            
            if (CodigoUsuario <= 0)
                return "Colaborador do projeto inválido!";

            if (CodigoProjeto <= 0)
                return "Projeto inválido!";

            if (string.IsNullOrWhiteSpace(Funcao))
                return "Função inválida!";

            // "      Exemplo de nome       "
            // "Exemplo de nome"
            Funcao = Funcao.Trim();

            return null;
        }
    }
}
