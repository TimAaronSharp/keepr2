
namespace keepr2.Repositories;

public class VaultKeepsRepository : IVaultKeepsRepository<VaultKeep, VaultKeepTracker>
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

  public List<VaultKeepTracker> GetByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
    vault_keeps.*,
    keeps.*,
    accounts.*,
    keep_count.*
    FROM vault_keeps
    INNER JOIN keeps ON keeps.id = vault_keeps.keep_id
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    INNER JOIN keep_count ON keep_count.id = keeps.id
    WHERE vault_keeps.vault_id = @vaultId;";

    return _db.Query(sql, (VaultKeep vaultKeep, VaultKeepTracker vaultKeepTracker, Profile account, Keep keep) =>
    {
      vaultKeepTracker.VaultKeepId = vaultKeep.Id;
      vaultKeepTracker.Creator = account;
      vaultKeepTracker.Kept = keep.Kept;
      return vaultKeepTracker;
    }, new { vaultId }).ToList();
  }
}
