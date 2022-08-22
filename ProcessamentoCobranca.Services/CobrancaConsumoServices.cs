using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Base;
using ProcessamentoCobranca.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;

namespace ProcessamentoCobranca.Services
{
    public class CobrancaConsumoServices : Services<CobrancaConsumo>, ICobrancaConsumoServices
    {
        private readonly ICobrancaConsumoRepository _cobrancaRepository;

        public CobrancaConsumoServices(ICobrancaConsumoRepository cobrancaConsumoRepository) : base(cobrancaConsumoRepository)
        {
            _cobrancaRepository = cobrancaConsumoRepository;
        }
    }
}
