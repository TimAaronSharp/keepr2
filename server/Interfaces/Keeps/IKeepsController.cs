namespace keepr2.Interfaces;

public interface IKeepsController<T>
{
  public Task<ActionResult<T>> Create([FromBody] T keepData);
  public ActionResult<List<T>> GetAll();
  public Task<ActionResult<T>> GetById(int keepId);
  public Task<ActionResult<T>> Edit([FromBody] T updateKeepData, int keepId);
  public Task<ActionResult<string>> Delete(int keepId);
}