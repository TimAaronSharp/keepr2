
namespace keepr2.Services;

public class KeepsService : IKeepsService<Keep>
{
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }
  private readonly KeepsRepository _repo;

  public Keep Create(Keep keepData)
  {
    throw new NotImplementedException();
  }

  public string Delete(int keepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public Keep Edit(int keepId, Keep updateKeepData, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetAll()
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  public Keep IncrementViews(int keepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }
}