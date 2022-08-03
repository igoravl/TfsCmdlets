namespace TfsCmdlets.Models
{
    /// <summary>
    /// Encapsulates the team board object
    /// </summary>
    public class Board : ModelBase<WebApiBoard>
    {
        public Board(WebApiBoard b, string project, string team)
            : base(b)
        {
            PSObjectExtensions.SetProperty(this, nameof(Project), project);
            PSObjectExtensions.SetProperty(this, nameof(Team), team);
        }

        public Guid Id => InnerObject.Id;

        public string Name => InnerObject.Name;

        public string Project => (string)this.GetProperty(nameof(Project)).Value;

        public string Team => (string)this.GetProperty(nameof(Team)).Value;

    }
}