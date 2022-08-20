using System;

namespace ProcessamentoCobranca.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
