using BibliotecaAPI.ValidationAttributes;

namespace BibliotecaAPI.DTOs
{
    public class AutorCreationDTO
    {
    
        public string Nombre { get; set; }
        [ExtensionValidationAttribute(new[] {"image/png","image/jpg","image/gif"})]
        [SizeValidationAttribute(5096)]
        public IFormFile Foto { get; set; }
    }
}
