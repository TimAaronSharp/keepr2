
namespace keepr2.Repositories;

public class KeepsRepository : IKeepsRepository<Keep>
{

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Keep Create(Keep keepData)
  {
    string sql = @"
    INSERT INTO
    keeps(name, description, img, creator_id)
    VALUES(@Name, @Description, @Img, @CreatorId);
    
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    WHERE keeps.id = LAST_INSERT_ID();";

    Keep createdKeep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).SingleOrDefault();
    return createdKeep;
  }

  public void Delete(int keepId)
  {
    throw new NotImplementedException();
  }

  public Keep Edit(Keep updateKeepData)
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetAll()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    INNER JOIN accounts ON accounts.id = keeps.creator_id;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  public Keep GetById(int keepId)
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  public int IncrementViews(int keepId)
  {
    throw new NotImplementedException();
  }
}