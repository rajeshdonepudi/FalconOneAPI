using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Models.DTOs.Users;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class UserLookupService : BaseService, IUserLookupService
    {
        public UserLookupService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantService, IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantService, identiconProvider)
        {

        }

        public async Task<IEnumerable<UserLookupDto>> GetAllUsersLookup(string searchTerm, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UserRepository.GetAllTenantUsersLookupAsync(TenantId, searchTerm, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<UserLookupDto>> GetAllUsersLookupByTenantId(Guid tenantId, string searchTerm, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UserRepository.GetAllTenantUsersLookupAsync(tenantId, searchTerm, cancellationToken);

            return result;
        }
    }
}
