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
        ItemProducaoRepository itemProducaoRepository = new ItemProducaoRepository();
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
                $"WHERE DataProducao = '{dataProducao.ToString("yyyy-MM-dd")}'");
        }

        public List<Producao> GetByProduto(long codigoBarras)
        {
            return Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao " +
                $"WHERE Produto = {codigoBarras}");
        }

        public Producao GetById(int id)
        {
            Producao producao = Get("SELECT Id, DataProducao, Produto, Quantidade " +
                "FROM dbo.Producao " +
                $"WHERE Id = {id}").FirstOrDefault();

            producao.Itens = itemProducaoRepository.GetById(producao.Id);
            return producao;
        }

        public List<Producao> SearchMPrimas(Func<Producao, bool> where)
        {
            return GetAllProducoes().Where(where).ToList();
        }

        public Producao Add(Producao producao)
        {
            string query = "INSERT INTO Producao" +
"(Produto, Quantidade) " +
"OUTPUT Inserted.Id " +
"VALUES(@produto, @quantidade)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@produto", producao.Produto);
            command.Parameters.AddWithValue("@quantidade", producao.Quantidade);

            var reader = command.ExecuteReader();
            reader.Read();
            producao.Id = int.Parse(reader["Id"].ToString());

            producao.Itens.ForEach(e =>
            {
                e.Id = producao.Id;
                itemProducaoRepository.Add(e);
            });

            return producao;
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
