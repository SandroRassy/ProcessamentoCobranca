using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Base;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;

namespace ProcessamentoCobranca.Repository
{
    public sealed class CobrancaConsumoRepository : Repository<CobrancaConsumo>, ICobrancaConsumoRepository
    {
        public CobrancaConsumoRepository(IMongoCollection<CobrancaConsumo> collectionName) : base(collectionName)
        {
        }

        public CobrancaConsumoRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public IQueryable<CobrancaConsumo> QueryRefMes(DateTime dataInicio, DateTime dataFim, string estado)
        {
            var retorno = _collectionName.AsQueryable<CobrancaConsumo>().Where(w => w.Estado == estado && w.DataVencimento >= dataInicio && w.DataVencimento <= dataFim);
            return retorno;
        }
    }
}
