using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Models
{
    public class Projeto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int CodigoCriadorProjeto { get; set; }
        public int CodigoTipoProjeto { get; set; }

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

            if (DataEntrega.Year < 1900)
                return "Data de entrega inválida!";

            if (CodigoCriadorProjeto <= 0)
                return "Criador do projeto inválido!";

            if (CodigoTipoProjeto <= 0)
                return "Tipo do projeto inválido!";

            return null;
        }
    }
}
