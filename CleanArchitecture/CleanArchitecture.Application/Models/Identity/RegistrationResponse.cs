namespace CleanArchitecture.Application.Models.Identity
{
    /// <summary>
    /// Modelo de respuesta de registro de usuarios.
    /// </summary>
    public class RegistrationResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
