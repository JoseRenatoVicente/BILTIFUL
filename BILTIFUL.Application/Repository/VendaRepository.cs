using BILTIFUL.Application.Repository.Base;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BILTIFUL.Application.Repository
{
    public class VendaRepository : RepositorySQL<Venda>
    {

        ItemVendaRepository itemVendaRepository = new ItemVendaRepository();

        public VendaRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public VendaRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<Venda> GetAllVendas()
        {
            return Get("SELECT Id, DataVenda, Cliente, ValorTotal " +
                "FROM dbo.Venda");
        }

        public List<Venda> GetByData(DateTime dataVenda)
        {
            return Get("SELECT Id, DataVenda, Cliente, ValorTotal " +
                "FROM dbo.Venda " +
                $"WHERE DataVenda = '{dataVenda.ToString("yyyy-MM-dd")}'");
        }

        public List<Venda> GetByCliente(string cpf)
        {
            return Get("SELECT Id, DataVenda, Cliente, ValorTotal " +
                "FROM dbo.Venda " +
                $"WHERE Cliente = '{cpf}')");
        }

        public Venda GetById(int id)
        {
            Venda venda = Get("SELECT Id, DataVenda, Cliente, ValorTotal " +
                "FROM dbo.Venda " +
                $"WHERE Id = {id}").FirstOrDefault();

            venda.Itens = itemVendaRepository.GetById(venda.Id);
            return venda;
        }

        public Venda Add(Venda venda)
        {
            string query = "INSERT INTO Venda" +
     "(Cliente, ValorTotal) " +
     "OUTPUT Inserted.Id " +
    "VALUES(@cliente, @valorTotal)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@cliente", venda.Cliente);
            command.Parameters.AddWithValue("@valorTotal", venda.ValorTotal);

            var reader = command.ExecuteReader();
            reader.Read();
            venda.Id = int.Parse(reader["Id"].ToString());

            venda.Itens.ForEach(e =>
            {
                e.Id = venda.Id;
                itemVendaRepository.Add(e);
            });

            return venda;
        }

        protected override Venda Map(IDataRecord record)
        {
            return new Venda()
            {
                Id = int.Parse(record["Id"].ToString()),
                DataVenda = DateTime.Parse(record["DataVenda"].ToString()),
                Cliente = record["Cliente"].ToString(),
                ValorTotal = float.Parse(record["ValorTotal"].ToString())
            };

        }
    }
}
