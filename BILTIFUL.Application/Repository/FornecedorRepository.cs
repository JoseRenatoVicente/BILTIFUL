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
    public class FornecedorRepository : RepositorySQL<Fornecedor>
    {
        public FornecedorRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public FornecedorRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Fornecedor> GetAllFornecedores()
        {
            return Get("SELECT CNPJ, RazaoSocial, DataAbertura, UltimaCompra, DataCadastro, Situacao FROM dbo.Fornecedor");
        }

        public Fornecedor GetByCNPJ(string cnpj)
        {
            return Get("SELECT CNPJ, RazaoSocial, DataAbertura, UltimaCompra, DataCadastro, Situacao " +
                "FROM dbo.Fornecedor " +
                $"WHERE CNPJ = '{cnpj}'").FirstOrDefault();
        }

        public List<Fornecedor> SearchFornecedores(Func<Fornecedor, bool> where)
        {
            return GetAllFornecedores().Where(where).ToList();
        }

        public Fornecedor Add(Fornecedor fornecedor)
        {
            string query = "INSERT INTO Fornecedor" +
                 "(CNPJ, RazaoSocial, DataAbertura, Situacao) " +
                "VALUES(@cnpj, @razaoSocial, @dataAbertura, @situacao)";

            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
            command.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
            command.Parameters.AddWithValue("@dataAbertura", fornecedor.DataAbertura);
            command.Parameters.AddWithValue("@situacao", (char)fornecedor.Situacao);

            command.ExecuteNonQuery();

            return fornecedor;
        }

        public Fornecedor Update(Fornecedor fornecedor)
        {
            string query = "UPDATE Fornecedor SET " +
                "CNPJ=@cnpj, RazaoSocial=@razaoSocial, DataAbertura=@dataAbertura, UltimaCompra=@ultimaCompra, DataCadastro=@dataCadastro, Situacao=@situacao " +
                "WHERE CNPJ=@cnpj";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
            command.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
            command.Parameters.AddWithValue("@dataAbertura", fornecedor.DataAbertura);
            command.Parameters.AddWithValue("@ultimaCompra", fornecedor.UltimaCompra);
            command.Parameters.AddWithValue("@dataCadastro", fornecedor.DataCadastro);
            command.Parameters.AddWithValue("@situacao", (char)fornecedor.Situacao);

            command.ExecuteNonQuery();

            return fornecedor;
        }

        public List<Fornecedor> AddRange(List<Fornecedor> mPrimas)
        {
            mPrimas.ForEach(e => Add(e));
            return mPrimas;
        }

        public bool Remove(string cnpj)
        {
            var command = CreateCommand($"UPDATE Fornecedor SET Situacao = '{(char)Situacao.Inativo}' WHERE CNPJ = @cnpj");
            command.Parameters.AddWithValue("@cnpj", cnpj);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        public bool FornecedorBloqueado(string cnpj)
        {
            string query = "SELECT CNPJ FROM dbo.Bloqueado WHERE CNPJ ='" + cnpj + "'";

            SqlDataReader reader = CreateCommand(query).ExecuteReader();

            return reader.Read();
        }

        public bool BloquearFornecedor(string cnpj)
        {
            var command = CreateCommand("INSERT INTO Bloqueado (CNPJ) VALUES (@cnpj)");
            command.Parameters.AddWithValue("@cnpj", cnpj);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        public bool DesbloquearFornecedor(string cnpj)
        {
            var command = CreateCommand("DELETE from Bloqueado WHERE CNPJ = @cnpj");
            command.Parameters.AddWithValue("@cnpj", cnpj);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        protected override Fornecedor Map(IDataRecord record)
        {
            return new Fornecedor()
            {
                CNPJ = record["CNPJ"].ToString(),
                RazaoSocial = record["RazaoSocial"].ToString(),
                DataAbertura = DateTime.Parse(record["DataAbertura"].ToString()),
                UltimaCompra = DateTime.Parse(record["UltimaCompra"].ToString()),
                DataCadastro = DateTime.Parse(record["DataCadastro"].ToString()),
                Situacao = (Situacao)char.Parse(record["Situacao"].ToString())
            };
        }
    }
}
