using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task AdicionarAsync(T entidade);
        Task<T> ObterPorIdAsync(int id);          
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<bool> RemoverAsync(int id);          
    }
}
