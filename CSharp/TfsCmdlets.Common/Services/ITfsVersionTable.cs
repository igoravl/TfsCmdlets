using TfsCmdlets.Models;

namespace TfsCmdlets.Services
{
    public interface ITfsVersionTable
    {
        ServerVersion GetServerVersion(Version version);
        int GetYear(int majorVersion);
        int GetMajorVersion(int year);
    }
}