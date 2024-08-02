using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace TfsCmdlets.Models
{
    [DataContract]
    public class ContributionNodeQuery
    {
        [DataMember(EmitDefaultValue = false, Name = "dataProviderContext")]
        public DataProviderContext DataProviderContext;

        [DataMember(Name = "queryOptions", EmitDefaultValue = false)]
        public ContributionQueryOptions QueryOptions { get; set; } 

        [DataMember(Name = "contributionIds")]
        public IEnumerable<string> ContributionIds { get; set; }

        [DataMember(Name = "includeProviderDetails", EmitDefaultValue = false)]
        public bool IncludeProviderDetails { get; set; }
    }

    [DataContract]
    public class ContributionNodeResponse
    {
        [DataMember(Name = "dataProviderSharedData")]
        public DataProviderSharedData DataProviderSharedData { get; set; }

        [DataMember(Name = "dataProviders")]
        public DataProviders DataProviders { get; set; }
    }


    [DataContract]
    public class DataProviderContext
    {
        [DataMember(Name = "properties")]
        public Dictionary<string, object> Properties { get; set; }

        [DataMember(Name = "sharedData", EmitDefaultValue = false)]
        public DataProviderSharedData SharedData { get; set; }
    }

    [DataContract]
    public class DataProviderSharedData : Dictionary<string, object>
    {
    }

    [DataContract]
    public class DataProviders : Dictionary<string, JObject>
    {
    }

    [Flags]
    public enum ContributionQueryOptions
    {
        [EnumMember(Value = "none")] None = 0,
        [EnumMember(Value = "includeSelf")] IncludeSelf = 16,
        [EnumMember(Value = "includeChildren")] IncludeChildren = 32,
        [EnumMember(Value = "includeSubTree")] IncludeSubTree = 96,
        [EnumMember(Value = "includeAll")] IncludeAll = IncludeSubTree | IncludeSelf,
        [EnumMember(Value = "ignoreConstraints")] IgnoreConstraints = 256,
    }
}