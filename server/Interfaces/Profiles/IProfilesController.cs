namespace keepr2.Interfaces;

// NOTE I generally don't like naming variables like this (T2, T3) but it seemed to fit well in this case as it is not as nebulous as a param for a method that really needs to be verbose. I could have just gone with a different letter like "J" how it's often convention in nested for loops, but I felt that would be just as, if not more, convoluted than just doing T2, as I feel T2/T3 gets the point across that it is just another variable type for this interface.
public interface IProfilesController<T, T2, T3>
{
  public ActionResult<T> GetById(string profileId);
  public ActionResult<List<T2>> GetKeepsByProfileId(string profileId);
  public Task<ActionResult<List<T3>>> GetVaultsByProfileId(string profileId);
}