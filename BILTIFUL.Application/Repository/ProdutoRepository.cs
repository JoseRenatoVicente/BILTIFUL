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
    public class ProdutoRepository : RepositorySQL<Produto>
    {

        public ProdutoRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ProdutoRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Produto> GetAllProdutos()
        {
            return Get("SELECT CodigoBarras, Nome, ValorVenda, UltimaVenda, DataCadastro, Situacao, QuantidadeEstoque FROM dbo.Produto");
        }

        public List<Produto> GetByNome(string nome)
        {
            return Get("SELECT CodigoBarras, Nome, ValorVenda, UltimaVenda, DataCadastro, Situacao, QuantidadeEstoque " +
                "FROM dbo.Produto " +
                $"WHERE Situacao='{(char)Situacao.Ativo}' AND UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public Produto GetByCodigoBarras(string codigoBarras)
        {
            return Get("SELECT CodigoBarras, Nome, ValorVenda, UltimaVenda, DataCadastro, Situacao, QuantidadeEstoque " +
                "FROM dbo.Produto " +
                $"WHERE Situacao='{(char)Situacao.Ativo}' AND CodigoBarras = {codigoBarras}").FirstOrDefault();
        }

        public List<Produto> SearchProdutos(Func<Produto, bool> where)
        {
            return GetAllProdutos().Where(where).ToList();
        }

        public Produto Add(Produto produto)
        {
            string query = "INSERT INTO Produto" +
                 "(Nome, ValorVenda, Situacao, QuantidadeEstoque) " +
                "VALUES(@nome, @valorVenda, @situacao, @quantidadeEstoque)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@valorVenda", produto.ValorVenda);
            command.Parameters.AddWithValue("@situacao", (char)produto.Situacao);
            command.Parameters.AddWithValue("@quantidadeEstoque", produto.QuantidadeEstoque);

            command.ExecuteNonQuery();

            return produto;
        }

        public Produto Update(Produto produto)
        {
            string query = "UPDATE Produto SET " +
                "Nome=@nome, ValorVenda=@valorVenda, Situacao=@situacao, QuantidadeEstoque=@quantidadeEstoque WHERE CodigoBarras=@codigoBarras";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@nome", produto.Nome);
            command.Parameters.AddWithValue("@valorVenda", produto.ValorVenda);
            command.Parameters.AddWithValue("@situacao", (char)produto.Situacao);
            command.Parameters.AddWithValue("@quantidadeEstoque", produto.QuantidadeEstoque);
            command.Parameters.AddWithValue("@codigoBarras", produto.CodigoBarras);

            command.ExecuteNonQuery();

            return produto;
        }

        public List<Produto> AddRange(List<Produto> produtos)
        {
            produtos.ForEach(e => Add(e));
            return produtos;
        }

        public bool Remove(long codigoBarras)
        {
            var command = CreateCommand($"UPDATE Produto SET Situacao='{(char)Situacao.Inativo}' WHERE CodigoBarras = @codigoBarras");
            command.Parameters.AddWithValue("@codigoBarras", codigoBarras);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        protected override Produto Map(IDataRecord record)
        {
            return new Produto()
            {
                CodigoBarras = long.Parse(record["CodigoBarras"].ToString()),
                Nome = record["Nome"].ToString(),
                ValorVenda = float.Parse(record["ValorVenda"].ToString()),
                UltimaVenda = DateTime.Parse(record["UltimaVenda"].ToString()),
                DataCadastro = DateTime.Parse(record["DataCadastro"].ToString()),
                Situacao = (Situacao)char.Parse(record["Situacao"].ToString()),
                QuantidadeEstoque = float.Parse(record["QuantidadeEstoque"].ToString())
            };
        }
    }
}
