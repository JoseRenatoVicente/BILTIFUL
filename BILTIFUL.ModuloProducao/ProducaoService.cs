using BILTIFUL.Core;
using BILTIFUL.Core.Controles;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BILTIFUL.ModuloProducao
{
    public class ProducaoService
    {

        CadastroService cadastroService = new CadastroService();

        public void SubMenu(SqlConnection connection)
        {
            string opcao = "";


            Console.Clear();
            Console.WriteLine("\n\t\t\t\t\t __________________________________________________");
            Console.WriteLine("\t\t\t\t\t|+++++++++++++++++++| PRODUÇÃO |+++++++++++++++++++|");
            Console.WriteLine("\t\t\t\t\t|1| - ADICIONAR PRODUÇÃO                           |");
            Console.WriteLine("\t\t\t\t\t|2| - LOCALIZAR PRODUÇÃO                           |");
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
                    EntradaDadosProducao(new Producao(),connection);
                    break;

                case "2":
                    Console.Clear();
                    cadastroService.LocalizarRegistro(connection);
                    break;

                default:
                    Console.WriteLine("\t\t\t\t\tOpção inválida! ");
                    Console.ReadKey();
                    SubMenu(connection);
                    break;
            }

        }

        public void BackMenu(SqlConnection connection)
        {
            Console.Write("\n\t\t\t Pressione qualquer tecla para voltar ao menu de Produção...");
            Console.ReadKey();
            Console.Clear();
            SubMenu(connection);
        }
        public void InstanciaBanco()
        {
            Controle conexao = new Controle();
            SubMenu(conexao.connection);
        }

        void EntradaDadosProducao(Producao producao, SqlConnection connection)
        {

            List<ItemProducao> itemProducoes = new List<ItemProducao>();
            Produto produto = new Produto();

            bool encontraproduto;
            do
            {
                encontraproduto = false;
                Console.Write("\n\t\t\tInsira o nome do produto a ser produzido: ");
                string nome = Console.ReadLine();
                string idproduto;
                connection.Open();

                String localizaproduto = "SELECT cbarras, nome, ucompra, dcadastro FROM dbo.Produto where nome = '" + nome + "'";

                using (SqlCommand command = new SqlCommand(localizaproduto, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("Codigo de barras: {0}\nNome: {1}\nUltima Compra: {2}\nData de Cadastro: {3}", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"));
                            producao.Produto = reader.GetString(0);
                            encontraproduto = true;
                        }
                    }
                }
                connection.Close();

            } while (encontraproduto == false);

                Console.Write("\n\t\t\tQuantidade de produtos: ");
                if (Int32.TryParse(Console.ReadLine(), out int quantidade))
                    producao.Quantidade = quantidade.ToString();


            bool materiaprima;
            do
            {
                itemProducoes.Add(EntradaDadosItemProducao(new ItemProducao(), connection));
                Console.Write("\n\t\t\tDeseja adicionar mais alguma materia prima? (S/N): ");
                string confirmar = Console.ReadLine().ToUpper();
                materiaprima = confirmar == "S";

            } while (materiaprima);

           // DadosProducao(producao);

            Console.Write("\n\t\t\tDeseja cadastrar a produção (S/N): ");
            string confirma = Console.ReadLine().ToUpper();

            if (confirma == "S")
            {
                Cadastro(producao, itemProducoes, connection);
            }
            itemProducoes.Clear();
            BackMenu(connection);

        }


        ItemProducao EntradaDadosItemProducao(ItemProducao itemProducao, SqlConnection connection)
        {

            
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

            bool buscar = false;
            do
            {
                Console.Write("\t\t\t\t\tInforme o ID referente a Materia-Prima que deseja adicionar : ");
                itemProducao.MateriaPrima = Console.ReadLine().ToUpper();

                connection.Open();

                String localizamprima = "SELECT id, nome, ucompra, dcadastro FROM dbo.MPrima where id = '" + itemProducao.MateriaPrima + "'";

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

            Console.WriteLine("Quantidade Materia prima");
            itemProducao.QuantidadeMateriaPrima = Console.ReadLine();
            itemProducao.Id = (cadastroService.NumeroElementos("Producao", connection) + 1).ToString().PadLeft(5, '0');

            return itemProducao;

        }

        void Cadastro(Producao producao, List<ItemProducao> itemProducaos, SqlConnection connection)
        {
            producao.Id = (cadastroService.NumeroElementos("Producao", connection)+1).ToString().PadLeft(5,'0');

            new Controle(producao,connection);

            itemProducaos.ForEach(c => { c.Id = producao.Id; new Controle(c,connection); });
            itemProducaos.Clear();
        }
    }
}
