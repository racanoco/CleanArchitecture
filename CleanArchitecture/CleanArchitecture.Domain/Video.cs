using CleanArchitecture.Common;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public Video()
        {
            Actors = new HashSet<Actor>();
        }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Clave foranea
        /// </summary>
        public int StreamerId { get; set; }

        public virtual Streamer? Streamer { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual Director Director { get; set; }
    }
}
