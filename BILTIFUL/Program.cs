using BILTIFUL.Application.Service;
using System;
using System.Globalization;

namespace BILTIFUL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

            Menu();

            /*ProdutoRepository produtoRepository = new ProdutoRepository();
            MateriaPrimaRepository materiaPrimaRepository = new MateriaPrimaRepository();
            FornecedorRepository fornecedorRepository = new FornecedorRepository();
            ClienteRepository clienteRepository = new ClienteRepository();

            produtoRepository.GetAllProdutos();

            Produto produto = new Produto { CodigoBarras = 789661700005, Nome = "Produto1", ValorVenda = 10, Situacao = Situacao.Ativo, QuantidadeEstoque = 1 };

            produtoRepository.Add(produto);
            produto.Nome = "produto editado";

            produtoRepository.Update(produto);

            produtoRepository.Remove(produto.CodigoBarras);

            materiaPrimaRepository.GetAllMPrimas();

            MPrima mPrima = new MPrima { Nome="Glicerina"};

            materiaPrimaRepository.Add(mPrima);
            mPrima.Nome = "Batom";

            materiaPrimaRepository.Update(mPrima);

            materiaPrimaRepository.Remove("MP0001");*/

            /*Fornecedor fornecedor = new Fornecedor { CNPJ = "112121", RazaoSocial = "Empresa 1", DataAbertura=DateTime.Now};
            fornecedorRepository.Add(fornecedor);
            fornecedor.RazaoSocial = "Empresa editada";

            fornecedorRepository.Update(fornecedor);

            fornecedorRepository.Remove("112121");*/

            /*Cliente cliente = new Cliente { Nome = "Jose", CPF = "11121213", DataNascimento = DateTime.Parse("2004/03/22") };
            clienteRepository.GetAllClientes();

            clienteRepository.Add(cliente);
            cliente.Nome = "Cliente editado";

            clienteRepository.GetAllClientes();

            clienteRepository.Update(cliente);

            clienteRepository.Remove(cliente.CPF);*/



            /*Repository<MPrima> repository = new Repository<MPrima>("MPrima.dat");

            repository.Add(new MPrima("Teste", Core.Entidades.Enums.Situacao.Inativo));

            foreach (var p in repository.GetAll())
                Console.WriteLine(p.Dados());

            Console.WriteLine(repository.GetByWhere(c => c.Nome == "teste2").Dados());

            Console.ReadKey();*/

            ;
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| BILTIFUL |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - PRODUCAO                                     |");
            Console.WriteLine("\t\t\t\t\t|2| - COMPRA                                       |");
            Console.WriteLine("\t\t\t\t\t|3| - VENDA                                        |");
            Console.WriteLine("\t\t\t\t\t|4| - CADASTRO                                     |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                         |");
            Console.Write("\t\t\t\t\t|__________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "0": Environment.Exit(0); break;

                case "1":
                    Console.Clear();
                    new ProducaoService().SubMenu();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    new CadastroService().SubMenu();
                    BackMenu();
                    break;

                default:
                    Console.WriteLine("\t\t\t\tOpção inválida! ");
                    BackMenu();
                    break;
            }
        }
        public static void BackMenu()
        {
            Console.WriteLine("\n\t\t\t\t Pressione qualquer tecla para voltar ao menu principal...");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}
