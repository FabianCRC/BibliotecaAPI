using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class CategoriaCreationDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
