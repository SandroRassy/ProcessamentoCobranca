using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services
{
    public class ClienteServices : IClienteServices
    {
        private readonly IClienteRepository<Cliente> _clienteRepository;        
        public ClienteServices(IClienteRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public void Insert(Cliente cliente)
        {
            _clienteRepository.Insert(cliente);
        }

        public Cliente Query(Guid key)
        {
            var result = _clienteRepository.Query(key);            
            return result;
        }

        public Cliente QueryFilter(string nome, string cpf)
        {
            var result = _clienteRepository.QueryFilter(nome, cpf);
            return result;
        }

        public IQueryable<Cliente> QueryAll()
        {
            var result = _clienteRepository.QueryAll();

            return result;
        }        
    }
}
