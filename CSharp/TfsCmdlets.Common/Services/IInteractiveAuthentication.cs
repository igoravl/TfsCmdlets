namespace TfsCmdlets.Services
{
    public interface IInteractiveAuthentication
    {
        string GetToken(Uri uri);                                                                                                       
    }
}