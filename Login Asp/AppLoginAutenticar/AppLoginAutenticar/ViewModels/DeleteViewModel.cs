using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppLoginAutenticar.ViewModels
{
    public class DeleteViewModel
    {
        [Display(Name = "ID")]
        [Required(ErrorMessage = "Informe o ID do Usuario")]

        public string UsuarioID { get; set; }

      
    }
}