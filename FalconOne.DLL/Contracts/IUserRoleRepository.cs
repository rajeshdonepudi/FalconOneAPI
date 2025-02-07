using FalconOne.Models.Dtos.Security.Roles;
using FalconOne.Models.Entities.Users;
using FalconOne.Models.EntityContracts;

namespace FalconOne.DAL.Contracts
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<bool> AddUsersToRoleAsync(Guid tenantId, AddUserToRoleDto model, CancellationToken cancellationToken);
    }
}
