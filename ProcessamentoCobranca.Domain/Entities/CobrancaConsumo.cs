using MongoDB.Bson.Serialization.Attributes;
using ProcessamentoCobranca.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public sealed class CobrancaConsumo : Cobranca
    {       
        public string ValorConsumo { get; set; }        

        public CobrancaConsumo(DateTime dataVencimento, string cpf, string valorcobranca, string valorconsumo)
        {
            DataVencimento = dataVencimento;
            CPF = cpf;
            ValorCobranca = valorcobranca;
            ValorConsumo = valorconsumo;
        }
    }
}
