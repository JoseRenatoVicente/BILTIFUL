using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Core.Entidades;

using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Banco
{
    public class bd_biltiful
    {
        public static string datasource = @"DESKTOP-KGQAT74";//instancia do servidor
        public static string database = "biltiful"; //Base de Dados
        public static string username = "sa"; //usuario da conexão
        public static string password = "246810"; //senha

        static string connString = @"Data Source=" + datasource + ";Initial Catalog="
                            + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

        SqlConnection connection = new SqlConnection(connString);

        //--------------------------------------

        public void InserirClienteBD(Cliente cliente)
        {
            using (connection)
            {
                connection.Close();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("adicionar_cliente", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cpf_cliente", SqlDbType.NVarChar).Value = cliente.CPF;
                sql_cmnd.Parameters.AddWithValue("@nome_cliente", SqlDbType.NVarChar).Value = cliente.Nome;
                sql_cmnd.Parameters.AddWithValue("@data_nascimento", SqlDbType.DateTime).Value = cliente.DataNascimento;
                sql_cmnd.Parameters.AddWithValue("@sexo", SqlDbType.Char).Value = (char)cliente.Sexo;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void LocalizarCliente(string cpf)
        {
            Console.WriteLine("\t\tCliente");
            Console.WriteLine("===============================");

            connection.Close();
            connection.Open();

            string sql = "select cpf_cliente, nome_cliente, convert(varchar,data_nascimento, 103), sexo, situacao_cliente from tb_cliente where cpf_cliente =" + cpf;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t\t\tCPF: {0}\n" +
                                          "\t\t\tNome: {1}\n" +
                                          "\t\t\tData Nascimento: {2}\n" +
                                          "\t\t\tSexo: {3}\n" +
                                          "\t\t\tSituação: {4}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        public void ExibirRegistroClientes()
        {
            SqlConnection connection = new SqlConnection(connString);

            Console.Clear();
            Console.WriteLine("\t\t\tCliente");
            Console.WriteLine("\t\t\t===============================");

            connection.Close();
            connection.Open();

            string sql = "select cpf_cliente, nome_cliente, convert(varchar,data_nascimento, 103), sexo, situacao_cliente from tb_cliente";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\n\t\t\tCPF: {0}\n" +
                                          "\t\t\tNome: {1}\n" +
                                          "\t\t\tData Nascimento: {2}\n" +
                                          "\t\t\tSexo: {3}\n" +
                                          "\t\t\tSituação: {4}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        public void InserirClienteInadimplente(string cpf)
        {
            using (connection)
            {
                connection.Close();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("adicionar_cliente_inadimplente", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cpf_cliente", SqlDbType.NVarChar).Value = cpf;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool VerificarClienteInadimplente(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);

            connection.Close();
            connection.Open();

            string select = "select cpf_cliente from tb_risco where cpf_cliente =" + cpf; 
            string sql = "delete from tb_risco where cpf_cliente =" + cpf;

            using (SqlCommand command =  new SqlCommand(select, connection))
            {
                using ( SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                }
            }
            connection.Close();
            return false;

        }

        public void RemoverClienteInadimplente(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);

            connection.Close();
            connection.Open();

            string sql = "delete from tb_risco where cpf_cliente =" + cpf;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using SqlDataReader reader = command.ExecuteReader();
            }
            connection.Close();
        }

        public bool VerificarCpfInadimplenteBD(string cpfCliente)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string sql = "select cpf_cliente from tb_risco where cpf_cliente =" + cpfCliente;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("\n\t\t\t\t     -----------------------------------------------------" +
                                                   "\n\t\t\t\t\tSolocitar Cliente conversar com a Gerencia!!");
                                Console.WriteLine("\t\t\t\t\t\t     CPF: {0}", reader.GetString(0));
                                Console.ReadKey();
                                return true;
                            }
                        }
                    }
                    connection.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
            /*            SqlConnection connection = new SqlConnection(connString);

                        using (connection)
                        {
                            connection.Close();
                            connection.Open();
                            string sql = "select cpf_cliente from tb_risco where cpf_cliente =" + cpfCliente;
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine("\n\t\t\t\t     -----------------------------------------------------" +
                                                          "\n\t\t\t\t\tSolocitar Cliente conversar com a Gerencia!!");
                                        Console.WriteLine("\t\t\t\t\t\t     CPF: {0}", reader.GetString(0));
                                        Console.ReadKey();
                                        return true;
                                    }
                                }
                            }
                        }
                        return false;*/
        }

        public bool VerificaCpfExisteBD(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string sql = "select cpf_cliente, nome_cliente, convert(varchar, data_nascimento, 103), sexo from tb_cliente where cpf_cliente =" + cpf;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("\n\t\t\t\t\t---------------------------\n" +
                                                  "\t\t\t\t\tCPF: {0}\n" +
                                                  "\t\t\t\t\tNome Cliente: {1}\n" +
                                                  "\t\t\t\t\tData Nascimento: {2}\n" +
                                                  "\t\t\t\t\tSexo: {3}\n", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                                return true;
                            }
                        }
                    }
                    connection.Close();
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //---------------------------- FORNECEDOR

        public void InserirFornecedorBD(Fornecedor fornecedor)
        {

            using (connection)
            {
                connection.Close();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("adicionar_fornecedor", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@cnpj_fornecedor", SqlDbType.NVarChar).Value = fornecedor.CNPJ;
                sql_cmnd.Parameters.AddWithValue("@razao_social", SqlDbType.NVarChar).Value = fornecedor.RazaoSocial;
                sql_cmnd.Parameters.AddWithValue("@data_abertura", SqlDbType.DateTime).Value = fornecedor.DataAbertura;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void LocalizarFornecedor(string cnpj)
        {
            Console.WriteLine("\n\t\t\tFornecedor");
            Console.WriteLine("\t\t\t===============================");

            connection.Close();
            connection.Open();

            string sql = "select cnpj_fornecedor, razao_social, convert(varchar,data_abertura, 103), convert(varchar,ultima_compra, 103), situacao_fornecedor from tb_fornecedor where cnpj_fornecedor =" + cnpj;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t\t\tCNPJ: {0}\n" +
                                          "\t\t\tRazão Social: {1}\n" +
                                          "\t\t\tData Abertura: {2}\n" +
                                          "\t\t\tUltima Compra: {3}\n" +
                                          "\t\t\tSituação: {4}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        public void BuscaCnpjFornecedoBD(string cnpj)
        {
            connection.Close();
            connection.Open();

            string sql = "select cnpj_fornecedor, razao_social from tb_fornecedor where cnpj_fornecedor =" + cnpj;
        }

        public void ExibirRegistroFornecedor()
        {
            SqlConnection connection = new SqlConnection(connString);

            Console.Clear();
            Console.WriteLine("\n\t\t\tFornecedor");
            Console.WriteLine("\t\t\t===============================");

            connection.Close();
            connection.Open();

            string sql = "select cnpj_fornecedor, razao_social, convert(varchar,data_abertura, 103), convert(varchar,ultima_compra, 103), situacao_fornecedor from tb_fornecedor";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\n\t\t\tCNPJ: {0}\n" +
                                          "\t\t\tRazão Social: {1}\n" +
                                          "\t\t\tData Abertura: {2}\n" +
                                          "\t\t\tUltima Compra: {3}\n" +
                                          "\t\t\tSituação: {4}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        //----------------------------  PRODUTO

        public void InserirProdutoBD(Produto produto)
        {
            using (connection)
            {
                connection.Close();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("adicionar_produto", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@codiga_barra", SqlDbType.NVarChar).Value = produto.CodigoBarras;
                sql_cmnd.Parameters.AddWithValue("@nome_produto", SqlDbType.NVarChar).Value = produto.Nome;
                sql_cmnd.Parameters.AddWithValue("@valor_venda", SqlDbType.Decimal).Value = produto.ValorVenda;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void LocalizarProduto(string produto)
        {
            Console.Clear();
            Console.WriteLine("\t\t\tProduto");
            Console.WriteLine("\t\t===============================");

            connection.Close();
            connection.Open();

            string sql = "select codigo_barra, nome_produto, valor_venda, convert(varchar, ultima_venda, 103), convert(varchar, data_cadastro, 103), situacao_produto from tb_produto where codigo_barra =" + produto;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t\tCódigo Barra: {0}\n" +
                                          "\t\tNome Produto: {1}\n" +
                                          "\t\tValor Venda {2}\n" +
                                          "\t\tUltima Compra: {3}\n" +
                                          "\t\tData Cadastro: {4}\n" +
                                          "\t\tSituação: {5}", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
            }
            connection.Close();
        }

        public void ExibirRegistroProduto()
        {
            SqlConnection connection = new SqlConnection(connString);

            Console.Clear();
            Console.WriteLine("\t\t\tProdutos");
            Console.WriteLine("\t\t=========================");

            connection.Close();
            connection.Open();

            string sql = "select codigo_Barra, nome_produto, valor_venda, convert(varchar, ultima_venda, 103), convert(varchar, data_cadastro, 103), situacao_produto from tb_produto";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\n\t\tCódigo Barra: {0}\n" +
                                          "\t\tNome Produto: {1}\n" +
                                          "\t\tValor Venda: {2}\n" +
                                          "\t\tUltima Compra: {3}\n" +
                                          "\t\tData Cadastro: {4}\n" +
                                          "\t\tSituação: {5}\n" +
                                          "\t\t--------------------------", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
            }
            connection.Close();
        }

        public bool VerificaProdutoExisteBD(string codproduto)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
                connection.Open();
                string sql = "select codigo_barra, nome_produto, valor_venda from tb_produto where codigo_barra =" + codproduto;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("\n\t\t\t\t\t-------------------------------\n" +
                                              "\t\t\t\t\tNome Produto: {1}\n" +
                                              "\t\t\t\t\tCódigo Barra: {0}\n" +
                                              "\t\t\t\t\tValor: {2}\n", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2));
                            return true;
                        }
                    }
                    connection.Close();
                }
                return false;
            }
        }

        //---------------------------- MATÉRIA PRIMA

        public void InserirMateriaPrimaBD(MPrima materiaprima)
        {
            using (connection)
            {
                connection.Close();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("adicionar_materia_prima", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@nome_mat_prima", SqlDbType.NVarChar).Value = materiaprima.Nome;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void LocalizarMateriaPrima(decimal materiaprima)
        {
            connection.Close();
            connection.Open();

            string sql = "select id_mat_prima, nome_mat_prima, convert(varchar, ultima_compra, 103), convert(varchar, data_cadastro, 103), situacao_mat_prima from tb_materia_prima where id_mat_prima = " + materiaprima;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t\tId Matéria Prima: {0}\n" +
                                          "\t\tNome Matéria Prima: {1}\n" +
                                          "\t\tUltima Compra: {2}\n" +
                                          "\t\tData Cadastro: {3}\n" +
                                          "\t\tSituação: {4}", reader.GetDecimal(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        public void ExibirRegistroMateriaPrima()
        {
            SqlConnection connection = new SqlConnection(connString);

            Console.Clear();
            Console.WriteLine("\t\t\tMatéria Prima");
            Console.WriteLine("\t\t===============================");
            connection.Close();
            connection.Open();

            string sql = "select id_mat_prima, nome_mat_prima, convert(varchar, ultima_compra, 103), convert(varchar, data_cadastro, 103), situacao_mat_prima from tb_materia_prima";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\t\tId Matéria Prima: {0}\n" +
                                          "\t\tNome Matéria Prima: {1}\n" +
                                          "\t\tUltima Compra: {2}\n" +
                                          "\t\tData Cadastro: {3}\n" +
                                          "\t\tSituação: {4}\n" +
                                          "\t\t------------------------------\n", reader.GetDecimal(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
                    }
                }
            }
            connection.Close();
        }

        //----------------------------- VENDA
        public void InserirVendaDB(Venda venda)
        {

        }

        public void InserirItemVendaBD(ItemVenda itemvenda)
        {

        }

        public void InserirCompraItem(string n, string s, string t, string a, string q)
        {

        }
    }
}
