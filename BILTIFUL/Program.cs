using BILTIFUL.Core.Controles;

using BILTIFUL.Core;
using System;
using BILTIFUL.ModuloCompra;
using BILTIFUL.ModuloProducao;
using BILTIFUL.ModuloVenda;

namespace BILTIFUL
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            Menu();
        }

        public static void Menu()
        {
            ProducaoService producaoService = new ProducaoService();
            VendaService vendaService = new VendaService();
            CompraService compraService = new CompraService();
            CadastroService cadastroService = new CadastroService();

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
                    producaoService.InstanciaBanco();
                    BackMenu();
                    break;

                case "2":
                    Console.Clear();
                    compraService.InstanciaBanco();
                    BackMenu();
                    break;

                case "3":
                    Console.Clear();
                    vendaService.InstanciaBanco();
                    BackMenu();
                    break;

                case "4":
                    Console.Clear();
                    cadastroService.InstanciaBanco();
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
