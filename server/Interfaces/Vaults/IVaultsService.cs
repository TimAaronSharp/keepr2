namespace keepr2.Interfaces;

public interface IVaultsService<T>
{
  public T Create(T vaultData);
  // NOTE private T GetById(int vaultId) - Will create in VaultsService. Still learning how to get private methods to work with interfaces (you have to declare a body/implement them - will experiment later)

  // NOTE The private GetById method above will be used to actually go get the vault by it's id through the repo from the database (other methods will use this as well that don't need/have access to the Profile userInfo). The public method below will use the private method to get the vault by it's id, and use the Profile userInfo to determine if the user is the creator of the vault and can view it if it is private.
  public T GetById(int vaultId, Profile userInfo);
  public List<T> GetByProfileId(string profileId, Profile userInfo);
  public T Edit(int vaultId, T updateVaultData, Profile userInfo);
  public string Delete(int vaultId, Profile userInfo);
}