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
using BILTIFUL.Core.Banco;

namespace BILTIFUL.ModuloVenda
{
    public class VendaService
    {

        Controle controle = new Controle();
        bd_biltiful banco = new bd_biltiful();
        ItemVenda vendaitem = new ItemVenda();
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
            Console.WriteLine("\t\t\t\t\t|3| - EXIBIR VENDAS CADASTRADAS                  |");
            Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                     |");
            Console.Write("\t\t\t\t\t|________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");
        }

        public void SubMenu()
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
                        CadastrarVenda();
                        break;
                    case 2:
                        servicocadastro.LocalizarRegistro();
                        break;

                    case 3:
                        if (servicocadastro.cadastros.vendas.Count() != 0)
                            new Registros(servicocadastro.cadastros.vendas, servicocadastro.cadastros.itensvenda);
                        else
                            Console.WriteLine("Nenhum Venda registrada");
                        break;

                    default:
                        Console.WriteLine("Digite Uma Opção invalida");
                        Console.ReadKey();
                        break;
                }
            } while (0 != opc);

        }

        public void CadastrarVenda()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t------------- Verificar CPF -------------\n");
            Console.Write("\t\t\t\t\tDigite o Cpf do cliente: ");

            string cpfCliente = Console.ReadLine();

            if (banco.VerificarCpfInadimplenteBD(cpfCliente))
            {
                return;
            }
            else
            {
                if (!banco.VerificaCpfExisteBD(cpfCliente))
                {
                    Console.WriteLine("\n\t\t\t\t\t-----------------------------------------" +
                                      "\n\t\t\t\t\t   CPF não encontrado\n" +
                                      "\t\t\t\t\t-----------------------------------------");
                    Console.Write("\t\t\t\t\tCadastrar um nome Cliente (S/N): ");
                    string cadNovoCliente = Console.ReadLine().ToUpper();
                    if (cadNovoCliente == "S" || cadNovoCliente == "SIM")
                    {
                        servicocadastro.CadastroCliente();
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\t\t\t\t\tRetornando para o Menu de Vendas... aperte qualquer tecla...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Write("\n\t\t\t\t\tConfirma dados Cliente (S/N): ");
                    if (char.TryParse(Console.ReadLine().ToUpper(), out char confirmarCliente))
                    {
                        if (confirmarCliente == 'S')
                        {
                            ItemVenda();
                        }
                    }
                }
            }
        }

        public void ItemVenda()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t------------ Cadastro de Venda ------------");
            int cont = 0;
            float quantidade = 1;

            string codigo = CodIdIncremento();
            do
            {
                Console.Write("\n\t\t\t\t\tCódigo do Produto: ");
                string codProduto = Console.ReadLine();
                if (banco.VerificaProdutoExisteBD(codProduto))
                {
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
                        // float valorUnitario = float.Parse(aux.ValorVenda.Insert(3, ","));
                        float valorTotal = quantidade;
                        if (valorTotal > 9999.99)
                        {
                            do
                            {
                                Console.WriteLine("\n\t\t\t\t\tValor Total superior ao permitido, máximo valor por item é R$ 9.999\n");
                                Console.Write("\t\t\t\t\tDigite a Quantidade do Produto: ");
                                if (float.TryParse(Console.ReadLine(), out float quantUnidadeTotal) && quantidade > 0)
                                {
                                    quantidade = quantUnidadeTotal;
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\tDigite uma quantidade válida!");
                                }
                                valorTotal = quantidade;
                            } while (valorTotal > 9999.99);
                        }
                        if (valorVenda + valorTotal == 99999.99)
                        {
                            Console.WriteLine("\n\t\t\t\t\tValor total de Compras atigindo, abrir novo cadastro de compras");
                            cont = 3;
                        }
                        else if (valorVenda + valorTotal > 99999.99)
                        {
                            do
                            {
                                Console.WriteLine($"\t\t\t\t\tPreço máximo por compra atingido, escolha outra quantidade do produto até R$ {99999 - valorVenda}\n");
                                Console.Write("\t\t\t\t\tDigite a Quantidade do Produto: ");
                                if (float.TryParse(Console.ReadLine(), out float quantValorTotal) && quantidade > 0)
                                {
                                    quantidade = quantValorTotal;
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\t\t\tDigite uma quantidade válida!");
                                }
                                valorTotal = quantidade;
                            } while (valorVenda + valorTotal > 99999.99);
                        }

                        valorVenda = valorTotal + valorVenda;

                        Console.WriteLine($"\n\t\t\t\t\tValor Total: R${valorTotal.ToString("F2").TrimStart('0')}");
                        cont++;

/*                        ItemVenda itemVenda = new ItemVenda(codigo, codProduto, quantidade.ToString().Replace(",", "").Replace(".", ""), valorUnitario.ToString("F2").Replace(",", "").Replace(".", ""));
                        controle.itensvenda.Add(new ItemVenda(codigo, codProduto, quantidade.ToString().Replace(",", "").Replace(".", ""), valorUnitario.ToString("F2").Replace(",", "").Replace(".", "")));*/
                        /*banco.InserirItemVendaBD(itemVenda);*/

                        //Console.WriteLine($"\t\t\t\t\t{quantidade} {aux.Nome} adicionados na venda!!");
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
                }
            } while (cont <= 2 || cont != 3);

            Console.Write("\n\t\t\t\t\tConfirmar Compras (S/N): ");
            string confirmarCompras = Console.ReadLine().ToUpper();
            if (confirmarCompras == "S" || confirmarCompras == "SIM")
            {
                SalvarItemVenda(codigo);
                controle.vendas.Add(new Venda(codigo, clienteVenda, valorVenda.ToString("F2").Replace(",", "").Replace(".", "")));
                new Controle(new Venda(codigo, clienteVenda, valorVenda.ToString("F2").Replace(",", "").Replace(".", "")));

                Console.WriteLine("\n\t\t\t\t\tCompra cadastrada com sucesso!!");
                Console.ReadKey();

            }
            else
            {
                RemoveItem(codigo);
                CodIdDecremento();
                Console.WriteLine("\n\t\t\t\t\tVenda Cancelada!!");
                Console.ReadKey();
            }
            controle = new Controle();
        }

        public void RemoveItem(string codigo)
        {
            codigo = codigo.PadLeft(5, '0');
            for (int i = 0; i < 3; i++)
            {
                controle.itensvenda.FindAll(delegate (ItemVenda iv)
                {
                    if (iv.Id == codigo)
                    {
                        Console.WriteLine("\t\t\t\t\tRemovendo compra...");
                        Console.WriteLine("\n\t\t\t\t\t" + iv);
                        controle.itensvenda.Remove(iv);
                    }
                    return true;
                });
            }

        }
        public string CodIdIncremento()
        {
            controle.codigos[2]++;
            SalvarCodigos();
            string cod = "" + controle.codigos[2];

            return cod;
        }
        public string CodIdDecremento()
        {
            controle.codigos[2]--;
            SalvarCodigos();
            string cod = "" + controle.codigos[2];

            return cod;
        }

        public void SalvarItemVenda(string codigo)
        {
            codigo = codigo.PadLeft(5, '0');

            foreach (ItemVenda iv in controle.itensvenda)
            {
                if (iv.Id == codigo)
                {
                    new Controle(new ItemVenda(codigo, iv.Produto, iv.Quantidade.ToString().Replace(",", "").Replace(".", ""), iv.ValorUnitario.ToString().Replace(",", "").Replace(".", "")));

                }
            }
        }
        public void SalvarCodigos()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Arquivos\\Controle.dat");
                sw.WriteLine(controle.codigos[0]);
                sw.WriteLine(controle.codigos[1]);
                sw.WriteLine(controle.codigos[2]);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool BuscarInadimplentes(string clientcpf, List<string> inadimplentes)
        {
            string clienteinadimplentes = inadimplentes.Find(delegate (string i) { return i == clientcpf.ToString(); });
            if (clienteinadimplentes == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Cliente BuscarCpf(string ccpf, List<Cliente> cliente)
        {
            Cliente clientecompra = cliente.Find(delegate (Cliente c) { return c.CPF == ccpf; });
            return clientecompra;
        }

    }
}
