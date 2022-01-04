using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ExtendedBaseController<CategoriaCreationDTO,Categoria,CategoriaDTO>
    {
 

        public CategoriasController(ApplicationDBContext applicationDBContext,
            IMapper mapper):base(applicationDBContext,mapper,"Categorias")
        {
        
        }

    }
}
