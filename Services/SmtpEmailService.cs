using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LoginFrontend.Services
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAccountBlockedEmailAsync(string toEmail, string userName, DateTime blockExpirationTime)
        {
            // Intentaremos usar tu correo de Gmail
            // Si el correo que usaste para generar la clave fue otro, 
            // simplemente cámbialo aquí.
            var senderEmail = _configuration["Smtp:Email"] ?? throw new InvalidOperationException("Falta configurar SMTP:Email en appsettings o User Secrets."); 
            var senderPassword = _configuration["Smtp:Password"] ?? throw new InvalidOperationException("Falta configurar SMTP:Password en appsettings o User Secrets."); 
            
            var incidentTime = DateTime.UtcNow.ToString("dd/MM/yyyy | HH:mm UTC");

            var htmlBody = $@"<!DOCTYPE html>
<html lang=""es"">
<head>
    <meta charset=""utf-8""/>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
    <title>Notificación de Seguridad</title>
    <style>
        body {{ margin: 0; padding: 0; background-color: #f3f4f5; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #191c1d; }}
        .wrapper {{ width: 100%; table-layout: fixed; background-color: #f3f4f5; padding-bottom: 40px; }}
        .main {{ margin: 0 auto; width: 100%; max-width: 600px; background-color: #ffffff; border-radius: 8px; box-shadow: 0 4px 10px rgba(0,0,0,0.05); overflow: hidden; margin-top: 40px; }}
        .header {{ background-color: #0e1c2b; padding: 40px 20px; text-align: center; }}
        .header h1 {{ color: #ffffff; margin: 0; font-size: 24px; font-weight: bold; }}
        .header p {{ color: #bac8dc; margin: 5px 0 0 0; font-size: 13px; text-transform: uppercase; letter-spacing: 1px; }}
        .content {{ padding: 30px; text-align: center; }}
        .content h2 {{ color: #0e1c2b; font-size: 20px; margin-top: 0; }}
        .content p {{ color: #454652; line-height: 1.6; font-size: 15px; text-align: justify; }}
        .details-box {{ background-color: #f8f9fa; border: 1px solid #e1e3e4; border-radius: 6px; padding: 20px; margin: 25px 0; text-align: left; }}
        .details-row {{ padding: 8px 0; border-bottom: 1px solid #e1e3e4; }}
        .details-row:last-child {{ border-bottom: none; }}
        .detail-label {{ color: #767683; font-size: 11px; text-transform: uppercase; font-weight: bold; margin: 0 0 4px 0; }}
        .detail-value {{ color: #0e1c2b; font-size: 15px; margin: 0; font-weight: bold; }}
        .detail-value.error {{ color: #ba1a1a; }}
        .btn {{ display: inline-block; background-color: #0e1c2b; color: #ffffff; text-decoration: none; padding: 12px 24px; border-radius: 6px; font-weight: bold; font-size: 14px; margin-top: 10px; }}
        .footer {{ background-color: #f3f4f5; border-top: 1px solid #e1e3e4; padding: 20px; text-align: center; }}
        .footer p {{ margin: 0 0 10px 0; color: #767683; font-size: 12px; }}
    </style>
</head>
<body>
    <div class=""wrapper"">
        <center>
            <div class=""main"">
                <div class=""header"">
                    <h1>Vanguardia Institucional</h1>
                    <p>Alerta Prioritaria de Seguridad</p>
                </div>
                <div class=""content"">
                    <h2>Acceso a la Cuenta Bloqueado</h2>
                    <p>Estimado(a) <strong>{userName}</strong>, su cuenta ha sido bloqueada temporalmente debido a múltiples intentos fallidos de inicio de sesión. Para mantener la integridad de nuestros activos institucionales, nuestros protocolos automatizados han restringido el acceso a sus credenciales hasta las <strong>{blockExpirationTime:HH:mm}</strong>.</p>
                    
                    <div class=""details-box"">
                        <div class=""details-row"">
                            <p class=""detail-label"">Hora del Incidente</p>
                            <p class=""detail-value"">{incidentTime}</p>
                        </div>
                        <div class=""details-row"">
                            <p class=""detail-label"">Tipo de Incidente</p>
                            <p class=""detail-value error"">Prevención de Fuerza Bruta</p>
                        </div>
                        <div class=""details-row"">
                            <p class=""detail-label"">Motivo del Bloqueo</p>
                            <p class=""detail-value"">Se excedió el límite máximo de 5 intentos fallidos de autenticación desde una IP no reconocida.</p>
                        </div>
                    </div>
                    
                    <a href=""#"" class=""btn"" style=""color: #ffffff;"">Contactar a Soporte</a>
                    
                    <div style=""margin-top: 30px; padding: 20px; background-color: #ffdea533; border-radius: 6px; text-align: left;"">
                        <h4 style=""color: #5d4201; margin: 0 0 5px 0; font-size: 15px;"">Mejore su Seguridad</h4>
                        <p style=""color: #5d4201; margin: 0; font-size: 13px;"">Le recomendamos activar la Autenticación Multifactor (MFA) para añadir una capa adicional de protección institucional a su cuenta.</p>
                    </div>
                </div>
                <div class=""footer"">
                    <p><strong>Vanguardia Institucional</strong></p>
                    <p>Esta es una notificación de seguridad automatizada. Por favor, no responda directamente a este correo.</p>
                    <p style=""font-size: 10px; text-transform: uppercase;"">© 2026 Comunicaciones Seguras. Protocolo 4.2</p>
                </div>
            </div>
        </center>
    </div>
</body>
</html>";

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, "Vanguardia Institucional IT"),
                Subject = "Alerta de Seguridad - Cuenta Bloqueada",
                Body = htmlBody,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword.Replace(" ", "")),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
