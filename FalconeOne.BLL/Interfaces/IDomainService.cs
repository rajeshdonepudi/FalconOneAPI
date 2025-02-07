using FalconOne.Models.Dtos.Domain;

namespace FalconeOne.BLL.Interfaces
{
    public interface IDomainService
    {
        Task<DomainWhoisDto?> GetDomainInfoAsync(string domainName);
    }
}