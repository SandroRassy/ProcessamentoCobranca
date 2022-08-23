using ProcessamentoCobranca.Domain.Entities;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente QueryFilter(string nome, string cpf);
    }
}
