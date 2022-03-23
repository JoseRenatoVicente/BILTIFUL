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
            return Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra");
        }

        public List<Compra> GetByData(DateTime dataCompra)
        {
            return Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra " +
                $"WHERE DataCompra = '{dataCompra.ToString("yyyy-MM-dd")}'");
        }

        public List<Compra> GetByFornecedor(string cnpj)
        {
            return Get("SELECT Id, DataCompra, Fornecedor, ValorTotal " +
                "FROM dbo.Compra " +
                $"WHERE Fornecedor = '{cnpj}')");
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
     "OUTPUT Inserted.Id "+
    "VALUES(@fornecedor, @valorTotal)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@fornecedor", compra.Fornecedor);
            command.Parameters.AddWithValue("@valorTotal", compra.ValorTotal);

            var reader = command.ExecuteReader();
            reader.Read();
            compra.Id = int.Parse(reader["Id"].ToString());

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
