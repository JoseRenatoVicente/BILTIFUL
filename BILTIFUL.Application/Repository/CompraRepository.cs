using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BILTIFUL.Application.Repository
{
    public class CompraRepository : RepositorySQL<Compra>
    {
        ItemCompraRepository itemCompraRepository = new ItemCompraRepository();
        public CompraRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public CompraRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }
        public List<Compra> GetAllCompras()
        {
            return Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao");
        }

        public List<Compra> GetByData(DateTime dataProducao)
        {
            return Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra " +
                $"WHERE DataProducao = '{dataProducao}')");
        }

        public List<Compra> GetByFornecedor(string cnpj)
        {
            return Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra " +
                $"WHERE CNPJ = '{cnpj}')");
        }

        public Compra GetById(int id)
        {
            Compra compra = Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra " +
                $"WHERE Id = {id}").FirstOrDefault();

            compra.Itens = itemCompraRepository.GetById(compra.Id);
            return compra;
        }


        public Compra Add(Compra compra)
        {
            string query = "INSERT INTO Compra" +
     "(Fornecedor, ValorTotal) " +
    "VALUES(@fornecedor, @valorTotal)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@fornecedor", compra.Fornecedor);
            command.Parameters.AddWithValue("@valorTotal", compra.ValorTotal);

            command.ExecuteNonQuery();

            compra.Itens.ForEach(e =>
            {
                e.Id = compra.Id;
                itemCompraRepository.Add(e);
            });

            return compra;
        }

        protected override Compra Map(IDataRecord record)
        {
            return new Compra()
            {
                Id = int.Parse(record["Id"].ToString()),
                DataCompra = DateTime.Parse(record["DataCompra"].ToString()),
                Fornecedor = record["Fornecedor"].ToString(),
                ValorTotal = float.Parse(record["ValorTotal"].ToString())
            };

        }

    }
}
