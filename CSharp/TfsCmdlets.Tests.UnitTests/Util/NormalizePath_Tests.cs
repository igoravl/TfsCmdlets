//using System;
//using TfsCmdlets.Util;
//using Xunit;

//namespace TfsCmdlets.Tests.UnitTests.Util
//{
//    public class NormalizePath_Tests
//    {
//        // NormalizeNodePath(string path, string projectName, string scope = "", bool includeScope = false, bool excludePath = false, bool includeLeadingSeparator = false, bool includeTrailingSeparator = false, bool includeTeamProject = false, char separator = '\\')

//        [Fact]
//        public void Throws_ArgumentNullException_On_Null_Path()
//        {
//            Assert.Throws<ArgumentNullException>("path", () =>
//                NodeUtil.NormalizeNodePath(null, "projectName")
//            );
//        }

//        [Fact]
//        public void Throws_ArgumentNullException_On_Null_Project()
//        {
//            Assert.Throws<ArgumentNullException>("projectName", () =>
//                NodeUtil.NormalizeNodePath("path", null)
//            );
//        }

//        [Fact]
//        public void Throws_When_Everything_Excluded()
//        {
//            Assert.Throws<ArgumentException>(() =>
//                NodeUtil.NormalizeNodePath("path", "projectName",
//                    scope: "scope",
//                    includeScope: false,
//                    includeTeamProject: false,
//                    includeLeadingSeparator: false,
//                    includeTrailingSeparator: false,
//                    excludePath: true,
//                    separator: '\\'));
//        }

//        [Fact]
//        public void Throws_When_Project_Included_But_Missing()
//        {
//            Assert.Throws<ArgumentNullException>("projectName", () =>
//                NodeUtil.NormalizeNodePath("path", "", includeTeamProject: true)
//            );
//        }

//        [Fact]
//        public void Throws_When_Scope_Included_But_Missing()
//        {
//            Assert.Throws<ArgumentNullException>("scope", () =>
//                NodeUtil.NormalizeNodePath("path", "projectName", includeScope: true)
//            );
//        }

//        [Fact]
//        public void Can_Return_Path_Only()
//        {
//            Assert.Equal("path", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_With_Leading_Sep()
//        {
//            Assert.Equal("\\path", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: true,
//                includeTrailingSeparator: false,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_With_Trailing_Sep()
//        {
//            Assert.Equal("path\\", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: true,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_With_Leading_Trailing_Sep()
//        {
//            Assert.Equal("\\path\\", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: true,
//                includeTrailingSeparator: true,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_Project()
//        {
//            Assert.Equal("projectName\\path", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: true,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_Scope()
//        {
//            Assert.Equal("scope\\path", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Return_Path_Project_Scope()
//        {
//            Assert.Equal("projectName\\scope\\path", NodeUtil.NormalizeNodePath("path", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: true,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '\\'));
//        }

//        [Fact]
//        public void Can_Use_Custom_Separators()
//        {
//            Assert.Equal("_projectName_scope_path1_path2_", NodeUtil.NormalizeNodePath("path1/path2", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: true,
//                includeLeadingSeparator: true,
//                includeTrailingSeparator: true,
//                separator: '_'));

//            Assert.Equal("_projectName_scope_path1_path2_", NodeUtil.NormalizeNodePath("path1\\path2", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: true,
//                includeLeadingSeparator: true,
//                includeTrailingSeparator: true,
//                separator: '_'));
//        }

//        [Fact]
//        public void Can_Handle_Paths_With_Embedded_Project()
//        {
//            Assert.Equal("path", NodeUtil.NormalizeNodePath("/projectName/path/", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));

//            Assert.Equal("projectName/path", NodeUtil.NormalizeNodePath("/projectName/path/", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: true,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));
//        }

//        [Fact]
//        public void Can_Handle_Paths_With_Embedded_Scope()
//        {
//            Assert.Equal("path", NodeUtil.NormalizeNodePath("/scope/path/", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));

//            Assert.Equal("scope/path", NodeUtil.NormalizeNodePath("/scope/path/", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));
//        }

//        [Fact]
//        public void Can_Handle_Paths_With_Embedded_Project_Scope()
//        {
//            Assert.Equal("path", NodeUtil.NormalizeNodePath("/projectName/scope/path/", "projectName",
//                scope: "scope",
//                includeScope: false,
//                includeTeamProject: false,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));

//            Assert.Equal("projectName/scope/path", NodeUtil.NormalizeNodePath("/scope/path/", "projectName",
//                scope: "scope",
//                includeScope: true,
//                includeTeamProject: true,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));
//        }

//        [Fact]
//        public void Can_Strip_Extra_Separators()
//        {
//            Assert.Equal("projectName/scope/path1/path2", NodeUtil.NormalizeNodePath("/path1//path2///", "/projectName/",
//                scope: "/scope/",
//                includeScope: true,
//                includeTeamProject: true,
//                includeLeadingSeparator: false,
//                includeTrailingSeparator: false,
//                separator: '/'));
//        }
//    }
//}