using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Base;
using ProcessamentoCobranca.Services.Interfaces;
using ProcessamentoCobranca.Services.Models.DTO;

namespace ProcessamentoCobranca.Services
{
    public class CobrancaConsumoServices : Services<CobrancaConsumo>, ICobrancaConsumoServices
    {
        private readonly ICobrancaConsumoRepository _cobrancaConsumoRepository;

        public CobrancaConsumoServices(ICobrancaConsumoRepository cobrancaConsumoRepository) : base(cobrancaConsumoRepository)
        {
            _cobrancaConsumoRepository = cobrancaConsumoRepository;
        }

        public void CalcularConsumo(Cobranca cobranca, Cliente cliente)
        {
            int digitoInical = int.Parse(cobranca.CPF.Substring(0, 2));
            int digitoFinal = int.Parse(cobranca.CPF.Substring(cobranca.CPF.Length - 2, 2));
            string valorConsumo = digitoInical.ToString() + digitoFinal.ToString() + ",00";

            var consumo = new CobrancaConsumo(cobranca, valorConsumo, cobranca.Key.ToString(), cliente.Estado);

            _cobrancaConsumoRepository.Insert(consumo);
        }

        public RelatorioConsumoDTO QueryFilter(string mesref, string estado)
        {
            var retorno = new RelatorioConsumoDTO();
            var mesrefsplit = mesref.Split('/');
            var ano = int.Parse(mesrefsplit[1]);
            var mes = int.Parse(mesrefsplit[0]);
            double totalconsumo = 0;
            double totalcobranca = 0;

            DateTime primeiroDiaDoMes = new DateTime(ano, mes, 1);
            DateTime ultimoDiaDoMes = new DateTime(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month, DateTime.DaysInMonth(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month)).AddMinutes(1439).AddSeconds(59);

            var consumo = _cobrancaConsumoRepository.QueryRefMes(primeiroDiaDoMes, ultimoDiaDoMes, estado.ToUpper()).ToList();

            foreach (var item in consumo)
            {
                totalconsumo += Double.Parse(item.ValorConsumo);
                totalcobranca += Double.Parse(item.ValorCobranca);
            }

            retorno.totalConsumo = String.Format("{0:0.00#}", totalconsumo);
            retorno.totalCobrado = String.Format("{0:0.00#}", totalcobranca);

            return retorno;
        }
    }
}
