using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Director : BaseDomainModel
    {        
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
