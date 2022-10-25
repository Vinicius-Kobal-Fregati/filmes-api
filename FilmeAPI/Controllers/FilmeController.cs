using AutoMapper;
using FilmeAPI.Data;
using FilmeAPI.Data.Dtos;
using FilmeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FilmeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost] // Usando o Dto no lugar da entidade.
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto); // Converte CreateFilmeDto para Filme.

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return CreatedAtAction(nameof(RecuperaFilmesPorId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        public IEnumerable<Filme> RecuperarFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesPorId(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(Filme => Filme.Id == id); // LINQ
            if (filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return Ok(filmeDto);
            }
                
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); // Repetição de código, precisamos arrumar depois

            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filme); // Sobrescreve filme com as informações de filmeDto.
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); // Repetição de código, precisamos arrumar depois

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
