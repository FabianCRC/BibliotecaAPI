using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class SizeValidationAttribute : ValidationAttribute
    {
        private readonly double pesoArchivoKb;

        public SizeValidationAttribute(double pesoArchivoKb)
        {
            this.pesoArchivoKb=pesoArchivoKb;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formFile = value as FormFile;
            if (formFile != null)
            {
                if (formFile.Length/1024>pesoArchivoKb)
                {
                    return new ValidationResult($"El peso maximo del archivo es:{pesoArchivoKb} sin embargo el enviado es de {formFile.Length/1024}");
                }
            }
            return ValidationResult.Success;
        }

    }
}
