namespace LoginFrontend.Models;

public class Usuario
{
    public int Id { get; set; }
    public string TipoDocumento { get; set; } = "DNI"; // DNI o CE
    public string NumeroDocumento { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string PrimerApellido { get; set; } = string.Empty;
    public string SegundoApellido { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? EmailSecundario { get; set; }
    public string? TelefonoMovil { get; set; }
    public string? TelefonoSecundarioTipo { get; set; }
    public string? TelefonoSecundario { get; set; }
    public string? FechaNacimiento { get; set; }
    public string Nacionalidad { get; set; } = "Peruana";
    public string Sexo { get; set; } = "Masculino";
    public string? TipoContratacion { get; set; }
    public string? FechaContratacion { get; set; }
    public string Cargo { get; set; } = string.Empty;
    public string Entidad { get; set; } = string.Empty;
    public string? FotoUrl { get; set; }
    public bool Activo { get; set; } = true;
    public int IntentosLoginFallidos { get; set; } = 0;
    public DateTime? BloqueadoHasta { get; set; }
}
