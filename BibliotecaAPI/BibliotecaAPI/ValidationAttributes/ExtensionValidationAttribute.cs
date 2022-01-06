using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class ExtensionValidationAttribute:ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public ExtensionValidationAttribute(string[] tiposValidos)
        {
            this.tiposValidos=tiposValidos;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formFile= value as FormFile;
            if(formFile != null)
            {
                if (!tiposValidos.Contains(formFile.ContentType))
                {
                    return new ValidationResult($"Los tipos validos son:{string.Join(",", tiposValidos)}");
                }
            }
            return ValidationResult.Success;
        }

    }
}
