
namespace keepr2.Services;

public class VaultKeepsService : IVaultKeepsService<VaultKeep, VaultKeepTracker>
{

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  // NOTE 🛠️ Create vaultKeep method. Passes the vaultKeepData to repo for creation in database.
  public VaultKeep Create(VaultKeep vaultKeepData, Profile userInfo)
  {
    Vault vault = _vaultsService.IsPrivateCheck(vaultKeepData.VaultId, userInfo);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot save a keep to another user's vault, {userInfo.Name}".ToUpper());
    }

    return _repo.Create(vaultKeepData);
  }

  public string Delete(int vaultKeepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public List<VaultKeepTracker> GetByVaultId(int vaultId, Profile userInfo)
  {
    Vault vault = _vaultsService.IsPrivateCheck(vaultId, userInfo);
    return _repo.GetByVaultId(vault.Id);
  }
}
