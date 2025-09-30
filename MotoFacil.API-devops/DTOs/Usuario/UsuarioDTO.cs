namespace MotoFacil.API.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int Tipo { get; set; }
    }
}