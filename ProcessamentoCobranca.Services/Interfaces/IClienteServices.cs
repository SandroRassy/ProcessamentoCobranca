using ProcessamentoCobranca.Domain.Entities;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface IClienteServices : IService<Cliente>
    {
        Cliente QueryFilter(string nome, string cpf);
    }
}
