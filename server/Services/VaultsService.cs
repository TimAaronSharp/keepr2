
namespace keepr2.Services;

public class VaultsService : IVaultsService<Vault>
{
  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }
  private readonly VaultsRepository _repo;

  public Vault Create(Vault vaultData)
  {
    throw new NotImplementedException();
  }

  public string Delete(int vaultId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public Vault Edit(int vaultId, Vault updateVaultData, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetByProfileId(string profileId, Profile userInfo)
  {
    throw new NotImplementedException();
  }
}
