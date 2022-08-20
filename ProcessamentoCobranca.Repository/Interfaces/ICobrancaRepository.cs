using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface ICobrancaRepository<T> : IRepository<T>
    {
        Cobranca QueryFilter(DateTime dataVencimento, string cpf);
        IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim, string cpf);
        IQueryable<Cobranca> QueryRefMes(DateTime dataInicio, DateTime dataFim);
        IQueryable<Cobranca> QueryCPF(string cpf);

    }
}
