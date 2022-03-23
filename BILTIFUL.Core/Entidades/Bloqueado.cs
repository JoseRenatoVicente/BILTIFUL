using BILTIFUL.Core.Entidades.Base;
using System;

namespace BILTIFUL.Core.Entidades
{
    public class Bloqueado : IEntidadeDataBase<Bloqueado>
    {
        public string CNPJ { get; set; }

        public string ConverterParaDAT()
        {
            return $"{CNPJ}";
        }

        public string Dados()
        {
            throw new NotImplementedException();
        }

        public Bloqueado ExtrairDados(string line)
        {
            CNPJ = line;
            return line != null ? this : null;
        }
    }
}
