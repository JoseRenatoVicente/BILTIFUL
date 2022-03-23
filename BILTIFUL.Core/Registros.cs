using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Core.Entidades;

namespace BILTIFUL.Core
{
    public class Registros
    {
        public Registros(List<Cliente> clientes)
        {

            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>CLIENTES<<<<<<<<");
                Console.WriteLine(clientes[i].Dados());
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t|1-primeiro");
                    Console.WriteLine("\t\t\t\t\t|2-anterior");
                }
                if (i < clientes.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t|3-proximo");
                    Console.WriteLine("\t\t\t\t\t|4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t|0-Sair");
                Console.Write("\t\t\t\t\t|Opção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= clientes.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = clientes.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<Fornecedor> fornecedor)
        {

            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>FORNECEDORES<<<<<<<<");
                Console.WriteLine(fornecedor[i].Dados());
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < fornecedor.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= fornecedor.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = fornecedor.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<MPrima> materiaprima)
        {
            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>MATERIAS PRIMAS<<<<<<<<");
                Console.WriteLine(materiaprima[i].Dados());
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < materiaprima.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= materiaprima.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = materiaprima.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<Produto> produto)
        {
            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>PRODUTOS<<<<<<<<");
                Console.WriteLine(produto[i].DadosProduto());
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < produto.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= produto.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = produto.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<Compra> compras, List<ItemCompra> itenscompras)
        {
            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>COMPRAS<<<<<<<<");
                Console.WriteLine(compras[i].Dados());
                Console.WriteLine("\t\t\t\t\tItens da compra: ");
                List<ItemCompra> itens = itenscompras.FindAll(p => p.Id == compras[i].Id);//encontra todos os itens com mesmo id da compra
                itens.ForEach(p => Console.WriteLine(p.Dados()));//mostra todos os itens
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < compras.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= compras.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = compras.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<Producao> producao, List<ItemProducao> itensproducao)
        {
            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>PRODUÇÕES<<<<<<<<");
                Console.WriteLine(producao[i].DadosProducao());
                Console.WriteLine("\t\t\t\t\tItens da produção: ");
                List<ItemProducao> itens = itensproducao.FindAll(p => p.Id == producao[i].Id);//encontra todos os itens com mesmo id da compra
                itens.ForEach(p => Console.WriteLine(p.Dados()));//mostra todos os itens
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < producao.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= producao.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = producao.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
        public Registros(List<Venda> vendas, List<ItemVenda> itensvendas)
        {
            int i = 0;
            string opc = "-1";
            while (opc != "0")
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t>>>>>>>>VENDAS<<<<<<<<");
                Console.WriteLine(vendas[i].Dados());
                Console.WriteLine("\t\t\t\t\tItens da venda: ");
                List<ItemVenda> itens = itensvendas.FindAll(p => p.Id == vendas[i].Id);//encontra todos os itens com mesmo id da compra
                itens.ForEach(p => Console.WriteLine(p.Dados()));//mostra todos os itens
                if (i > 0)
                {
                    Console.WriteLine("\t\t\t\t\t1-primeiro");
                    Console.WriteLine("\t\t\t\t\t2-anterior");
                }
                if (i < vendas.Count() - 1)
                {
                    Console.WriteLine("\t\t\t\t\t3-proximo");
                    Console.WriteLine("\t\t\t\t\t4-ultimo");
                }
                Console.WriteLine("\t\t\t\t\t0-Sair");
                Console.Write("\t\t\t\t\tOpção: ");
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
                            Console.WriteLine("\t\t\t\t\tNão existe registro antes deste");
                        Console.ReadKey();
                        break;
                    case "3":
                        if (i + 1 <= vendas.Count() - 1)
                            i++;
                        else
                            Console.WriteLine("\t\t\t\t\tNão existe registro depois deste");
                        Console.ReadKey();
                        break;
                    case "4":
                        i = vendas.Count() - 1;
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            }
        }
    }
}