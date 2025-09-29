using AutoMapper;
using MotoFacil.API.DTOs.Moto;
using MotoFacil.API.Infrastructure.Persistence;
using MotoFacil.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MotoFacil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController(MotoService service, IMapper mapper) : ControllerBase
    {
        private readonly MotoService _service = service;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDetalhesDTO>>> GetMotos()
        {
            var motos = await _service.ObterTodasAsync();
            return Ok(_mapper.Map<List<MotoDetalhesDTO>>(motos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MotoDetalhesDTO>> GetMoto(long id)
        {
            var moto = await _service.ObterPorIdAsync(id);
            if (moto == null) return NotFound();
            return Ok(_mapper.Map<MotoDetalhesDTO>(moto));
        }

        [HttpPost]
        public async Task<ActionResult<MotoDetalhesDTO>> PostMoto(MotoDTO dto)
        {
            var novaMoto = _mapper.Map<Moto>(dto);
            await _service.AdicionarAsync(novaMoto);
            return CreatedAtAction(nameof(GetMoto), new { id = novaMoto.Id }, _mapper.Map<MotoDetalhesDTO>(novaMoto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(long id, MotoDTO dto)
        {
            var moto = await _service.ObterPorIdAsync(id);
            if (moto == null) return NotFound();
            moto.Modelo = dto.Modelo;
            moto.Placa = dto.Placa;
            moto.Chassi = dto.Chassi;
            moto.ProblemaIdentificado = dto.ProblemaIdentificado;
            await _service.AtualizarAsync(moto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoto(long id)
        {
            var moto = await _service.ObterPorIdAsync(id);
            if (moto == null) return NotFound();
            await _service.RemoverAsync(moto);
            return NoContent();
        }
    }
}