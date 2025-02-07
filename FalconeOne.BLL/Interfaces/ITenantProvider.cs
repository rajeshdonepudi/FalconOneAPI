namespace FalconeOne.BLL.Interfaces
{
    public interface ITenantProvider
    {
        Task<Guid> GetTenantIdAsync();
        Task<Guid> ResolveTenantIdByRefererAsync(string referer);
    }
}
