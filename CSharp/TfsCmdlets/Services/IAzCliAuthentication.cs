namespace TfsCmdlets.Services
{
    public interface IAzCliAuthentication
    {
        string GetToken(Uri uri, bool useMsi = false);
    }
}
