using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Services.Models.DTO;

namespace ProcessamentoCobranca.Services.Interfaces
{
    public interface ICobrancaConsumoServices : IService<CobrancaConsumo>
    {
        void CalcularConsumo(Cobranca cobranca, Cliente cliente);
        RelatorioConsumoDTO QueryFilter(string mesref, string estado);
    }
}
