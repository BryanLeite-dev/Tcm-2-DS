using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;


namespace DataBase
{
    public class Banco : IDisposable
    {
        private readonly MySqlConnection conexao;

        public Banco()
        {
            conexao = new MySqlConnection("server=localhost; user id=root; password=12345678; database=dbAutorizacao");
            conexao.Open();
        }


        public void ExecutaComando(string StrQuery)

        {
            var vComando = new MySqlCommand
            {
                CommandText = StrQuery,
                CommandType = CommandType.Text,
                Connection = conexao
            };

            vComando.ExecuteNonQuery();
        }

        public MySqlDataReader RetornaComando(string StrQuery)
        {
            var comando = new MySqlCommand(StrQuery, conexao);
            return comando.ExecuteReader();
        }

        public void Dispose()
        {
            if (conexao.State == ConnectionState.Open)
                conexao.Close();
        }
    }
}
