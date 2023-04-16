using System.Runtime.Serialization;
using System.Text;
using Elderberry.Managers;

namespace Elderberry.TUI;

internal static class Renderer {
    private const int SEP = 6; // represents the space in between the sidebar and contents

    private static readonly string[] _sidebarContents = {
        "Home",
        "Quick Access",
        "My Drive",
        "Important Local Files",
        "Personal Downloads"
    };

    public static void FilesSelectedRender(FSOInfo[] contents, int selected) {
        var sb = new StringBuilder();

        int sidebarLength = Console.WindowWidth / 4;
        int contentsLength = (int)((Console.WindowWidth - sidebarLength - SEP) / 3);
        string[] sidebar = Formatter.FormatVertically(_sidebarContents, Console.WindowWidth / 4);
        
        sb.Append(Navigator.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', sidebarLength + SEP));
        sb.Append("Name");
        sb.Append(new string('-', contentsLength));
        sb.Append("Size");
        sb.Append(new string('-', contentsLength));
        sb.Append("Modified");
        sb.Append(new string('-', contentsLength - 16));
        sb.Append('\n');

        int contentStart = selected >= sidebar.Length ? Math.Abs(selected - sidebar.Length) : 0;
        for (int i = 0; i < sidebar.Length; i++) {
            sb.Append(sidebar[i]);
            sb.Append("  | ");
            sb.Append(i == selected && i < contents.Length ? "* " : "  ");
            if (i + contentStart < contents.Length) {
                sb.Append(Formatter.FormatString(contents[i + contentStart].Name, contentsLength, 4));
                sb.Append(Formatter.FormatString(contents[i + contentStart].Size, contentsLength, 4));
                sb.Append(contents[i + contentStart].DateModified);
            }
            sb.Append('\n');
        }

        Console.Write(sb.ToString());
    }

    public static void SidebarSelectedRender(FSOInfo[] contents, int selected) {
        var sb = new StringBuilder();

        // new sidebar size
        int maxLength = Math.Max(_sidebarContents.Max(s => s.Length), Console.WindowWidth / 4);

        sb.Append(Navigator.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', Console.WindowWidth));
        sb.Append('\n');

        string[] sidebar = Formatter.FormatVertically(_sidebarContents, maxLength);
        for (int i = 0; i < sidebar.Length; i++) {
            sb.Append(i == selected ? " * " : "   ");
            sb.Append(sidebar[i]);
            sb.Append(" | ");
            if (i < contents.Length) sb.Append(contents[i].Name);
            sb.Append('\n');
        }

        Console.Write(sb.ToString());
    }
}