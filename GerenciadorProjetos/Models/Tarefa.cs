using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Models
{
    public class Tarefa
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Prazo { get; set; }
        public int CodigoAtribuidorTarefa { get; set; }
        public int CodigoStatusTarefa { get; set; }
        public int CodigoProjeto { get; set; }

        public string Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                return "Nome inválido!";

            // "      Exemplo de nome       "
            // "Exemplo de nome"
            Nome = Nome.Trim();

            if (string.IsNullOrWhiteSpace(Descricao))
                return "Descrição inválida!";

            Descricao = Descricao.Trim();

            if (Prazo.Year < 1900)
                return "Prazo inválido!";

            if (CodigoAtribuidorTarefa <= 0)
                return "Atribuidor de tarefa inválido!";

            if (CodigoStatusTarefa <= 0)
                return "Status de tarefa inválido!";

            if (CodigoProjeto <= 0)
                return "Código de projeto inválido!";

            return null;
        }
    }
}
