using DataBase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLoginAutenticar.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UsuNome { get; set; }

        [Required]
        [MaxLength(50)]
        [Remote("SelectLogin","Autenticacao",ErrorMessage ="Login já existente")]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        MySqlConnection Conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

        MySqlCommand Comando = new MySqlCommand();

             
        public void InsertUsuario(Usuario usuario)
        {
            Conexao.Open();
            Comando.CommandText = "Call spInsertUsuarios(@UsuNome,@Login,@Senha);";
            Comando.Parameters.Add("@UsuNome",MySqlDbType.VarChar).Value= usuario.UsuNome;
            Comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value= usuario.Login;
            Comando.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            Comando.Connection = Conexao;
            Comando.ExecuteNonQuery();
            Conexao.Close();

        }

        public void DeleteUsuario(Usuario usuario)
        {
            Conexao.Open();
            Comando.CommandText = "Call spDeleteUsuario(@UsuarioID);";
            Comando.Parameters.Add("@UsuarioID", MySqlDbType.VarChar).Value = usuario.UsuarioId;
            Comando.Connection = Conexao;
            Comando.ExecuteNonQuery();
            Conexao.Close();

        }

        public string SelectLogin(string vLogin)
        {
            Conexao.Open();
            Comando.CommandText = "Call spSelectLogin(@Login);";
            Comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            Comando.Connection = Conexao;
            string Login = (string)Comando.ExecuteScalar();
            Conexao.Close();
            if (Login == null)
            { 
                Login = "";
            }    
            return Login;

        }

        public Usuario SelectUsuarios(string vLogin)
        {
            Conexao.Open();
            Comando.CommandText = "Call spSelectUsuarios(@Login);";
            Comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            Comando.Connection = Conexao;
            var readUsuario = Comando.ExecuteReader();
            var tempUsuario = new Usuario();
            

            if (readUsuario.Read())
            {
                tempUsuario.UsuarioId = int.Parse(readUsuario["UsuarioID"].ToString());
                tempUsuario.UsuNome = readUsuario["UsuNome"].ToString();
                tempUsuario.Login = readUsuario["Login"].ToString();
                tempUsuario.Senha = readUsuario["Senha"].ToString();
            };
            readUsuario.Close();
            Conexao.Close();
            return tempUsuario;

        }

        public Usuario ListarUsuarios()
        {
            Conexao.Open();
            Comando.CommandText = "Call spListarUsuarios();";
            Comando.Connection = Conexao;
            var readUsuario = Comando.ExecuteReader();
            var tempUsuario = new Usuario();


            if (readUsuario.Read())
            {
                tempUsuario.UsuarioId = int.Parse(readUsuario["UsuarioID"].ToString());
                tempUsuario.UsuNome = readUsuario["UsuNome"].ToString();
                tempUsuario.Login = readUsuario["Login"].ToString();
                tempUsuario.Senha = readUsuario["Senha"].ToString();
            };
            readUsuario.Close();
            Conexao.Close();
            return tempUsuario;

        }

        public void UpdateSenha(Usuario usuario)
        {
            Conexao.Open();
            Comando.CommandText = "Call spUpdateSenha(@Login,@Senha);";
            Comando.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            Comando.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            Comando.Connection = Conexao;
            Comando.ExecuteNonQuery();
            Conexao.Close();

        }

    }
}