using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Models
{
    public class Contribuicao
    {
        public int Codigo { get; set; }
        public int CodigoUsuario { get; set; }
        public int CodigoTarefa { get; set; }

        public string Validar()
        {
            
            if (CodigoUsuario <= 0)
                return "Contribuidor da tarefa inválido!";

            if (CodigoTarefa <= 0)
                return "Taerfa inválida!";


            return null;
        }
    }
}
