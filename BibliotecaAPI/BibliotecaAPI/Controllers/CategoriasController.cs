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
    public class CategoriasController : Controller
    {
        private readonly ApplicationDBContext applicationDBContext;
        private readonly IMapper mapper;

        public CategoriasController(ApplicationDBContext applicationDBContext,
            IMapper mapper)
        {
            this.applicationDBContext=applicationDBContext;
            this.mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var categorias = await applicationDBContext.Categoria.ToListAsync();
            return mapper.Map<List<CategoriaDTO>>(categorias);
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await applicationDBContext.Categoria.FirstOrDefaultAsync(c => c.Id==id);
            if (categoria==null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CategoriaDTO>(categoria));
        }
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaCreationDTO categoriaCreationDTO)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreationDTO);

            applicationDBContext.Categoria.Add(categoria);
            await applicationDBContext.SaveChangesAsync();

            var dto = mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CategoriaCreationDTO categoriaCreationDTO)
        {
            var categoria = await applicationDBContext.Categoria.FirstOrDefaultAsync(c => c.Id==id);
            if (categoria==null)
            {
                return NotFound();
            }
            mapper.Map(categoriaCreationDTO, categoria);
            applicationDBContext.Entry(categoria).State=EntityState.Modified;
            await applicationDBContext.SaveChangesAsync();


            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await applicationDBContext.Categoria.FirstOrDefaultAsync(c => c.Id==id);
            if (categoria==null)
            {
                return NotFound();
            }
            applicationDBContext.Entry(categoria).State=EntityState.Deleted;
            await applicationDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
