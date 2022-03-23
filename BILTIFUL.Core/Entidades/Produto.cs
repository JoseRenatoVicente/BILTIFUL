using BILTIFUL.Core.Entidades.Base;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Globalization;

namespace BILTIFUL.Core.Entidades
{
    public class Produto : IEntidadeDataBase<Produto>
    {
        public long CodigoBarras { get; set; } 
        public string Nome { get; set; }
        public float ValorVenda { get; set; }
        public DateTime UltimaVenda { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;
        public float QuantidadeEstoque { get; set; }

        public Produto()
        {
        }

        public Produto(string nome, float valorVenda)
        {
            Nome = nome;
            ValorVenda = valorVenda;
        }

        public void RetirarEstoque(int quantidade)
        {
            if (QuantidadeEstoque >= quantidade)
            {
                QuantidadeEstoque -= quantidade;
            }
        }

        public bool EstaDisponivel(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }

        public string Dados()
        {
            return $"\n\t\t\t\t\t----------------------------\n" +
                   $"\n\t\t\t\t\tCod. Barra: {CodigoBarras}\n" +
                   $"\t\t\t\t\tNome: {Nome}\n" +
                   $"\t\t\t\t\tValor Unitário: R$ {ValorVenda}\n" +
                   $"\n\t\t\t\t\t----------------------------\n";
        }

        public string ConverterParaDAT()
        {
            return $"{CodigoBarras}{Nome.PadRight(20).Substring(0, 20)}{ValorVenda.ToString().PadLeft(5, '0')}{UltimaVenda.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{(char)Situacao}";
        }

        public string DadosProduto()
        {
            return "-------------------------------------------\nCodigo de barras: " + CodigoBarras + "\nNome: " + Nome + "\nValor venda: " + string.Format(CultureInfo.GetCultureInfo("pt-BR"), " {0:C}", ValorVenda) + "\nData de ultima venda: " + UltimaVenda.ToString("dd/MM/yyyy") + "\nData de cadastro: " + DataCadastro.ToString("dd/MM/yyyy") + "\nSituação: " + Situacao;
        }

        public Produto ExtrairDados(string line)
        {
            if (line == null) return null;

            Nome = line.Substring(5, 20).Trim();
            ValorVenda = float.Parse(line.Substring(30, 10));
            UltimaVenda = DateTime.Parse(line.Substring(40, 10));
            DataCadastro = DateTime.Parse(line.Substring(50, 10));
            Situacao = (Situacao)char.Parse(line.Substring(60, 1));

            return this;
        }
    }
}