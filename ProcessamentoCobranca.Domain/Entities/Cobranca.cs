using MongoDB.Bson.Serialization.Attributes;
using ProcessamentoCobranca.Domain.Base;

namespace ProcessamentoCobranca.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Cobranca : Entity
    {
        public DateTime DataVencimento { get; set; }
        public string CPF { get; set; }
        public string ValorCobranca { get; set; }

        public Cobranca()
        {

        }

        public Cobranca(DateTime dataVencimento, string cpf, string valorcobranca)
        {
            DataVencimento = dataVencimento;
            CPF = cpf;
            ValorCobranca = valorcobranca;
        }
    }
}
