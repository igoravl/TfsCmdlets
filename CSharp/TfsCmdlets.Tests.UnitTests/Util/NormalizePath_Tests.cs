using System;
using TfsCmdlets.Services.Impl;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Util
{
    public class NormalizePath_Tests
   {
       // NormalizeNodePath(string path, string projectName, string scope = "", bool includeScope = false, bool excludePath = false, bool includeLeadingSeparator = false, bool includeTrailingSeparator = false, bool includeTeamProject = false, char separator = '\\')

       [Fact]
       public void Throws_ArgumentNullException_On_Null_Path()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Throws<ArgumentNullException>("path", () =>
               nodeUtil.NormalizeNodePath(null, "projectName")
           );
       }

       [Fact]
       public void Throws_ArgumentNullException_On_Null_Project()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Throws<ArgumentNullException>("projectName", () =>
               nodeUtil.NormalizeNodePath("path", null, includeTeamProject: true)
           );

           Assert.Equal("path", nodeUtil.NormalizeNodePath("path", null, includeTeamProject: false));
       }

       [Fact]
       public void Throws_When_Everything_Excluded()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Throws<ArgumentException>(() =>
               nodeUtil.NormalizeNodePath("path", "projectName",
                   scope: "scope",
                   includeScope: false,
                   includeTeamProject: false,
                   includeLeadingSeparator: false,
                   includeTrailingSeparator: false,
                   excludePath: true,
                   separator: '\\'));
       }

       [Fact]
       public void Throws_When_Project_Included_But_Missing()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Throws<ArgumentNullException>("projectName", () =>
               nodeUtil.NormalizeNodePath("path", "", includeTeamProject: true)
           );
       }

       [Fact]
       public void Throws_When_Scope_Included_But_Missing()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Throws<ArgumentNullException>("scope", () =>
               nodeUtil.NormalizeNodePath("path", "projectName", includeScope: true)
           );
       }

       [Fact]
       public void Can_Return_Path_Only()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("path", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_With_Leading_Sep()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("\\path", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: true,
               includeTrailingSeparator: false,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_With_Trailing_Sep()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("path\\", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: true,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_With_Leading_Trailing_Sep()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("\\path\\", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: true,
               includeTrailingSeparator: true,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_Project()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("projectName\\path", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_Scope()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("scope\\path", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));
       }

       [Fact]
       public void Can_Return_Path_Project_Scope()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("projectName\\scope\\path", nodeUtil.NormalizeNodePath("path", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));
       }

       [Fact]
       public void Can_Use_Custom_Separators()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("_projectName_scope_path1_path2_", nodeUtil.NormalizeNodePath("path1/path2", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: true,
               includeTrailingSeparator: true,
               separator: '_'));

           Assert.Equal("_projectName_scope_path1_path2_", nodeUtil.NormalizeNodePath("path1\\path2", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: true,
               includeTrailingSeparator: true,
               separator: '_'));
       }

       [Fact]
       public void Can_Handle_Paths_With_Embedded_Project()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("path", nodeUtil.NormalizeNodePath("/projectName/path/", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("projectName/path", nodeUtil.NormalizeNodePath("/projectName/path/", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));
       }

       [Fact]
       public void Can_Handle_Paths_With_Embedded_Scope()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("path", nodeUtil.NormalizeNodePath("/scope/path/", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("scope/path", nodeUtil.NormalizeNodePath("/scope/path/", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));
       }

       [Fact]
       public void Can_Handle_Paths_With_Embedded_Project_Scope()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("path", nodeUtil.NormalizeNodePath("/projectName/scope/path/", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("projectName/scope/path", nodeUtil.NormalizeNodePath("/scope/path/", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));
       }

       [Fact]
       public void Can_Strip_Extra_Separators()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("projectName/scope/path1/path2", nodeUtil.NormalizeNodePath("/path1//path2///", "/projectName/",
               scope: "/scope/",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));
       }

       [Fact]
       public void Can_Handle_Root_Paths()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("", nodeUtil.NormalizeNodePath("//", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("projectName", nodeUtil.NormalizeNodePath("//", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("scope", nodeUtil.NormalizeNodePath("//", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("projectName/scope", nodeUtil.NormalizeNodePath("//", "projectName",
               scope: "scope",
               includeScope: true,
               includeTeamProject: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '/'));

           Assert.Equal("/", nodeUtil.NormalizeNodePath("//", "projectName",
               scope: "scope",
               includeScope: false,
               includeTeamProject: false,
               includeLeadingSeparator: true,
               includeTrailingSeparator: false,
               separator: '/'));
       }

       [Fact]
       public void Can_Handle_Backslash_Root_Paths()
       {
           var nodeUtil = new NodeUtilImpl();

           Assert.Equal("", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: false,
               includeScope: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));

           Assert.Equal("projectName", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: true,
               includeScope: false,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));

           Assert.Equal("scope", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: false,
               includeScope: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));

           Assert.Equal("projectName\\scope", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: true,
               includeScope: true,
               includeLeadingSeparator: false,
               includeTrailingSeparator: false,
               separator: '\\'));

           Assert.Equal("\\", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: false,
               includeScope: false,
               includeLeadingSeparator: true,
               includeTrailingSeparator: false,
               separator: '\\'));

           Assert.Equal("\\", nodeUtil.NormalizeNodePath("\\", "projectName",
               scope: "scope",
               includeTeamProject: false,
               includeScope: false,
               includeLeadingSeparator: true,
               includeTrailingSeparator: true,
               separator: '\\'));
       }
    }
}