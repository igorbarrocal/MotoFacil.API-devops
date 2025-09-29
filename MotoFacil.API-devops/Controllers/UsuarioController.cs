using AutoMapper;
using MotoFacil.API.DTOs.Usuario;
using MotoFacil.API.Infrastructure.Persistence;
using MotoFacil.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MotoFacil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(UsuarioService service, IMapper mapper) : ControllerBase
    {
        private readonly UsuarioService _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _service.ObterTodasAsync();
            return Ok(_mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(long id)
        {
            var usuario = await _service.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(_mapper.Map<UsuarioDTO>(usuario));
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO dto)
        {
            if (await _service.EmailExisteAsync(dto.Email)) return BadRequest("Email já cadastrado.");
            var novoUsuario = _mapper.Map<Usuario>(dto);
            await _service.AdicionarAsync(novoUsuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = novoUsuario.Id }, _mapper.Map<UsuarioDTO>(novoUsuario));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, UsuarioDTO dto)
        {
            var usuario = await _service.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            if (await _service.EmailExisteAsync(dto.Email, id)) return BadRequest("Email já cadastrado.");
            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;
            usuario.Tipo = (TipoUsuario)dto.Tipo;
            await _service.AtualizarAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _service.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            await _service.RemoverAsync(usuario);
            return NoContent();
        }
    }
}