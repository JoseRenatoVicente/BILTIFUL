using BILTIFUL.Application.Repository;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BILTIFUL.Application.Service
{
    public class ProducaoService
    {
        private ProducaoRepository producaoRepository = new ProducaoRepository();
        private MateriaPrimaRepository materiaPrimaRepository = new MateriaPrimaRepository();
        private ProdutoRepository produtoRepository = new ProdutoRepository();

        private CadastroService cadastroService = new CadastroService();
        public void SubMenu()
        {
            string opcao = "";


            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| PRODUÇÃO |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - ADICIONAR PRODUÇÃO                           |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR PRODUÇÃO                           |");
            Console.WriteLine("\t\t\t\t\t|3| - EXIBIR PRODUÇÃO CADASTRADAS                  |");
            Console.WriteLine("\t\t\t\t\t|0| - SAIR                                         |");
            Console.Write("\t\t\t\t\t|__________________________________________________|\n" +
                          "\t\t\t\t\t|Opção: ");


            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "0":
                    break;

                case "1":
                    Console.Clear();
                    if (!MateriaPrimaVazia())
                    {
                        EntradaDadosProducao(new Producao());
                    }

                    break;

                case "2":
                    Console.Clear();
                    if (!ProducaoVazia())
                    {
                        Localizar();
                    }

                    break;

                case "3":
                    Console.Clear();
                    if (!ProducaoVazia())
                    {
                        ImpressaoDoRegistro();
                    }
                
                    break;
                default:
                    Console.WriteLine("\t\t\t\t\tOpção inválida! ");
                    Console.ReadKey();
                    SubMenu();
                    break;
            }
        }

        public void BackMenu()
        {
            Console.WriteLine("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Produção...");
            Console.ReadKey();
            Console.Clear();
            SubMenu();
        }

        public bool ProducaoVazia()
        {
            if (producaoRepository.Count() == 0)
            {
                Console.WriteLine("\n\t\t\tNenhuma produção cadastrada no sistema.");
                BackMenu();
                return true;
            }

            return false;
        }

        public bool MateriaPrimaVazia()
        {
            if (materiaPrimaRepository.Count() == 0)
            {
                Console.WriteLine("\n\t\t\tNenhuma Materia Prima cadastrada no sistema.");
                BackMenu();
                return true;
            }

            return false;
        }

        private void EntradaDadosProducao(Producao producao)
        {

            Produto produto = new Produto();

            if (produtoRepository.Count() == 0)
            {
                Console.WriteLine("\n\t\t\tNenhum produto cadastrado");
            }

            Console.WriteLine("\n\t\t\tDeseja cadastrar um novo produto para Produção? Sim/Não");
            string existe = Console.ReadLine().ToLower();
            if (existe == "s" || existe == "sim")
            {
                produto = cadastroService.CadastroProduto();
                if (produto != null)
                {
                    producao.Produto = produto.CodigoBarras;
                }
            }
            else if (existe == "n" || existe == "nao" || existe == "não")
            {
                do
                {
                    Console.WriteLine("\n\t\t\tInsira o nome do produto a ser localizado:");
                    string nome = Console.ReadLine();

                    produto = produtoRepository.GetByNome(nome).FirstOrDefault();

                    if (produto != null)
                    {
                        producao.Produto = produto.CodigoBarras;
                        Console.WriteLine(produto.DadosProduto());
                    }
                    else
                    {
                        Console.WriteLine("\n\t\t\tProduto não localizado");
                    }

                } while (produto == null);

            }
            else
            {
                EntradaDadosProducao(new Producao());
            }

            if (producao.Quantidade == 0)
            {
                Console.Write("\n\t\t\tQuantidade de produtos: ");
                if (int.TryParse(Console.ReadLine(), out int quantidade))
                {
                    producao.Quantidade = quantidade;
                }
                else
                {
                    Console.WriteLine("\n\t\t\tQuantidade inválida");
                    EntradaDadosProducao(producao);
                }
            }

            bool materiaprima;
            do
            {
                producao.Itens = new List<ItemProducao>();
                producao.Itens.Add(EntradaDadosItemProducao(new ItemProducao()));
                Console.WriteLine("\n\t\t\tDeseja adicionar mais alguma materia prima? Sim/Não");
                string confirmar = Console.ReadLine().ToLower();
                materiaprima = confirmar == "s" || confirmar == "sim";

            } while (materiaprima);

            DadosProducao(producao);

            Console.WriteLine("\n\t\t\tDeseja cadastrar a produção? Sim/Não");
            string confirma = Console.ReadLine().ToLower();

            if (confirma == "s" || confirma == "sim")
            {
                producaoRepository.Add(producao);
            }
            BackMenu();

        }

        private ItemProducao EntradaDadosItemProducao(ItemProducao itemProducao)
        {

            int posicao = 0;

            int materiasprimas = 0;

            Console.WriteLine("\n\t\t\tQual a materia prima utilizada na produção?");
            List<MPrima> mPrimas =  materiaPrimaRepository.GetAllMPrimas();

            mPrimas.ForEach(c => Console.WriteLine(++posicao + "- " + c.Nome));

            materiasprimas = int.Parse(Console.ReadLine());


            itemProducao.MateriaPrima = mPrimas[materiasprimas - 1].Id;


            if (itemProducao.QuantidadeMateriaPrima == 0)
            {
                Console.Write("\n\t\t\tQuantidade Materia prima: ");
                if (int.TryParse(Console.ReadLine(), out int quantidade))
                {
                    itemProducao.QuantidadeMateriaPrima = quantidade;
                }
                else
                {
                    Console.WriteLine("\n\t\t\tQuantidade inválida");
                    EntradaDadosItemProducao(itemProducao);
                }
            }

            /*Console.WriteLine("Quantidade Materia prima");
            while (!float.TryParse(Console.ReadLine(), out float quantidadeMateriaPrima))
            {
                Console.WriteLine("Quantos produtos serão produzidos");
                itemProducao.QuantidadeMateriaPrima = quantidadeMateriaPrima;
            }*/

            return itemProducao;

        }

        private void DadosProducao(Producao producao)
        {
            Console.WriteLine(producao.Dados());
            Console.WriteLine("\n\t\t\tProduto: ");
            Console.WriteLine(produtoRepository.GetByCodigoBarras(producao.Produto.ToString()).DadosProduto());


            foreach (ItemProducao itemProducao in producao.Itens)
            {
                Console.WriteLine("\n\t\t\tQuantidade Materia prima: " + itemProducao.QuantidadeMateriaPrima);
                Console.WriteLine(materiaPrimaRepository.GetById(itemProducao.MateriaPrima).Dados());
            }
        }

        private void ImpressaoDoRegistro()
        {

            List<Producao> producoes = producaoRepository.GetAllProducoes();

            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                DadosProducao(producoes[i]);
                Console.WriteLine();
                if (i > 0)
                {
                    Console.WriteLine("1-primeiro");
                    Console.WriteLine("2-anterior");
                }
                if (i < producoes.Count - 1)
                {
                    Console.WriteLine("3-proximo");
                    Console.WriteLine("4-ultimo");
                }
                Console.WriteLine("0-Sair");
                Console.Write("Opção: ");
                opc = Console.ReadLine();
                switch (opc)
                {
                    case "1":
                        i = 0;
                        break; ;
                    case "2":
                        if (i - 1 >= 0)
                            i--;
                        else
                            Console.WriteLine("Não existe registro antes deste");
                        break;
                    case "3":
                        if (i + 1 <= producoes.Count - 1)
                            i++;
                        else
                            Console.WriteLine("Não existe registro depois deste");
                        break;
                    case "4":
                        i = producoes.Count - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }

            }

        }

        private void Localizar()
        {

            Console.WriteLine("Digite o nome do produto para localizar a produção dele.");
            string busca = Console.ReadLine();

            Produto produto = new Produto();
            produtoRepository.GetByNome(busca);
            Producao producao;
            producao = produto != null ? producaoRepository.GetByProduto(produto.CodigoBarras).FirstOrDefault() : null;

            if (producao != null)
            {
                DadosProducao(producao);
            }
            else
            {
                Console.WriteLine("Nenhuma produção encontrada para esse produto\n\n" + (produto != null ? produto.DadosProduto() : string.Empty));
            }

            BackMenu();
        }

    }
}
