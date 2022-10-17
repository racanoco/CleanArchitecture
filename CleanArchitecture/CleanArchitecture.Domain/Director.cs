using CleanArchitecture.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {        
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int VideoId { get; set; }

        public virtual Video? Video { get; set; }
    }
}
