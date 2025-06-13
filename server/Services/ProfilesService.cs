namespace keepr2.Services;

public class ProfilesService
{
  // NOTE 🏗️ Class constructor.
  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  // NOTE 💉 Dependency injections.
  private readonly ProfilesRepository _repo;
}