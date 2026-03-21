using LoginFrontend.Models;

namespace LoginFrontend.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Usuarios.Any())
            return;

        var usuarios = new Usuario[]
        {
            new()
            {
                TipoDocumento = "DNI",
                NumeroDocumento = "07079879",
                Nombres = "July Camila",
                PrimerApellido = "Mendoza",
                SegundoApellido = "Quispe",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Email = "test@minsa.gob.pe",
                TelefonoMovil = "+51 999 999 999",
                FechaNacimiento = "15/04/1944",
                Nacionalidad = "Peruana",
                Sexo = "Femenino",
                TipoContratacion = "CAS",
                FechaContratacion = "09/03/2015",
                Cargo = "Administrador de Recursos",
                Entidad = "011 Ministerio de Salud",
                FotoUrl = "/img/avatar-placeholder.png",
                Activo = true
            },
            new()
            {
                TipoDocumento = "DNI",
                NumeroDocumento = "12345678",
                Nombres = "Adriana",
                PrimerApellido = "Osorio",
                SegundoApellido = "Montes",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                Email = "adriana.osorio@ceplan.gob.pe",
                TelefonoMovil = "+51 988 888 888",
                FechaNacimiento = "22/08/1985",
                Nacionalidad = "Peruana",
                Sexo = "Femenino",
                TipoContratacion = "CAS",
                FechaContratacion = "15/06/2018",
                Cargo = "Operador",
                Entidad = "011 Ministerio de Salud",
                FotoUrl = "/img/avatar-placeholder.png",
                Activo = true
            }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();
    }
}
