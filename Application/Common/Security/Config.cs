using Duende.IdentityServer.Models;

namespace Application.Common.Security;

public static class Config
{
    public const string GeneralAccessScope = "generalAccess";
    public const string SecureAccessScope = "secureAccess";
    public const string CreateOfficeScope = "createOffice";
    public const string LockDoorScope = "lockDoor";
    public const string UnlockDoorScope = "unlockDoor";
    public const string AddDoorScope = "addDoor";
    public const string ReadOfficeScope = "readOffice";
    public const string ReadHistoryScope = "readHistory";

    public const string EmployeeClientId = "employee";
    public const string EmployeeClientSecret = "employeeSecret";

    public const string OfficeManagerClientId = "officeManager";
    public const string OfficeManagerClientSecret = "officeManagerSecret";

    public const string DirectorClientId = "director";
    public const string DirectorClientSecret = "directorSecret";

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(GeneralAccessScope, "General Access"),
            new ApiScope(SecureAccessScope, "Secure Access"),
            new ApiScope(CreateOfficeScope, "Create Office"),
            new ApiScope(LockDoorScope, "Lock Door"),
            new ApiScope(UnlockDoorScope, "Unlock Door"),
            new ApiScope(AddDoorScope, "Add Door"),
            new ApiScope(ReadOfficeScope, "Read Office"),
            new ApiScope(ReadHistoryScope, "Get History")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = EmployeeClientId,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(EmployeeClientSecret.Sha256()) },
                AllowedScopes = { GeneralAccessScope, LockDoorScope, UnlockDoorScope }
            },
            new Client
            {
                ClientId = OfficeManagerClientId,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(OfficeManagerClientSecret.Sha256()) },
                AllowedScopes = { GeneralAccessScope, SecureAccessScope, AddDoorScope, ReadOfficeScope, LockDoorScope, UnlockDoorScope }
            },
            new Client
            {
                ClientId = DirectorClientId,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(DirectorClientSecret.Sha256()) },
                AllowedScopes = { GeneralAccessScope, SecureAccessScope, CreateOfficeScope, AddDoorScope, ReadHistoryScope, ReadOfficeScope, LockDoorScope, UnlockDoorScope }
            }
        };
}
