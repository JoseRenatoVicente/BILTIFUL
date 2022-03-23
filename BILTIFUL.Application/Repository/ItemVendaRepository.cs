using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Application.Repository
{
    public class ItemVendaRepository : RepositorySQL<ItemVenda>
    {
        public ItemVendaRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ItemVendaRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<ItemVenda> GetAllItensVenda()
        {
            return Get("SELECT Id, Produto, Quantidade, ValorUnitario FROM dbo.ItemVenda");
        }

        public List<ItemVenda> GetById(int id)
        {
            return Get("SELECT Id, Produto, Quantidade, ValorUnitario " +
            "FROM dbo.ItemVenda " +
            $"WHERE Id = {id}");
        }

        public ItemVenda Add(ItemVenda itemVenda)
        {
            string query = "INSERT INTO ItemVenda" +
                 "(Id, Produto, Quantidade, ValorUnitario, TotalItem) " +
                "VALUES(@id, @produto, @quantidade, @valorUnitario, @totalItem)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", itemVenda.Id);
            command.Parameters.AddWithValue("@produto", itemVenda.Produto);
            command.Parameters.AddWithValue("@quantidade", itemVenda.Quantidade);
            command.Parameters.AddWithValue("@valorUnitario", itemVenda.ValorUnitario);
            command.Parameters.AddWithValue("@totalItem", itemVenda.TotalItem);

            command.ExecuteNonQuery();

            return itemVenda;
        }

        protected override ItemVenda Map(IDataRecord record)
        {
            return new ItemVenda()
            {
                Id = int.Parse(record["Id"].ToString()),
                Produto = record["Produto"].ToString(),
                Quantidade = float.Parse(record["Quantidade"].ToString()),
                ValorUnitario = float.Parse(record["ValorUnitario"].ToString())
            };

        }
    }
}
