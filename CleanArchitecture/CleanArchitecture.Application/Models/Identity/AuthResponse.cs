namespace CleanArchitecture.Application.Models.Identity
{
    /// <summary>
    /// Modelo de respuesta de autorización de usuarios.
    /// </summary>
    public class AuthResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
