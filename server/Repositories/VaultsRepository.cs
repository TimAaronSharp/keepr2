
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