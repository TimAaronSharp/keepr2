
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

  // NOTE Create keep request method. 🛠️ Gets user info for authentication and sets keepData.CreatorId = userInfo.Id to prevent users from creating a keep with another user's id.
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

  // NOTE Edit keep request path. Gets user info for authentication.
  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> Edit([FromBody] Keep updateKeepData, int keepId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_keepsService.Edit(keepId, updateKeepData, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE GetAll keeps request method. 🧺 Gets all keeps from the database
  [HttpGet]
  public ActionResult<List<Keep>> GetAll()
  {
    try
    {
      return Ok(_keepsService.GetAll());
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE GetById keep request method. 🔍 Gets a keep by its id (goes through _keepsService.IncrementViews())
  [HttpGet("{keepId}")]
  public async Task<ActionResult<Keep>> GetById(int keepId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_keepsService.IncrementViews(keepId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}