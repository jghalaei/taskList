using GenericContracts.Common;

namespace User.Core.Entities;

public class AppUser : EntityBase
{

    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}