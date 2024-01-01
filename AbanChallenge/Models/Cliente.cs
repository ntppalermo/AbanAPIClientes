using System.ComponentModel.DataAnnotations;

namespace AbanChallenge.Models
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Cliente
    {
        [Key]
        [Required(ErrorMessage = "El campo 'Id' es obligatorio y no puede ser nulo o cero")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo 'Nombres' es obligatorio")]
        public required string Nombres { get; set; }

        [Required(ErrorMessage = "El campo 'Apellidos' es obligatorio")]
        public required string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? CUIT { get; set; }
        public string? Domicilio { get; set; }
        public string? Celular { get; set; }

        [EmailAddress(ErrorMessage = "El campo 'Email' no tiene un formato de dirección de correo electrónico válido.")]
        public string? Email { get; set; }



    }
}
