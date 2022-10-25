using FilmeAPI.Data;
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

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
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
                return Ok(filme);
            }
                
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] Filme filmeNovo)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); // Repetição de código, precisamos arrumar depois

            if (filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeNovo.Titulo; // Atribuição manual, devemos evitar isso...
            filme.Genero = filmeNovo.Genero;
            filme.Diretor = filmeNovo.Diretor;
            filme.Duracao = filmeNovo.Duracao;
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
