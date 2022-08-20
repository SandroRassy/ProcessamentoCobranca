using MongoDB.Driver;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Base;
using ProcessamentoCobranca.Repository.Context;
using ProcessamentoCobranca.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository
{
    public sealed class ClienteRepository : Repository<Cliente>, IClienteRepository<Cliente>
    {
        public ClienteRepository(IMongoCollection<Cliente> collectionName) : base(collectionName)
        {
        }

        public ClienteRepository(IConnectionFactory connectionFactory, string databaseName, string collectionName)
            : base(connectionFactory, databaseName, collectionName)
        {
        }

        public Cliente QueryFilter(string nome, string cpf)
        {
            var retorno = new Cliente();

            if (!String.IsNullOrEmpty(cpf))
                retorno = _collectionName.AsQueryable<Cliente>().FirstOrDefault(w => w.CPF == cpf);

            if (!String.IsNullOrEmpty(nome))
                retorno = _collectionName.AsQueryable<Cliente>().FirstOrDefault(w => w.Nome == nome);

            return retorno;
        }
    }
}
