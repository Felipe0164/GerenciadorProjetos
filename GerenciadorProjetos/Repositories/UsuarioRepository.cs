using GerenciadorProjetos.Models;
using GerenciadorProjetos.ViewModels;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace GerenciadorProjetos.Repositories
{
    public static class UsuarioRepository
    {
        public static List<UsuarioViewModel> Listar()
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT nome_usuario, login_usuario, tu.nome_tipoUsuario AS tipo_usuario
                FROM usuario AS u
                INNER JOIN tipousuario AS tu ON(u.tipo_usuario = tu.codigo_tipoUsuario)
                WHERE u.excluido_usuario = 0;
                ", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<UsuarioViewModel> lista = new List<UsuarioViewModel>();

                        while (reader.Read())
                        {
                            lista.Add(new UsuarioViewModel()
                            {
                                Codigo = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Login = reader.GetString(2),
                                TipoUsuario = reader.GetString(3)
                            });
                        }

                        return lista;
                    }
                }
            }
        }

        public static Usuario Obter(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                SELECT nome_usuario, login_usuario, tu.nome_tipoUsuario AS tipo_usuario
                FROM usuario AS u
                INNER JOIN tipousuario AS tu ON(u.tipo_usuario = tu.codigo_tipoUsuario)
                WHERE u.excluido_usuario = 0 AND u.codigo_usuario=@codigo_usuario;
                ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", codigo);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        return new Usuario()
                        {
                            Codigo = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Login = reader.GetString(2),
                            Tipo = reader.GetInt32(3)
                        };
                    }
                }
            }
        }

        public static void Criar(Usuario usuario)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                INSERT INTO usuario (nome_usuario, login_usuario, senha_usuario, tipo_usuario, excluido_usuario) 
                VALUES (@nome_usuario, @login_usuario, @senha_usuario, @tipo_usuario, 0)
                ", connection))
                {
                    command.Parameters.AddWithValue("@nome_usuario", usuario.Nome);
                    command.Parameters.AddWithValue("@login_usuario", usuario.Login);
                    command.Parameters.AddWithValue("@senha_usuario", usuario.Senha);
                    command.Parameters.AddWithValue("@tipo_usuario", usuario.Tipo);
                    

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Alterar(Usuario usuario)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                    UPDATE usuarioSET 
	                    nome_usuario= @nome_usuario, 
	                    login_usuario= @login_usuario, 
	                    senha_usuario= @senha_usuario, 
	                    tipo_usuario= @tipo_usuario 
                    WHERE (codigo_usuario= @codigo_usuario)
                    ", connection))
                {
                    command.Parameters.AddWithValue("@nome_usuario", usuario.Nome);
                    command.Parameters.AddWithValue("@login_usuario", usuario.Login);
                    command.Parameters.AddWithValue("@senha_usuario", usuario.Senha);
                    command.Parameters.AddWithValue("@tipo_usuario", usuario.Tipo);

                    command.Parameters.AddWithValue("@codigo_usuario", usuario.Codigo);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Excluir(int codigo)
        {
            using (MySqlConnection connection = Sql.Open())
            {
                using (MySqlCommand command = new MySqlCommand(@"
                    UPDATE usuario SET
                    excluido_usuario = 1
                    WHERE codigo_usuario = @codigo_usuario
                    ", connection))
                {
                    command.Parameters.AddWithValue("@codigo_usuario", codigo);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
