namespace CleanArchitecture.Application.Models.Identity
{
    /// <summary>
    /// Modelo para solicitud de autenticación de usuarios.
    /// </summary>
    public class AuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
