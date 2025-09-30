namespace MotoFacil.API.Infrastructure.Persistence
{
    public enum TipoUsuario
    {
        ADMINISTRADOR,
        MECANICO
    }
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; }
    }
}