using BILTIFUL.Core.Entidades.Base;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class ItemCompra : EntidadeBase, IEntidadeDataBase<ItemCompra>
    {
        public DateTime DataCompra { get; set; } = DateTime.Now;
        //ID materia prima
        public string MateriaPrima { get; set; }
        public float Quantidade { get; set; }
        public float ValorUnitario { get; set; }
        public float TotalItem => Quantidade * ValorUnitario;

        public ItemCompra()
        {
        }

        public ItemCompra(string materiaPrima, float quantidade, float valorUnitario)
        {
            MateriaPrima = materiaPrima;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public string ConverterParaDAT()
        {
            return $"{Id}{DataCompra.ToString("dd/MM/yyyy")}{MateriaPrima}{Quantidade}{ValorUnitario}{TotalItem}";
        }
        public string Dados()
        {
            return $"\t\t\t\t\tMateria prima: {MateriaPrima}\n\t\t\t\t\tQuantidade: {Quantidade}\n\t\t\t\t\tValor unitario: {ValorUnitario}\n\t\t\t\t\tTotal: {TotalItem}\n\t\t\t\t\t-------------------------------------------";
        }

        public ItemCompra ExtrairDados(string line)
        {
            if (line == null) return null;

            Id = int.Parse(line.Substring(0, 5));
            DataCompra = DateTime.Parse(line.Substring(5, 10));
            Quantidade = int.Parse(line.Substring(21, 5));
            ValorUnitario = float.Parse(line.Substring(26, 5));

            return this;
        }
    }
}
