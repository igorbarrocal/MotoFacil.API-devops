using MotoFacil.API.Infrastructure.Contexts;
using MotoFacil.API.Infrastructure.Persistence;
using MotoFacil.API.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MotoFacil.API.Infrastructure.Repositories
{
    public class MotoRepository(MotoFacilContext context) : IMotoRepository
    {
        private readonly MotoFacilContext _context = context;

        public async Task<List<Moto>> ObterTodasAsync() => await _context.Motos.ToListAsync();
        public async Task<Moto?> ObterPorIdAsync(long id) => await _context.Motos.FindAsync(id);
        public async Task<Moto> AdicionarAsync(Moto moto)
        {
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }
        public async Task AtualizarAsync(Moto moto)
        {
            _context.Motos.Update(moto);
            await _context.SaveChangesAsync();
        }
        public async Task RemoverAsync(Moto moto)
        {
            _context.Motos.Remove(moto);
            await _context.SaveChangesAsync();
        }
    }
}