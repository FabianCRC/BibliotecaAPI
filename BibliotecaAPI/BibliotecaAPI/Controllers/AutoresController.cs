using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Interfaces;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ExtendedBaseController<AutorCreationDTO, Autor, AutorDTO>
    {
        private readonly ApplicationDBContext applicationDBContext;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public AutoresController(ApplicationDBContext applicationDBContext,
            IMapper mapper, IAlmacenadorArchivos almacenadorArchivos) : base(applicationDBContext, mapper, "Autores")
        {
            this.applicationDBContext=applicationDBContext;
            this.mapper=mapper;
            this.almacenadorArchivos=almacenadorArchivos;
        }

        public override async Task<ActionResult<AutorDTO>> Post([FromForm] AutorCreationDTO tCreation)
        {
            var entity = mapper.Map<Autor>(tCreation);
            if (tCreation.Foto != null)
            {
                string fotoUrl = await GuardarFoto(tCreation.Foto);
                entity.Foto=fotoUrl;
            }
            applicationDBContext.Autores.Add(entity);
            await applicationDBContext.SaveChangesAsync();

            var dto= mapper.Map<AutorDTO>(entity);

            return new CreatedAtActionResult(nameof(Get),"Autores",new {id=entity.Id},dto);
        }

        public override async Task<ActionResult> Put(int id, [FromForm] AutorCreationDTO tCreation)
        {
            var entity = await applicationDBContext.Autores.FirstOrDefaultAsync(a => a.Id==id);
            if (entity == null)
            {
                return NotFound();
            }
            mapper.Map(tCreation, entity);
            if (tCreation.Foto!=null)
            {
                if (!string.IsNullOrEmpty(entity.Foto))
                {
                    await almacenadorArchivos.Borrar(entity.Foto, ConstantesDeAplicacion.ContenedorDeAutores);
                }
                string fotoUrl = await GuardarFoto(tCreation.Foto);
                entity.Foto=fotoUrl;
            }
            applicationDBContext.Entry(entity).State=EntityState.Modified;
            await applicationDBContext.SaveChangesAsync();
            return NoContent();
        }

        private async Task<string> GuardarFoto(IFormFile foto)
        {
            using var stream = new MemoryStream();

            await foto.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            return await almacenadorArchivos.Crear(fileBytes,
                foto.ContentType, Path.GetExtension(foto.FileName), ConstantesDeAplicacion.ContenedorDeAutores, Guid.NewGuid().ToString());
        } 
    }
}
