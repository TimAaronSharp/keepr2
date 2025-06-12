
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

  public Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
  {
    throw new NotImplementedException();
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
