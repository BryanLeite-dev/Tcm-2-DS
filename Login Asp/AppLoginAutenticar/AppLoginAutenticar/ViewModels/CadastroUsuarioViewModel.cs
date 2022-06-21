using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace AppLoginAutenticar.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(100, ErrorMessage = "Limite máximo de 100 caracteres")]
        public string UsuNome { get; set; }

        [Required(ErrorMessage = "Informe seu login")]
        [MaxLength(100, ErrorMessage = "Limite máximo de 50 caracteres")]
        [Remote("SelectLogin", "Autenticacao", ErrorMessage = "O login já existe!")]

        public string Login { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [MinLength(6, ErrorMessage = "Limite mínimo de 6 caracteres")]
        [MaxLength(100, ErrorMessage = "Limite máximo de 100 caracteres")]
        [DataType(DataType.Password)]

        public string Senha { get; set; }

        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas são diferentes")]


        public string ConfirmarSenha { get; set; }
    }
}