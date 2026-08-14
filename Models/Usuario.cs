using System.ComponentModel.DataAnnotations;

namespace Sistema_Contactos.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Usuario")]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Contraseña { get; set; } = string.Empty;

    [Display(Name = "Correo electrónico")]
    [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
    public string? Email { get; set; }

    [Display(Name = "Nombre completo")]
    [StringLength(150)]
    public string? NombreCompleto { get; set; }

    [Display(Name = "Activo")]
    public bool Activo { get; set; } = true;

    [Display(Name = "Fecha de creación")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
