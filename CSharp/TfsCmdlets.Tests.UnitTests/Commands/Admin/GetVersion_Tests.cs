﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using TfsCmdlets.Controllers.Admin;
using TfsCmdlets.Models;
using TfsCmdlets.Services;
using Xunit;

namespace TfsCmdlets.Tests.UnitTests.Commands.Admin
{
    public class GetVersion_Tests
    {
        [Fact]
        public void Can_Get_Hosted_Version()
        {
            // Arrange

            var fixture = new CommandsFixture();

            var tfsVersionTable = Substitute.For<ITfsVersionTable>();

            var restApi = Substitute.For<IRestApiService>();
            restApi.InvokeAsync(null, null).ReturnsForAnyArgs(fixture.GetAsyncResponseFromString(@"
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" dir=""ltr""  lang=""en-US"" >

<head>
  <meta name=""msapplication-config"" content=""none"" />
  <meta http-equiv=""X-UA-Compatible"" content=""IE=11"" />
  
  <link id=""page-icon"" rel=""shortcut icon""  href=""https://cdn.vsassets.io/content/icons/favicon.ico""  />
  
  <meta name=""referrer"" content=""strict-origin-when-cross-origin"" /><script data-description=""contentInit"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.$DEBUG = false;window.__scriptType = ""ES2017"";window.__moduleNamespaces = [""create-react-class"",""prop-types"",""react"",""react-dom"",""react-dom-factories"",""react-transition-group"",""tslib"",""VSS/Core"",""mousetrap"",""VSS/Platform"",""VSSUI"",""VSS/Legacy"",""VSS/Features/PlatformUI"",""Tfs/Platform"",""Search/LegacyWebApi"",""Search/Coordinator"",""TFS/Services"",""VSS/Ext"",""TFS/GlobalBanner"",""TFS/Views/SuiteMePage"",""VSS/Features/Services"",""Tfs/Frame"",""VSS/Features/Search""];window.__asyncModules = [];window.__extensionsStaticRoot = ""https://cdn.vsassets.io/ext/"";window.__extensionsLocalStaticRoot = ""/tshooter/_static/_ext/"";window.resourceLoadStrategy = 'stylesFirst';var preTraceServiceStartErrors = []; var onPreTraceServiceError = function (event) { preTraceServiceStartErrors.push(event) }; window.addEventListener('error', onPreTraceServiceError);var previousResourceLoadCompletion = window.performance.now(); var resourceLoadTimes = {}; window.logResourceLoad = function (id) {var now = window.performance.now(); resourceLoadTimes[id] = { current: now, previous: previousResourceLoadCompletion }; previousResourceLoadCompletion = now; }; </script><script data-description=""disableCdn"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">function cdnReload(fromTimeout) { 
document.cookie = 'TFS-CDN=disabled;max-age=28800;path=/;secure'; 
document.cookie = 'TFS-CDNTRACE=report;path=/;secure'; 
if(fromTimeout){document.cookie = 'TFS-CDNTIMEOUT=report;path=/;secure'; }
document.location.reload(true); 
}
var cdnTimeout = setTimeout(function () { cdnReload(true); }, 10000);</script><script data-description=""trackLoadedBundles"" nonce=""U/Xr4zNpez85U/QLO8vQkw==""> var loadedScripts = {}; document.addEventListener('scriptLoaded', function (e) { if (e && e.detail) { loadedScripts[e.detail.id] = true;} });</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-web/core-content/ms.vss-web.core-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""12509"" data-clientid=""ms.vss-web.core-content/core-content/ms.vss-web.core-content.min.css"" data-sourcecontribution=""ms.vss-web.core-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.core-content/core-content/ms.vss-web.core-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-web/platform-content/ms.vss-web.platform-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""4114"" data-clientid=""ms.vss-web.platform-content/platform-content/ms.vss-web.platform-content.min.css"" data-sourcecontribution=""ms.vss-web.platform-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.platform-content/platform-content/ms.vss-web.platform-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-core-content/ms.vss-features.ui-core-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""30448"" data-clientid=""ms.vss-features.ui-core-content/ui-core-content/ms.vss-features.ui-core-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-core-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-core-content/ui-core-content/ms.vss-features.ui-core-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-spinner-content/ms.vss-features.ui-spinner-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""854"" data-clientid=""ms.vss-features.ui-spinner-content/ui-spinner-content/ms.vss-features.ui-spinner-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-spinner-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-spinner-content/ui-spinner-content/ms.vss-features.ui-spinner-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-widgets-content/ms.vss-features.ui-widgets-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""36520"" data-clientid=""ms.vss-features.ui-widgets-content/ui-widgets-content/ms.vss-features.ui-widgets-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-widgets-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-widgets-content/ui-widgets-content/ms.vss-features.ui-widgets-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-input-content/ms.vss-features.ui-input-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""6704"" data-clientid=""ms.vss-features.ui-input-content/ui-input-content/ms.vss-features.ui-input-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-input-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-input-content/ui-input-content/ms.vss-features.ui-input-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-page-content/ms.vss-features.ui-page-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""11547"" data-clientid=""ms.vss-features.ui-page-content/ui-page-content/ms.vss-features.ui-page-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-page-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-page-content/ui-page-content/ms.vss-features.ui-page-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/platform-ui-content/ms.vss-features.platform-ui-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""72"" data-clientid=""ms.vss-features.platform-ui-content/platform-ui-content/ms.vss-features.platform-ui-content.min.css"" data-sourcecontribution=""ms.vss-features.platform-ui-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.platform-ui-content/platform-ui-content/ms.vss-features.platform-ui-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-tfs-web/platform-content/ms.vss-tfs-web.platform-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""1018"" data-clientid=""ms.vss-tfs-web.platform-content/platform-content/ms.vss-tfs-web.platform-content.min.css"" data-sourcecontribution=""ms.vss-tfs-web.platform-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.platform-content/platform-content/ms.vss-tfs-web.platform-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-layer-content/ms.vss-features.ui-layer-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""4363"" data-clientid=""ms.vss-features.ui-layer-content/ui-layer-content/ms.vss-features.ui-layer-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-layer-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-layer-content/ui-layer-content/ms.vss-features.ui-layer-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-card-content/ms.vss-features.ui-card-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""1232"" data-clientid=""ms.vss-features.ui-card-content/ui-card-content/ms.vss-features.ui-card-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-card-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-card-content/ui-card-content/ms.vss-features.ui-card-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-web/ext-content/ms.vss-web.ext-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""497"" data-clientid=""ms.vss-web.ext-content/ext-content/ms.vss-web.ext-content.min.css"" data-sourcecontribution=""ms.vss-web.ext-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.ext-content/ext-content/ms.vss-web.ext-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-tfs-web/global-banner-content/ms.vss-tfs-web.global-banner-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""124"" data-clientid=""ms.vss-tfs-web.global-banner-content/global-banner-content/ms.vss-tfs-web.global-banner-content.min.css"" data-sourcecontribution=""ms.vss-tfs-web.global-banner-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.global-banner-content/global-banner-content/ms.vss-tfs-web.global-banner-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-tfs-web/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""10636"" data-clientid=""ms.vss-tfs-web.suite-me-page-content/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.min.css"" data-sourcecontribution=""ms.vss-tfs-web.suite-me-page-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.suite-me-page-content/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""536"" data-clientid=""ms.vss-features.ui-progress-bar-content/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.min.css"" data-sourcecontribution=""ms.vss-features.ui-progress-bar-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-progress-bar-content/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-tfs-web/frame-content/ms.vss-tfs-web.frame-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""10681"" data-clientid=""ms.vss-tfs-web.frame-content/frame-content/ms.vss-tfs-web.frame-content.min.css"" data-sourcecontribution=""ms.vss-tfs-web.frame-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.frame-content/frame-content/ms.vss-tfs-web.frame-content.min.css');</script><link href=""https://cdn.vsassets.io/v/M196_20211215.8/_ext/ms.vss-features/search-content/ms.vss-features.search-content.min.css"" rel=""stylesheet"" crossorigin=""anonymous"" data-contentlength=""3410"" data-clientid=""ms.vss-features.search-content/search-content/ms.vss-features.search-content.min.css"" data-sourcecontribution=""ms.vss-features.search-content"" data-loadingcontent=""true"" /><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.search-content/search-content/ms.vss-features.search-content.min.css');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-web/core-content/ms.vss-web.core-content.es6.cnS3PJO62VXcxG3V.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""184149"" data-clientid=""ms.vss-web.core-content/core-content/ms.vss-web.core-content.es6.min.js"" data-sourcecontribution=""ms.vss-web.core-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.core-content/core-content/ms.vss-web.core-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-web/platform-content/ms.vss-web.platform-content.es6.NSbq23FcOw4zcRso.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""108485"" data-clientid=""ms.vss-web.platform-content/platform-content/ms.vss-web.platform-content.es6.min.js"" data-sourcecontribution=""ms.vss-web.platform-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.platform-content/platform-content/ms.vss-web.platform-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-core-content/ms.vss-features.ui-core-content.es6.T_BcMwRpevWkD0zk.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""78067"" data-clientid=""ms.vss-features.ui-core-content/ui-core-content/ms.vss-features.ui-core-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-core-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-core-content/ui-core-content/ms.vss-features.ui-core-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-spinner-content/ms.vss-features.ui-spinner-content.es6.t__tN8vLw1_7SjPcI.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""1335"" data-clientid=""ms.vss-features.ui-spinner-content/ui-spinner-content/ms.vss-features.ui-spinner-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-spinner-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-spinner-content/ui-spinner-content/ms.vss-features.ui-spinner-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-widgets-content/ms.vss-features.ui-widgets-content.es6.2BrVbuOfEweEYg0c.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""122354"" data-clientid=""ms.vss-features.ui-widgets-content/ui-widgets-content/ms.vss-features.ui-widgets-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-widgets-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-widgets-content/ui-widgets-content/ms.vss-features.ui-widgets-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-input-content/ms.vss-features.ui-input-content.es6.y2t0d44OCRSe5TwV.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""12982"" data-clientid=""ms.vss-features.ui-input-content/ui-input-content/ms.vss-features.ui-input-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-input-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-input-content/ui-input-content/ms.vss-features.ui-input-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-page-content/ms.vss-features.ui-page-content.es6.pjdC__HqKMzCzgeyU.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""23763"" data-clientid=""ms.vss-features.ui-page-content/ui-page-content/ms.vss-features.ui-page-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-page-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-page-content/ui-page-content/ms.vss-features.ui-page-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-web/legacy-content/ms.vss-web.legacy-content.es6.1CeJr1SgmqCNvnhJ.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""7210"" data-clientid=""ms.vss-web.legacy-content/legacy-content/ms.vss-web.legacy-content.es6.min.js"" data-sourcecontribution=""ms.vss-web.legacy-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.legacy-content/legacy-content/ms.vss-web.legacy-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/platform-ui-content/ms.vss-features.platform-ui-content.es6.1QBqwG5mCGGtiZY__.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""22053"" data-clientid=""ms.vss-features.platform-ui-content/platform-ui-content/ms.vss-features.platform-ui-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.platform-ui-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.platform-ui-content/platform-ui-content/ms.vss-features.platform-ui-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-tfs-web/platform-content/ms.vss-tfs-web.platform-content.es6.mO6m0QS1__4HbcGXF.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""21174"" data-clientid=""ms.vss-tfs-web.platform-content/platform-content/ms.vss-tfs-web.platform-content.es6.min.js"" data-sourcecontribution=""ms.vss-tfs-web.platform-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.platform-content/platform-content/ms.vss-tfs-web.platform-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-search-web/search-legacywebapi/ms.vss-search-web.search-legacywebapi.es6.tpTSVuPfbhh8dBlY.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""5726"" data-clientid=""ms.vss-search-web.search-legacywebapi/search-legacywebapi/ms.vss-search-web.search-legacywebapi.es6.min.js"" data-sourcecontribution=""ms.vss-search-web.search-legacywebapi"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-search-web.search-legacywebapi/search-legacywebapi/ms.vss-search-web.search-legacywebapi.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-search-web/search-coordinator/ms.vss-search-web.search-coordinator.es6.dSfGgrv5vCChHpmE.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""4032"" data-clientid=""ms.vss-search-web.search-coordinator/search-coordinator/ms.vss-search-web.search-coordinator.es6.min.js"" data-sourcecontribution=""ms.vss-search-web.search-coordinator"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-search-web.search-coordinator/search-coordinator/ms.vss-search-web.search-coordinator.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-layer-content/ms.vss-features.ui-layer-content.es6.Wp55VNcWF3vwnn5_.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""15967"" data-clientid=""ms.vss-features.ui-layer-content/ui-layer-content/ms.vss-features.ui-layer-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-layer-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-layer-content/ui-layer-content/ms.vss-features.ui-layer-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-tfs-web/services-content/ms.vss-tfs-web.services-content.es6.ZGJDoc7F0PbKhNcf.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""10178"" data-clientid=""ms.vss-tfs-web.services-content/services-content/ms.vss-tfs-web.services-content.es6.min.js"" data-sourcecontribution=""ms.vss-tfs-web.services-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.services-content/services-content/ms.vss-tfs-web.services-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-card-content/ms.vss-features.ui-card-content.es6.WQMVox8c_b07P3l2.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""3619"" data-clientid=""ms.vss-features.ui-card-content/ui-card-content/ms.vss-features.ui-card-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-card-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-card-content/ui-card-content/ms.vss-features.ui-card-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-web/ext-content/ms.vss-web.ext-content.es6.INlJsTyLwLKHefUp.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""21278"" data-clientid=""ms.vss-web.ext-content/ext-content/ms.vss-web.ext-content.es6.min.js"" data-sourcecontribution=""ms.vss-web.ext-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-web.ext-content/ext-content/ms.vss-web.ext-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-tfs-web/global-banner-content/ms.vss-tfs-web.global-banner-content.es6.C1yEK9u__xNydusiB.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""11281"" data-clientid=""ms.vss-tfs-web.global-banner-content/global-banner-content/ms.vss-tfs-web.global-banner-content.es6.min.js"" data-sourcecontribution=""ms.vss-tfs-web.global-banner-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.global-banner-content/global-banner-content/ms.vss-tfs-web.global-banner-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-tfs-web/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.es6.1hNOTR0Jgwx3ZRbI.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""15383"" data-clientid=""ms.vss-tfs-web.suite-me-page-content/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.es6.min.js"" data-sourcecontribution=""ms.vss-tfs-web.suite-me-page-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.suite-me-page-content/suite-me-page-content/ms.vss-tfs-web.suite-me-page-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.es6.5p6SHULt8DfseHFc.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""3312"" data-clientid=""ms.vss-features.ui-progress-bar-content/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.ui-progress-bar-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.ui-progress-bar-content/ui-progress-bar-content/ms.vss-features.ui-progress-bar-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/services-content/ms.vss-features.services-content.es6.wL3mzx7zoM_gmyTl.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""2579"" data-clientid=""ms.vss-features.services-content/services-content/ms.vss-features.services-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.services-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.services-content/services-content/ms.vss-features.services-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-tfs-web/frame-content/ms.vss-tfs-web.frame-content.es6.fcKjFGNcrC1Bw125.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""47689"" data-clientid=""ms.vss-tfs-web.frame-content/frame-content/ms.vss-tfs-web.frame-content.es6.min.js"" data-sourcecontribution=""ms.vss-tfs-web.frame-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-tfs-web.frame-content/frame-content/ms.vss-tfs-web.frame-content.es6.min.js');</script><script src=""https://cdn.vsassets.io/ext/ms.vss-features/search-content/ms.vss-features.search-content.es6._dqQiqd7vJXu6tPk.min.js"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="" data-contentlength=""22668"" data-clientid=""ms.vss-features.search-content/search-content/ms.vss-features.search-content.es6.min.js"" data-sourcecontribution=""ms.vss-features.search-content"" data-loadingcontent=""true""></script><script data-description=""logResourceLoad"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.logResourceLoad('ms.vss-features.search-content/search-content/ms.vss-features.search-content.es6.min.js');</script><script data-description=""cdnFallback"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">if (!window.LWL) { cdnReload(false); }</script><script data-description=""clearCdnTimeout"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">window.clearTimeout(cdnTimeout);</script><script data-description=""fireLoadEvents"" nonce=""U/Xr4zNpez85U/QLO8vQkw=="">var contentElements = document.querySelectorAll(""[data-loadingcontent=true]""); for (var i=0; i<contentElements.length; i++) { var loadTimes = resourceLoadTimes[contentElements[i].getAttribute('data-clientid')]; window.__contentLoaded(contentElements[i], true, loadTimes && loadTimes.current, loadTimes && loadTimes.previous); }</script>
  
  <script id=""dataProviders"" type=""application/json"">{""data"":{""ms.vss-web.component-data"":{""content"":[{""componentId"":""ms.vss-tfs-web.suite-me-page-view-component"",""componentType"":""suiteMePageView""}],""page"":[{""componentId"":""ms.vss-tfs-web.suite-collection-me-page-component"",""componentType"":""layout"",""componentProperties"":{""className"":""full-size vertical-layout"",""orientation"":""column"",""components"":[{""componentType"":""ms.vss-tfs-web.frame.top-level-header"",""errorBoundaryProps"":{""handlerComponentType"":""tfs.page-error-handler""}},{""componentType"":""layout"",""componentProperties"":{""className"":""flex-grow v-scroll-auto"",""orientation"":""row"",""components"":[{""componentType"":""ms.vss-tfs-web.frame.top-level-navigation"",""errorBoundaryProps"":{""handlerComponentType"":""tfs.page-error-handler""}},{""componentType"":""layout"",""componentProperties"":{""className"":""flex-grow scroll-hidden"",""orientation"":""column"",""components"":[{""componentRegion"":""content-header"",""componentProperties"":{""className"":""flex-row flex-noshrink""}},{""componentRegion"":""content"",""componentProperties"":{""className"":""suite-me-page-content flex-row flex-grow relative v-scroll-auto"",""role"":""main""},""errorBoundaryProps"":{""handlerComponentType"":""tfs.page-error-handler""}},{""componentRegion"":""content-footer"",""componentProperties"":{""className"":""flex-row flex-noshrink""}}]}}]}},{""componentRegion"":""footer"",""componentProperties"":{""className"":""footer flex-row flex-noshrink""}}],""_sharedData"":{""routes"":[""ms.vss-admin-web.collection-admin-hub-route"",""ms.vss-admin-web.admin-access-levels-route""]}}}],""navigation"":[{""componentId"":""ms.vss-tfs-web.organization-navigation-component"",""componentType"":""organization-navigation""}],""header"":[{""componentId"":""ms.vss-tfs-web.top-navigation-header-button"",""componentType"":""ms.vss-tfs-web.frame.top-navigation-header-button"",""componentProperties"":{""componentOrder"":100,""key"":""top-navigation-header-button""}},{""componentId"":""ms.vss-tfs-web.suite-me-suite-logo"",""componentType"":""ms.vss-tfs-web.suite-logo"",""componentProperties"":{""componentOrder"":200,""key"":""suite-logo"",""routeId"":""ms.vss-tfs-web.suite-me-page-route"",""alwaysExpanded"":true}},{""componentId"":""ms.vss-tfs-web.vertical-header-composite"",""componentType"":""ms.vss-tfs-web.composite"",""componentProperties"":{""componentOrder"":10400,""key"":""composite""}},{""componentId"":""ms.vss-tfs-web.vertical-header-search"",""componentType"":""expandableSearchHeader"",""componentProperties"":{""componentOrder"":10000,""key"":""search""}},{""componentId"":""ms.vss-tfs-web.vertical-header-help"",""componentType"":""ms.vss-tfs-web.help"",""componentProperties"":{""componentOrder"":10390,""key"":""helpMenu""}},{""componentId"":""ms.vss-tfs-web.vertical-header-user-settings"",""componentType"":""ms.vss-tfs-web.user-settings"",""componentProperties"":{""componentOrder"":10400,""key"":""userSettingsMenu""}},{""componentId"":""ms.vss-tfs-web.vertical-header-marketplace"",""componentType"":""ms.vss-tfs-web.market"",""componentProperties"":{""componentOrder"":10380,""key"":""marketMenu""}},{""componentId"":""ms.vss-tfs-web.vertical-header-my-work-flyout"",""componentType"":""ms.vss-tfs-web.my-work"",""componentProperties"":{""componentOrder"":10200,""key"":""myWorkFlyout""}},{""componentId"":""ms.vss-tfs-web.vertical-header-me-control"",""componentType"":""ms.vss-tfs-web.me-control"",""componentProperties"":{""componentOrder"":10500,""key"":""me-control""}},{""componentId"":""ms.vss-tfs-web.vertical-header-filler"",""componentType"":""layoutFiller"",""componentProperties"":{""componentOrder"":5000,""adjustmentIgnore"":true,""key"":""filler""}}],""content-header"":[{""componentId"":""ms.vss-tfs-web.vertical-global-messages-component"",""componentType"":""globalMessages"",""componentProperties"":{""componentOrder"":100}},{""componentId"":""ms.vss-tfs-web.vertical-global-actions-component"",""componentType"":""globalActions"",""componentProperties"":{""componentOrder"":110}}]},""ms.vss-web.page-data"":{""esNextSupported"":true,""hostId"":""8d90cd1a-3cca-4fd5-9415-7a8345eea201"",""hostType"":4,""hostName"":""tshooter"",""hostPath"":""/tshooter/"",""serviceInstanceType"":""00025394-6065-48ca-87d9-7f5672854ef7"",""isBundlingEnabled"":true,""isCdnEnabled"":true,""isCdnAvailable"":true,""isHosted"":true,""isProduction"":true,""isDebugMode"":false,""isDevfabric"":false,""isSslOnly"":true,""isTracePointCollectionEnabled"":false,""scriptType"":""ES2017"",""serviceVersion"":""Dev18.M196.1 (AzureDevOps_M196_20211215.8)"",""sessionId"":""f94b9bd1-20dd-44be-9666-96b27c30fc6d""}</script>
  
  
  <title>Projects</title>
</head>

<body class=""lwp"">
  <noscript><div class=""absolute-fill flex-column flex-center""><h2>JavaScript is Disabled</h2><p>Please enable javascript and refresh the page</p></div></noscript>
  <input type=""hidden"" name=""__RequestVerificationToken"" value=""DbFti50naoRCHXZB3PsY8_GEORkZbLbXbilzOND9KBS5-ZrXudjHaRamdLQ2"" />
  <div data-componentregion=""page"" class=""full-size""></div>
  <div class=""bolt-portal-host absolute-fill no-events scroll-hidden""></div>
</body>

</html>
"));

            var powerShell = Substitute.For<IPowerShellService>();

            var data = Substitute.For<IDataManager>();
            data.GetCollection(null).ReturnsForAnyArgs(fixture.GetAzdoCollection());

            var logger = Substitute.For<ILogger>();

            var parms = Substitute.For<IParameterManager>();

            var cmd = new GetVersionController(tfsVersionTable, restApi, powerShell, data, parms, logger);

            // Act

            var actual = cmd.Invoke().First();

            // Assert

            Assert.True(actual.IsHosted); ;
        }
    }
}
