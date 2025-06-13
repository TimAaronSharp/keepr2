
namespace keepr2.Services;

public class VaultsService : IVaultsService<Vault>
{
  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }
  private readonly VaultsRepository _repo;

  // NOTE 🛠️ Create vault method. Passes the vaultData to repo for creation in database.
  public Vault Create(Vault vaultData)
  {
    return _repo.Create(vaultData);
  }

  public string Delete(int vaultId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  // NOTE 🧵 Edit vault method. Updates vault with new updateVaultData, and sends the updated vault to repo. Performs verification that the user is the vault creator.
  public Vault Edit(int vaultId, Vault updateVaultData, Profile userInfo)
  {
    Vault vault = GetById(vaultId);

    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot edit another user's vault, {userInfo.Name}".ToUpper());
    }

    vault.Name = updateVaultData.Name ?? vault.Name;
    vault.Description = updateVaultData.Description ?? vault.Description;
    vault.Img = updateVaultData.Img ?? vault.Img;
    vault.IsPrivate = updateVaultData.IsPrivate ?? vault.IsPrivate;

    _repo.Edit(vault);
    return vault;
  }

  // NOTE 🔍 Get vault by id method. Gets vault by it's id from repo. Does null check in case user is guessing id's or for some reason it comes back null.
  private Vault GetById(int vaultId)
  {
    Vault vault = _repo.GetById(vaultId);

    if (vault == null)
    {
      throw new Exception($"Invalid vault id: {vault.Id}");
    }
    return vault;
  }
  // NOTE 🔐 Is vault private check method. Checks if the user requesting the vault is the creator of the vault, and if the vault is private (only the vault creator can view a private vault).
  public Vault IsPrivateCheck(int vaultId, Profile userInfo)
  {
    Vault vault = GetById(vaultId);

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
