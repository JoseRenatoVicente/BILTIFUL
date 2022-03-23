using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Application.Repository
{
    public class ItemCompraRepository : RepositorySQL<ItemCompra>
    {

        public ItemCompraRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ItemCompraRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<ItemCompra> GetAllItensCompra()
        {
            return Get("SELECT Id, DataCompra, MateriaPrima, Quantidade, ValorUnitario FROM dbo.ItemCompra");
        }

        public List<ItemCompra> GetById(int id)
        {
            return Get("SELECT Id, DataCompra, MateriaPrima, Quantidade, ValorUnitario " +
    "FROM dbo.ItemCompra " +
    $"WHERE Id = {id}");
        }

        public ItemCompra Add(ItemCompra itemCompra)
        {
            string query = "INSERT INTO ItemCompra" +
                 "(Id, MateriaPrima, Quantidade, ValorUnitario, TotalItem) " +
                "VALUES(@id, @materiaPrima, @quantidade, @valorUnitario, @totalItem)";
            var command = CreateCommand(query);


            command.Parameters.AddWithValue("@id", itemCompra.Id);
            command.Parameters.AddWithValue("@materiaPrima", itemCompra.MateriaPrima);
            command.Parameters.AddWithValue("@quantidade", itemCompra.Quantidade);
            command.Parameters.AddWithValue("@valorUnitario", itemCompra.ValorUnitario);
            command.Parameters.AddWithValue("@totalItem", itemCompra.TotalItem);

            command.ExecuteNonQuery();

            return itemCompra;
        }

        protected override ItemCompra Map(IDataRecord record)
        {
            return new ItemCompra()
            {

                Id = int.Parse(record["Id"].ToString()),
                DataCompra = DateTime.Parse(record["DataProducao"].ToString()),
                MateriaPrima = record["MateriaPrima"].ToString(),
                Quantidade = float.Parse(record["Quantidade"].ToString()),
                ValorUnitario = float.Parse(record["ValorUnitario"].ToString())
            };

        }
    }
}
