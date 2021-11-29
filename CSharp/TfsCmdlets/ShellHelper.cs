using System;
using System.Collections.Generic;
using TfsCmdlets.Services;

namespace TfsCmdlets
{
    public static class ShellHelper
    {
        public static string GetPrompt()
        {
            const string ANSI_BG_BLUE = "\x1b[44m";
            const string ANSI_BG_CYAN = "\x1b[46m";
            const string ANSI_BG_MAGENTA = "\x1b[45m";
            const string ANSI_BG_GRAY = "\x1b[40;1m";
            const string ANSI_FG_GRAY = "\x1b[30;1m";
            const string ANSI_FG_WHITE = "\x1b[37;1m";
            const string ANSI_RESET = "\x1b[40m\x1b[0m";
            var NOT_CONNECTED = $"{Environment.NewLine}{ANSI_BG_GRAY}{ANSI_FG_GRAY}[Not connected]{ANSI_RESET}{Environment.NewLine}";

            var connections = ServiceLocator.Instance.GetExport<ICurrentConnections>();

            List<string> segments;
            string colorScheme;

            if (connections.Collection is { } tpc)
            {
                colorScheme = tpc.IsHosted ? $"{ANSI_BG_CYAN}{ANSI_FG_WHITE}" : $"{ANSI_BG_MAGENTA}{ANSI_FG_WHITE}";

                segments = new List<string>();

                switch (tpc.Uri.Host.ToLowerInvariant())
                {
                    case "dev.azure.com":
                    {
                        segments.Add("dev.azure.com");
                        segments.Add(tpc.DisplayName);
                        break;
                    }
                    case { } s when s.EndsWith(".visualstudio.com"):
                    {
                        segments.Add(s);
                        break;
                    }
                    default:
                    {
                        segments.Add(tpc.Uri.Host);
                        break;
                    }
                }
            }
            else
            {
                return NOT_CONNECTED;
            }

            if (connections.Project is { } tp) segments.Add(tp.Name);

            if (connections.Team is { } t) segments.Add(t.Name);
            
            var userName = tpc.AuthorizedIdentity.UniqueName ??
                           tpc.AuthorizedIdentity.Properties["Account"].ToString();

            return $"{Environment.NewLine}{colorScheme}[{string.Join(" > ", segments.ToArray())} {ANSI_BG_BLUE}({userName}){colorScheme}]{ANSI_RESET}{Environment.NewLine}";
        }
    }
}