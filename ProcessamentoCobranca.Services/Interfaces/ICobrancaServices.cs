﻿using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface ICobrancaServices
    {
        IQueryable<Cobranca> QueryAll();

        Cobranca Query(Guid key);
        Cobranca QueryFilter(DateTime dataVencimento, string cpf);
        void Insert(Cobranca product);
    }
}