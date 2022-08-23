using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Base;
using ProcessamentoCobranca.Services.Interfaces;

namespace ProcessamentoCobranca.Services
{
    public class ClienteServices : Services<Cliente>, IClienteServices
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteServices(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Cliente QueryFilter(string nome, string cpf)
        {
            var result = _clienteRepository.QueryFilter(nome, cpf);
            return result;
        }
    }
}
