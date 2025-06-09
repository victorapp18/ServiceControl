using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceControl.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task AdicionarAsync(T entidade);
        Task<T> ObterPorIdAsync(int id);          // ← de string para int
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<bool> RemoverAsync(int id);          // ← de string para int (caso queira remoção)
    }
}
