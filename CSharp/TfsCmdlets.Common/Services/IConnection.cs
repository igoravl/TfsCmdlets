namespace TfsCmdlets.Services
{
    internal interface IConnection
    {
        T GetClient<T>();

        T GetService<T>();
    }
}
