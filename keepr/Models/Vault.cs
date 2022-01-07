using System;

namespace keepr.Models
{
  public class Vault
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public Profile Creator { get; set; }
  }
  public class VaultKeepViewModel : Vault
  {
    public int VaultKeepId { get; set; }
  }
}

// NOTE trackedhabits repository GetAllTrackedHabitsByAccountId double join