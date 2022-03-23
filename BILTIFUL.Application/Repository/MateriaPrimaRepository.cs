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
    public class MateriaPrimaRepository : RepositorySQL<MPrima>
    {
        public MateriaPrimaRepository()
        {
            connection = new SqlConnection(Configuration.ConnectionString);
        }

        public MateriaPrimaRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public List<MPrima> GetAllMPrimas()
        {
            return Get("SELECT Id, Nome, UltimaCompra, DataCadastro, Situacao FROM dbo.MPrima");
        }

        public List<MPrima> GetByNome(string nome)
        {
            return Get("SELECT Id, Nome, UltimaCompra, DataCadastro, Situacao " +
                "FROM dbo.MPrima " +
                $"WHERE UPPER(Nome) LIKE UPPER('{nome}%')");
        }

        public MPrima GetById(string id)
        {
            return Get("SELECT Id, Nome, UltimaCompra, DataCadastro, Situacao " +
                "FROM dbo.MPrima " +
                $"WHERE Id = '{id}'").FirstOrDefault();
        }

        public List<MPrima> SearchMPrimas(Func<MPrima, bool> where)
        {
            return GetAllMPrimas().Where(where).ToList();
        }

        public MPrima Add(MPrima mPrima)
        {
            mPrima.Id = "MP" + (Count() + 1).ToString().PadLeft(4, '0');

            string query = "INSERT INTO MPrima" +
                 "(Id, Nome, UltimaCompra, DataCadastro, Situacao) " +
                "VALUES(@id, @nome, @ultimaCompra, @dataCadastro, @situacao)";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@id", mPrima.Id);
            command.Parameters.AddWithValue("@nome", mPrima.Nome);
            command.Parameters.AddWithValue("@ultimaCompra", mPrima.UltimaCompra);
            command.Parameters.AddWithValue("@dataCadastro", mPrima.DataCadastro);
            command.Parameters.AddWithValue("@situacao", (char)mPrima.Situacao);

            command.ExecuteNonQuery();

            return mPrima;
        }

        public MPrima Update(MPrima mPrima)
        {
            string query = "UPDATE MPrima SET " +
                "Nome=@nome WHERE Id=@id";
            var command = CreateCommand(query);

            command.Parameters.AddWithValue("@nome", mPrima.Nome);
            command.Parameters.AddWithValue("@id", mPrima.Id);

            command.ExecuteNonQuery();

            return mPrima;
        }

        public List<MPrima> AddRange(List<MPrima> mPrimas)
        {
            mPrimas.ForEach(e => Add(e));
            return mPrimas;
        }

        public bool Remove(string id)
        {
            var command = CreateCommand($"UPDATE MPrima SET Situacao='{(char)Situacao.Inativo}' WHERE Id = @id");
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() == 1 ? true : false;
        }

        protected override MPrima Map(IDataRecord record)
        {
            return new MPrima()
            {
                Id = record["Id"].ToString(),
                Nome = record["Nome"].ToString(),
                UltimaCompra = DateTime.Parse(record["UltimaCompra"].ToString()),
                DataCadastro = DateTime.Parse(record["DataCadastro"].ToString()),
                Situacao = (Situacao)char.Parse(record["Situacao"].ToString())
            };

        }
    }
}
