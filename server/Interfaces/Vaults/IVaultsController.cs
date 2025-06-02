namespace keepr2.Interfaces;

// NOTE I generally don't like naming variables like this (T2) but it seemed to fit well in this case as it is not as nebulous as a param for a method that really needs to be verbose. I could have just gone with a different letter like "J" how it often convention in nested for loops, but I felt that would be just as, if not more, convoluted than just doing T2, as I feel T2 gets the point across that it is just another variable type for this interface.
public interface IVaultsController<T, T2>
{
  // NOTE Add async to async methods in controller (Task returns)
  public Task<ActionResult<T>> Create([FromBody] T vaultData);
  public Task<ActionResult<T>> GetById(int vaultId);
  public Task<ActionResult<List<T2>>> GetKeepsByVaultId(int vaultId);
  public Task<ActionResult<T>> Edit([FromBody] T updateVaultData, int vaultId);
  public Task<ActionResult<string>> Delete(int vaultId);

}