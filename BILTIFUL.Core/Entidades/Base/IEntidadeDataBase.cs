namespace BILTIFUL.Core.Entidades.Base
{
    public interface IEntidadeDataBase<TEntity>
    {
        public string ConverterParaDAT();
        public string Dados();
        public TEntity ExtrairDados(string line);
    }
}
