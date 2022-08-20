using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services
{
    public class CobrancaServices : ICobrancaServices
    {
        private readonly ICobrancaRepository<Cobranca> _cobrancaRepository;

        public CobrancaServices(ICobrancaRepository<Cobranca> cobrancaRepository)
        {
            _cobrancaRepository = cobrancaRepository;
        }
        public void Insert(Cobranca cobranca)
        {
            _cobrancaRepository.Insert(cobranca);
        }

        public Cobranca Query(Guid key)
        {
            var result = _cobrancaRepository.Query(key);

            return result;
        }

        public IQueryable<Cobranca> QueryAll()
        {
            var result = _cobrancaRepository.QueryAll();

            return result;
        }

        public Cobranca QueryFilter(DateTime dataVencimento, string cpf)
        {
            var result = _cobrancaRepository.QueryFilter(dataVencimento, cpf);
            return result;
        }
    }
}
