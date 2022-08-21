

namespace ProcessamentoCobranca.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> QueryAll();        
        T Query(Guid key);        
        void Insert(T obj);
    }
}
