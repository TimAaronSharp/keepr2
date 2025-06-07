
namespace keepr2.Services;

public class KeepsService : IKeepsService<Keep>
{
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }
  private readonly KeepsRepository _repo;

  // NOTE Creates
  public Keep Create(Keep keepData)
  {
    return _repo.Create(keepData);
  }

  public string Delete(int keepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  public Keep Edit(int keepId, Keep updateKeepData, Profile userInfo)
  {
    throw new NotImplementedException();
  }
  // NOTE Gets all keeps
  public List<Keep> GetAll()
  {
    return _repo.GetAll();
  }

  // NOTE Gets keep by it's id. Does null check in case user is guessing id's or for some reason it comes back null.
  private Keep GetById(int keepId)
  {
    Keep keep = _repo.GetById(keepId);

    if (keep == null)
    {
      throw new Exception($"Invalid keep id: {keepId}");
    }

    return keep;
  }

  public List<Keep> GetByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  // NOTE Checks if user is the creator of the keep. If not, increments the view count. If user is creator it returns the keep as is so that the creator can't artificially inflate view count.
  public Keep IncrementViews(int keepId, Profile userInfo)
  {

    Keep keep = _repo.GetById(keepId);

    if (keep.CreatorId == userInfo.Id)
    {
      return keep;
    }
    keep.Views = _repo.IncrementViews(keep.Id);
    return keep;
  }
}