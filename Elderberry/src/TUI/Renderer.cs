﻿using System.Runtime.Serialization;
using System.Text;
using System.Xml.XPath;
using Elderberry.Managers;

namespace Elderberry.TUI;

internal static class Renderer {
    private const int SEP = 6; // represents the space in between the sidebar and contents

    private static readonly string[] _sidebarContentsElements = {
        NavManager.SidebarContents[0].element,
        NavManager.SidebarContents[1].element,
        NavManager.SidebarContents[2].element,
        NavManager.SidebarContents[3].element,
        NavManager.SidebarContents[4].element
    };

    public static void FilesSelectedRender(FSOInfo[] contents, int selected) {
        var sb = new StringBuilder();

        int sidebarWidth = Console.WindowWidth / 4;
        int contentsWidth = (int)((Console.WindowWidth - sidebarWidth - SEP) / 3);
        string[] sidebar = Formatter.FormatVertically(_sidebarContentsElements, sidebarWidth);
        
        sb.Append(NavManager.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', sidebarWidth + SEP));
        sb.Append("Name");
        sb.Append(new string('-', contentsWidth));
        sb.Append("Size");
        sb.Append(new string('-', contentsWidth));
        sb.Append("Modified");
        sb.Append(new string('-', Math.Max(0, contentsWidth - 16)));
        sb.Append('\n');

        int offset = Math.Max(0, selected - (sidebar.Length - 1));
        for (int i = 0; i < sidebar.Length; i++) {
            sb.Append(sidebar[i]);
            sb.Append("  | ");
            sb.Append(i + offset == selected && i + offset < contents.Length ? "* " : "  ");
            if (i + offset < contents.Length) {
                sb.Append(Formatter.FormatString(contents[i + offset].Name, contentsWidth, 4));
                sb.Append(Formatter.FormatString(contents[i + offset].Size, contentsWidth, 4));
                sb.Append(Formatter.FormatString(contents[i + offset].DateModified, contentsWidth / 2));
            }
            sb.Append('\n');
        }

        Console.Write(sb.ToString());
    }

    public static void SidebarSelectedRender(FSOInfo[] contents, int selected) {
        var sb = new StringBuilder();

        // new sidebar size
        int maxLength = Math.Max(_sidebarContentsElements.Max(s => s.Length), Console.WindowWidth / 4);

        sb.Append(NavManager.CurrentPath);
        sb.Append('\n');
        sb.Append(new string('-', Console.WindowWidth));
        sb.Append('\n');

        string[] sidebar = Formatter.FormatVertically(_sidebarContentsElements, maxLength);
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