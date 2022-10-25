using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.Dtos
{
    // Igual a nossa entidade, mas só com as informações que queremos que seja recebida pelo body, nesse caso,
    // tudo menos o Id.
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "O campo título é obrigatório")] // Não aceita nulo ou vazio
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage = "O gênero não pode passar de 30 caracteres")] // Tamanho máximo aceito
        public string Genero { get; set; }
        [Range(1, 600, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 600 minutos")] // Só aceita valores entre esses
        public int Duracao { get; set; }
    }
}
