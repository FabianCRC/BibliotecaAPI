using AutoMapper;
using BibliotecaAPI.Interfaces;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    public class ExtendedBaseController<TCreation, TEntity, TDTO> : ControllerBase
        where TEntity : class, IHaveId
    {
        private readonly ApplicationDBContext applicationDBContext;
        private readonly IMapper mapper;
        private readonly string controllerName;

        public ExtendedBaseController(ApplicationDBContext applicationDBContext,
            IMapper mapper,string controllerName)
        {
            this.applicationDBContext=applicationDBContext;
            this.mapper=mapper;
            this.controllerName=controllerName;
        }
        [HttpGet]
        public virtual async Task<ActionResult<List<TDTO>>> Get()
        {
            var categorias = await applicationDBContext.Set<TEntity>().ToListAsync();
            return mapper.Map<List<TDTO>>(categorias);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDTO>> Get(int id)
        {
            var entidad = await applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id==id);
            if (entidad==null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TDTO>(entidad));
        }
        [HttpPost]
        public virtual async Task<ActionResult<TDTO>> Post(TCreation tCreation)
        {
            var entidad = mapper.Map<TEntity>(tCreation);

            applicationDBContext.Set<TEntity>().Add(entidad);
            await applicationDBContext.SaveChangesAsync();

            var dto = mapper.Map<TDTO>(entidad);
            return new CreatedAtActionResult(nameof(Get),controllerName, new { id = entidad.Id }, dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, TCreation tCreation)
        {
            var entidad = await applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id==id);
            if (entidad==null)
            {
                return NotFound();
            }
            mapper.Map(tCreation, entidad);
            applicationDBContext.Entry(entidad).State=EntityState.Modified;
            await applicationDBContext.SaveChangesAsync();


            return NoContent();
        }
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var entidad = await applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id==id);
            if (entidad==null)
            {
                return NotFound();
            }
            applicationDBContext.Entry(entidad).State=EntityState.Deleted;
            await applicationDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
