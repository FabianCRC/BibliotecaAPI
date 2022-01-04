using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/editoriales")]
    public class EditorialesController : ExtendedBaseController<EditorialCreationDTO,Editorial,EditorialDTO>
    {
        
        public EditorialesController(ApplicationDBContext applicationDBContext,
            IMapper mapper):base(applicationDBContext,mapper,"Editoriales")
        {
        }
    }
}
