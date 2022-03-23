using BILTIFUL.Core.Entidades.Base;
using BILTIFUL.Core.Entidades.Enums;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class MPrima : EntidadeBase, IEntidadeDataBase<MPrima>
    {
        public new string Id { get; set; }
        public string Nome { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public MPrima()
        {
        }

        public MPrima(string nome)
        {
            Nome = nome;
        }

        public MPrima(string nome, Situacao situacao)
        {
            Nome = nome;
            Situacao = situacao;
        }

        public MPrima(string id, string nome, DateTime ucompra, DateTime dcadastro, Situacao situacao)
        {
            Id = id;
            Nome = nome;
            UltimaCompra = ucompra;
            DataCadastro = dcadastro;
            Situacao = situacao;
        }



        public string ConverterParaDAT()
        {
            return $"MP{Id.ToString().PadLeft(4, '0')}{Nome.PadRight(20).Substring(0, 20)}{UltimaCompra.ToString("dd/MM/yyyy")}{DataCadastro.ToString("dd/MM/yyyy")}{(char)Situacao}";
        }
        public string Dados()
        {
            return "\t\t\t\t\t-------------------------------------------\n\t\t\t\t\tId: MP" + Id.ToString().PadLeft(4, '0') + "\n\t\t\t\t\tNome: " + Nome + "\n\t\t\t\t\tData de ultima compra: " + UltimaCompra.ToString("dd/MM/yyyy") + "\n\t\t\t\t\tData de cadastro: " + DataCadastro.ToString("dd/MM/yyyy") + "\n\t\t\t\t\tSituação: " + Situacao;

        }

        public MPrima ExtrairDados(string line)
        {
            if (line == null) return null;


            Nome = line.Substring(6, 20).Trim();
            UltimaCompra = DateTime.Parse(line.Substring(26, 10));
            DataCadastro = DateTime.Parse(line.Substring(36, 10));
            Situacao = (Situacao)char.Parse(line.Substring(46, 1));

            return this;
        }
    }
}
