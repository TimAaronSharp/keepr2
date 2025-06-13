
namespace keepr2.Services;

public class VaultKeepsService : IVaultKeepsService<VaultKeep, Vault>
{

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }
  private readonly VaultKeepsRepository _repo;
  private readonly VaultsService _vaultsService;

  public VaultKeep Create(VaultKeep vaultKeepData, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public string Delete(int vaultKeepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetByVaultId(int vaultId, Profile userInfo)
  {
    throw new NotImplementedException();
  }
}
