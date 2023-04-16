using Elderberry.IO;
using System.Text;

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

    public static void FilesSelectedRender(string[] contents, int selected) {
        StringBuilder sb = new StringBuilder();

        sb.Append(Navigator.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', Console.WindowWidth));
        sb.Append('\n');

        string[] sidebar = Formatter.FormatVertically(_sidebarContents, 16);
        for (int i = 0; i < sidebar.Length; i++) {
            sb.Append(sidebar[i]);
            sb.Append("  | ");
            sb.Append(i == selected && i < contents.Length ? "* " : "  ");
            if (i < contents.Length) sb.Append(contents[i]);
            sb.Append('\n');
        }

        Console.Write(sb.ToString());
    }

    public static void SidebarSelectedRender(string[] contents, int selected) {
        StringBuilder sb = new StringBuilder();

        // new sidebar size
        int maxLength = _sidebarContents.Max(s => s.Length);

        sb.Append(Navigator.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', Console.WindowWidth));
        sb.Append('\n');

        string[] sidebar = Formatter.FormatVertically(_sidebarContents, maxLength);
        for (int i = 0; i < sidebar.Length; i++) {
            sb.Append(i == selected ? " * " : "   ");
            sb.Append(sidebar[i]);
            sb.Append(" | ");
            if (i < contents.Length) sb.Append(contents[i]);
            sb.Append('\n');
        }

        Console.Write(sb.ToString());
    }
}