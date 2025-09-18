namespace TfsCmdlets {

    [AttributeUsage(System.AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class CmdletControllerAttribute : ExportAttribute
    {
        public Type DataType { get; }

        public string CustomCmdletName { get; set; }

        public string[] CustomVerbs { get; set; }

        public string[] CustomNouns { get; set; }

        public Type CustomBaseClass { get; set; }

        public Type Client { get; set; }


        public CmdletControllerAttribute() : base(typeof(IController))
        {
        }

        public CmdletControllerAttribute(Type dataType) : base(typeof(IController))
        {
            DataType = dataType;
        }
    }
}