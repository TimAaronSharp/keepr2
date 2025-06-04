namespace keepr2.Interfaces;

public interface IKeepsController<T>
{
  // NOTE Add async to async methods in controller (Task returns)
  public Task<ActionResult<T>> Create([FromBody] T keepData);
  public ActionResult<List<T>> GetAll();
  public Task<ActionResult<T>> GetById(int keepId);
  public Task<ActionResult<T>> Edit([FromBody] T updateKeepData, int keepId);
  public Task<ActionResult<string>> Delete(int keepId);
}