using FalconeOne.BLL.Interfaces;
using FalconOne.DAL.Contracts;
using FalconOne.Models.Dtos.Domain;
using FalconOne.Models.Entities.Users;
using IdenticonSharp.Identicons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace FalconeOne.BLL.Services
{
    public class DomainService : BaseService, IDomainService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DomainService(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITenantProvider tenantProvider, IIdenticonProvider identiconProvider, IHttpClientFactory httpClientFactory)
            : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantProvider, identiconProvider)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DomainWhoisDto?> GetDomainInfoAsync(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
            {
                return null;
            };

            var apiKey = _configuration.GetValue<string>("ip2LocationAPIKey");
            var client = _httpClientFactory.CreateClient("Domain");

            var result = await client.GetFromJsonAsync<DomainWhoisDto>($"https://api.ip2whois.com/v2?key={apiKey}&domain={domainName}");

            return result;
        }
    }

    public class AdvancedSettingsService : BaseService, IAdvancedSettingsService
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public AdvancedSettingsService(UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            ITenantProvider tenantService,
            IPasswordHasher<object> passwordHasher, IIdenticonProvider identiconProvider) : base(userManager, unitOfWork, httpContextAccessor, configuration, tenantService, identiconProvider)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            var hashedPassword = await Task.FromResult(_passwordHasher.HashPassword(new object(), password));

            return hashedPassword;
        }

        public async Task<bool> UpdateProfileForAllUsers(byte[] data, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UserRepository.UpdateProfilePictureForAllUsers(data, cancellationToken);

            return result;
        }
    }
}
