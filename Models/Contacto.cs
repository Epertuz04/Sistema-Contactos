using System.ComponentModel.DataAnnotations;

namespace Sistema_Contactos.Models;

public class Contacto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "La cédula debe tener entre 5 y 20 caracteres.")]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 150 caracteres.")]
    [Display(Name = "Apellidos")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de nacimiento")]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no es válido.")]
    [Display(Name = "Teléfono")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(250, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 250 caracteres.")]
    [Display(Name = "Dirección")]
    public string Direccion { get; set; } = string.Empty;

    [Display(Name = "Edad")]
    public int Edad
    {
        get
        {
            if (FechaNacimiento == default)
            {
                return 0;
            }

            var hoy = DateTime.Today;
            var edad = hoy.Year - FechaNacimiento.Year;

            if (hoy < FechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }
    }
}
