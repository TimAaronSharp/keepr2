using System.ComponentModel.DataAnnotations;

namespace keepr2.Models;

public class VaultKeep : RepoItem<int>
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
}

public class VaultKeepTracker : Keep
{
  public int VaultKeepId { get; set; }
}