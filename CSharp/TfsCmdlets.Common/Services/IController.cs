namespace TfsCmdlets.Services
{
    public interface IController
    {
        IEnumerable InvokeCommand();

        string Verb {get;}

        string Noun {get;}

        string CommandName {get;}

        Type DataType {get;}
    }
}