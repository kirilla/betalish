namespace Betalish.Application.Commands.Users.DeleteUser;

public class DeleteUserCommandModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Confirmed { get; set; }
}
