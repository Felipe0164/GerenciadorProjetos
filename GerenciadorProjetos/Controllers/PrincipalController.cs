using Microsoft.AspNetCore.Mvc;
using GerenciadorProjetos.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GerenciadorProjetos.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PrincipalController : Controller
    {
        protected int CodigoUsuario { get; private set; }
        protected string NomeUsuario { get; private set; }
        protected string LoginUsuario { get; private set; }

        public string App_Data
        {
            get
            {
                return AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            }
        }

        public IActionResult Erro(string mensagem)
        {
            return new ContentResult()
            {
                Content = mensagem,
                ContentType = "text/plain",
                StatusCode = 500
            };
        }

        public string CaminhoFotos(int id)
        {
            return System.IO.Path.Combine(App_Data, "Fotos", id + ".jpg");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor action;

            if ((action = (context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)) != null)
            {
                if (action.ControllerName.Equals("Home"))
                {
                    if (action.ActionName.Equals("Login") ||
                        action.ActionName.Equals("EfetuarLogin"))
                    {
                        // Pode ignorar o estado de login

                        base.OnActionExecuting(context);

                        return;
                    }
                }
            }

            string cookie = Request.Cookies["usuario"];
            if (cookie != null && Usuario.ValidarLogado(cookie, out int codigo, out string nome, out string login))
            {
                // Tudo ok!
                CodigoUsuario = codigo;
                NomeUsuario = nome;
                LoginUsuario = login;

                base.OnActionExecuting(context);

                return;
            }

            // Não estava logado!
            context.Result = new RedirectToActionResult("Login", "Home", null, false);
        }
    }
}
