namespace keepr2.Interfaces;

// NOTE I generally don't like naming variables like this (T2) but it seemed to fit well in this case as it is not as nebulous as a param for a method that really needs to be verbose. I could have just gone with a different letter like "J" how it's often convention in nested for loops, but I felt that would be just as, if not more, convoluted than just doing T2, as I feel T2 gets the point across that it is just another variable type for this interface.
public interface IVaultKeepsRepository<T, T2>
{
  public T Create(T vaultKeepData);
  public T GetById(int vaultKeepId);
  public List<T2> GetByVaultId(int vaultId);
  public void Delete(int vaultKeepId);
}