
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
    throw new NotImplementedException();
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
    throw new NotImplementedException();
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