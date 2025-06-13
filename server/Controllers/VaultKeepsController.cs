
namespace keepr2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase, IVaultKeepsController<VaultKeep>
{

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
  }
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0Provider;

  // NOTE 🛠️ Create vaultKeep request method. Gets user info for authentication and sets vaultKeepData.CreatorId = userInfo.Id to prevent users from creating a vaultKeep with another user's id.
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepData.CreatorId = userInfo.Id;
      return Ok(_vaultKeepsService.Create(vaultKeepData, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE 💣 Delete vaultKeep request method. Gets user info for authentication.
  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> Delete(int vaultKeepId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_vaultKeepsService.Delete(vaultKeepId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
