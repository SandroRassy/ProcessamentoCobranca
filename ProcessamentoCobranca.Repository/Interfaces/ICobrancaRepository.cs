using ProcessamentoCobranca.Domain.Entities;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface ICobrancaRepository : IRepository<Cobranca>
    {
        Cobranca QueryFilter(DateTime dataVencimento, string cpf);
        IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim, string cpf);
        IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim);
        IQueryable<Cobranca> QueryCPF(string cpf);

    }
}
