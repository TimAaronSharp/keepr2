namespace keepr2.Models;

// NOTE Abstract class that has props that every server class model will need.
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}