using ProcessamentoCobranca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface ICobrancaConsumoServices : IService<CobrancaConsumo>
    {
        void CalcularConsumo(Cobranca cobranca, Cliente cliente);
    }
}
