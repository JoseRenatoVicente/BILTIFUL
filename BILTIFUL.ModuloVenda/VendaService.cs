using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System.Data.SqlClient;
using System.Data;

namespace BILTIFUL.ModuloVenda
{
    public class VendaService
    {

        Controle controle = new Controle();
        List<ItemVenda> vendaitem = new List<ItemVenda>();
        CadastroService servicocadastro = new CadastroService();
        Venda venda = new Venda();

        string clienteVenda;
        float valorVenda = 0;
        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t ________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| VENDAS |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - CADASTRAR VENDA                            |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR VENDA                            |");
            Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                     |");
            Console.Write("\t\t\t\t\t|________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");
        }

        public void SubMenu(SqlConnection connection)
        {
            int opc;

            do
            {
                Menu();
                if (int.TryParse(Console.ReadLine(), out int CanParse))
                {
                    opc = CanParse;
                }
                else
                {
                    opc = -1;
                }
                switch (opc)
                {
                    case 0:
                        break;
                    case 1:
                        CadastrarVenda(connection);
                        break;
                    case 2:
                        servicocadastro.LocalizarRegistro(connection);
                        break;
                    default:
                        Console.WriteLine("Digite Uma Opção invalida");
                        Console.ReadKey();
                        break;
                }
            } while (0 != opc);

        }
        public void InstanciaBanco()
        {
            Controle conexao = new Controle();
            SubMenu(conexao.connection);
        }
        public void CadastrarVenda(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t------------- Verificar CPF -------------\n");
            Console.Write("\t\t\t\t\tDigite o Cpf do cliente: ");

            clienteVenda = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
            bool inadimplente = false;

            connection.Open();
            String clientebloqueado = "SELECT cpf_cliente FROM dbo.Risco";
            using (SqlCommand command = new SqlCommand(clientebloqueado, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (clienteVenda == reader.GetString(0))
                            inadimplente = true;
                    }
                }
            }
            connection.Close();

            if (inadimplente == true)
            {
                Console.WriteLine("\t\t\t\t\tCliente na lista de inadimplente");
                return;
            }

            bool clienteencontrado = false;
            connection.Open();

            String cliente = "SELECT cpf, nome, dnascimento, sexo, ucompra, dcadastro, situacao FROM dbo.Cliente";
            using (SqlCommand command = new SqlCommand(cliente, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (clienteVenda == reader.GetString(0))
                        {
                            Console.WriteLine("CPF: {0}\nNome: {1}\nData de Nascimento: {2}\nSexo: {3}\nUltima Compra: {4}\nData de Cadastro: {5}\nSituação:{6}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), (Sexo)char.Parse(reader.GetString(3)), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetDateTime(5).ToString("dd/MM/yyyy"), (Situacao)char.Parse(reader.GetString(6)));
                            clienteencontrado = true;
                        }
                    }
                }
            }
            connection.Close();

            if (clienteencontrado == false)
            {
                Console.WriteLine("\t\t\t\t\tCliente nao encontrado.");
            }
            else
            {
                Console.Write("\n\t\t\t\t\tConfirma dados Cliente (S/N): ");
                if (char.TryParse(Console.ReadLine().ToUpper(), out char confirmarCliente))
                {
                    if (confirmarCliente == 'S')
                    {
                        ItemVenda(connection);
                    }
                }
            }
            //Console.Clear();


        }
        //else
        //{
        //    Console.Write("\n\t\t\t\t\tDigite um CPF!!");
        //    Console.ReadKey();
        //}



        public void ItemVenda(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t------------ Cadastro de Venda ------------");
            int cont = 0;
            float quantidade = 1;

            string codigo = (servicocadastro.NumeroElementos("Venda", connection) + 1).ToString().PadLeft(5, '0');
            string idproduto = "0";
            do
            {
                Console.Write("\n\t\t\t\t\tCódigo do Produto: ");
                bool encontraproduto;
                do
                {
                    encontraproduto = false;
                    Console.Write("\n\t\t\tInsira o nome do produto: ");
                    string nome = Console.ReadLine();
                    
                    connection.Open();

                    String localizaproduto = "SELECT cbarras, nome, ucompra, dcadastro FROM dbo.Produto where nome = '" + nome + "'";

                    using (SqlCommand command = new SqlCommand(localizaproduto, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("Codigo de barras: {0}\nNome: {1}\nUltima Compra: {2}\nData de Cadastro: {3}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"));
                                idproduto = reader.GetString(0);
                                encontraproduto = true;
                            }
                        }
                    }
                    connection.Close();

                } while (encontraproduto == false);
                Console.Write("\t\t\t\t\tDigite a Quantidade do Produto (1 / 999): ");
                    if (float.TryParse(Console.ReadLine(), out float CanParse))
                    {
                        quantidade = CanParse;
                        if (quantidade > 999 || quantidade <= 0)
                        {

                            do
                            {
                                Console.WriteLine("\t\t\t\t\tQuantidade do produto tem que ser maior que 0 e menor que 999 unidades\n");
                                Console.Write("\t\t\t\t\tDigite a Quantidade do Produto: ");
                                if (float.TryParse(Console.ReadLine(), out float quantMax))
                                {
                                    quantidade = quantMax;
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\tDigite uma quantidade válida!");
                                }
                            } while (quantidade > 999 || quantidade <= 0);
                        }

                        cont++;

                        vendaitem.Add(new ItemVenda(codigo, idproduto, quantidade.ToString()));

                        Console.WriteLine($"\t\t\t\t\t adicionados na venda!!");
                        if (cont <= 2)
                        {
                            Console.Write("\n\t\t\t\t\tDeseja adiciona outro Item (S/N): ");
                            string adicionarNovoItem = Console.ReadLine().ToUpper();

                            if (adicionarNovoItem == "S" || adicionarNovoItem == "SIM")
                            {
                            }
                            else
                            {
                                cont = 3;
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine("\t\t\t\t\tDigite uma quantidade válida!!");
                    }
                
            } while (cont <= 2 || cont != 3);

            Console.Write("\n\t\t\t\t\tConfirmar Compra (S/N): ");
            string confirmarCompras = Console.ReadLine().ToUpper();
            if (confirmarCompras == "S" || confirmarCompras == "SIM")
            {
                Venda venda = new Venda(codigo, clienteVenda);
                new Controle(venda , connection);
                Console.WriteLine("\n\t\t\t\t\tCompra cadastrada com sucesso!!");
                Console.ReadKey();
                vendaitem.ForEach(c => { c.Id = codigo; new Controle(c, connection); });
                vendaitem.Clear();
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("VendaVtotal", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.Decimal).Value = venda.Id;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                Console.WriteLine("\n\t\t\t\t\tVenda Cancelada!!");
                vendaitem.Clear();
                Console.ReadKey();
            }
        }
    }
}
