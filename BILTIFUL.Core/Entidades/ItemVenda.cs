using BILTIFUL.Core.Entidades.Base;

namespace BILTIFUL.Core.Entidades
{
    public class ItemVenda : EntidadeBase, IEntidadeDataBase<ItemVenda>
    {
        //ID produto
        public long Produto { get; set; }
        public float Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public float TotalItem => Quantidade * ValorUnitario;
        public ItemVenda()
        {
        }

        public string ConverterParaDAT()
        {
            return $"{Id}{Produto}{Quantidade}{ValorUnitario}{TotalItem}";
        }
        public string Dados()
        {
            return $"-------------------------------------------\nProduto: {Produto}\nQuantidade: {Quantidade}\nValor total: {TotalItem}\n-------------------------------------------";
        }

        public ItemVenda ExtrairDados(string line)
        {
            if (line == null) return null;

            Id = int.Parse(line.Substring(0, 5));
            Produto = int.Parse(line.Substring(5, 12));
            Quantidade = float.Parse(line.Substring(17, 3));
            ValorUnitario = int.Parse(line.Substring(20, 5));

            return this;
        }
    }
}
