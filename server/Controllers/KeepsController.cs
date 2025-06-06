
namespace keepr2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase, IKeepsController<Keep>
{

  public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
  {
    _keepsService = keepsService;
    _auth0Provider = auth0Provider;
  }
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0Provider;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      return Ok(_keepsService.Create(keepData));

    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
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