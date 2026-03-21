# Sistema de Autenticacion y Gestion de Usuarios

Este proyecto es una aplicacion web desarrollada en ASP.NET Core 8 MVC con el objetivo de funcionar como una prueba de concepto para un flujo de autenticacion de usuarios con estilos de interfaz basados en lineamientos gubernamentales (Peru).

## Alcance del Proyecto

El sistema abarca el ciclo completo y administracion de una sesion basica para un usuario, incluyendo las siguientes vistas y funcionalidades:

1. **Activacion de Cuenta:** Vista de bienvenida tras crear una cuenta, con redireccion al inicio de sesion.
2. **Login Validado:** Manejo de autenticacion mediante el Documento de Identidad y Contrasena. La aplicacion cuenta con un limite de hasta 4 intentos fallidos. Al quinto error, el sistema bloquea por 15 minutos (a continuacion se redirige a pantalla de bloqueo). Las contrasenas se validan utilizando encriptacion BCrypt.
3. **Cuenta Bloqueada:** Pantalla de advertencia en caso de sobrepasar intentos de ingreso.
4. **Perfil (Gestion de Usuarios):** Tablero basico (dashboard) que muestra datos simulados del empleado y controla un contador vital del tiempo de expiracion. Antes del cierre total de sesion, aparece un modal temporizado en Javascript que notifica la inminente desconexion, el cual permite tambien extender la validez de la token local.

## Stack Tecnologico

- **Backend:** ASP.NET Core 8 MVC
- **Lenguaje:** C#
- **Frontend:** HTML5, CSS (Bootstrap 5) y Javascript (para logica AJAX de extension de token temporizada).
- **Base de Datos:** SQLite (Entity Framework Core) para un despliegue local eficiente sin necesidad de dependencias externas.

## Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.

## Ejecucion del Proyecto (Local)

Para iniciar el proyecto de forma local, ejecuta el siguiente comando en la raiz de la descarga:

```bash
dotnet restore
dotnet run
```

Una vez que el servidor se encuentre ejecutando, puedes ir en el navegador a: `http://localhost:5000`

## Credenciales de Prueba

Al crear la aplicacion se alimenta la base de datos por primera vez con dos usuarios semilla de pruebas. Puedes utilizar al usuario de pruebas predeterminado para ingresar a "Gestion de Usuarios":

- **DNI:** `07079879`
- **Contrasena:** `Admin123!`
