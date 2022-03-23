using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BILTIFUL.Application.Repository
{
    public class ClienteRepository : RepositorySQL<Cliente>
    {
        public ClienteRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ClienteRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Cliente> GetAllClientes()
        {
            return Get("SELECT CPF, Nome, DataNascimento, Sexo, UltimaCompra, DataCadastro, Situacao FROM dbo.Cliente");
        }

        public List<Cliente> GetByNome(string nome)
        {
            return Get("SELECT CPF, Nome, DataNascimento, Sexo, UltimaCompra, DataCadastro, Situacao " +
                "FROM dbo.Cliente " +
                 $"WHERE UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public Cliente GetByCPF(string cpf)
        {
            return Get("SELECT CPF, Nome, DataNascimento, Sexo, UltimaCompra, DataCadastro, Situacao " +
                "FROM dbo.Cliente " +
                $"WHERE CPF LIKE '{cpf}%'").FirstOrDefault();
        }

        public List<Cliente> SearchClientes(Func<Cliente, bool> where)
        {
            return GetAllClientes().Where(where).ToList();
        }

        public Cliente Add(Cliente cliente)
        {
            string query = "INSERT INTO Cliente" +
                 "(CPF, Nome, DataNascimento, Sexo, UltimaCompra, DataCadastro, Situacao) " +
                "VALUES(@cpf, @nome, @dataNascimento, @sexo, @ultimaCompra, @dataCadastro, @situacao)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@cpf", cliente.CPF);
            command.Parameters.AddWithValue("@nome", cliente.Nome);
            command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
            command.Parameters.AddWithValue("@sexo", (char)cliente.Sexo);
            command.Parameters.AddWithValue("@ultimaCompra", cliente.UltimaCompra);
            command.Parameters.AddWithValue("@dataCadastro", cliente.DataCadastro);
            command.Parameters.AddWithValue("@situacao", (char)cliente.Situacao);

            command.ExecuteNonQuery();

            return cliente;
        }

        public Cliente Update(Cliente cliente)
        {
            string query = "UPDATE Cliente SET " +
                "Nome=@nome, DataNascimento=@dataNascimento, Sexo=@sexo, UltimaCompra=@ultimaCompra, DataCadastro=@dataCadastro, Situacao=@situacao " +
                "WHERE CPF=@cpf";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@nome", cliente.Nome);
            command.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
            command.Parameters.AddWithValue("@sexo", (char)cliente.Sexo);
            command.Parameters.AddWithValue("@ultimaCompra", cliente.UltimaCompra);
            command.Parameters.AddWithValue("@dataCadastro", cliente.DataCadastro);
            command.Parameters.AddWithValue("@situacao", (char)cliente.Situacao);
            command.Parameters.AddWithValue("@cpf", cliente.CPF);


            command.ExecuteNonQuery();

            return cliente;
        }

        public List<Cliente> AddRange(List<Cliente> clientes)
        {
            clientes.ForEach(e => Add(e));
            return clientes;
        }

        public bool Remove(string cpf)
        {
            var command = CreateCommand($"UPDATE Cliente SET Situacao='{(char)Situacao.Inativo}' WHERE CPF = @cpf");
            command.Parameters.AddWithValue("@cpf", cpf);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }


        public bool ClienteBloqueado(string cpf)
        {
            string query = "SELECT CPF FROM dbo.Risco WHERE CPF ='" + cpf + "'";

            SqlDataReader reader = CreateCommand(query).ExecuteReader();

            return reader.Read();
        }

        public bool BloquearCliente(string cpf)
        {
            var command = CreateCommand("INSERT INTO Risco (CPF) VALUES (@cpf)");
            command.Parameters.AddWithValue("@cpf", cpf);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        public bool DesbloquearCliente(string cpf)
        {
            var command = CreateCommand("DELETE from Risco WHERE CPF = @cpf");
            command.Parameters.AddWithValue("@cpf", cpf);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        protected override Cliente Map(IDataRecord record)
        {
            return new Cliente()
            {
                CPF = record["CPF"].ToString(),
                Nome = record["Nome"].ToString(),
                DataNascimento = DateTime.Parse(record["DataNascimento"].ToString()),
                Sexo = (Sexo)char.Parse(record["Sexo"].ToString()),
                UltimaCompra = DateTime.Parse(record["UltimaCompra"].ToString()),
                DataCadastro = DateTime.Parse(record["DataCadastro"].ToString()),
                Situacao = (Situacao)char.Parse(record["Situacao"].ToString())
            };

        }
    }
}

