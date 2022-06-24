using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using DataBase;
using Dominio;

namespace DLL
{
    public class ClienteDAO
    {

        private Banco dbAutorizacao;

        public void Inserir(Cliente cliente)
        {
            var strQuery = "";
            strQuery += "INSERT INTO tblCliente(IdUsu, NomeUsu, Cargo, Nasc)";
            strQuery += string.Format("VALUES (default, '{0}', '{1}', STR_TO_DATE('{2}','%d/%m/%Y %T'));", cliente.NomeUsu, cliente.Cargo, cliente.Nasc);

            using (dbAutorizacao = new Banco())
            {
                dbAutorizacao.ExecutaComando(strQuery);
            }
        }

        public void Atualiza(Cliente cliente)
        {
            var strQuery = "";
            strQuery += "UPDATE tblCliente SET";
            strQuery += string.Format(" NomeUsu = '{0}', ", cliente.NomeUsu);
            strQuery += string.Format("Cargo = '{0}', ", cliente.Cargo);
            strQuery += string.Format("Nasc = STR_TO_DATE('{0}','%d/%m/%Y %T')", cliente.Nasc);
            strQuery += string.Format("WHERE IdUsu = '{0}';", cliente.IdUsu);

            using (dbAutorizacao = new Banco())
            {
                dbAutorizacao.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Cliente cliente)
        {
            if (cliente.IdUsu > 0)
            {
                Atualiza(cliente);
            }
            else
            {
                Inserir(cliente);
            }
        }

        public void Excluir(int cliente)
        {
            using (dbAutorizacao = new Banco())
            {
                var strQuery = string.Format("DELETE FROM tblCliente where IdUsu = {0}", cliente);
                dbAutorizacao.ExecutaComando(strQuery);
            }
        }

        public Cliente SelecionarId(int Id)
        {
            Cliente usu = new Cliente();

            using (dbAutorizacao = new Banco())
            {
                string strQuery = string.Format("SELECT*FROM tblCliente where IdUsu= {0};", Id);
                var leitor = dbAutorizacao.RetornaComando(strQuery);
                leitor.Read();

                usu.IdUsu = int.Parse(leitor["IdUsu"].ToString());
                usu.NomeUsu = leitor["NomeUsu"].ToString();
                usu.Cargo = leitor["Cargo"].ToString();
                usu.Nasc = DateTime.Parse(leitor["Nasc"].ToString());

                leitor.Close();

                return usu;
            }
        }

        public List<Cliente> Listar()
        {
            using (dbAutorizacao = new Banco())
            {
                var strQuery = "SELECT*FROM tblCliente;";
                var retorno = dbAutorizacao.RetornaComando(strQuery);
                return ListadeCliente(retorno);
            }
        }

        public List<Cliente> ListadeCliente(MySqlDataReader retorno)
        {
            var clientes = new List<Cliente>();
            while (retorno.Read())
            {
                var Tempcliente = new Cliente()
                {
                    IdUsu = int.Parse(retorno["IdUsu"].ToString()),
                    NomeUsu = retorno["NomeUsu"].ToString(),
                    Cargo = retorno["Cargo"].ToString(),
                    Nasc = DateTime.Parse(retorno["Nasc"].ToString())
                };
                clientes.Add(Tempcliente);
            }

            retorno.Close();
            return clientes;
        }
    }
}
