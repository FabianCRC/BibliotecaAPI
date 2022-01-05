﻿using BibliotecaAPI.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BibliotecaAPI.Utilidades
{
    public class AlmacenadorDeArchivosEnLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorDeArchivosEnLocal(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment=webHostEnvironment;
            this.httpContextAccessor=httpContextAccessor;
        }

        public Task Borrar(string ruta, string container)
        {
            string wwwrootPath = webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }
            var nombreArchivo = Path.GetFileName(ruta);
            string pathFinal = Path.Combine(wwwrootPath,container,nombreArchivo);

            if (File.Exists(pathFinal))
            {
                File.Delete(pathFinal);
            }
            return Task.CompletedTask;
        }

        public async Task<string> Crear(byte[] file, string contentType, string extension, string container, string nombre)
        {
            string wwwrootPath = webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }

            string carpetaArchivo = Path.Combine(wwwrootPath, container);

            if (!Directory.Exists(carpetaArchivo))
            {
                Directory.CreateDirectory(carpetaArchivo);
            }

            string nombreFinal = $"{nombre}{extension}";
            string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);

            await File.WriteAllBytesAsync(rutaFinal, file);

            string urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");
            return dbUrl;

        }

     
    }
}
