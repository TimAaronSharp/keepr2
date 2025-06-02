namespace keepr2.Interfaces;

public interface IKeepsService<T>
{
  public T Create(T keepData);
  public List<T> GetAll();
  // NOTE private T GetById(int keepId) - Will create in KeepsService. Still learning how to get private methods to work with interfaces (you have to declare a body/implement them - will experiment later)
  public List<T> GetByProfileId(string profileId);
  public T IncrementViews(int keepId, Profile userInfo);
  public T Edit(int keepId, T updateKeepData, Profile userInfo);
  public string Delete(int keepId, Profile userInfo);

}