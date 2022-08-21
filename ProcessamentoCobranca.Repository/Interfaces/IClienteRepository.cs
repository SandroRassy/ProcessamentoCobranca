using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente QueryFilter(string nome, string cpf);
    }
}
