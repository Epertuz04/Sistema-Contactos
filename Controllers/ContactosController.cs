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

    public async Task<IActionResult> Edit(int? id)
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (id == null)
        {
            return NotFound();
        }

        var contacto = await _context.Contactos.FindAsync(id);
        if (contacto == null)
        {
            return NotFound();
        }

        return View(contacto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Contacto contacto)
    {
        if (!IsAuthenticated())
        {
            return RedirectToAction("Login", "Account");
        }

        if (id != contacto.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(contacto);
        }

        try
        {
            _context.Update(contacto);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Contacto {contacto.Nombre} {contacto.Apellidos} actualizado exitosamente.");
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!ContactoExists(contacto.Id))
            {
                return NotFound();
            }
            _logger.LogError(ex, "Error de concurrencia al actualizar contacto.");
            ModelState.AddModelError("", "Error al actualizar el contacto. Intente nuevamente.");
            return View(contacto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al actualizar contacto {contacto.Nombre}.");
            ModelState.AddModelError("", "Error al guardar los cambios. Por favor, intente nuevamente.");
            return View(contacto);
        }
    }

    private bool ContactoExists(int id)
    {
        return _context.Contactos.Any(e => e.Id == id);
    }
}
