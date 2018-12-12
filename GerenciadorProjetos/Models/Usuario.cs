using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Models
{
    public class Usuario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Tipo { get; set; }
    

        public string Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                return "Nome inválido!";

            // "      Exemplo de nome       "
            // "Exemplo de nome"
            Nome = Nome.Trim();

            if (string.IsNullOrWhiteSpace(Login))
                return "Descrição inválida!";

            Login = Login.Trim();

            if (Tipo <= 0)
                return "Tipo de usuário inválido!";

            return null;
        }
    }
}
