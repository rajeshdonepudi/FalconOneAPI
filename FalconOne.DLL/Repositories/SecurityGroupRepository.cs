using FalconOne.DAL.Contracts;
using FalconOne.Extensions.EntityFramework;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Security.Permissions;
using FalconOne.Models.Dtos.Security.SecurityGroups;
using FalconOne.Models.DTOs.Users;
using FalconOne.Models.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FalconOne.DAL.Repositories
{
    public class SecurityGroupRepository : GenericRepository<SecurityGroup>, ISecurityGroupRepository
    {
        public SecurityGroupRepository(FalconOneContext context, IMemoryCache cache) : base(context, cache)
        {
        }
        public async Task<PagedList<UserInfoDto>> GetAllUsersBySecurityGroupPaginatedAsync(Guid securityGroupId, PageParams pageParams, CancellationToken cancellationToken)
        {
            var result = await _context.UserSecurityGroups
                                       .Where(x => x.SecurityGroupId == securityGroupId)
                                       .Select(u => u.TenantUser.User)
                                       .Select(u => new UserInfoDto
                                       {
                                           Avatar = u.ProfilePicture.Base64,
                                           Email = u.Email,
                                           LastName = u.LastName,
                                           FirstName = u.FirstName,
                                           FullName = u.FirstName + " " + u.LastName,
                                           ResourceAlias = u.ResourceAlias,
                                           Phone = u.PhoneNumber,
                                           IsActive = u.IsActive,
                                           EmailConfirmed = u.EmailConfirmed,
                                           PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                                           LockoutEnabled = u.LockoutEnabled,
                                           TwoFactorEnabled = u.TwoFactorEnabled,
                                           IsLocked = u.LockoutEnd.HasValue,
                                           LockoutEnd = u.LockoutEnd.HasValue ? u.LockoutEnd.Value.DateTime : null,
                                           CreatedOn = u.CreatedOn

                                       }).ToPagedListAsync(pageParams, cancellationToken);

            return result;
        }
        public async Task<bool> AddSecurityGroup(CreateSecurityGroupDto model, Guid tenantId, CancellationToken cancellationToken)
        {
            var isExist = await _context.SecurityGroups.AnyAsync(x => x.TenantId == tenantId && x.Name.ToLower() == model.Name.ToLower());

            if (!isExist)
            {
                await _context.SecurityGroups.AddAsync(new SecurityGroup
                {
                    TenantId = tenantId,
                    Name = model.Name,
                    CreatedOn = DateTime.UtcNow,
                },cancellationToken);

                var result = await _context.SaveChangesAsync(cancellationToken);

                return result > 0;
            }

            return false;
        }

        public async Task<bool> DeleteSecurityGroup(Guid securityGroupId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<SecurityGroupsListDto>> GetTenantSecurityGroupsAsync(FilterSecurityGroupsDto model, Guid tenantId, CancellationToken cancellationToken)
        {
            var query = _context.SecurityGroups.Where(x => x.TenantId == tenantId);

            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(x => x.Name.Contains(model.SearchTerm));
            }

            var res = await query.Select(x => new SecurityGroupsListDto
            {
                Id = x.Id,
                Name = x.Name,
                UsersInGroup = x.TenantUserSecurityGroups.Count(),
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
            }).OrderByDescending(x => x.CreatedOn).ToPagedListAsync(model, cancellationToken);

            return res;
        }

        public async Task<IEnumerable<KeyValuePair<string, Guid>>> GetTenantSecurityGroupsLookupAsync(Guid tenantId, string searchTerm, CancellationToken cancellationToken)
        {
            var result = await _context.SecurityGroups
                                .Where(x => x.TenantId == tenantId)
                                .Where(x => x.Name.Contains(searchTerm))
                                .Select(z => new KeyValuePair<string, Guid>(z.Name, z.Id)).ToListAsync(cancellationToken);
            return result;
        }

        public async Task<SecurityGroupInfoDto> GetSecurityGroupInfo(Guid tenantId, Guid securityGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.SecurityGroups.Where(x => x.Id == securityGroupId && x.TenantId == tenantId)
                                   .Select(x => new SecurityGroupInfoDto
                                   {
                                       Name= x.Name,
                                       NoOfPermissions = x.AssociatedPermissions.Count(),
                                       NoOfUsers = x.TenantUserSecurityGroups.Count(),
                                       CreatedOn = x.CreatedOn,
                                       ModifiedOn = x.ModifiedOn
                                   }).FirstOrDefaultAsync(cancellationToken);
            return result;
        }
    }
}

