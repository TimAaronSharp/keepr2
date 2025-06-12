namespace keepr2.Interfaces;

public interface IKeepsRepository<T>
{
  public T Create(T keepData);
  public List<T> GetAll();
  public T GetById(int keepId);
  public List<T> GetByProfileId(string profileId);
  public int IncrementViews(int keepId);
  public void Edit(T updateKeepData);
  public void Delete(int keepId);
}