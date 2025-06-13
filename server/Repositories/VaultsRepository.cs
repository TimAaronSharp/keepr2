
namespace keepr2.Repositories;

public class VaultsRepository : IVaultsRepository<Vault>
{

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;
  // NOTE 🛠️ Creates new vault in database.
  public Vault Create(Vault vaultData)
  {
    string sql = @"
    INSERT INTO
    vaults(name, description, img, is_private, creator_id)
    VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);
    
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, vaultData).SingleOrDefault();
  }

  // NOTE 💣 Deletes vault from database.
  public void Delete(int vaultId)
  {
    string sql = "DELETE FROM vaults where id = @vaultId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { vaultId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} vaults were deleted, which is very bad and means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }

  // NOTE 🧵 Edits vault in database.
  public void Edit(Vault updateVaultData)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    description = @Description,
    img = @Img,
    is_private = @IsPrivate
    WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, updateVaultData);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} vaults were edited, which is very bad and means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  // NOTE 🔍 Gets vault by id from database.
  public Vault GetById(int vaultId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = @vaultId;";

    return _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { vaultId }).SingleOrDefault();
  }

  // NOTE 🔍🖼️📄 Gets vaults by profile id from database.
  public List<Vault> GetByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    INNER JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.creator_id = @profileId;";

    return _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { profileId }).ToList();
  }
}