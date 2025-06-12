
namespace keepr2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase, IVaultsController<Vault, Keep>
{
  // NOTE 🏗️ Class constructor.
  public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider)
  {
    _vaultsService = vaultsService;
    _auth0Provider = auth0Provider;
  }
  // NOTE 💉 Dependency injections.
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;
  // NOTE 🛠️ Create vault request method. Gets user info for authentication and sets vaultData.CreatorId = userInfo.Id to prevent users from creating a vault with another user's id.
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      return Ok(_vaultsService.Create(vaultData));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  public Task<ActionResult<string>> Delete(int vaultId)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<Vault>> Edit([FromBody] Vault updateVaultData, int vaultId)
  {
    throw new NotImplementedException();
  }
  // NOTE 🔍 Get vault by id request method. User info is retrieved and passed to _vaultsService.IsPrivateCheck() to check if the user is the creator of the vault (only the vault creator can view a private vault).
  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetById(int vaultId)
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_vaultsService.IsPrivateCheck(vaultId, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  public Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
  {
    throw new NotImplementedException();
  }
}
