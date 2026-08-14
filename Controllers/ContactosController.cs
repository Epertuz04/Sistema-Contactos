using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_Contactos.Data;
using Sistema_Contactos.Models;

namespace Sistema_Contactos.Controllers;

public class ContactosController : Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<ContactosController> _logger;

    public ContactosController(AppDbContext context, ILogger<ContactosController> logger)
    {
        _context = context;
        _logger = logger;
    }

    private bool IsAuthenticated()
    {
        var usuarioId = HttpContext.Session.GetString("UsuarioId");
        return !string.IsNullOrEmpty(usuarioId);
    }

    public async Task<IActionResult> Index()
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var contactos = await _context.Contactos.OrderByDescending(c => c.Id).ToListAsync();
            return View(contactos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la lista de contactos.");
            return View(new List<Contacto>());
        }
    }
}
