using Microsoft.AspNetCore.Mvc;
using LoginFrontend.Data;
using LoginFrontend.Models;

namespace LoginFrontend.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Account/Activated
    public IActionResult Activated(string nombre = "July")
    {
        ViewBag.Nombre = nombre;
        return View();
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login(bool expired = false)
    {
        if (expired)
        {
            ViewBag.SessionExpired = true;
        }
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public IActionResult Login(string tipoDocumento, string numeroDocumento, string password)
    {
        if (string.IsNullOrWhiteSpace(numeroDocumento) || string.IsNullOrWhiteSpace(password))
        {
            ViewBag.Error = "Por favor, complete todos los campos.";
            ViewBag.TipoDocumento = tipoDocumento;
            return View();
        }

        var usuario = _context.Usuarios
            .FirstOrDefault(u => u.TipoDocumento == tipoDocumento && u.NumeroDocumento == numeroDocumento);

        // User does not exist
        if (usuario == null)
        {
            ViewBag.Error = "El usuario ingresado no existe.";
            ViewBag.TipoDocumento = tipoDocumento;
            ViewBag.NumeroDocumento = numeroDocumento;
            return View();
        }

        // Check if account is blocked
        if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta.Value > DateTime.Now)
        {
            return RedirectToAction("Blocked");
        }

        // If block has expired, reset
        if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta.Value <= DateTime.Now)
        {
            usuario.BloqueadoHasta = null;
            usuario.IntentosLoginFallidos = 0;
            _context.SaveChanges();
        }

        // Validate password
        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);

        if (!passwordValid)
        {
            usuario.IntentosLoginFallidos++;

            if (usuario.IntentosLoginFallidos >= 5)
            {
                // Block account for 15 minutes
                usuario.BloqueadoHasta = DateTime.Now.AddMinutes(15);
                _context.SaveChanges();
                return RedirectToAction("Blocked");
            }

            _context.SaveChanges();

            int intentosRestantes = 5 - usuario.IntentosLoginFallidos;
            ViewBag.Error = $"Contraseña incorrecta. Le quedan {intentosRestantes} intento(s).";
            ViewBag.TipoDocumento = tipoDocumento;
            ViewBag.NumeroDocumento = numeroDocumento;
            return View();
        }

        // Successful login - reset failed attempts
        usuario.IntentosLoginFallidos = 0;
        usuario.BloqueadoHasta = null;
        _context.SaveChanges();

        // Create session
        HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
        HttpContext.Session.SetString("UsuarioNombre", $"{usuario.PrimerApellido} {usuario.SegundoApellido}, {usuario.Nombres}");
        HttpContext.Session.SetString("UsuarioCargo", usuario.Cargo);
        HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString("o"));

        return RedirectToAction("Profile", "Users");
    }

    // GET: /Account/Blocked
    public IActionResult Blocked()
    {
        return View();
    }

    // POST: /Account/Logout
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
