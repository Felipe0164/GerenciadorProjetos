using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GerenciadorProjetos.Repositories
{
    public static class ColaboracaoRepository
    {
        public static List<ColaboracaoViewModel> Listar()
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT c.codigo_colaboracao, c.codigo_usuario, c.codigo_projeto, c.funcao_projeto, u.nome_usuario, p.nome_projeto
                FROM colaboracao AS c
                INNER JOIN usuario AS u ON (c.codigo_usuario = u.codigo_usuario)
                INNER JOIN projeto AS p ON (c.codigo_projeto = p.codigo_projeto)
                WHERE p.excluido_projeto = 0 AND u.excluido_usuario = 0
                ", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<ColaboracaoViewModel> lista = new List<ColaboracaoViewModel>();

                        while (reader.Read())
                        {
                            lista.Add(new ColaboracaoViewModel()
                            {
                                Codigo = reader.GetInt32(0),
                                CodigoUsuario = reader.GetInt32(1),
                                CodigoProjeto = reader.GetInt32(2),
                                Funcao =reader.GetString(3),

                                NomeColaborador = reader.GetString(4),
                                NomeProjeto = reader.GetString(5)
                            });
                        }

                        return lista;
                    }
                }
            }
        }

        public static Colaboracao Obter(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT c.codigo_colaboracao, c.codigo_usuario, c.codigo_projeto, c.funcao_projeto, u.nome_usuario, p.nome_projeto
                FROM colaboracao AS c
                INNER JOIN usuario AS u ON (c.codigo_usuario = u.codigo_usuario)
                INNER JOIN projeto AS p ON (c.codigo_projeto = p.codigo_projeto)
                WHERE p.excluido_projeto = 0 AND u.excluido_usuario = 0 AND c.codigo_colobaracao = @codigo_colobaracao
", connection))
                {
                    command.Parameters.AddWithValue("@codigo_colaboracao", codigo);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Colaboracao()
                        {
                            Codigo = reader.GetInt32(0),
                            CodigoUsuario = reader.GetInt32(1),
                            CodigoProjeto = reader.GetInt32(2),
                            Funcao = reader.GetString(3)
                        };
                    }
                }
            }
        }

        public static void Criar(Colaboracao colaboracao)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                INSERT INTO 
                    colaboracao (codigo_usuario, codigo_projeto, funcao_projeto) 
                VALUES 
                    (@codigo_usuario, @codigo_projeto, @funcao_projeto);
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", colaboracao.CodigoUsuario);
                    command.Parameters.AddWithValue("@codigo_projeto", colaboracao.CodigoProjeto);
                    command.Parameters.AddWithValue("@funcao_projeto", colaboracao.Funcao);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Alterar(Colaboracao colaboracao)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                UPDATE colaboracao SET 
                    codigo_usuario = @codigo_usuario, 
                    codigo_projeto = @codigo_projeto, 
                    funcao_projeto = @funcao_projeto 
                WHERE (codigo_colaboracao = @codigo_colaboracao)
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", colaboracao.CodigoUsuario);
                    command.Parameters.AddWithValue("@codigo_projeto", colaboracao.CodigoProjeto);
                    command.Parameters.AddWithValue("@funcao_projeto", colaboracao.Funcao);

                    command.Parameters.AddWithValue("@codigo_colaboracao", colaboracao.Codigo);

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
