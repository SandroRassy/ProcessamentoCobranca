using MongoDB.Bson.Serialization.Attributes;
using ProcessamentoCobranca.Domain.Base;

namespace ProcessamentoCobranca.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public sealed class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public string CPF { get; set; }

        public Cliente(string nome, string estado, string cpf)
        {
            Nome = nome;
            Estado = estado;
            CPF = cpf;
        }

        public Cliente()
        {

        }

    }
}
