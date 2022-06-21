using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppLoginAutenticar.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage = "Informe a senha atual")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres!")]
        [DataType(DataType.Password)]

        public string SenhaAtual { get; set; }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "Informe a nova senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres!")]
        [DataType(DataType.Password)]

        public string NovaSenha { get; set; }

        [Display(Name = "Confirma Senha")]
        [Required(ErrorMessage = "Confirme a Senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas são diferentes!")]
    
        public string ConfirmarSenha { get; set; }
    }
}