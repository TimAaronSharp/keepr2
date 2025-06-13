
namespace keepr2.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase, IProfilesController<Profile, Keep, Vault>
{
  // NOTE 🏗️ Class constructor.
  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, KeepsService keepsService, VaultsService vaultsService)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  // NOTE 💉 Dependency injections.
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  // NOTE 🔍📄 Get profile by id request method.
  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetById(string profileId)
  {
    try
    {
      return Ok(_profilesService.GetById(profileId));
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
  {
    throw new NotImplementedException();
  }
}