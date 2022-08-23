using ProcessamentoCobranca.Domain.Interfaces;

namespace ProcessamentoCobranca.Domain.Base
{
    public abstract class Entity : IEntity
    {
        public Guid Key { get; set; }

        protected Entity()
        {
            this.Key = Guid.NewGuid();
        }
    }
}
