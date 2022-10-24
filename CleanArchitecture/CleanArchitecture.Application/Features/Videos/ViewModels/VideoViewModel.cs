namespace CleanArchitecture.Application.Features.Videos.ViewModels
{
    public class VideoViewModel
    {
        public string Name { get; set; } = string.Empty;       

        /// <summary>
        /// Clave foranea
        /// </summary>
        public int StreamerId { get; set; }
    }
}
