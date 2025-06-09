
namespace keepr2.Services;

public class KeepsService : IKeepsService<Keep>
{
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }
  private readonly KeepsRepository _repo;

  // NOTE Passes the keepData to repo.
  public Keep Create(Keep keepData)
  {
    return _repo.Create(keepData);
  }

  public string Delete(int keepId, Profile userInfo)
  {
    throw new NotImplementedException();
  }

  // NOTE Gets keep by id, updates keep with new updateKeepData, and sends the updated keep to repo. Performs verification that the user is the keep creator.
  public Keep Edit(int keepId, Keep updateKeepData, Profile userInfo)
  {

    Keep keep = GetById(keepId);

    if (keep.CreatorId != userInfo.Id)
    {
      throw new Exception($"You cannot edit another user's keep, {userInfo.Name}".ToUpper());
    }

    keep.Name = updateKeepData.Name ?? keep.Name;
    keep.Description = updateKeepData.Description ?? keep.Description;

    _repo.Edit(keep);
    return keep;
  }
  // NOTE Gets all keeps from repo.
  public List<Keep> GetAll()
  {
    return _repo.GetAll();
  }

  // NOTE Gets keep by it's id from repo. Does null check in case user is guessing id's or for some reason it comes back null.
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

  // NOTE Increments the keep's view count. ➕ If user is the keep creator it returns the keep as is so that the creator can't artificially inflate view count. (Of course they could still do this while logged out).
  public Keep IncrementViews(int keepId, Profile userInfo)
  {

    Keep keep = GetById(keepId);

    if (keep.CreatorId == userInfo?.Id)
    {
      return keep;
    }
    keep.Views = _repo.IncrementViews(keep.Id);
    return keep;
  }
}