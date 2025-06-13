namespace keepr2.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  // NOTE 💉 Dependency injections.
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;

  // NOTE 🏗️ Class constructor.
  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  // NOTE 🔍📄 Get user account method request path.
  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // NOTE 🔍🏦📄 Get user vaults by account id method request path.
  [Authorize]
  [HttpGet("vaults")]
  public async Task<ActionResult<Vault>> GetMyVaults()
  {
    try
    {
      Profile userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_vaultsService.GetByProfileId(userInfo.Id, userInfo));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
