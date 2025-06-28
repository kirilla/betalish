namespace Betalish.Application.EmailTemplates.Signups
{
    public interface ISignupEmailTemplate
    {
        EmailMessage Create(Signup signup);
    }
}
