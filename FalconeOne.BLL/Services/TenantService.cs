﻿using FalconeOne.BLL.Helpers;
using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Helpers.Helpers;
using FalconOne.Models.Dtos.Tenants;
using FalconOne.Models.Entities.Common;
using FalconOne.Models.Entities.Tenants;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FalconeOne.BLL.Services
{
    public class TenantService : BaseService, ITenantService
    {
        public TenantService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ITenantProvider tenantProvider, IIdenticonProvider identiconProvider) :
            base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
        }

        public async Task<TenantLookupDto> GetTenantInfoAsync(CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TenantRepository.GetTenantBasicInfoByIdAsync(TenantId, cancellationToken);

            if (result is null)
            {
                throw new Exception(MessageHelper.GeneralErrors.INVALID_REQUEST);
            }

            return result;
        }

        public async Task<IEnumerable<KeyValuePair<string, Guid>>> GetTenantsLookupForDirectoryAsync(string? searchTerm, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Enumerable.Empty<KeyValuePair<string, Guid>>();
            }

            var result = await _unitOfWork.TenantRepository.GetTenantsLookupForDirectoryAsync(searchTerm, cancellationToken);

            return result;
        }

        public async Task<TenantManagementDashboardInfoDto> GetTenantManagementDashboardInfo(CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TenantRepository.GetTenantManagementDashboardInfoAsync(cancellationToken);

            return result;
        }

        public async Task<PagedList<TenantDetailsDto>> GetAllTenantsAsync(PageParams model, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TenantRepository.GetAllTenantsAsync(model, cancellationToken);

            return result;
        }

        public async Task<bool> AddTenantAsync(AddTenantDto model, CancellationToken cancellationToken)
        {
            var tenant = new Tenant
            {
                CreatedOn = DateTime.UtcNow,
                Name = model.Name,
                Host = model.Host,
            };

            if (!string.IsNullOrEmpty(model.ProfilePicture))
            {
                tenant.ProfilePicture = new Image
                {
                    Title = "profile_picture_" + model.Host,
                    Data = Convert.FromBase64String(model.ProfilePicture),
                };
            }

            await _unitOfWork.TenantRepository.AddAsync(tenant, cancellationToken);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task<TenantDetailsDto> GetTenantDetailsAsync(string resourceAlias, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TenantRepository.GetTenantDetailsAsync(resourceAlias, cancellationToken);

            return result;
        }
    }
}
