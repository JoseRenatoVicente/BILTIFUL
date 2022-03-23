using BILTIFUL.Core.Entidades.Base;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class Compra : EntidadeBase
    {
        public DateTime DataCompra { get; set; } = DateTime.Now;
        //CNPJ
        public string Fornecedor { get; set; }
        public string ValorTotal { get; set; }
        public Compra()
        {

        }

        public Compra( string id , string fornecedor)//criação
        {
            Id = id.PadLeft(5,'0');
            Fornecedor = fornecedor;
        }

        public Compra(string id,DateTime dataCompra, string fornecedor, string valorTotal)//leitura
        {
            Id = id;
            DataCompra = dataCompra;
            Fornecedor = fornecedor;
            ValorTotal = valorTotal;
        }

        public string ConverterParaEDI()
        {
            return $"{Id}{DataCompra.ToString("dd/MM/yyyy")}{Fornecedor}{ValorTotal}";
        }
        public string DadosCompra()
        {
            return "\t\t\t\t\t-------------------------------------------\n\t\t\t\t\tId: " + Id + "\n\t\t\t\t\tData de compra: " + DataCompra.ToString("dd/MM/yyyy") + "\n\t\t\t\t\tFornecedor: " + Fornecedor + "\n\t\t\t\t\tValor da compra: " + float.Parse(ValorTotal.Insert(5, ","));
        }
    }
}
