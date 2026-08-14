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

    public IActionResult Create()
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Contacto contacto)
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(contacto);
        }

        try
        {
            _context.Add(contacto);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Contacto {contacto.Nombre} {contacto.Apellidos} creado exitosamente.");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al crear contacto {contacto.Nombre}.");
            ModelState.AddModelError("", "Error al guardar el contacto. Por favor, intente nuevamente.");
            return View(contacto);
        }
    }
}
