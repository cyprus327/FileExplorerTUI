using Elderberry.IO;
using Elderberry.TUI;

namespace Elderberry;

internal sealed class Program {
    private static void Main() {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;

        while (true) {
            Console.Clear();

            if (Navigator.FilesSelected) {
                Renderer.FilesSelectedRender(
                    contents: Navigator.GetCurrentContentsTrimmed().ToArray(),
                    selected: Navigator.Selected);
            } else {
                Renderer.SidebarSelectedRender(
                    contents: Navigator.GetCurrentContentsTrimmed().ToArray(),
                    selected: Navigator.SidebarSelected);
            }

            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.W: Navigator.MoveUp(1); break;
                case ConsoleKey.S: Navigator.MoveDown(1); break;
                case ConsoleKey.A: Navigator.MoveBack(1); break;
                case ConsoleKey.D: Navigator.MoveForward(); break;
                case ConsoleKey.U: Navigator.Undo(); break;
                case ConsoleKey.Tab: Navigator.FilesSelected = !Navigator.FilesSelected; break;
            }
        }
    }
}