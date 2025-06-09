using ServiceControl.Domain.Entities;
using ServiceControl.Domain.Interfaces;
using ServiceControl.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceControl.Infrastructure.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly AppDbContext _context;

        public RegistroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SalvarAsync(Registro registro)
        {
            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Registro>> ObterTodosAsync()
        {
            return await _context.Registros.ToListAsync();
        }
    }
}
