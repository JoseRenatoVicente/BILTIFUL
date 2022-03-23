using BILTIFUL.Core.Entidades.Base;
using BILTIFUL.Core.Entidades.Enums;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class Fornecedor : IEntidadeDataBase<Fornecedor>
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Fornecedor()
        {
        }

        public Fornecedor(string cNPJ, string razaoSocial, DateTime dataAbertura)
        {
            CNPJ = cNPJ;
            RazaoSocial = razaoSocial;
            DataAbertura = dataAbertura;
        }

        public string DadosFornecedorCompra()
        {
            return "\t\t\t\t\tFornecedor:\t" + RazaoSocial + "\n\t\t\t\t\tCnpj:\t" + CNPJ + "\n\t\t\t\t\tData de Abertura:\t" + DataAbertura.ToString("dd/MM/yyyy");
        }

        public string ConverterParaDAT()
        {
            return $"{CNPJ.ToString().PadLeft(14, '0')}{RazaoSocial.PadRight(50).Substring(0, 50)}{DataAbertura.ToString("dd/MM/yyyy")}{UltimaCompra.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{(char)Situacao}";
        }
        public string Dados()
        {
            return "-------------------------------------------\nRazão social: " + RazaoSocial + "\nCNPJ: " + CNPJ.ToString().PadLeft(14, '0') + "\nData de abertura: " + DataAbertura.ToString("dd/MM/yyyy") + "\nData de ultima compra: " + UltimaCompra.ToString("dd/MM/yyyy") + "\nData de cadastro: " + DataCadastro.ToString("dd/MM/yyyy") + "\nSituação: " + Situacao;
        }

        public Fornecedor ExtrairDados(string line)
        {
            if (line == null) return null;

            CNPJ = line.Substring(0, 14);
            RazaoSocial = line.Substring(14, 50).Trim();
            DataAbertura = DateTime.Parse(line.Substring(64, 10));
            UltimaCompra = DateTime.Parse(line.Substring(74, 10));
            DataCadastro = DateTime.Parse(line.Substring(84, 10));
            Situacao = (Situacao)char.Parse(line.Substring(94, 1));

            return this;
        }

    }
}
