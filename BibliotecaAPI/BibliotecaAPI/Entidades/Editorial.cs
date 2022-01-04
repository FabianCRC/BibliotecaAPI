using BibliotecaAPI.Interfaces;

namespace BibliotecaAPI.Entidades
{
    public class Editorial:IHaveId
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
    }
}
