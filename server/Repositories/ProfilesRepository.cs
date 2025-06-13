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
}