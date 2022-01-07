namespace keepr.Models
{
  public class Profile
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
  }

  public class Account : Profile
  {
    // Do I need to have anything extra here since both profile and account have id name email picture?
  }
}