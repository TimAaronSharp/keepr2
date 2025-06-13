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

  // NOTE 🔍📄 Get profile by id method.
  public Profile GetById(string profileId)
  {
    Profile profile = _repo.GetById(profileId);

    if (profile == null)
    {
      throw new Exception($"Invalid profile id: {profileId}");
    }
    return profile;
  }
}