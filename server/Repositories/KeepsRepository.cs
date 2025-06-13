
namespace keepr2.Repositories;

public class KeepsRepository : IKeepsRepository<Keep>
{

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  // NOTE 🛠️ Creates new keep in database.
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

  // NOTE 💣 Deletes keep from database.
  public void Delete(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { keepId });

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} were deleted, which is very bad and means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg.");
    }
  }

  // NOTE 🧵 Edits keep in database.
  public void Edit(Keep updateKeepData)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description
    WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, updateKeepData);

    if (rowsAffected != 1)
    {
      throw new Exception($"{rowsAffected} were updated, which is very bad and means your code is bad and you should feel bad. -Dr. Johnathan Alfred Zoidberg");
    }
  }

  // NOTE 🧺 Gets all keeps from database.
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

  // NOTE 🔍 Gets keep by id from database.
  public Keep GetById(int keepId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*,
    keep_count.*
    FROM keeps
    INNER JOIN accounts ON accounts.id = keeps.creator_id
    INNER JOIN keep_count ON keep_count.id = keeps.id
    WHERE keeps.id = @keepId;";

    Keep foundKeep = _db.Query(sql, (Keep keep, Profile account, Keep keepCount) =>
    {
      keep.Creator = account;
      keep.Kept = keepCount.Kept;
      return keep;
    }, new { keepId }).SingleOrDefault();
    return foundKeep;
  }

  public List<Keep> GetByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  // NOTE ➕ Increments a keep's view count in database.
  public int IncrementViews(int keepId)
  {
    string sql = @"
    UPDATE keeps
    SET views = views + 1
    WHERE id = @keepId LIMIT 1;
    
    SELECT views FROM keeps WHERE id = @keepId LIMIT 1;";

    return _db.ExecuteScalar<int>(sql, new { keepId });

  }
}