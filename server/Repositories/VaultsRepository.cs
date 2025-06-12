
namespace keepr2.Repositories;

public class VaultsRepository : IVaultsRepository<Vault>
{

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Vault Create(Vault vaultData)
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  public Vault Edit(Vault updateVaultData)
  {
    throw new NotImplementedException();
  }

  public void Delete(int vaultId)
  {
    throw new NotImplementedException();
  }
}