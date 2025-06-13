
namespace keepr2.Repositories;

public class VaultKeepsRepository : IVaultKeepsRepository<VaultKeep, Vault>
{


  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  // NOTE 🛠️ Creates vaultKeep in database.
  public VaultKeep Create(VaultKeep vaultKeepData)
  {
    string sql = @"
    INSERT INTO
    vault_keeps(keep_id, vault_id, creator_id)
    VALUES(@KeepId, @VaultId, @CreatorId);

    SELECT * FROM vault_keeps WHERE id = LAST_INSERT_ID();";

    return _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
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
