using BibliotecaAPI.Interfaces;

namespace BibliotecaAPI.Entidades
{
    public class Categoria : IHaveId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
