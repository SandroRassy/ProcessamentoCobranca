using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface ICobrancaServices : IService<Cobranca>
    {       
        IQueryable<Cobranca> QueryFilter(string mesref, string cpf);     
    }
}
