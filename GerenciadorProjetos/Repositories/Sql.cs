using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GerenciadorProjetos.Repositories
{
    public static class Sql
    {
        public static MySqlConnection Open()
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=gerenciador;Uid=root;Pwd=root;");

            connection.Open();

            return connection;
        }
    }
}
