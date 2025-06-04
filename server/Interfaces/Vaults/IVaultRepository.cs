namespace keepr2.Interfaces;

public interface IVaultRepository<T>
{
  public T Create(T vaultData);
  public T GetById(int vaultId);
  public List<T> GetByProfileId(string profileId);
  public T Edit(T updateVaultData);
  public void Delete(int vaultId);
}