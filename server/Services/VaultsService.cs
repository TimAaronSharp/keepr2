
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
    return _repo.Create(vaultData);
  }

  public string Delete(int vaultId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public Vault Edit(int vaultId, Vault updateVaultData, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  private Vault GetById(int vaultId)
  {
    Vault vault = _repo.GetById(vaultId);

    if (vault == null)
    {
      throw new Exception($"Invalid vault id: {vault.Id}");
    }
    return vault;
  }
  // NOTE 🔐 Method to check if the user requesting the vault is the creator of the vault, and if the vault is private (only the vault creator can view a private vault).
  public Vault IsPrivateCheck(int vaultId, Profile userInfo)
  {
    Vault vault = _repo.GetById(vaultId);

    if (vault.IsPrivate == true && vault.CreatorId != userInfo?.Id)
    {
      throw new Exception($"Invalid vault id: {vault.Id} 😜");
    }

    return vault;
  }

  public List<Vault> GetByProfileId(string profileId, Profile userInfo)
  {
    throw new NotImplementedException();
  }
}
