using AppLoginAutenticar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppLoginAutenticar.ViewModels;
using AppLoginAutenticar.Utils;
using System.Security.Claims;

namespace AppLoginAutenticar.Controllers
{
    public class AutenticacaoController : Controller
    {
        Usuario usuario = new Usuario();
        // GET: Autenticacao
        [HttpGet]
        public ActionResult Insert()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Insert(CadastroUsuarioViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

                usuario.UsuNome = viewmodel.UsuNome;
                usuario.Login = viewmodel.Login;
                usuario.Senha = Hash.GerarHash(viewmodel.Senha);
            
            usuario.InsertUsuario(usuario);
            TempData["MensagemLogin"] = "Cadastro realizado com sucesso, faça o login!";
            return RedirectToAction("Login", "Autenticacao");
        }

        public ActionResult SelectLogin(string Login) 
        {
            bool LoginExist;
            string login = usuario.SelectLogin(Login);

            if (login.Length == 0) 
            {
                LoginExist = false;
            }
            else 
            { 
                LoginExist = true; 
            }
            return Json(!LoginExist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string ReturnUrl) 
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewmodel);
        }

        [HttpPost]

        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }
                        
            usuario = usuario.SelectUsuarios(viewmodel.Login);
            
            if(usuario == null || usuario.Login != viewmodel.Login)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(viewmodel);
            }

            if(usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreto");
                return View(viewmodel);
            }

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Login),
                    new Claim("Login", usuario.Login)
                },
                    "AppAplicationCookie"
                );

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
            {
                return Redirect(viewmodel.UrlRetorno);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("AppAplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]

        public ActionResult AlterarSenha() 
        { 
            return View(); 
        }

        [Authorize]
        [HttpPost]

        public ActionResult AlterarSenha(AlterarSenhaViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            usuario = usuario.SelectUsuarios(login);

            if(Hash.GerarHash(viewmodel.NovaSenha) == usuario.Senha)
            {
                ModelState.AddModelError("Senha Atual", "Senha Incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(viewmodel.NovaSenha);

            usuario.UpdateSenha(usuario);
            TempData["MensagemLogin"] = "Senha alterada com sucesso!";
            return RedirectToAction("Index", "Administrativo");
        }

        public ActionResult Delete(string ReturnUrl)
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Delete(DeleteViewModel viewModel)
        {
          
            usuario.DeleteUsuario(usuario);

            return View();
        }

        public ActionResult Listar() 
        {
            List<Usuario> listarUsuarios = new List<Usuario>()
            {
                new Usuario
                {
                    UsuarioId = 1,
                    UsuNome = "@",
                    Login = "HEHE",
                    Senha = "Que isso meu fi"
                },

                new Usuario
                {
                    UsuarioId = 2,
                    UsuNome = "Jorge",
                    Login = "HEHE",
                    Senha = "Tome"
                },

                new Usuario
                {
                    UsuarioId = 3,
                    UsuNome = "Jorge",
                    Login = "HEHE",
                    Senha = "Ai mamae"
                }
            };

            ViewBag.List = listarUsuarios;
            ViewData["listarUsuarios"] = listarUsuarios;
            return View(listarUsuarios);
        }
    }
}