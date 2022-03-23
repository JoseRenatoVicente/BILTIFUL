using BILTIFUL.Application.Repository;
using BILTIFUL.Core;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;

namespace BILTIFUL.Application.Service
{
    public class CadastroService
    {
        ProdutoRepository produtoRepository = new ProdutoRepository();
        VendaRepository vendaRepository = new VendaRepository();
        ItemVendaRepository itemVendaRepository = new ItemVendaRepository();
        ProducaoRepository producaoRepository = new ProducaoRepository();
        MateriaPrimaRepository materiaPrimaRepository = new MateriaPrimaRepository();
        FornecedorRepository fornecedorRepository = new FornecedorRepository();
        ClienteRepository clienteRepository = new ClienteRepository();
        CompraRepository compraRepository = new CompraRepository();
        ItemCompraRepository itemCompraRepository = new ItemCompraRepository();
        public CadastroService()
        {
        }


        public void SubMenu()
        {
            string opc;
            do
            {
                opc = Menu();
                switch (opc)
                {
                    case "1":
                        Console.Clear();
                        CadastroCliente();
                        BackSubMenuCadastro();
                        break;
                    case "2":
                        Console.Clear();
                        CadastroProduto();
                        BackSubMenuCadastro();
                        break;
                    case "3":
                        Console.Clear();
                        CadastroFornecedor();
                        BackSubMenuCadastro();
                        break;
                    case "4":
                        Console.Clear();
                        CadastroMateriaPrima();
                        BackSubMenuCadastro();
                        break;
                    case "5":
                        Console.Clear();
                        CadastroInadimplente();
                        BackSubMenuCadastro();
                        break;
                    case "6":
                        Console.Clear();
                        CadastroBloqueado();
                        BackSubMenuCadastro();
                        break;
                    case "7":
                        Console.Clear();
                        RemoverInadimplencia();
                        BackSubMenuCadastro();
                        break;
                    case "8":
                        Console.Clear();
                        RemoverBloqueio();
                        BackSubMenuCadastro();
                        break;
                    case "9":
                        Console.Clear();
                        MostrarRegistro();
                        BackSubMenuCadastro();
                        break;
                    case "10":
                        Console.Clear();
                        LocalizarRegistro();
                        BackSubMenuCadastro();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Opção invalida!");
                        break;
                }


            } while (opc != "0");
        }

        public void BackSubMenuCadastro()
        {
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Cadastro...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
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
        public Cliente CadastroCliente()
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
            if (clienteRepository.GetByCPF(cpf) != null)
            {
                Console.WriteLine("\t\t\t\t\tCliente com esse CPF ja existe");
                return null;
            }
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

            return clienteRepository.Add(new Cliente(cpf, nome, dnascimento, sexo));
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
        public Fornecedor CadastroFornecedor()
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
            if (fornecedorRepository.GetByCNPJ(cnpj) != null)
            {
                Console.WriteLine("\t\t\t\t\tFornecedor com esse cnpj ja existe");
                return null;
            }
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

            return fornecedorRepository.Add(new Fornecedor(cnpj, rsocial, dabertura));
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
        public Produto CadastroProduto()
        {
            string svalor;
            float valor;
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
                if (!float.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0))
                    Console.WriteLine("\t\t\t\t\tValor invalido!");
            } while (!float.TryParse(svalor, out valor) || (valor > 99999) || (valor <= 0));

            return produtoRepository.Add(new Produto(nome, valor));
        }

        public MPrima CadastroMateriaPrima()
        {
            string nome;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO MATERIA PRIMA===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o nome da Materia Prima: ");
                nome = Console.ReadLine().Trim();
            } while (nome == "");

            return materiaPrimaRepository.Add(new MPrima(nome));

        }
        public string CadastroInadimplente()
        {
            string cpf;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE INADIMPLENTE===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cpf do inadimplente: ");
                cpf = Console.ReadLine().Trim().Replace(".", "").Replace("-", ""); ;
                if (!ValidaCpf(cpf))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\nDigite novamente");
            } while (!ValidaCpf(cpf));
            if (clienteRepository.ClienteBloqueado(cpf))
            {
                Console.WriteLine("\t\t\t\t\tInadimplente com esse CPF ja existe");
                return null;
            }

            if (clienteRepository.GetByCPF(cpf) != null)
            {
                clienteRepository.BloquearCliente(cpf);
                Console.WriteLine("\t\t\t\t\tCliente adicionado a lista de Inadimplente");
                return cpf;
            }
            else
            {
                Console.WriteLine("\t\t\t\t\tCliente não encontrado");
            }
            return null;
        }
        public string CadastroBloqueado()
        {
            string cnpj;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========CADASTRO DE BLOQUEADO===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o CNPJ do fornecedor: ");
                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(cnpj))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCnpj invalido!\nDigite novamente");
            } while (!ValidaCnpj(cnpj));
            if (fornecedorRepository.FornecedorBloqueado(cnpj))
            {
                Console.WriteLine("\t\t\t\t\tFornecedor com esse cnpj ja existe");
                return null;
            }


            if (fornecedorRepository.GetByCNPJ(cnpj) != null)
            {
                fornecedorRepository.BloquearFornecedor(cnpj);
                Console.WriteLine("\t\t\t\t\tFornecedor adicionado a lista de Inadimplente");
                return cnpj;
            }
            else
            {
                Console.WriteLine("\t\t\t\t\tFornecedor não encontrado");
            }

            return null;
        }

        public void RemoverInadimplencia()
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

            if (clienteRepository.ClienteBloqueado(inadimplente))
            {
                clienteRepository.DesbloquearCliente(inadimplente);
                Console.WriteLine("\t\t\t\t\tCpf Liberado");
            }

        }
        public void RemoverBloqueio()
        {
            string cnpj;
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t===========REMOVER DE BLOQUEADO===========");
            do
            {
                Console.Write("\t\t\t\t\tDigite o cnpj do fornecedor bloqueado: ");
                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                if (!ValidaCnpj(cnpj))//valida cpf
                    Console.WriteLine("\t\t\t\t\tCpf invalido!\n\t\t\t\t\tDigite novamente");
            } while (!ValidaCnpj(cnpj));

            if (fornecedorRepository.FornecedorBloqueado(cnpj))
            {
                fornecedorRepository.DesbloquearFornecedor(cnpj);
                Console.WriteLine("\t\t\t\t\tCNPJ Liberado");
            }
            else
            {
                Console.WriteLine("\t\t\t\t\tFornecedor já esta desbloqueado ou não existe");
            }
        }

        public void MostrarRegistro()
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
                        if (clienteRepository.Count() != 0)
                            new Registros(clienteRepository.GetAllClientes());
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum cliente registrado");
                        break;
                    case "2":
                        if (fornecedorRepository.Count() != 0)
                            new Registros(fornecedorRepository.GetAllFornecedores());
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum fornecedor registrado");
                        break;
                    case "3":
                        if (materiaPrimaRepository.Count() != 0)
                            new Registros(materiaPrimaRepository.GetAllMPrimas());
                        else
                            Console.WriteLine("\t\t\t\t\tNenhuma materia prima registrada");
                        break;
                    case "4":
                        if (produtoRepository.Count() != 0)
                            new Registros(produtoRepository.GetAllProdutos());
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "5":
                        /*if (vendaRepository.Count() != 0)
                            new Registros(vendaRepository.vendas, cadastros.itensvenda);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhuma venda registrada");*/
                        break;
                    case "6":
                        if (compraRepository.Count() != 0)
                            new Registros(compraRepository.GetAllCompras(), itemCompraRepository.GetAllItensCompra());
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                        break;
                    case "7":
                       /* if (producaoRepository.Count() != 0)
                            new Registros(producaoRepository.GetAllProducoes(), cadastros.itensproducao);
                        else
                            Console.WriteLine("\t\t\t\t\tNenhum produto registrado");*/
                        break;
                    case "0":

                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção invalida");
                        break;
                }
                Console.ReadKey();
            } while (opc != "0");
        }
        public void LocalizarRegistro()
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

                switch (opc)
                {

                    case "1":
                        Console.Clear();
                        string cpf;
                        do
                        {
                            Console.Write("\t\t\t\t\tDigite o cpf que deseja localizar: ");
                            cpf = Console.ReadLine();
                            if (!ValidaCpf(cpf))
                            {
                                Console.WriteLine("\t\t\t\t\tCPF INVÁLIDO, TENTE NOVAMENTE!");
                            }
                        } while (ValidaCpf(cpf) != true);

                        Cliente localizarcliente = clienteRepository.SearchClientes(p => p.CPF == (cpf))[0];
                        if (localizarcliente != null)
                        {
                            encontrado = true;
                            Console.WriteLine(localizarcliente.Dados());
                        }
                        BackSubMenuLocalizar();
                        break;
                    case "2":
                        Console.Clear();
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

                        Fornecedor localizarfornecedor = fornecedorRepository.SearchFornecedores(p => p.CNPJ == cnpj)[0];
                        if (localizarfornecedor != null)
                        {
                            encontrado = true;
                            Console.WriteLine(localizarfornecedor.Dados());
                        }
                        BackSubMenuLocalizar();
                        break;
                    case "3":
                        Console.Clear();
                        Console.Write("\t\t\t\t\tDigite o nome que deseja localizar: ");
                        string nomeMateriaPrima = Console.ReadLine().Trim().ToLower();
                        List<MPrima> localizarmprima = materiaPrimaRepository.GetByNome(nomeMateriaPrima);
                        if (localizarmprima != null)
                        {
                            encontrado = true;
                            localizarmprima.ForEach(p => Console.WriteLine(p.Dados()));
                        }
                        break;
                    case "4":
                        Console.Clear();
                        Console.Write("\t\t\t\t\tDigite o nome do produto que deseja localizar: ");
                        string nomeProduto = Console.ReadLine().Trim().ToLower();
                        List<Produto> localizaProduto = produtoRepository.GetByNome(nomeProduto);
                        if (localizaProduto != null)
                        {
                            encontrado = true;
                            localizaProduto.ForEach(p => Console.WriteLine(p.DadosProduto()));
                        }
                        BackSubMenuLocalizar();
                        break;
                    case "5":
                        Console.Clear();
                        Console.Write("\t\t\t\t\tDigite a data de venda que deseja localizar(dd/mm/aaaa): ");
                        DateTime dvenda = DateTime.Parse(Console.ReadLine());
                        /* List<Venda> localizavenda =  cadastros.vendas.FindAll(p => p.DataVenda == dvenda);
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
                         }*/
                        BackSubMenuLocalizar();
                        break;
                    case "6":
                        Console.Clear();
                        Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                        DateTime dcompra = DateTime.Parse(Console.ReadLine());
                        /*List<Compra> localizacompra = cadastros.compras.FindAll(p => p.DataCompra == dcompra);
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
                        }*/
                        BackSubMenuLocalizar();
                        break;
                    case "7":
                        Console.Clear();
                        Console.Write("\t\t\t\t\tDigite o data de produção que deseja localizar(dd/mm/aaaa): ");
                        DateTime dproducao = DateTime.Parse(Console.ReadLine());
                        /*List<Producao> localizaproducao = cadastros.producao.FindAll(p => p.DataProducao == dproducao);
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
                        }*/
                        BackSubMenuLocalizar();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("\t\t\t\t\tOpção inválida! ");
                        Console.ReadKey();
                        break;
                }
                if (encontrado == false && opc != "0")
                    Console.WriteLine("\t\t\t\t\tRegistro não encontrado");
            } while (opc != "0");

        }
        public void BackSubMenuLocalizar()
        {
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Localizar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}