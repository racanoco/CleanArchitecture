namespace CleanArchitecture.Application.Models.Identity
{
    /// <summary>
    /// Modelo que representa las propiedades/settings del Token.
    /// </summary>
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double DurationInMinutes { get; set; } = 0;
    }
}
