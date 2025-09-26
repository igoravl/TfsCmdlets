namespace TfsCmdlets;

[AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class ModelAttribute: Attribute
{
    public Type DataType { get; }

    public ModelAttribute(Type dataType)
    {
        DataType = dataType;
    }
}