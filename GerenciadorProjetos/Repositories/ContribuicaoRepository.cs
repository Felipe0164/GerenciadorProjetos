using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GerenciadorProjetos.Repositories
{
    public static class ContribuicaoRepository
    {
        public static List<ContribuicaoViewModel> Listar()
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT co.codigo_contribuicao, co.codigo_usuario, co.codigo_tarefa, u.nome_usuario, t.nome_tarefa
                FROM contribuicao AS co
                INNER JOIN usuario AS u ON (co.codigo_usuario = u.codigo_usuario)
                INNER JOIN tarefa AS t ON (t.codigo_tarefa = co.codigo_tarefa)
                ", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<ContribuicaoViewModel> lista = new List<ContribuicaoViewModel>();

                        while (reader.Read())
                        {
                            lista.Add(new ContribuicaoViewModel()
                            {
                                Codigo = reader.GetInt32(0),
                                CodigoUsuario = reader.GetInt32(1),
                                CodigoTarefa = reader.GetInt32(2),
                                
                                NomeContribuidor = reader.GetString(3),
                                NomeTarefa = reader.GetString(4)
                            });
                        }

                        return lista;
                    }
                }
            }
        }

        public static Contribuicao Obter(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT co.codigo_contribuicao, co.codigo_usuario, co.codigo_tarefa, u.nome_usuario, t.nome_tarefa
                FROM contribuicao AS co
                INNER JOIN usuario AS u ON (co.codigo_usuario = u.codigo_usuario)
                INNER JOIN tarefa AS t ON (t.codigo_tarefa = co.codigo_tarefa)
                WHERE co.codigo_contribuicao = @codigo_contribuicao
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_contribuicao", codigo);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Contribuicao()
                        {
                            Codigo = reader.GetInt32(0),
                            CodigoUsuario = reader.GetInt32(1),
                            CodigoTarefa = reader.GetInt32(2)
                            
                        };
                    }
                }
            }
        }

        public static void Criar(Contribuicao contribuicao)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                INSERT INTO contribuicao (codigo_usuario, codigo_tarefa) 
                VALUES (@codigo_usuario, @codigo_tarefa);
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", contribuicao.CodigoUsuario);
                    command.Parameters.AddWithValue("@codigo_tarefa", contribuicao.CodigoTarefa);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Alterar(Contribuicao contribuicao)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                UPDATE contribuicao SET 
                    codigo_usuario = @codigo_usuario, 
                    codigo_tarefa = @codigo_tarefa 
                WHERE (codigo_contribuicao = @codigo_contribuicao)
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", contribuicao.CodigoUsuario);
                    command.Parameters.AddWithValue("@codigo_tarefa", contribuicao.CodigoTarefa);

                    command.Parameters.AddWithValue("@codigo_contribuicao", contribuicao.Codigo);

                    command.ExecuteNonQuery();
                }
            }
        }

       public static void Excluir(int codigo)
        {
            /* using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                UPDATE projeto SET
                excluido_projeto = 1
                WHERE codigo_projeto = @codigo_projeto
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_colaboracao", codigo);

                    command.ExecuteNonQuery();
                }
            }*/
        }
    }
    }
