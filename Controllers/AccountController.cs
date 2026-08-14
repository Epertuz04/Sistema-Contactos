using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Contactos.Data;
using Sistema_Contactos.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sistema_Contactos.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AccountController> _logger;

    public AccountController(AppDbContext context, ILogger<AccountController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == model.Usuario && u.Activo);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }

            var contraseñaHash = HashPassword(model.Password);
            if (!VerifyPassword(model.Password, usuario.Contraseña))
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }

            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("NombreUsuario", usuario.NombreUsuario);
            HttpContext.Session.SetString("NombreCompleto", usuario.NombreCompleto ?? usuario.NombreUsuario);

            _logger.LogInformation($"Usuario {usuario.NombreUsuario} inició sesión exitosamente.");
            return RedirectToAction("Index", "Contactos");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al intentar iniciar sesión.");
            ModelState.AddModelError("", "Error al procesar la solicitud.");
            return View(model);
        }
    }

    public IActionResult Logout()
    {
        var usuarioId = HttpContext.Session.GetString("UsuarioId");
        if (!string.IsNullOrEmpty(usuarioId))
        {
            _logger.LogInformation($"Usuario {HttpContext.Session.GetString("NombreUsuario")} cerró sesión.");
        }

        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
