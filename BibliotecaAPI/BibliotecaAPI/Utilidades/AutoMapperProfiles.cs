using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Mapping Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaCreationDTO>();

            //Mapping Editorial
            CreateMap<Editorial, EditorialDTO>().ReverseMap();
            CreateMap<Editorial, EditorialCreationDTO>();

            //Mapping Autor
            CreateMap<Autor, AutorDTO>().ReverseMap();
            CreateMap<Autor, AutorCreationDTO>().ForMember(x=>x.Foto,options=>options.Ignore());

        }
    }
}
