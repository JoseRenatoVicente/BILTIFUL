using BILTIFUL.Application.Repository;
using BILTIFUL.Core;
using BILTIFUL.Core.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BILTIFUL.Application.Service
{
    public class CompraService
    {
        CadastroService cadastroService = new CadastroService();

        CompraRepository compraRepository = new CompraRepository();
        ItemCompraRepository itemCompraRepository = new ItemCompraRepository();
        MateriaPrimaRepository materiaPrimaRepository = new MateriaPrimaRepository();

        FornecedorRepository fornecedorRepository = new FornecedorRepository();

        //List<Fornecedor> testes = new List<Fornecedor>();
        //public void AdicionarFornecedor()
        //{

        //    testes.Add(new Fornecedor(1, "fornecedor1"));
        //    testes.Add(new Fornecedor(2, "fornecedor2"));
        //}
        string cnpj;
        public void SubMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| COMPRAS |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - CADASTRAR COMPRA                            |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR COMPRA                            |");
            Console.WriteLine("\t\t\t\t\t|3| - EXIBIR COMPRAS CADASTRADAS                  |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                        |");
            Console.Write("\t\t\t\t\t|_________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");

            string opc = Console.ReadLine();
            switch (opc)
            {
                case "1":
                    CadastrarCompra();

                    break;
                case "2":
                    LocalizarCompra(compraRepository.GetAllCompras(), itemCompraRepository.GetAllItensCompra());
                    break;
                case "3":
                    if (compraRepository.Count() != 0)
                        new Registros(compraRepository.GetAllCompras(), itemCompraRepository.GetAllItensCompra());
                    else
                        Console.WriteLine("\t\t\t\t\tNenhum produto registrado");
                    break;
                case "0":
                    break;
                default:
                    break;
            }
        }

        public void LocalizarCompra(List<Compra> compras, List<ItemCompra> itens)
        {
            string opc;
            do
            {

                Console.Clear();
                Console.WriteLine("\t\t\t\t\t________________________________________________");
                Console.WriteLine("\t\t\t\t\t|++++++++++++| MENU DE LOCALIZAÇÃO |+++++++++++|");
                Console.WriteLine("\t\t\t\t\t|1| - LOCALIZAR POR DATA                       |");
                Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR POR FORNECEDOR                 |");
                Console.WriteLine("\t\t\t\t\t|3| - LOCALIZAR POR ID                         |");
                Console.WriteLine("\t\t\t\t\t|0| - VOLTAR                                   |");
                Console.Write("\t\t\t\t\t|______________________________________________|\n" +
                              "\t\t\t\t\t|Opção: ");
                opc = Console.ReadLine();
                bool encontrado = false;
                Console.Clear();
                switch (opc)
                {
                    case "1":
                        Console.Write("\t\t\t\t\tDigite o data de compra que deseja localizar(dd/mm/aaaa): ");
                        DateTime dcompra = DateTime.Parse(Console.ReadLine());
                        List<Compra> localizacompra = compraRepository.GetByData(dcompra);
                        if (localizacompra != null)
                        {

                            foreach (Compra p in localizacompra)
                            {
                                Console.WriteLine(p.Dados());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemCompra i in itemCompraRepository.GetAllItensCompra())
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.Dados());
                                    encontrado = true;
                                }
                                Console.ReadKey();
                            }
                        }
                        if (encontrado != true)
                        {
                            Console.WriteLine("\t\t\t\t\tNenhuma compra encontrada");
                            Console.ReadKey();
                        }

                        break;

                    case "2":

                        bool saida = false;

                        Console.Write("\t\t\t\t\tDigite o CNPJ do fornecedor que deseja localizar: ");

                        cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                        saida = true;
                        List<Compra> localizacnpj = compraRepository.GetByFornecedor(cnpj);
                        if (localizacnpj != null)
                        {

                            foreach (Compra p in localizacnpj)
                            {
                                Console.WriteLine(p.Dados());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemCompra i in itemCompraRepository.GetAllItensCompra())
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.Dados());
                                    encontrado = true;
                                }
                                Console.ReadKey();
                            }
                        }
                        if (encontrado != true)
                        {
                            Console.WriteLine("\t\t\t\t\tNenhuma compra encontrada");
                            Console.ReadKey();
                        }

                        break;
                    case "3":
                        Console.Write("\t\t\t\t\tDigite o ID da compra que deseja localizar: ");
                        int idCompra = int.Parse(Console.ReadLine());
                        /*List<Compra> localizaId = compraRepository.GetById(idCompra);
                        if (localizaId != null)
                        {

                            foreach (Compra p in localizaId)
                            {
                                Console.WriteLine(p.Dados());
                                Console.WriteLine("\t\t\t\t\tItens: ");
                                foreach (ItemCompra i in itemCompraRepository.GetAllItensCompra())
                                {
                                    if (i.Id == p.Id)
                                        Console.WriteLine(i.Dados());
                                    encontrado = true;
                                }
                                Console.ReadKey();
                            }
                        }*/
                        if (encontrado != true)
                        {
                            Console.WriteLine("\t\t\t\t\tNenhuma compra encontrada");
                            Console.ReadKey();
                        }

                        break;
                    case "0":
                        SubMenu();
                        break;
                    default:
                        Console.WriteLine("Selecione uma opcao valida");
                        break;
                }

            } while (opc != "0");
        }




        public void CadastrarCompra()
        {


            string opc = "a";

            Console.Clear();
            do
            {
                Console.WriteLine("\n\t\t\t\t\t------------CADASTRAR COMPRA------------");
                Console.Write("\t\t\t\t\tInforme o CNPJ do forncedor : ");


                cnpj = Console.ReadLine().Trim().Replace(".", "").Replace("-", "").Replace("/", "");


                if (fornecedorRepository.FornecedorBloqueado(cnpj))
                {
                    Console.WriteLine("\t\t\t\t\tFornecedor bloqueado para compra");
                    return;
                }

                Fornecedor fornecedorCompra = BuscarCnpj(cnpj.ToString(), fornecedorRepository.GetAllFornecedores());
                if (fornecedorCompra == null)
                {
                    Console.WriteLine("\t\t\t\t\tFornecedor nao encontrado.");
                }
                else
                {
                    Console.WriteLine(fornecedorCompra.DadosFornecedorCompra());
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
            ItemCompra();


        }
        public void ItemCompra()
        {
            int cont = 0;
            string saida = "a";
            string opcp = "a";
            float[] stringValor = new float[4];
            float valor, quantidadeTeste;
            float[] valorQuantidade = new float[4];
            float[] quantidade = new float[4];
            float[] totalItem = new float[4];
            string[] idMPrima = new string[4];
            float[] quantidadeString = new float[4];
            float valorTotal = 0;
            string[] totalItemString = new string[4];
            bool valorTotalMaior = false;
            do
            {

                do
                {
                    opcp = "a";
                    string buscarMPrima;
                    Console.Clear();
                    do
                    {
                        Console.Write("\t\t\t\t\tInforme o nome da Materia-Prima : ");
                        buscarMPrima = Console.ReadLine();
                    } while (ImprimirMPrima(materiaPrimaRepository.GetAllMPrimas(), buscarMPrima) != true);
                    MPrima mPrimaCompra;
                    bool buscar = true;
                    do
                    {
                        Console.Write("\t\t\t\t\tInforme o ID referente a Materia-Prima que deseja adicionar : ");
                        idMPrima[cont] = Console.ReadLine().ToUpper();

                        mPrimaCompra = BuscaMPrima(idMPrima[cont], materiaPrimaRepository.GetAllMPrimas());
                        if (mPrimaCompra == null)
                        {
                            Console.WriteLine("\t\t\t\t\tMateria-Prima nao encontrada.");
                            buscar = false;

                        }
                        else
                        {
                            buscar = true;
                        }
                    } while (buscar != true);

                    Console.WriteLine(mPrimaCompra.Dados());
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

                            if (float.TryParse(Console.ReadLine(), out stringValor[cont]))
                            {
                                valorQuantidade[cont] = stringValor[cont];

                                if ((stringValor[cont] > 99999) || (stringValor[cont] <= 0))
                                    Console.WriteLine("\t\t\t\t\tValor invalido!");

                            }
                            else
                            {
                                Console.WriteLine("\t\t\t\t\tInforme um valor correto");
                                Console.WriteLine("\t\t\t\t\tPressione uma tecla para voltar");
                                Console.ReadKey();
                            }
                        } while ((stringValor[cont] > 99999) || (stringValor[cont] <= 0));
                        do
                        {
                            do
                            {
                                do
                                {
                                    Console.Write("\t\t\t\t\tInforme a quantidade: ");
                                    if (float.TryParse(Console.ReadLine(), out float confirmar1))
                                    {
                                        quantidadeString[cont] = confirmar1;

                                        quantidade[cont] = quantidadeString[cont];

                                        totalItemString[cont] = (quantidade[cont] * valorQuantidade[cont]).ToString("F2");
                                        totalItem[cont] = (quantidade[cont] * valorQuantidade[cont]);
                                        if (quantidadeString[cont] > 99999 || quantidadeString[cont] <= 0)
                                            Console.WriteLine("\t\t\t\t\tValor invalido!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\t\t\t\t\tInforme uma quantidade correta");
                                        Console.WriteLine("\t\t\t\t\tPressione uma tecla para voltar");
                                        Console.ReadKey();
                                    }
                                } while (quantidadeString[cont] > 99999 || quantidadeString[cont] <= 0);
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
                string valorTotalString = (valorTotal.ToString("F2"));
                valorTotalString = valorTotalString.Trim().Replace(".", "").Replace(",", "");

                Compra compra = new Compra(cnpj, valorTotal);
                compra.Itens = new List<ItemCompra>();

                for (int i = 0; i < cont; i++)
                {
                    compra.Itens.Add(new ItemCompra(idMPrima[i], quantidadeString[i], stringValor[i]));
                }

                compraRepository.Add(compra);
            }
            else
            {
                SubMenu();
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
                Console.WriteLine(m.Dados());
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
