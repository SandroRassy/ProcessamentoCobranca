using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface IClienteRepository<T> : IRepository<T>
    {
        Cliente QueryFilter(string nome, string cpf);
    }
}
