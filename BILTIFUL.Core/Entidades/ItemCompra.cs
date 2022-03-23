using BILTIFUL.Core.Entidades.Base;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class ItemCompra : EntidadeBase
    {
        public DateTime DataCompra { get; set; } = DateTime.Now;
        //ID materia prima
        public string MateriaPrima { get; set; }
        public string Quantidade { get; set; }
        public string ValorUnitario { get; set; }
        public string TotalItem { get; set; }

        public ItemCompra()
        {
        }

        public ItemCompra(string id,string materiaPrima, string quantidade, string valorUnitario)
        {
            Id = id.PadLeft(5,'0');
            MateriaPrima = materiaPrima;
            Quantidade = quantidade.PadLeft(5,'0');
            ValorUnitario = valorUnitario.PadLeft(5, '0');
        }

        public ItemCompra(string id, DateTime dataCompra, string materiaPrima, string quantidade, string valorUnitario, string totalItem)
        {
            Id = id;
            DataCompra = dataCompra;
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            TotalItem = totalItem;
        }

        public string ConverterParaEDI()
        {
            return $"{Id}{DataCompra.ToString("dd/MM/yyyy")}{MateriaPrima}{Quantidade}{ValorUnitario}{TotalItem}";
        }
        public string DadosItemCompra()
        {
            return $"\t\t\t\t\tMateria prima: {MateriaPrima}\n\t\t\t\t\tQuantidade: {float.Parse(Quantidade.Insert(3, ","))}\n\t\t\t\t\tValor unitario: {float.Parse(ValorUnitario.Insert(3, ","))}\n\t\t\t\t\tTotal: {float.Parse(TotalItem.Insert(4, ","))}\n\t\t\t\t\t-------------------------------------------";
        }
    }
}
