using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Dominio
{
    public class Cliente
    {
        public int cd_Cliente { get; set; }
        [MaxLength(13)]
        public int no_Telefone { get; set; }
        [MaxLength(80)]
        public string nm_Cliente { get; set; }
        [MaxLength(80)]
        public string ds_Email { get; set; }
        [MaxLength(8)]
        public int no_CEP { get; set; }
        [MaxLength(80)]
        public string ds_Complemento { get; set; }
        [MaxLength(80)]
        public string nm_Logradoro { get; set; }
        [MaxLength(5)]
        public int no_Logradoro { get; set; }
        [MaxLength(40)]
        public string nm_Cidade { get; set; }
        [MaxLength(40)]
        public string nm_Bairro { get; set; }
        [MaxLength(11)]
        public int no_CPF { get; set; }
        [MaxLength(14)]
        public int no_CNPJ { get; set; }
    }
}
