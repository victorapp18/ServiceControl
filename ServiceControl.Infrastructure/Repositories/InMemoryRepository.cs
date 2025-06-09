using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceControl.Infrastructure.Repositories
{
    public class InMemoryRepository : IRegistroRepository
    {
        private readonly ConcurrentDictionary<Guid, Registro> _registros = new();

        public Task SalvarAsync(Registro registro)
        {
            // Gera um ID único (Guid) e atribui ao registro
            var newId = Guid.NewGuid();
            registro.Id = newId;
            _registros[newId] = registro;
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Registro>> ObterTodosAsync()
        {
            return Task.FromResult(_registros.Values.AsEnumerable());
        }
    }
}
