using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GerenciadorProjetos.Repositories
{
    public static class TarefaRepository
    {
        public static List<TarefaViewModel> Listar()
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
SELECT 
    t.codigo_tarefa, 
    nome_tarefa, 
    descricao_tarefa,
    prazo_tarefa,
    atribuidor_tarefa, 
    status_tarefa, 
    t.codigo_projeto,
    u.nome_usuario,
    st.descricao_statusTarefa
FROM TAREFA AS t
INNER JOIN projeto AS p ON(t.codigo_projeto = p.codigo_projeto)
INNER JOIN usuario AS u ON(t.atribuidor_tarefa = u.codigo_usuario)
INNER JOIN statustarefa AS st ON(st.codigo_statusTarefa = t.status_tarefa)
WHERE t.excluido_tarefa = 0
", connection))
               
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<TarefaViewModel> lista = new List<TarefaViewModel>();

                        while (reader.Read())
                        {
                            lista.Add(new TarefaViewModel()
                            {
                                Codigo = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Descricao = reader.GetString(2),
                                Prazo = reader.GetDateTime(3),
                                CodigoAtribuidorTarefa = reader.GetInt32(4),
                                CodigoStatusTarefa = reader.GetInt32(5),
                                CodigoProjeto = reader.GetInt32(6),

                                NomeAtribuidorTarefa = reader.GetString(7),
                                DescricaoStatusTarefa = reader.GetString(8)
                            });
                        }

                        return lista;
                    }
                }
            }
        }

        public static Tarefa Obter(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
SELECT 
    t.codigo_tarefa, 
    nome_tarefa, 
    descricao_tarefa,
    prazo_tarefa,
    atribuidor_tarefa, 
    status_tarefa, 
    t.codigo_projeto,
    u.nome_usuario,
    st.descricao_statusTarefa
FROM TAREFA AS t
INNER JOIN projeto AS p ON(t.codigo_projeto = p.codigo_projeto)
INNER JOIN usuario AS u ON(t.atribuidor_tarefa = u.codigo_usuario)
INNER JOIN statustarefa AS st ON(st.codigo_statusTarefa = t.status_tarefa)
WHERE t.excluido_tarefa = 0 AND t.codigo_tarefa = @codigo_tarefa;
", connection))
                {
                    command.Parameters.AddWithValue("@codigo_tarefa", codigo);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Tarefa()
                        {
                            Codigo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Descricao = reader.GetString(2),
                            Prazo = reader.GetDateTime(3),
                            CodigoAtribuidorTarefa = reader.GetInt32(4),
                            CodigoStatusTarefa = reader.GetInt32(5),
                            CodigoProjeto = reader.GetInt32(6),
                            
                        };
                    }
                }
            }
        }

        public static void Criar(Tarefa tarefa)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
INSERT INTO tarefa (nome_tarefa, descricao_tarefa, prazo_tarefa, atribuidor_tarefa, status_tarefa,codigo_projeto, excluido_tarefa)
VALUES (@nome_tarefa, @descricao_tarefa, @prazo_tarefa, @atribuidor_tarefa, @status_tarefa, @codigo_projeto, 0)
", connection))
                {
                    command.Parameters.AddWithValue("@nome_tarefa", tarefa.Nome);
                    command.Parameters.AddWithValue("@descricao_tarefa", tarefa.Descricao);
                    command.Parameters.AddWithValue("@prazo_tarefa", tarefa.Prazo);
                    command.Parameters.AddWithValue("@atribuidor_tarefa", tarefa.CodigoAtribuidorTarefa);
                    command.Parameters.AddWithValue("@status_tarefa", tarefa.CodigoStatusTarefa);
                    command.Parameters.AddWithValue("@codigo_projeto", tarefa.CodigoProjeto);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Editar(Tarefa tarefa)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                    UPDATE tarefa SET
                    nome_tarefa = @nome_tarefa,
                    descricao_tarefa = @descricao_tarefa,
                    prazo_tarefa = @prazo_tarefa,
                    atribuidor_tarefa = @atribuidor_tarefa,
                    status_tarefa = @status_tarefa,
                    codigo_projeto = @codigo_projeto
                    WHERE codigo_tarefa = @codigo_tarefa AND excluido_tarefa = 0;
                    ", connection))
                {
                    command.Parameters.AddWithValue("@nome_tarefa", tarefa.Nome);
                    command.Parameters.AddWithValue("@descricao_tarefa", tarefa.Descricao);
                    command.Parameters.AddWithValue("@prazo_tarefa", tarefa.Prazo);
                    command.Parameters.AddWithValue("@atribuidor_tarefa", tarefa.CodigoAtribuidorTarefa);
                    command.Parameters.AddWithValue("@status_tarefa", tarefa.CodigoStatusTarefa);
                    command.Parameters.AddWithValue("@codigo_projeto", tarefa.CodigoProjeto);

                    command.Parameters.AddWithValue("@codigo_tarefa", tarefa.Codigo);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Excluir(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                UPDATE tarefa SET
                excluido_tarefa = 1
                WHERE codigo_tarefa = @codigo_tarefa
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_tarefa", codigo);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
