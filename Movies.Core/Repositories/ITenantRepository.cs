public  interface ITenantRepository
{
    Task<bool> ValidateClientAsync(string clientId, string clientSecret);
}