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
    public class ProducaoRepository : RepositorySQL<Producao>
    {
        public ProducaoRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public ProducaoRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }
        public List<Producao> GetAllProducoes()
        {
            return Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao");
        }

        public List<Producao> GetByData(DateTime dataProducao)
        {
            return Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao " +
                $"WHERE UPPER(Nome) LIKE UPPER('{dataProducao}%')");
        }

        public Producao GetById(int id)
        {
            Producao producao = Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao " +
                $"WHERE Id = {id}").FirstOrDefault();

            producao.Itens = GetItensProducaoById(producao.Id);
            return producao;
        }

        public List<ItemProducao> GetItensProducaoById(int id)
        {
            string query = "SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao " +
                $"WHERE Id = {id}";

            SqlDataReader reader = CreateCommand(query).ExecuteReader();

            List<ItemProducao> items = new List<ItemProducao>();
            while (reader.Read())
            {
                items.Add(MapItemProducao(reader));
            }
            return items;
        }

        public List<Producao> SearchMPrimas(Func<Producao, bool> where)
        {
            return GetAllProducoes().Where(where).ToList();
        }

        public Producao Add(Producao producao)
        {
            var command = CreateCommand("InserirProducao");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ProdutoCodigoBarras", producao.Produto);
            command.Parameters.AddWithValue("@Quantidade", producao.Quantidade);
            command.ExecuteNonQuery();
            

            producao.Itens.ForEach(e =>
            {
                e.Id = producao.Id;
                AddItemProducao(e);
            });

            return producao;
        }

        public void AddItemProducao(ItemProducao itemProducao)
        {
            var command = CreateCommand("InserirItemProducao");
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", itemProducao.Id);
            command.Parameters.AddWithValue("@MateriaPrimaId", itemProducao.MateriaPrima);
            command.Parameters.AddWithValue("@QuantidadeMateriaPrima", itemProducao.QuantidadeMateriaPrima);

            command.ExecuteNonQuery();
        }

        public bool Remove(string id)
        {
            var command = CreateCommand($"UPDATE MPrima SET Situacao='{(char)Situacao.Inativo}' WHERE Id = @id");
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }


        protected ItemProducao MapItemProducao(IDataRecord record)
        {
            return new ItemProducao()
            {
                Id = int.Parse(record["Id"].ToString()),
                DataProducao = DateTime.Parse(record["DataProducao"].ToString()),
                MateriaPrima = record["MateriaPrima"].ToString(),
                QuantidadeMateriaPrima = float.Parse(record["QuantidadeMateriaPrima"].ToString())
            };
        }

        protected override Producao Map(IDataRecord record)
        {
            return new Producao()
            {
                Id = int.Parse(record["Id"].ToString()),
                DataProducao = DateTime.Parse(record["DataProducao"].ToString()),
                Produto = long.Parse(record["Produto"].ToString()),
                Quantidade = float.Parse(record["Quantidade"].ToString())
            };

        }

    }
}
