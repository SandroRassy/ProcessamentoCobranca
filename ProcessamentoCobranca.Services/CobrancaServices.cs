using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Repository.Interfaces;
using ProcessamentoCobranca.Services.Base;
using ProcessamentoCobranca.Services.Interfaces;

namespace ProcessamentoCobranca.Services
{
    public class CobrancaServices : Services<Cobranca>, ICobrancaServices
    {
        private readonly ICobrancaRepository _cobrancaRepository;

        public CobrancaServices(ICobrancaRepository cobrancaRepository) : base(cobrancaRepository)
        {
            _cobrancaRepository = cobrancaRepository;
        }

        public IQueryable<Cobranca> QueryFilter(string mesref, string cpf)
        {
            if (!String.IsNullOrEmpty(mesref))
            {
                var mesrefsplit = mesref.Split('/');
                var ano = int.Parse(mesrefsplit[1]);
                var mes = int.Parse(mesrefsplit[0]);

                DateTime primeiroDiaDoMes = new DateTime(ano, mes, 1);
                DateTime ultimoDiaDoMes = new DateTime(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month, DateTime.DaysInMonth(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month)).AddMinutes(1439).AddSeconds(59);

                if (String.IsNullOrEmpty(cpf))
                    return _cobrancaRepository.QueryRefMes(primeiroDiaDoMes, ultimoDiaDoMes);
                else
                    return _cobrancaRepository.QueryRefMes(primeiroDiaDoMes, ultimoDiaDoMes, cpf);
            }
            else
            {
                return _cobrancaRepository.QueryCPF(cpf);
            }

        }
    }
}
