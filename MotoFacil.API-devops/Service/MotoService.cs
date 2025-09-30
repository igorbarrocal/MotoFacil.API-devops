using MotoFacil.API.Infrastructure.Persistence;
using MotoFacil.API.Infrastructure.Repositories.Interfaces;

namespace MotoFacil.API.Services
{
    public class MotoService(IMotoRepository repo)
    {
        private readonly IMotoRepository _repo = repo;

        public async Task<List<Moto>> ObterTodasAsync() => await _repo.ObterTodasAsync();
        public async Task<Moto?> ObterPorIdAsync(long id) => await _repo.ObterPorIdAsync(id);
        public async Task<Moto> AdicionarAsync(Moto moto) => await _repo.AdicionarAsync(moto);
        public async Task AtualizarAsync(Moto moto) => await _repo.AtualizarAsync(moto);
        public async Task RemoverAsync(Moto moto) => await _repo.RemoverAsync(moto);
    }
}