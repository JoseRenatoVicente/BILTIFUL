using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Application.Repository
{
    public class ItemProducaoRepository : RepositorySQL<ItemProducao>
    {
        public ItemProducaoRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ItemProducaoRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<ItemProducao> GetAllItensProducao()
        {
            return Get("SELECT Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima FROM dbo.ItemProducao");
        }

        public List<ItemProducao> GetById(int id)
        {
            return Get("SELECT Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima " +
            "FROM dbo.ItemProducao " +
            $"WHERE Id = {id}");
        }

        public ItemProducao Add(ItemProducao itemVenda)
        {
            string query = "INSERT INTO ItemProducao" +
                 "(Id, DataProducao, MateriaPrima, QuantidadeMateriaPrima) " +
                "VALUES(@id, @dataProducao, @materiaPrima, @quantidadeMateriaPrima)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", itemVenda.Id);
            command.Parameters.AddWithValue("@dataProducao", itemVenda.DataProducao);
            command.Parameters.AddWithValue("@materiaPrima", itemVenda.MateriaPrima);
            command.Parameters.AddWithValue("@quantidadeMateriaPrima", itemVenda.QuantidadeMateriaPrima);

            command.ExecuteNonQuery();

            return itemVenda;
        }

        protected override ItemProducao Map(IDataRecord record)
        {
            return new ItemProducao()
            {
                Id = int.Parse(record["Id"].ToString()),
                DataProducao = DateTime.Parse(record["DataProducao"].ToString()),
                MateriaPrima = record["MateriaPrima"].ToString(),
                QuantidadeMateriaPrima = float.Parse(record["QuantidadeMateriaPrima"].ToString())
            };

        }
    }
}
