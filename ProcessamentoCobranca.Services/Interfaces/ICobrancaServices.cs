using ProcessamentoCobranca.Domain.Entities;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface ICobrancaServices : IService<Cobranca>
    {
        IQueryable<Cobranca> QueryFilter(string mesref, string cpf);
    }
}
