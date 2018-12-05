using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GerenciadorProjetos.Repositories
{
    public static class ProjetoRepository
    {
        public static List<ProjetoViewModel> Listar()
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
SELECT codigo_projeto, nome_projeto, descricao_projeto, dataentrega_projeto, criador_projeto, tipo_projeto,
nome_usuario, descricao_tipoProjeto
FROM projeto
INNER JOIN usuario ON usuario.codigo_usuario = projeto.criador_projeto
INNER JOIN tipoprojeto ON tipoprojeto.codigo_tipoProjeto = projeto.tipo_projeto
WHERE excluido_projeto = 0
", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<ProjetoViewModel> lista = new List<ProjetoViewModel>();

                        while (reader.Read())
                        {
                            lista.Add(new ProjetoViewModel()
                            {
                                Codigo = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Descricao = reader.GetString(2),
                                DataEntrega = reader.GetDateTime(3),
                                CodigoCriadorProjeto = reader.GetInt32(4),
                                CodigoTipoProjeto = reader.GetInt32(5),

                                NomeCriadorProjeto = reader.GetString(6),
                                DescricaoTipoProjeto = reader.GetString(7)
                            });
                        }

                        return lista;
                    }
                }
            }
        }

        public static Projeto Obter(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
SELECT codigo_projeto, nome_projeto, descricao_projeto, dataentrega_projeto, criador_projeto, tipo_projeto
FROM projeto
WHERE codigo_projeto = @codigo_projeto AND excluido_projeto = 0
", connection))
                {
                    command.Parameters.AddWithValue("@codigo_projeto", codigo);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Projeto()
                        {
                            Codigo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Descricao = reader.GetString(2),
                            DataEntrega = reader.GetDateTime(3),
                            CodigoCriadorProjeto = reader.GetInt32(4),
                            CodigoTipoProjeto = reader.GetInt32(5)
                        };
                    }
                }
            }
        }

        public static void Criar(Projeto projeto)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
INSERT INTO projeto (nome_projeto, descricao_projeto, dataentrega_projeto, criador_projeto, tipo_projeto, excluido_projeto)
VALUES (@nome_projeto, @descricao_projeto, @dataentrega_projeto, @criador_projeto, @tipo_projeto, 0)
", connection))
                {
                    command.Parameters.AddWithValue("@nome_projeto", projeto.Nome);
                    command.Parameters.AddWithValue("@descricao_projeto", projeto.Descricao);
                    command.Parameters.AddWithValue("@dataentrega_projeto", projeto.DataEntrega);
                    command.Parameters.AddWithValue("@criador_projeto", projeto.CodigoCriadorProjeto);
                    command.Parameters.AddWithValue("@tipo_projeto", projeto.CodigoTipoProjeto);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Alterar(Projeto projeto)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
UPDATE projeto SET
nome_projeto = @nome_projeto,
descricao_projeto = @descricao_projeto,
dataentrega_projeto = @dataentrega_projeto,
criador_projeto = @criador_projeto,
tipo_projeto = @tipo_projeto
WHERE codigo_projeto = @codigo_projeto AND excluido_projeto = 0
", connection))
                {
                    command.Parameters.AddWithValue("@nome_projeto", projeto.Nome);
                    command.Parameters.AddWithValue("@descricao_projeto", projeto.Descricao);
                    command.Parameters.AddWithValue("@dataentrega_projeto", projeto.DataEntrega);
                    command.Parameters.AddWithValue("@criador_projeto", projeto.CodigoCriadorProjeto);
                    command.Parameters.AddWithValue("@tipo_projeto", projeto.CodigoTipoProjeto);

                    command.Parameters.AddWithValue("@codigo_projeto", projeto.Codigo);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Excluir(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
UPDATE projeto SET
excluido_projeto = 1
WHERE codigo_projeto = @codigo_projeto
", connection))
                {
                    command.Parameters.AddWithValue("@codigo_projeto", codigo);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
