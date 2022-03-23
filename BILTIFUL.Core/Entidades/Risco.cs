using BILTIFUL.Core.Entidades.Base;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class Risco : IEntidadeDataBase<Risco>
    {
        public string CPF { get; set; }

        public string ConverterParaDAT()
        {
            return $"{CPF}";
        }

        public string Dados()
        {
            throw new NotImplementedException();
        }

        public Risco ExtrairDados(string line)
        {
            CPF = line;
            return line != null ? this : null;
        }
    }
}
