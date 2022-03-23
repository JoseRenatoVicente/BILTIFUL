using System.Collections.Generic;
using BILTIFUL.Core.Entidades.Base;

namespace BILTIFUL.Core.Entidades
{
    public class ItemVenda : EntidadeBase
    {
        //ID produto
        public string Produto { get; set; }
        public string Quantidade { get; set; }
        public string ValorUnitario { get; set; }
        public string TotalItem { get; set; }
        public ItemVenda()
        {
        }

        public ItemVenda(string id,string produto, string qtd)
        {
            Id = id;
            this.Produto = produto;
            this.Quantidade = qtd;

        }

        public ItemVenda(string id,string produto, string quantidade, string valorUnitario)
        {
            Id=id.PadLeft(5,'0');
            Produto = produto;
            Quantidade = quantidade.PadLeft(3,'0');
            ValorUnitario = valorUnitario.PadLeft(5,'0');
            TotalItem = (float.Parse(Quantidade) * float.Parse(ValorUnitario)).ToString().PadLeft(6,'0');
        }

        public override string ToString()
        {
            
            return $"\n\t\t\t\t\tCódigo id: {Id}\n" +
                   $"\t\t\t\t\tCódigo produto: {Produto}\n"+
                   $"\t\t\t\t\tCódigo Quantidade : {Quantidade}"; 
        }

        public Produto CodigoProdutoValido(string codproduto, List<Produto> list)
        {
            Produto aux = list.Find(i => i.CodigoBarras == codproduto);

            if (aux == null)
            {
                System.Console.WriteLine("\t\t\t\t\tNenhum Produto encontrado!!");
            }
            else
            {
                System.Console.WriteLine(aux.ExibirProd());
            }
            return aux;
        }
        public string ConverterParaEDI()
        {
            return $"{Id}{Produto}{Quantidade}{ValorUnitario}{TotalItem}";
        }
        public string DadosItemVenda()
        {
            return $"-------------------------------------------\nProduto: {Produto}\nQuantidade: {float.Parse(Quantidade.Insert(3, ","))}\nValor total: {float.Parse(TotalItem.Insert(3, ","))}\n-------------------------------------------";
        }
    }
}
