namespace keepr2.Repositories;

public class ProfilesRepository
{
  // NOTE 🏗️ Class constructor.
  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  // NOTE 💉 Dependency injections.
  private readonly IDbConnection _db;

  // NOTE 🔍📄 Gets profile by id from database.
  public Profile GetById(string profileId)
  {
    string sql = "SELECT * FROM accounts WHERE id = @profileId LIMIT 1;";

    return _db.Query<Profile>(sql, new { profileId }).SingleOrDefault();
  }
}