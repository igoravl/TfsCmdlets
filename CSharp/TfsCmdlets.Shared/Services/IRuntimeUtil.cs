namespace TfsCmdlets.Services
{
    public interface IRuntimeUtil
    {
        string TargetFramework { get; }

        string Platform { get; }

        string OperatingSystem { get; }
    }
}
