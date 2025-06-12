
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

  public Task<ActionResult<Vault>> GetById(int vaultId)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int vaultId)
  {
    throw new NotImplementedException();
  }
}
