using Sistema_Contactos.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sistema_Contactos.Data;

public static class AppDbContextSeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Usuarios.Any())
        {
            return;
        }

        var usuarios = new List<Usuario>
        {
            new Usuario
            {
                NombreUsuario = "admin",
                Contraseña = HashPassword("admin123"),
                NombreCompleto = "Administrador",
                Email = "admin@sistemantactos.com",
                Activo = true,
                FechaCreacion = DateTime.Now
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();
    }

    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
