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
        public string IdBoleto { get; set; }
        public string Estado { get; set; }

        public CobrancaConsumo(Cobranca obj, string valorconsumo, string idboleto, string estado)
        {
            DataVencimento = obj.DataVencimento;
            CPF = obj.CPF;
            ValorCobranca = obj.ValorCobranca;            
            Key = obj.Key;
            ValorConsumo = valorconsumo;
            IdBoleto = idboleto;
            Estado = estado;
        }
    }
}
