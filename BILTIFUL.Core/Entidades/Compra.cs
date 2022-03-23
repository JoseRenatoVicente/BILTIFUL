using BILTIFUL.Core.Entidades.Base;
using System;
using System.Collections.Generic;

namespace BILTIFUL.Core.Entidades
{
    public class Compra : EntidadeBase, IEntidadeDataBase<Compra>
    {
        public DateTime DataCompra { get; set; } = DateTime.Now;
        //CNPJ
        public string Fornecedor { get; set; }
        public float ValorTotal { get; set; }
        public List<ItemCompra> Itens { get; set; }
        public Compra()
        {

        }

        public Compra(string fornecedor, float valorTotal)
        {
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }

        public string ConverterParaDAT()
        {
            return $"{Id}{DataCompra.ToString("dd/MM/yyyy")}{Fornecedor}{ValorTotal}";
        }
        public string Dados()
        {
            return "\t\t\t\t\t-------------------------------------------\n\t\t\t\t\tId: " + Id + "\n\t\t\t\t\tData de compra: " + DataCompra.ToString("dd/MM/yyyy") + "\n\t\t\t\t\tFornecedor: " + Fornecedor + "\n\t\t\t\t\tValor da compra: " + ValorTotal;
        }

        public Compra ExtrairDados(string line)
        {
            if (line == null) return null;

            Id = int.Parse(line.Substring(0, 5));
            DataCompra = DateTime.Parse(line.Substring(5, 10));
            Fornecedor = line.Substring(15, 14);
            ValorTotal = float.Parse(line.Substring(29, 7));

            return this;
        }
    }
}
