
namespace keepr2.Controllers;

[ApiController]
[Route("api[controller]")]

public class KeepsController : ControllerBase, IKeepsController<Keep>
{
  public KeepsController(KeepsService keepsService)
  {
    this.keepsService = keepsService;
  }
  private readonly KeepsService keepsService;

  public Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<string>> Delete(int keepId)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<Keep>> Edit([FromBody] Keep updateKeepData, int keepId)
  {
    throw new NotImplementedException();
  }

  public ActionResult<List<Keep>> GetAll()
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<Keep>> GetById(int keepId)
  {
    throw new NotImplementedException();
  }
}