using MotoFacil.API.Infrastructure.Persistence;

namespace MotoFacil.API.Infrastructure.Repositories.Interfaces
{
    public interface IMotoRepository
    {
        Task<List<Moto>> ObterTodasAsync();
        Task<Moto?> ObterPorIdAsync(long id);
        Task<Moto> AdicionarAsync(Moto moto);
        Task AtualizarAsync(Moto moto);
        Task RemoverAsync(Moto moto);
    }
}