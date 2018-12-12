using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using GerenciadorProjetos.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GerenciadorProjetos.Models
{
    public class Usuario
    {
        // 1234
        private const string SenhaPadrao = "nASQ3H8lnWf0yjEVnaA3FQ==:mHcnKzyXu2zYJ6CUXw3X/AuLbXivf3O092nI3RFevhc=";

        private const int CodigoHash = 0x3ae7fef3;

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

        public static string CriarHashSenha(string senha)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashSenha = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1024,
                numBytesRequested: 256 / 8));

            return Convert.ToBase64String(salt) + ":" + hashSenha;
        }

        public static string EfetuarLogin(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrEmpty(senha))
            {
                return null;
            }

            try
            {
                string senhaBanco = UsuarioRepository.ObterSenhaBanco(login, out int codigo);
                if (senhaBanco == null)
                {
                    return null;
                }

                string[] partes = senhaBanco.Split(':');

                byte[] salt = Convert.FromBase64String(partes[0]);

                string hashSenha = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: senha,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1024,
                    numBytesRequested: 256 / 8));

                if (!hashSenha.Equals(partes[1]))
                {
                    return null;
                }

                salt = new byte[32];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                string token = BitConverter.ToString(salt).Replace("-", "");

                UsuarioRepository.GravarToken(codigo, token);

                return (codigo ^ CodigoHash).ToString("X8") + token;
            }
            catch
            {
                return null;
            }
        }

        public static void EfetuarLogout(int codigo)
        {
            UsuarioRepository.ApagarToken(codigo);
        }

        public static bool ValidarLogado(string cookie, out int codigo, out string nome, out string login)
        {
            codigo = 0;
            nome = null;
            login = null;

            if (cookie == null || cookie.Length != 72)
            {
                return false;
            }

            if (int.TryParse(cookie.Substring(0, 8), System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out codigo) == false)
            {
                return false;
            }

            codigo ^= CodigoHash;

            return UsuarioRepository.ValidarToken(codigo, cookie.Substring(8), out nome, out login);
        }
    }
}
