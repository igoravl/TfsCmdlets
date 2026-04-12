namespace TfsCmdlets.Models
{
    public class GitItem : ModelBase<Microsoft.TeamFoundation.SourceControl.WebApi.GitItem>
    {
        public GitItem(Microsoft.TeamFoundation.SourceControl.WebApi.GitItem item, string project, string repository) : base(item)
        {
            Project = project;
            Repository = repository;
        }

        public string Project
        {
            get => this.GetProperty("Project").Value as string;
            set => this.SetProperty("Project", value);
        }

        public string Repository
        {
            get => this.GetProperty("Repository").Value as string;
            set => this.SetProperty("Repository", value);
        }
    }
}