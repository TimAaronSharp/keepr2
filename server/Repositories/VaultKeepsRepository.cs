
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
  // NOTE 💣 Deletes vaultKeep from database.
  public void Delete(int vaultKeepId)
  {
    string sql = "DELETE FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { vaultKeepId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} vaultKeeps were deleted, which means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    string sql = "SELECT * FROM vault_keeps WHERE id = @vaultKeepId;";

    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
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
