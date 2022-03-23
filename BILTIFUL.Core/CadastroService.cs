using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;

namespace BILTIFUL.Core
{
    public class CadastroService
    {
        public CadastroService()
        {
        }

        public void SubMenu(SqlConnection connection)
        {

            string opc;
            do
            {
                opc = Menu();
                switch (opc)
                {
                    case "1":
                        CadastroCliente(connection);
                        break;
                    case "2":
                        CadastroProduto(connection); ;
                        break;
                    case "3":
                        CadastroFornecedor(connection);
                        break;
                    case "4":
                        CadastroMateriaPrima(connection); ;
                        break;
                    case "5":
                        CadastroInadimplente(connection); ;
                        break;
                    case "6":
                        CadastroBloqueado(connection); ;
                        break;
                    case "7":
                        RemoverInadimplencia(connection); ;
                        break;
                    case "8":
                        CadastroCliente(connection); ;
                        break;
                    case "9":
                        CadastroCliente(connection); ;
                        break;
                    case "10":
                        LocalizarRegistro(connection);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção invalida!");
                        break;
                }

                Console.Clear();
            } while (opc != "0");
        }
        private string Menu()
        {
            string opc;
            Console.WriteLine("\n\t\t\t\t\t________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| MENU |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - CADASTRAR CLIENTE                        |");
            Console.WriteLine("\t\t\t\t\t|2| - CADASTRAR PRODUTO                        |");
            Console.WriteLine("\t\t\t\t\t|3| - CADASTRAR FORNECEDOR                     |");
            Console.WriteLine("\t\t\t\t\t|4| - CADASTRAR MATERIA PRIMA                  |");
            Console.WriteLine("\t\t\t\t\t|5| - ADICIONAR CLIENTE COMO INADIMPLENTE      |");
            Console.WriteLine("\t\t\t\t\t|6| - ADICIONAR FORNECEDOR A LISTA DE BLOQUEADO|");
            Console.WriteLine("\t\t\t\t\t|7| - REMOVER CLIENTE DA LISTA DE INADIMPLENTE |");
            Console.WriteLine("\t\t\t\t\t|8| - REMOVER FORNECEDOR DA LISTA DE BLOQUEADO |");
            Console.WriteLine("\t\t\t\t\t|9| - MOSTRAR REGISTROS                        |");
            Console.WriteLine("\t\t\t\t\t|10| - LOCALIZAR REGISTROS                     |");
            Console.WriteLine("\t\t\t\t\t|0| - VOLTAR PARA O MENU PRINCIPAL             |");
            Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");
            opc = Console.ReadLine();
            return opc;
        }
        public void InstanciaBanco()
        {
            Controle conexao = new Controle();
            SubMenu(conexao.connection);
        }
        public Cliente CadastroCliente(SqlConnection connection)
        {
            string cpf;
            string datanascimento;
            string csexo;
            DateTime dnascimento;
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO CLIENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tCPF: ");
                cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");//tira o ponto e o traço caso digitado
                if (!ValidaCpf(cpf))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCpf(cpf));//enquanto cpf nao for valido digitar denovo
            do
            {
                Console.Write("\t\t\t\t\tNome: ");
                nome = Console.ReadLine().Trim();
                if (nome == "")
                    Console.WriteLine("\t\t\t\t\tNome nao pode ser vazio");
            } while (nome == "");
            do
            {
                Console.Write("\t\t\t\t\tData de nascimento(dd/mm/aaaa): ");
                datanascimento = Console.ReadLine();
                if (!DateTime.TryParse(datanascimento, out dnascimento))
                    Console.WriteLine("\t\t\t\t\tData invalida!");
            } while (!DateTime.TryParse(datanascimento, out dnascimento));
            if ((DateTime.Now - dnascimento).Days / 365 < 18)
            {
                Console.WriteLine("\t\t\t\t\tDeve ter pelomenos 18 anos para ser cliente!");
                return null;
            }
            do
            {
                Console.Write("\t\t\t\t\tSexo(M-Masculino e F-Feminino): ");
                csexo = Console.ReadLine().ToUpper();
            } while ((csexo != "M") && (csexo != "F"));
            Sexo sexo = (Sexo)char.Parse(csexo);

            Cliente cliente = new Cliente(cpf, nome, dnascimento, sexo);

            Controle controle_cliente = new Controle(cliente,connection);

            return cliente;
        }
        public static bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        public Fornecedor CadastroFornecedor(SqlConnection connection)
        {
            string dataabertura;
            DateTime dabertura;
            string rsocial;
            string cnpj;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO FORNECEDOR===========");
            do
            {
                Console.Write("\t\t\t\t\tCNPJ: ");
                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");//tira o ponto e o traço caso digitado
                if (!ValidaCnpj(cnpj))//valida c
                    Console.WriteLine("\t\t\t\t\tCnpj invalido!\nDigite novamente");

            } while (!ValidaCnpj(cnpj));//enquanto cpf nao for valido digitar denovo
            do
            {
                Console.Write("\t\t\t\t\tRazão Social: ");
                rsocial = Console.ReadLine().Trim();
            } while (rsocial == "");
            do
            {
                Console.Write("\t\t\t\t\tData de abertura(dd/mm/aaaa): ");
                dataabertura = Console.ReadLine();
                if (!DateTime.TryParse(dataabertura, out dabertura))
                    Console.WriteLine("\t\t\t\t\tData invalida!");
            } while (!DateTime.TryParse(dataabertura, out dabertura));
            if ((DateTime.Now - dabertura).Days < 180)
            {
                Console.WriteLine("\t\t\t\t\tDeve ter se passado pelo menos 6 meses desde a abertura!");
                return null;
            }


            Fornecedor fornecedor = new Fornecedor(cnpj, rsocial, dabertura);

            new Controle(fornecedor,connection);

            return fornecedor;
        }
        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        public Produto CadastroProduto(SqlConnection connection)
        {
            string svalor;
            int valor;
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO PRODUTO===========");
            do
            {
                Console.Write("\t\t\t\t\tNome: ");
                nome = Console.ReadLine().Trim();
            } while (nome == "");
            do
            {
                Console.Write("\t\t\t\t\tValor($$$,$$)(valor precisa ser menor que 1000,00): ");
                svalor = Console.ReadLine().Trim().Replace(".", "").Replace(",", "");
                if (!int.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0))
                    Console.WriteLine("\t\t\t\t\tValor invalido!");
            } while (!int.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0));



            string cod = "" + (NumeroElementos("Produto", connection) + 1);

            Produto produto = new Produto(cod, nome, svalor);

            new Controle(produto, connection);

            return produto;
        }
        public int NumeroElementos(string tabela, SqlConnection connection)
        {
            int NumeroProdutos = 0;

            connection.Open();

            String sql = "SELECT count(*) FROM dbo." + tabela;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    NumeroProdutos = reader.GetInt32(0);
                }
            }
            connection.Close();

            return NumeroProdutos;
        }
        public MPrima CadastroMateriaPrima(SqlConnection connection)
        {
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO MATERIA PRIMA===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o nome da Materia Prima: ");
                nome = Console.ReadLine().Trim();
            } while (nome == "");

            string cod = "" + (NumeroElementos("MPrima",connection) + 1);

            MPrima mPrima = new MPrima(cod, nome);

            new Controle(mPrima, connection);

            return mPrima;

        }
        public string CadastroInadimplente(SqlConnection connection)
        {
            string inadimplente;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cpf do inadimplente: ");
                inadimplente = Console.ReadLine().Trim().Replace(".", "").Replace("-", ""); ;
                if (!ValidaCpf(inadimplente))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\nDigite novamente");
            } while (!ValidaCpf(inadimplente));

            new Controle(inadimplente, true, connection);
            return inadimplente;
        }
        public string CadastroBloqueado(SqlConnection connection)
        {
            string bloqueado;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE BLOQUEADO===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o CNPJ do fornecedor: ");
                bloqueado = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(bloqueado))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCnpj invalido!\nDigite novamente");
            } while (!ValidaCnpj(bloqueado));

            new Controle(bloqueado, true, connection);
            return bloqueado;
        }
        public void RemoverInadimplencia(SqlConnection connection)
        {
            string inadimplente;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========REMOVER DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cpf inadimplente: ");
                inadimplente = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
                if (!ValidaCpf(inadimplente))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCpf(inadimplente));

            new Controle(inadimplente, false, connection);
            Console.WriteLine("\t\t\t\t\tCpf Liberado");

        }
        public void RemoverBloqueado(SqlConnection connection)
        {
            string bloqueado;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========REMOVER DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cnpj bloqueado: ");
                bloqueado = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(bloqueado))//valida cpf
                    Console.WriteLine("\t\t\t\t\tcnpj invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCnpj(bloqueado));

            new Controle(bloqueado, false, connection);
            Console.WriteLine("\t\t\t\t\tCNPJ Liberado");

        }

       /* public void MostrarRegistro()
        {
            string opc;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|+++++++++++++| MENU DE REGISTROS |++++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - REGISTROS DE CLIENTES                    |");
                Console.WriteLine("\t\t\t\t\t|2| - REGISTROS DE FORNECEDORES                |");
                Console.WriteLine("\t\t\t\t\t|3| - REGISTROS DE MATERIAS PRIMAS             |");
                Console.WriteLine("\t\t\t\t\t|4| - REGISTROS DE PRODUTOS                    |");
                Console.WriteLine("\t\t\t\t\t|5| - REGISTROS DE VENDAS                      |");
                Console.WriteLine("\t\t\t\t\t|6| - REGISTROS DE COMPRAS                     |");
                Console.WriteLine("\t\t\t\t\t|7| - REGISTROS DE PRODUÇÃO                    |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                opc = Console.ReadLine();
                switch (opc)
                {
                    case "1":
                        if (cadastros.clientes.Count() != 0)
                            new Registros(cadastros.clientes);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum cliente registrado");
                        break;
                    case "2":
                        if (cadastros.fornecedores.Count() != 0)
                            new Registros(cadastros.fornecedores);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum fornecedor registrado");
                        break;
                    case "3":
                        if (cadastros.materiasprimas.Count() != 0)
                            new Registros(cadastros.materiasprimas);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhuma materia prima registrada");
                        break;
                    case "4":
                        if (cadastros.produtos.Count() != 0)
                            new Registros(cadastros.produtos);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "5":
                        if (cadastros.vendas.Count() != 0)
                            new Registros(cadastros.vendas, cadastros.itensvenda);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhuma venda registrada");
                        break;
                    case "6":
                        if (cadastros.compras.Count() != 0)
                            new Registros(cadastros.compras, cadastros.itenscompra);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "7":
                        if (cadastros.producao.Count() != 0)
                            new Registros(cadastros.producao, cadastros.itensproducao);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "0":

                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                Console.ReadKey();
            } while (opc != "0");
        }*/
        public void LocalizarRegistro(SqlConnection connection)
        {
            string opc;
            do
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|++++++++++++| MENU DE LOCALIZAÇÃO |+++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - LOCALIZAR CLIENTES                       |");
                Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR FORNECEDORES                   |");
                Console.WriteLine("\t\t\t\t\t|3| - LOCALIZAR MATERIAS PRIMAS                |");
                Console.WriteLine("\t\t\t\t\t|4| - LOCALIZAR PRODUTOS                       |");
                Console.WriteLine("\t\t\t\t\t|5| - LOCALIZAR VENDAS                         |");
                Console.WriteLine("\t\t\t\t\t|6| - LOCALIZAR COMPRAS                        |");
                Console.WriteLine("\t\t\t\t\t|7| - LOCALIZAR PRODUÇÕES                      |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                opc = Console.ReadLine();
                bool encontrado = false;
                Console.Clear();
                switch (opc)
                {

                    case "1":
                        string cpf;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cpf que deseja localizar: ");
                            cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", "");
                            if (!ValidaCpf(cpf))
                            {
                                Console.WriteLine("\t\t\t\t\tCPF INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCpf(cpf) != true);
                        Console.Clear();

                        connection.Open();

                        String localizacliente = "SELECT cpf, nome, dnascimento, sexo, ucompra, dcadastro, situacao FROM dbo.Cliente where cpf = " + cpf;

                        using (SqlCommand command = new SqlCommand(localizacliente, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("CPF: {0}\nNome: {1}\nData de Nascimento: {2}\nSexo: {3}\nUltima Compra: {4}\nData de Cadastro: {5}\nSituação:{6}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"),(Sexo)char.Parse(reader.GetString(3)), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetDateTime(5).ToString("dd/MM/yyyy"), (Situacao)char.Parse(reader.GetString(6)));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;

                    case "2":
                        string cnpj;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cnpj que deseja localizar: ");
                            cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                            if (!ValidaCnpj(cnpj))
                            {
                                Console.WriteLine("\t\t\t\t\tCNPJ INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCnpj(cnpj) != true);
                        Console.Clear();

                        connection.Open();

                        String localizafornecedor = "SELECT cnpj, rsocial, dabertura, ucompra, dcadastro, situacao FROM dbo.Fornecedor where cnpj = '" + cnpj + "'";

                        using (SqlCommand command = new SqlCommand(localizafornecedor, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("CNPJ: {0}\nRazão social: {1}\nData de Abertura: {2}\nUltima Compra: {3}\nData de Cadastro: {4}\nSituação:{5}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), (Situacao)char.Parse(reader.GetString(5)));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;

                    case "3":
                        Console.Write("\t\t\t\t\tDigite o nome que deseja localizar: ");
                        string nomeMateriaPrima = Console.ReadLine().Trim();
                        Console.Clear();
                        connection.Open();

                        String localizamprima = "SELECT id, nome, ucompra, dcadastro, situacao FROM dbo.MPrima where nome = '" + nomeMateriaPrima + "'";

                        using (SqlCommand command = new SqlCommand(localizamprima, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("ID: {0}\nNome: {1}\nUltima Compra: {2}\nData de Cadastro: {3}\nSituação:{4}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"), (Situacao)char.Parse(reader.GetString(4)));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;

                    case "4":
                        Console.Write("\t\t\t\t\tDigite o nome do produto que deseja localizar: ");
                        string nomeProduto = Console.ReadLine().Trim();
                        connection.Open();

                        String localizaproduto = "SELECT cbarras, nome, vvenda, ucompra, dcadastro, situacao FROM dbo.Produto where nome = '" + nomeProduto + "'";

                        using (SqlCommand command = new SqlCommand(localizaproduto, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("Codigo de barras: {0}\nNome: {1}\nValor de Venda: {2}\nUltima compra: {3}\nData de Cadastro: {4}\nSituação:{5}", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), (Situacao)char.Parse(reader.GetString(5)));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;

                    case "5":
                        Console.Write("\t\t\t\t\tDigite a data de venda que deseja localizar(dd/mm/aaaa): ");
                        DateTime datavenda = DateTime.Parse(Console.ReadLine());
                        connection.Open();

                        String localizavenda = "SELECT id, dvenda, vtotal, cpf_cliente, Produto.nome, qtd, vunitario, titem FROM dbo.Venda join dbo.Item_Venda on Item_Venda.id_venda = Venda.id join Produto on Item_Venda.cbarras_produto = Produto.cbarras where dvenda = '" + datavenda.ToString("yyyy/MM/dd") + "'";

                        using (SqlCommand command = new SqlCommand(localizavenda, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("Numero Venda: {0}\nData Venda: {1}\nValor de Venda: {2}\nCPF: cliente: {3}\nProduto: {4}\nQuantidade: {5}\nValor Unitario: {6}\nTotal do item: {7}\n--------------------------------------------------",
                                        reader.GetString(0), reader.GetDateTime(1).ToString("dd/MM/yyyy"), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetDecimal(6), reader.GetDecimal(7));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;
                    case "6":
                        Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                        DateTime dcompra = DateTime.Parse(Console.ReadLine());
                        String localizacompra = "  SELECT Compra.id, dcompra, vtotal, cnpj_fornecedor, MPrima.nome, qtd, vunitario, titem FROM dbo.Compra join dbo.Item_Compra on Item_Compra.id_compra = Compra.id join MPrima on Item_Compra.id_mprima = MPrima.id where dcompra = '" + dcompra.ToString("yyyy/MM/dd") + "'";
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(localizacompra, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("Numero Compra: {0}\nData Compra: {1}\nValor de compra: {2}\nCNPJ Fornecedor: {3}\nMateria Prima: {4}\nQuantidade: {5}\nValor Unitario: {6}\nTotal do item: {7}\n--------------------------------------------------",
                                        reader.GetString(0), reader.GetDateTime(1).ToString("dd/MM/yyyy"), reader.GetDecimal(2), reader.GetString(3), reader.GetString(4), reader.GetDecimal(5), reader.GetDecimal(6), reader.GetDecimal(7));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;
                    case "7":
                        Console.Write("\t\t\t\t\tDigite o data de produção que deseja localizar(dd/mm/aaaa): ");
                        DateTime dproducao = DateTime.Parse(Console.ReadLine());
                        String localizaproducao = "  SELECT Producao.id, Producao.dproducao, qtd, Produto.nome, MPrima.nome, qtdmp FROM dbo.Producao join dbo.Item_Producao on Item_Producao.id_producao = Producao.id join Produto on Produto.cbarras = Producao.cbarras_produto join MPrima on Mprima.id = Item_Producao.id_mprima where Producao.dproducao = '" + dproducao.ToString("yyyy/MM/dd") + "'";
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(localizaproducao, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine("Numero Producao: {0}\nData Producao: {1}\nQuantidade producao: {2}\nProduto: {3}\nMateria Prima: {4}\nQuantidade materia prima: {5}\n--------------------------------------------------",
                                        reader.GetString(0), reader.GetDateTime(1).ToString("dd/MM/yyyy"), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetDecimal(5));
                                    encontrado = true;
                                }
                            }
                        }
                        connection.Close();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                if (encontrado == false && opc != "0")
                    Console.WriteLine("\t\t\t\t\tRegistro não encontrado");
                Console.ReadKey();
            } while (opc != "0");
        }

        /* public void EditarRegistros()
         {
             string opc;
             do
             {
                 Console.Clear();
                 Console.WriteLine("\t\t\t\t\t________________________________________________");
                 Console.WriteLine("\t\t\t\t\t|++++++++++++++| MENU DE EDIÇÃO |++++++++++++++|");
                 Console.WriteLine("\t\t\t\t\t|1| - EDITAR CLIENTES                          |");
                 Console.WriteLine("\t\t\t\t\t|2| - EDITAR FORNECEDORES                      |");
                 Console.WriteLine("\t\t\t\t\t|3| - EDITAR MATERIAS PRIMAS                   |");
                 Console.WriteLine("\t\t\t\t\t|4| - EDITAR PRODUTOS                          |");
                 Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                 Console.Write("\t\t\t\t\t|_________________________________________________|\n" +
                               "\t\t\t\t\t|Opção: ");
                 Console.WriteLine("\t________________________________________________");
                 Console.WriteLine("\t|++++++++++++++| MENU DE EDIÇÃO |++++++++++++++|");
                 Console.WriteLine("\t|1| - EDITAR CLIENTES                          |");
                 Console.WriteLine("\t|2| - EDITAR FORNECEDORES                      |");
                 Console.WriteLine("\t|3| - EDITAR MATERIAS PRIMAS                   |");
                 Console.WriteLine("\t|4| - EDITAR PRODUTOS                          |");
                 Console.WriteLine("\t|0| - VOLTAR                                   |");
                 Console.Write("\t|______________________________________________|\n" +
                               "\t|Opção: ");
                 opc = Console.ReadLine();
                 bool encontrado = false;
                 Console.Clear();
                 switch (opc)
                 {

                     case "1":
                         string cpf;
                         do
                         {
                             Console.Write("\t\t\t\t\tDigite o CPF do cliente que deseja alterar: ");
                             cpf = Console.ReadLine();
                             Console.Write("Digite o CPF do cliente que deseja alterar: ");
                             cpf = Console.ReadLine().Replace(".", "").Replace("-", "");
                             if (!ValidaCpf(cpf))
                             {
                                 Console.WriteLine("\t\t\t\t\tCPF INVÁLIDO, TENTE NOVAMENTE!");
                             }
                         } while (ValidaCpf(cpf) != true);
                         Console.Clear();

                         Cliente localizarcliente = cadastros.clientes.Find(p => p.CPF == long.Parse(cpf));
                         if (localizarcliente != null)
                         {
                             string opca;
                             encontrado = true;
                             Console.WriteLine(localizarcliente.DadosCliente());
                             do
                             {
                                 Console.WriteLine("\n\n\t\t\t\t\tSOMENTE O NOME É POSSÍVEL ALTERAR!");
                                 Console.WriteLine("\t\t\t\t\tDeseja alterar o nome? [S - Sim] [N - Não] ");
                                 opcao = Console.ReadLine().ToUpper();
                                 if (opcao == "S")
                                 Console.WriteLine("\n\nSOMENTE O NOME É POSSÍVEL ALTERAR!");
                                 Console.WriteLine("Deseja alterar o nome? [S - Sim] [N - Não] ");
                                 opca = Console.ReadLine().ToUpper();
                                 if (opca == "S")
                                 {
                                     Console.Write("\n\t\t\t\t\tInforme o novo Nome: ");
                                     string novoNome = Console.ReadLine();
                                     localizarcliente.Nome = novoNome;
                                     Console.WriteLine(localizarcliente.DadosCliente());
                                     Console.WriteLine("\t\t\t\t\tNOVO NOME ALTERADO COM SUCESSO!");
                                     Console.ReadLine();//////////
                                     Console.WriteLine("NOVO NOME ALTERADO COM SUCESSO!");
                                     Console.ReadLine();
                                     new Controle(localizarcliente);

                                     Console.ReadLine();
                                     break;
                                 }
                                 else if (opca == "N")
                                 {
                                     Console.Clear();
                                     Console.WriteLine("\t\t\t\t\tALTERAÇÃO CANCELADA");
                                     break;
                                 }
                             } while (opca != "S" || opca != "N");
                         }
                         break;
                     case "2":
                         string cnpj, opc2 = null;
                         do
                         {
                             Console.Write("\t\t\t\t\tDigite o cnpj que deseja localizar: ");
                             cnpj = Console.ReadLine();
                             if (!ValidaCnpj(cnpj))
                             Console.Write("Digite o cnpj que deseja localizar: ");
                             cnpj = Console.ReadLine().Replace(".", "").Replace("/", "").Replace("-", "");
                             if (ValidaCnpj(cnpj))
                             {
                                 Console.WriteLine("\t\t\t\t\tCNPJ INVÁLIDO, TENTE NOVAMENTE!");
                             }
                         } while (ValidaCnpj(cnpj) != true);
                         Console.Clear();

                         Fornecedor localizarfornecedor = cadastros.fornecedores.Find(p => p.CNPJ == long.Parse(cnpj));
                         if (localizarfornecedor != null)
                         {
                             encontrado = true;
                             Console.WriteLine(localizarfornecedor.DadosFornecedor());
                         }
                                 Fornecedor localizarfornecedor = cadastros.fornecedores.Find(p => p.CNPJ == long.Parse(cnpj));
                                 if (localizarfornecedor != null)
                                 {
                                     encontrado = true;
                                     Console.WriteLine(localizarfornecedor.DadosFornecedor());
                                 }
                                 else
                                 {
                                     Console.WriteLine("CNPJ não encontrado");
                                     Console.ReadLine();
                                 }
                                 Console.WriteLine("CNPJ INVÁLIDO, TENTE NOVAMENTE!");
                                 Console.WriteLine("Deseja Procurar novamente? [S - sim] [N - Não]");
                                 opc2 = Console.ReadLine().ToUpper();
                                 if (opc2 == "N")
                                 {
                                     break;
                                 }
                                 Console.Clear();
                             }
                         } while (opc2 != "S");
                         Console.Clear();


                         break;
                     case "3":
                         Console.Write("\t\t\t\t\tDigite o nome que deseja localizar: ");
                         string nomeMateriaPrima = Console.ReadLine().Trim().ToLower();
                         List<MPrima> localizarmprima = cadastros.materiasprimas.FindAll(p => p.Nome.ToLower() == nomeMateriaPrima);
                         if (localizarmprima != null)
                         {
                             encontrado = true;
                             localizarmprima.ForEach(p => Console.WriteLine(p.DadosMateriaPrima()));
                         }
                         break;
                     case "4":
                         Console.Write("\t\t\t\t\tDigite o nome do produto que deseja localizar: ");
                         string nomeProduto = Console.ReadLine().Trim().ToLower();
                         List<Produto> localizaProduto = cadastros.produtos.FindAll(p => p.Nome.ToLower() == nomeProduto);
                         if (localizaProduto != null)
                         {
                             encontrado = true;
                             localizaProduto.ForEach(p => Console.WriteLine(p.DadosProduto()));
                         }
                         break;
                     case "5":
                         Console.Write("\t\t\t\t\tDigite a data de venda que deseja localizar(dd/mm/aaaa): ");
                         DateTime dvenda = DateTime.Parse(Console.ReadLine());
                         List<Venda> localizavenda = cadastros.vendas.FindAll(p => p.DataVenda == dvenda);
                         if (localizavenda != null)
                         {
                             encontrado = true;
                             foreach (Venda p in localizavenda)
                             {
                                 Console.WriteLine(p.DadosVenda());
                                 Console.WriteLine("\t\t\t\t\tItens: ");
                                 foreach (ItemVenda i in cadastros.itensvenda)
                                 {
                                     if (i.Id == p.Id)
                                         Console.WriteLine(i.DadosItemVenda());
                                 }
                             }
                         }
                         break;
                     case "6":
                         Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                         DateTime dcompra = DateTime.Parse(Console.ReadLine());
                         List<Compra> localizacompra = cadastros.compras.FindAll(p => p.DataCompra == dcompra);
                         if (localizacompra != null)
                         {
                             encontrado = true;
                             foreach (Compra p in localizacompra)
                             {
                                 Console.WriteLine(p.DadosCompra());
                                 Console.WriteLine("\t\t\t\t\tItens: ");
                                 foreach (ItemCompra i in cadastros.itenscompra)
                                 {
                                     if (i.Id == p.Id)
                                         Console.WriteLine(i.DadosItemCompra());
                                 }
                             }
                         }
                         break;
                     case "7":
                         Console.Write("\t\t\t\t\tDigite o data de produção que deseja localizar(dd/mm/aaaa): ");
                         DateTime dproducao = DateTime.Parse(Console.ReadLine());
                         List<Producao> localizaproducao = cadastros.producao.FindAll(p => p.DataProducao == dproducao);
                         if (localizaproducao != null)
                         {
                             encontrado = true;
                             foreach (Producao p in localizaproducao)
                             {
                                 Console.WriteLine(p.DadosProducao());
                                 Console.WriteLine("\t\t\t\t\tItens: ");
                                 foreach (ItemProducao i in cadastros.itensproducao)
                                 {
                                     if (i.Id == p.Id)
                                         Console.WriteLine(i.DadosItemProducao());
                                 }
                             }
                         }
                         break;
                     case "0":
                         break;
                     default:
                         Console.WriteLine("\t\t\t\t\tOpção invalida");
                         break;
                 }
                 if (encontrado == false && opc != "0")
                     Console.WriteLine("\t\t\t\t\tRegistro não encontrado");
                 Console.ReadKey();
             } while (opc != "0");
         }*/
    }
}