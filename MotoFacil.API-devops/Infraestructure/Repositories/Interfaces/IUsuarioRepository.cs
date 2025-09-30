using MotoFacil.API.Infrastructure.Persistence;

namespace MotoFacil.API.Infrastructure.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ObterTodasAsync();
        Task<Usuario?> ObterPorIdAsync(long id);
        Task<Usuario> AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task RemoverAsync(Usuario usuario);
        Task<bool> EmailExisteAsync(string email, long? ignoreId = null);
    }
}