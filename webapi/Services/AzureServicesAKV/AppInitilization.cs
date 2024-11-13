using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

public class AppInitilization : IAppInitization
{
    private readonly IConfiguration _config;
    public AppInitilization(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task<string> AppInit()
    {
        var clientId = "6fa8beb8-df1f-4819-a8ad-029cb7376e2a";
        var clientSecret = _config["clientApp:secret"];
        var vaultUri = new Uri("https://akvdevpersonal.vault.azure.net/");
        var tenantId = "6e5c60a5-e933-4d9a-bc06-35f8d0325328";
        var secretName = "sgkey";
        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        var client = new SecretClient(vaultUri, credential);
        KeyVaultSecret secret = await client.GetSecretAsync(secretName);

        return secret.Value;
    }

}