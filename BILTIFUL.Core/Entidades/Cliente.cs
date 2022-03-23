using BILTIFUL.Core.Entidades.Base;
using BILTIFUL.Core.Entidades.Enums;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class Cliente : IEntidadeDataBase<Cliente>
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public bool Bloqueado { get; set; }

        public Cliente()
        {
        }

        public Cliente(string cPF, string nome, DateTime dataNascimento, Sexo sexo)
        {
            CPF = cPF;
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public string ConverterParaDAT()
        {
            return $"{CPF.ToString().PadLeft(11, '0')}{Nome.PadRight(50).Substring(0, 50)}{DataNascimento.ToString("dd/MM/yyyy")}{(char)Sexo}{UltimaCompra.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{(char)Situacao}";
        }

        public string Dados()
        {
            return "-------------------------------------------\n|Nome: " + Nome + "\n|CPF: " + CPF + "\n|Data de nascimento: " + DataNascimento.ToString("dd/MM/yyyy") + "\n|Sexo: " + Sexo + "\n|Ultima compra: " + UltimaCompra.ToString("dd/MM/yyyy") + "\n|Data de cadastro: " + DataCadastro.ToString("dd/MM/yyyy") + "\n|Situação: " + Situacao;
        }

        public string VendasCliente()
        {
            return $"\n\t\t\t\t\t-------------- Informações --------------" +
                    $"\n\t\t\t\t\tCpf: {CPF.ToString().PadLeft(11, '0')}" +
                    $"\n\t\t\t\t\tNome: {Nome}" +
                    $"\n\t\t\t\t\tData Ultima Compra: {UltimaCompra.ToString("dd/MM/yyyy")}" +
                    $"\n\t\t\t\t\t-----------------------------------------";
        }

        public Cliente ExtrairDados(string line)
        {
            if (line == null) return null;

            CPF = line.Substring(0, 11);
            Nome = line.Substring(11, 50).Trim();
            DataNascimento = DateTime.Parse(line.Substring(61, 10));
            Sexo = (Sexo)char.Parse(line.Substring(71, 1));
            UltimaCompra = DateTime.Parse(line.Substring(72, 10));
            DataCadastro = DateTime.Parse(line.Substring(82, 10));
            Situacao = (Situacao)char.Parse(line.Substring(92, 1));

            return this;
        }
    }

}
