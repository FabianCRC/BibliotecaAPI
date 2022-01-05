namespace BibliotecaAPI.DTOs
{
    public class AutorCreationDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IFormFile Foto { get; set; }
    }
}
