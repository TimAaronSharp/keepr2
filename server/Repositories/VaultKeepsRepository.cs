
namespace keepr2.Repositories;

public class VaultKeepsRepository : IVaultKeepsRepository<VaultKeep, Vault>
{


  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public VaultKeep Create(VaultKeep vaultKeepData)
  {
    throw new NotImplementedException();
  }

  public void Delete(int vaultKeepId)
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetByVaultId(int vaultId)
  {
    throw new NotImplementedException();
  }
}
