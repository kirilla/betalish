namespace Betalish.Web.Models;

public class ClientNavbarViewModel
{
    public IUserToken UserToken { get; set; }
    public Client Client { get; set; }
    
    public ClientNavbarViewModel(IUserToken userToken, Client client)
    {
        UserToken = userToken;
        Client = client;
    }
}
