namespace CleanArchitecture.Application.Models.Identity
{
    /// <summary>
    /// Modelo de Solicitud de registro de usuarios.
    /// </summary>
    public class RegistrationRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
