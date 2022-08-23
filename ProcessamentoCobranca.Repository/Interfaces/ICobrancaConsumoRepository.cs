using ProcessamentoCobranca.Domain.Entities;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface ICobrancaConsumoRepository : IRepository<CobrancaConsumo>
    {
        IQueryable<CobrancaConsumo> QueryRefMes(DateTime dataInicio, DateTime dataFim, string estado);
    }
}
