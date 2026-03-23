# Sistema de Autenticación y Gestión de Usuarios (Vanguardia Institucional)

Este proyecto es una aplicación web robusta desarrollada en **ASP.NET Core 8 MVC**. Funciona como una prueba de concepto avanzada para un flujo de autenticación de usuarios de grado empresarial (Identity Management) con estándares de diseño UI/UX gubernamentales modernos basados en Glassmorphism y CSS Variables.

---

## 🎯 Alcance del Proyecto

El sistema abarca el ciclo de vida completo de seguridad, sesión y administración simulada de un usuario:

1. **Login Validado:** Manejo de autenticación robusta mediante DNI y Contraseña validada con encriptación militar **BCrypt**.
2. **Mitigación de "Fuerza Bruta" (Bloqueo y Correo):** El sistema rastrea intentos de inicio erróneos en la base de datos SQL. Al 5to intento fallido consecutivo:
   - Bloquea geográficamente el acceso al usuario por 15 minutos exactos.
   - Dispara automáticamente una **Notificación de Seguridad** por correo electrónico (SMTP asíncrono) perfectamente maquetada en HTML con hora del incidente y motivo.
3. **Session Management Temporal:** Tablero de sesión que controla un temporizador del estado inactivo. Antes del cierre total (20 min), activa un script Vanilla JavaScript que despliega un Modal de advertencia, brindando al usuario la oportunidad de extender la validez de su *Token* local por AJAX sin recargar la página.
4. **Editor de Perfil "En Línea":** Integración asíncrona (`fetch` API) para actualizar dinámicamente el correo electrónico del usuario interactuando con la base de datos sin un solo parpadeo en pantalla.

---

## 🛠️ Stack Tecnológico Actualizado

- **Backend:** C# (.NET 8.0) y ASP.NET Core MVC (Server-Side Rendering).
- **Capa de Datos:** Entity Framework Core 8.0 (ORM) con **SQL Server** (Soporta clústeres locales y despliegue remoto Cloud).
- **Criptografía:** `BCrypt.Net-Next` para salting y hashing seguro asimétrico.
- **Frontend / Diseño UI:** Arquitectura "Pixel-Perfect" usando HTML5 Semántico, CSS3 moderno (Variables Design Tokens en el `:root`), Flexbox, CSS Grid y utilidades de Bootstrap 5 reescritas. Diseños ultra-responsivos (Offcanvas Sidebar, Media Queries).
- **Notificaciones (SMTP):** Implementación nativa con `System.Net.Mail.SmtpClient` inyectada mediante Patrón de Inyección de Dependencias (`IEmailService`) para entrega a nivel mundial (Gmail/Outlook).

---

## 🚀 Requisitos Previos (Para Evaluadores)

Para ejecutar este proyecto en tu entorno de desarrollo local, asegúrate de contar con:
1. [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Servidor **SQL Server** (LocalDB, Express o Enterprise) corriendo localmente o credenciales para uno remoto.
3. Una cuenta de correo Gmail (con *Contraseña de Aplicación* si planeas modificar el remitente de los correos automáticos).

---

## 💻 Ejecución y Migración Inicial (Local)

Al haber migrado de SQLite a SQL Server, necesitas inicializar la conexión y volcar la primera migración de tablas al SQL antes de poder entrar a la aplicación.

### Paso 1: Configurar la Conexión de Base de Datos
Abre el archivo `appsettings.json` en la raíz del proyecto. Y configura adecuadamente tu `DefaultConnection` apuntando a tu instancia de SQL Server local. Por ejemplo:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LoginFrontendDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### Paso 2: Volcar la Base de Datos (Entity Framework)
Abre una terminal (`cmd` o `powershell`) en la carpeta donde clonaste el repositorio y ejecuta:
```bash
dotnet restore
dotnet tool install --global dotnet-ef   # Solo si no tienes las EF Tools
dotnet ef database update
```
*Este comando leerá las migraciones existentes y creará las tablas junto a sus usuarios 'Semillas' (Datos pre-cargados de prueba).*

### Paso 3: Arrancar el Servidor
```bash
dotnet run
```
Abre tu navegador (preferiblemente Chrome/Edge) y ve a: `http://localhost:5000` (o el puerto listado en tu consola, usualmente `5032`).

---

## 🔑 Credenciales Predeterminadas (Testing Base de Datos)
La ejecución anterior sembrará la base de datos automáticamente (gracias al `DbInitializer`).
Utiliza esta cuenta principal para evaluar todo el Módulo PEI y el Editor de Correo:

- **DNI:** `07079879`
- **Contraseña:** `Admin123!`

> **Tip de Evaluación (Bloqueo Total):** Para visualizar la pantalla estricta de *Cuenta Bloqueada* junto con el envío funcional del Correo de Alerta de Seguridad SMTP; intenta loguearte a propósito introduciendo cualquier contraseña incorrecta repetidas veces hasta llegar a 0 intentos. Las alertas dinámicas del formulario en pantalla te notificarán el proceso.
