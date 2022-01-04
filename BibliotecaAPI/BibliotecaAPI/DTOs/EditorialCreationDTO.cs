using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class EditorialCreationDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
