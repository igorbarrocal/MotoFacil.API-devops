using MotoFacil.API.DTOs.Usuario;
using Swashbuckle.AspNetCore.Filters;

namespace MotoFacil.API.Examples
{
    public class UsuarioDTOExample : IExamplesProvider<UsuarioDTO>
    {
        public UsuarioDTO GetExamples()
        {
            return new UsuarioDTO
            {
                Nome = "Igor Dias Barrocal",
                Email = "igor@gmail.com",
                Senha = "12345678",
                Tipo = 0 // ADMINISTRADOR
            };
        }
    }
}