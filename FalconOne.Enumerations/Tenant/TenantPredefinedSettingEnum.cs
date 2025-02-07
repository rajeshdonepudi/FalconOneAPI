using System.ComponentModel.DataAnnotations;

namespace FalconOne.Enumerations.Tenants
{
    public enum TenantPredefinedSettingEnum
    {
        [Display(Name = "Secret Key")]
        SecretKey = 1,

        [Display(Name = "Access Token Lifetime (Minutes)")]
        TokenLifetimeInMinutes,

        [Display(Name = "Refresh Token Lifetime (Days)")]
        RefreshTokenLifetimeInDays
    }
}
