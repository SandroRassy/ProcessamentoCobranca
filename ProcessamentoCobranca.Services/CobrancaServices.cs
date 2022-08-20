﻿using ProcessamentoCobranca.Domain.Entities;
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

        public IQueryable<Cobranca> QueryFilter(string mesref, string cpf)
        {
            if (!String.IsNullOrEmpty(mesref))
            {
                var mesrefsplit = mesref.Split('/');
                var ano = int.Parse(mesrefsplit[1]);
                var mes = int.Parse(mesrefsplit[0]);

                DateTime primeiroDiaDoMes = new DateTime(ano, mes, 1);
                DateTime ultimoDiaDoMes = new DateTime(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month, DateTime.DaysInMonth(primeiroDiaDoMes.Year, primeiroDiaDoMes.Month)).AddMinutes(1439).AddSeconds(59);

                if(String.IsNullOrEmpty(cpf))
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
