namespace keepr2.Interfaces;

public interface IVaultKeepsController<T>
{
  public Task<ActionResult<T>> Create([FromBody] T vaultKeepData);
  public Task<ActionResult<string>> Delete(int vaultKeepId);
}