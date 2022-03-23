using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;

namespace BILTIFUL.Core.Controles
{
    public class Controle
    {
        //uma lista para cada arquivos que eu tiver
        public SqlConnection connection { get; set; }

        public Controle()//esse construtora instanciará todas as listas qnd feito
        {
            try
            {
                var datasource = @"DESKTOP-BTO2B72";//instancia do servidor
                var database = "BILTIFUL"; //Base de Dados
                var username = "sa"; //usuario da conexão
                var password = "locao2.0"; //senha

                //sua string de conexão 
                string connString = @"Data Source=" + datasource + ";Initial Catalog="
                            + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

                //cria a instância de conexão com a base de dados
                connection = new SqlConnection(connString);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public Controle(Cliente cliente, SqlConnection connection)
        {

            if (cliente != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarCliente", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cpf", SqlDbType.VarChar).Value = cliente.CPF;
                sql_cmnd.Parameters.AddWithValue("@nome", SqlDbType.NVarChar).Value = cliente.Nome;
                sql_cmnd.Parameters.AddWithValue("@dnascimento", SqlDbType.Date).Value = cliente.DataNascimento.ToString("yyyy/MM/dd");
                sql_cmnd.Parameters.AddWithValue("@sexo", SqlDbType.Char).Value = (char)cliente.Sexo;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public Controle(Fornecedor fornecedor, SqlConnection connection)
        {

            if (fornecedor != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarFornecedor", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cnpj", SqlDbType.VarChar).Value = fornecedor.CNPJ;
                sql_cmnd.Parameters.AddWithValue("@rsocial", SqlDbType.NVarChar).Value = fornecedor.RazaoSocial;
                sql_cmnd.Parameters.AddWithValue("@dabertura", SqlDbType.Date).Value = fornecedor.DataAbertura.ToString("yyyy/MM/dd");
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public Controle(Produto produto, SqlConnection connection)
        {

            if (produto != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarProduto", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cbarras", SqlDbType.VarChar).Value = produto.CodigoBarras;
                sql_cmnd.Parameters.AddWithValue("@nome", SqlDbType.NVarChar).Value = produto.Nome;
                sql_cmnd.Parameters.AddWithValue("@vvenda", SqlDbType.Decimal).Value = produto.ValorVenda.Insert(3, ".");

                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public Controle(MPrima materiaprima, SqlConnection connection)
        {

            if (materiaprima != null)
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarMPrima", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = materiaprima.Id;
                sql_cmnd.Parameters.AddWithValue("@nome", SqlDbType.NVarChar).Value = materiaprima.Nome;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public Controle(Compra compra, SqlConnection connection)
        {

            if (compra != null)
            {
                connection.Open();

                SqlCommand sql_cmnd = new SqlCommand("AdicionarCompra", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = compra.Id;
                sql_cmnd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.NVarChar).Value = compra.Fornecedor;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public Controle(ItemCompra itemcompra, SqlConnection connection)
        {

            if (itemcompra != null)
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarItemCompra", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@qtd", SqlDbType.Decimal).Value = itemcompra.Quantidade.Insert(3,".");
                sql_cmnd.Parameters.AddWithValue("@vunitario", SqlDbType.VarChar).Value = itemcompra.ValorUnitario.Insert(3, ".");
                sql_cmnd.Parameters.AddWithValue("@id_compra", SqlDbType.VarChar).Value = itemcompra.Id;
                sql_cmnd.Parameters.AddWithValue("@id_mprima", SqlDbType.VarChar).Value = itemcompra.MateriaPrima;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }

        }
        public Controle(Producao producao, SqlConnection connection)
        {

            if (producao != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarProducao", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = producao.Id;
                sql_cmnd.Parameters.AddWithValue("@qtd", SqlDbType.Int).Value = int.Parse(producao.Quantidade);
                sql_cmnd.Parameters.AddWithValue("@cbarras_produto", SqlDbType.VarChar).Value = producao.Produto;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public Controle(ItemProducao itemproducao, SqlConnection connection)
        {

            if (itemproducao != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarItemProducao", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@qtdmp", SqlDbType.Decimal).Value = itemproducao.QuantidadeMateriaPrima.ToString().PadLeft(5,'0').Insert(3,".");
                sql_cmnd.Parameters.AddWithValue("@id_producao", SqlDbType.VarChar).Value = itemproducao.Id;
                sql_cmnd.Parameters.AddWithValue("@id_mprima", SqlDbType.VarChar).Value = itemproducao.MateriaPrima;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();
            }

        }
        public Controle(Venda venda, SqlConnection connection)
        {

            if (venda != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarVenda", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.VarChar).Value = venda.Id;
                sql_cmnd.Parameters.AddWithValue("@cpf_cliente", SqlDbType.NVarChar).Value = venda.Cliente;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();

            }

        }
        public Controle(ItemVenda itemvenda, SqlConnection connection)
        {

            if (itemvenda != null)
            {

                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("AdicionarItemVenda", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@qtd", SqlDbType.Decimal).Value = int.Parse(itemvenda.Quantidade);
                sql_cmnd.Parameters.AddWithValue("@id_venda", SqlDbType.VarChar).Value = itemvenda.Id;
                sql_cmnd.Parameters.AddWithValue("@cbarras_produto", SqlDbType.VarChar).Value = itemvenda.Produto;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }


        public Controle(string chave, bool escolha, SqlConnection connection)//true adicionar e false deletar
        {

            if (escolha == true)
            {

                if (CadastroService.ValidaCpf(chave))
                {


                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("AdicionarInadimplente", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@cpf_cliente", SqlDbType.VarChar).Value = chave;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();


                }

                if (CadastroService.ValidaCnpj(chave))
                {

                    using (connection)
                    {
                        connection.Open();
                        SqlCommand sql_cmnd = new SqlCommand("AdicionarBloqueado", connection);
                        sql_cmnd.CommandType = CommandType.StoredProcedure;
                        sql_cmnd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.VarChar).Value = chave;
                        sql_cmnd.ExecuteNonQuery();
                        connection.Close();
                    }

                }

            }
            else
            {
                if (CadastroService.ValidaCpf(chave))
                {


                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("RemoverInadimplente", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@cpf_inadimplente", SqlDbType.VarChar).Value = chave;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();


                }
                if (CadastroService.ValidaCnpj(chave))
                {

                    connection.Open();

                    SqlCommand sql_cmnd = new SqlCommand("RemoverBloqueado", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.VarChar).Value = chave;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();


                }
            }

        }
    }

}
