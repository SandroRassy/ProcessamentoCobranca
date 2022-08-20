using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface IClienteServices
    {
        IQueryable<Cliente> QueryAll();

        Cliente Query(Guid key);
        Cliente QueryFilter(string nome, string cpf);
        void Insert(Cliente product);
    }
}
