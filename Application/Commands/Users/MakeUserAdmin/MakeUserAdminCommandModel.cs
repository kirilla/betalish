namespace Betalish.Application.Commands.Users.MakeUserAdmin;

public class MakeUserAdminCommandModel
{
    public int UserId { get; set; }

    public bool Confirmed { get; set; }
}
