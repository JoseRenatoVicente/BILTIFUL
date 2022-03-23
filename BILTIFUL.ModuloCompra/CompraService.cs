using BILTIFUL.Core;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BILTIFUL.ModuloCompra
{
    public class CompraService
    {
        CadastroService cadastroService = new CadastroService();

        string cnpj;
        public void SubMenu(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| COMPRAS |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - CADASTRAR COMPRA                            |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR COMPRA                            |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                        |");
            Console.Write("\t\t\t\t\t|_________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");

            string opc = Console.ReadLine();
            switch (opc)
            {
                case "1":
                    CadastrarCompra(connection);

                    break;
                case "2":
                    cadastroService.LocalizarRegistro(connection);
                    break;
                case "0":
                    break;
                default:
                    break;
            }
        }
        public void InstanciaBanco()
        {
            Controle conexao = new Controle();
            SubMenu(conexao.connection);
        }

        public void CadastrarCompra(SqlConnection connection)
        {


            string opc = "a";

            Console.Clear();
            do
            {
                Console.WriteLine("\n\t\t\t\t\t------------CADASTRAR COMPRA------------");
                Console.Write("\t\t\t\t\tInforme o CNPJ do forncedor : ");


                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                bool bloqueado = false;

                connection.Open();
                String fornecedorbloqueado = "SELECT cnpj_fornecedor FROM dbo.Bloqueado";
                using (SqlCommand command = new SqlCommand(fornecedorbloqueado, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (cnpj == reader.GetString(0))
                                bloqueado = true;
                        }
                    }
                }
                connection.Close();

                if (bloqueado == true)
                {
                    Console.WriteLine("\t\t\t\t\tFornecedor bloqueado para compra");
                    return;
                }

                bool fornecedoencontrado = false;
                connection.Open();
                String localizafornecedor = "SELECT cnpj, rsocial, ucompra FROM dbo.Fornecedor";
                using (SqlCommand command = new SqlCommand(localizafornecedor, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (cnpj == reader.GetString(0))
                            {
                                Console.WriteLine("CNPJ: {0}\nRazão social: {1}\nUltima Compra: {2}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"));
                                fornecedoencontrado = true;
                            }
                        }
                    }
                }
                connection.Close();
               
                if (fornecedoencontrado == false)
                {
                    Console.WriteLine("\t\t\t\t\tFornecedor nao encontrado.");
                }
                else
                {
                    Console.WriteLine("\t\t\t\t\t------------------------------");
                    do
                    {
                        Console.Write("\t\t\t\t\tConfirma dados do Fornecedor (S/N): ");
                        opc = Console.ReadLine().ToUpper();

                        if ((opc != "S" & opc != "N"))
                        {
                            Console.Write("\t\t\t\t\tEscolha uma opcao valida : ");

                        }
                    } while (opc != "S" & opc != "N");
                    if (opc == "N")
                    {
                        Console.WriteLine("");

                    }
                }

            } while (opc != "S");
            ItemCompra(connection);


        }
        public void ItemCompra(SqlConnection connection)
        {
            int cont = 0;
            string saida = "a";
            string opcp = "a";
            string[] stringValor = new string[4];
            double valor, quantidadeTeste;
            double[] valorQuantidade = new double[4];
            double[] quantidade = new double[4];
            double[] totalItem = new double[4];
            string[] idMPrima = new string[4];
            string[] quantidadeString = new string[4];
            double valorTotal = 0;
            string[] totalItemString = new string[4];
            bool valorTotalMaior = false;
            do
            {

                do
                {
                    opcp = "a";
                    string buscarMPrima;
                    bool encontramateriaprima = false;
                    Console.Clear();
                    do
                    {
                        encontramateriaprima = false;
                        Console.Write("\t\t\t\t\tInforme o nome da Materia-Prima : ");
                        buscarMPrima = Console.ReadLine();
                        connection.Open();

                        String localizamprima = "SELECT id, nome, ucompra, dcadastro FROM dbo.MPrima where nome = '" + buscarMPrima + "'";

                        using (SqlCommand command = new SqlCommand(localizamprima, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("ID: {0}\nNome: {1}\nUltima Compra: {2}\nData de Cadastro: {3}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"));
                                    encontramateriaprima = true;
                                }
                            }
                        }
                        connection.Close();
                    } while (encontramateriaprima != true);

                    MPrima mPrimaCompra;
                    bool buscar = false;
                    do
                    {
                        Console.Write("\t\t\t\t\tInforme o ID referente a Materia-Prima que deseja adicionar : ");
                        idMPrima[cont] = Console.ReadLine().ToUpper();

                        connection.Open();

                        String localizamprima = "SELECT id, nome, ucompra, dcadastro FROM dbo.MPrima where id = '" + idMPrima[cont] + "'";

                        using (SqlCommand command = new SqlCommand(localizamprima, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("ID: {0}\nNome: {1}", reader.GetString(0), reader.GetString(1));
                                    buscar = true;
                                }
                            }
                        }
                        connection.Close();
                        if (buscar == false)
                            Console.WriteLine("\t\t\t\t\tMateria-Prima nao encontrada.");
                        else
                            buscar = true;
                    } while (buscar != true);
                    Console.WriteLine("\t\t\t\t\t-------------------------------------------");
                    do
                    {
                        Console.Write("\t\t\t\t\tConfirma dados da Matéria-Prima (S/N): ");
                        opcp = Console.ReadLine().ToUpper();
                        if ((opcp != "S" & opcp != "N"))
                        {
                            Console.WriteLine("\t\t\t\t\tEscolha uma opcao válida");

                        }
                    } while (opcp != "S" & opcp != "N");
                    if (opcp == "N")
                    {
                        Console.WriteLine("");

                    }
                    else
                    {

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t\t\t\t-----------------------------------------");
                            Console.WriteLine("\t\t\t\t\tInforme o valor unitario da Materia-Prima");
                            Console.Write("\t\t\t\t\tValor($$$,$$) (valor precisa ser menor que 1000,00): ");

                            if (double.TryParse(Console.ReadLine(), out double confirmar2))
                            {
                                stringValor[cont] = confirmar2.ToString();
                                valorQuantidade[cont] = double.Parse(stringValor[cont]);
                                stringValor[cont] = stringValor[cont].Trim().Replace(".", "").Replace(",", "");
                                if (!double.TryParse(stringValor[cont], out valor) || (valor > 99999) || (valor <= 0))
                                    Console.WriteLine("\t\t\t\t\tValor invalido!");

                            }
                            else
                            {
                                Console.WriteLine("\t\t\t\t\tInforme um valor correto");
                                Console.WriteLine("\t\t\t\t\tPressione uma tecla para voltar");
                                Console.ReadKey();
                            }
                        } while (!double.TryParse(stringValor[cont], out valor) || (valor > 99999) || (valor <= 0));
                        do
                        {
                            do
                            {
                                do
                                {
                                    Console.Write("\t\t\t\t\tInforme a quantidade: ");
                                    if (double.TryParse(Console.ReadLine(), out double confirmar1))
                                    {
                                        quantidadeString[cont] = confirmar1.ToString();

                                        quantidade[cont] = double.Parse(quantidadeString[cont]);
                                        quantidadeString[cont] = quantidadeString[cont].Trim().Replace(".", "").Replace(",", "");
                                        totalItemString[cont] = (quantidade[cont] * valorQuantidade[cont]).ToString("F2");
                                        totalItem[cont] = (quantidade[cont] * valorQuantidade[cont]);
                                        if (!double.TryParse(quantidadeString[cont], out quantidadeTeste) || (quantidadeTeste > 99999) || (quantidadeTeste <= 0))
                                            Console.WriteLine("\t\t\t\t\tValor invalido!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\t\t\t\t\tInforme uma quantidade correta");
                                        Console.WriteLine("\t\t\t\t\tPressione uma tecla para voltar");
                                        Console.ReadKey();
                                    }
                                } while (!double.TryParse(quantidadeString[cont], out quantidadeTeste) || (quantidadeTeste > 99999) || (quantidadeTeste <= 0));
                            } while (TotalItem(valorQuantidade[cont], quantidade[cont]) != true);
                            valorTotal = valorTotal + totalItem[cont];
                            if (valorTotal > 99999.99)
                            {
                                Console.WriteLine("\t\t\t\t\tValor total da compra maior que permitido favor inserir outra quantidade");
                                valorTotalMaior = true;
                            }
                        } while (valorTotalMaior != false);
                    }


                } while (opcp != "S");
                Console.WriteLine("\n\t\t\t\t\tMateria-Prima:\t{0}\n\t\t\t\t\tValor Unitario:\t{1}\n\t\t\t\t\tQuantidade:\t{2}\n\t\t\t\t\tTotal Item:\t{3}", idMPrima[cont], valorQuantidade[cont], quantidade[cont], totalItemString[cont]);
                

                //valorTotal = valorTotal + totalItem[cont];

                cont++;
                if (cont == 3)
                {
                    Console.WriteLine("\t\t\t\t\tLimite de Materia-Prima atingido por compra");
                    Console.ReadKey();
                }
                else
                {
                    Console.Write("\n\t\t\t\t\tDeseja adicionar mais materia-prima (S/N): ");
                    saida = Console.ReadLine().ToUpper();
                }

            } while ((saida != "N") & (cont != 3));
            for (int i = 0; i < cont; i++)
            {
                Console.WriteLine("\n\n\t\t\t\t\tMateria-Prima:\t{0}\n\t\t\t\t\tValor Unitario:\t{1}\n\t\t\t\t\tQuantidade:\t{2}\n\t\t\t\t\tTotal Item:\t{3}", idMPrima[i], valorQuantidade[i], quantidade[i], totalItemString[i]);
            }
            Console.Write("\n\t\t\t\t\tConfirmar a compra (S/N): ");
            string confirmar = Console.ReadLine();
            if (confirmar == "S")
            {
                string cod = "" + (cadastroService.NumeroElementos("Compra", connection)+1);
                string valorTotalString = (valorTotal.ToString("F2"));
                valorTotalString = valorTotalString.Trim().Replace(".", "").Replace(",", "");



                Compra compra = new Compra(cod, cnpj);
                new Controle(compra, connection);
                for (int i = 0; i < cont; i++)
                {

                    quantidadeString[i] = quantidadeString[i].Trim().Replace(".", "").Replace(",", "");
                    totalItemString[i] = totalItemString[i].Trim().Replace(".", "").Replace(",", "");


                    ItemCompra itemCompra = new ItemCompra(cod, idMPrima[i], quantidadeString[i], stringValor[i]);
                    new Controle(itemCompra,connection);
                }
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("CompraVtotal", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@id", SqlDbType.Decimal).Value = compra.Id;
                sql_cmnd.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                SubMenu(connection);
            }
        }


        public bool TotalItem(double valor, double quantidade)
        {

            double totalCompra = valor * quantidade;
            if (totalCompra >= 10000)
            {
                Console.WriteLine("\t\t\t\t\tValor ultrapasssou o valor total permetido por compra");
                Console.WriteLine("\t\t\t\t\tFavor informar outra quantidade e outro valor de Materia-Prima");
                Console.WriteLine("\t\t\t\t\tQuantidade disponivel para compra: {0}", 9999 / valor);
                return false;
            }
            else
            { return true; }
        }
        public Fornecedor BuscarCnpj(string fcnpj, List<Fornecedor> fornecedor)
        {
            Fornecedor fornecedorcompra = fornecedor.Find(delegate (Fornecedor f) { return f.CNPJ == fcnpj; });

            return fornecedorcompra;
        }

        public bool BuscarBloqueado(string fcnpj, List<string> bloqueados)
        {
            string buscar = bloqueados.Find(x => x == fcnpj.ToString());
            if (buscar == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public MPrima BuscaMPrima(string idMPrima, List<MPrima> mPrima)
        {
            MPrima mPrimaCompra = mPrima.Find(delegate (MPrima mP) { return mP.Id == idMPrima; });

            return mPrimaCompra;
        }

        public bool ImprimirMPrima(List<MPrima> mPrima, string buscarMPrima)
        {
            bool buscar = false;
            List<MPrima> listaMprima = mPrima.FindAll(delegate (MPrima m) { return m.Nome.ToLower() == buscarMPrima.ToLower(); });
            listaMprima.ForEach(delegate (MPrima m)
            {
                Console.WriteLine(m.DadosMateriaPrima());
                Console.WriteLine("\t\t\t\t\t-----------------------------------------");
                buscar = true;
            });
            if (buscar == false)
            {
                Console.WriteLine("\t\t\t\t\tMateria-Prima nao encontrada");

            }
            return buscar;
        }



    }
}
