using Microsoft.AspNetCore.Mvc;
using LoginFrontend.Data;

namespace LoginFrontend.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Users/Profile
    public IActionResult Profile()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null)
        {
            return RedirectToAction("Login", "Account", new { expired = true });
        }

        var usuario = _context.Usuarios.Find(userId);
        if (usuario == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View(usuario);
    }

    // POST: /Users/ExtendSession (AJAX)
    [HttpPost]
    public IActionResult ExtendSession()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioId");
        if (userId == null)
        {
            return Json(new { success = false, message = "Sesión no encontrada" });
        }

        // Refresh the session timestamp
        HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString("o"));
        return Json(new { success = true });
    }
}
