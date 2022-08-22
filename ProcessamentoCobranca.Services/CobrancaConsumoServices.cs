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
        private readonly ICobrancaConsumoRepository _cobrancaConsumoRepository;

        public CobrancaConsumoServices(ICobrancaConsumoRepository cobrancaConsumoRepository) : base(cobrancaConsumoRepository)
        {
            _cobrancaConsumoRepository = cobrancaConsumoRepository;
        }

        public void CalcularConsumo(Cobranca cobranca)
        {
            string digitoInical = cobranca.CPF.Substring(0, 2);
            string digitoFinal = cobranca.CPF.Substring(cobranca.CPF.Length - 2, 2);
            string valorConsumo = digitoInical + digitoFinal + ",00";

            var consumo = new CobrancaConsumo(cobranca, valorConsumo, cobranca.Key.ToString());

            _cobrancaConsumoRepository.Insert(consumo);
        }
    }
}
