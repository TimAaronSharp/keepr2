
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

  public Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<string>> Delete(int vaultKeepId)
  {
    throw new NotImplementedException();
  }
}
