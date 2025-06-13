
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

  // NOTE 💣 Delete vaultKeep method. Gets vaultKeep by id, verifies user is the vaultKeep creator (if not throws exception), and sends vaultKeepId to repo for deletion from database.
  public string Delete(int vaultKeepId, Profile userInfo)
  {
    VaultKeep vaultKeep = GetById(vaultKeepId);

    if (vaultKeep.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot delete another user's vaultKeep, {userInfo.Name}".ToUpper());
    }

    _repo.Delete(vaultKeep.Id);
    return $"VaultKeep id: {vaultKeep.Id} has been deleted. You monster";
  }

  private VaultKeep GetById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repo.GetById(vaultKeepId);

    if (vaultKeep == null)
    {
      throw new Exception($"Invalid vaultKeep id: {vaultKeepId}");
    }
    return vaultKeep;
  }

  public List<VaultKeepTracker> GetByVaultId(int vaultId, Profile userInfo)
  {
    Vault vault = _vaultsService.IsPrivateCheck(vaultId, userInfo);
    return _repo.GetByVaultId(vault.Id);
  }
}
