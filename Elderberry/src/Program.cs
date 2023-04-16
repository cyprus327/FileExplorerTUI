using Elderberry.Managers;
using Elderberry.TUI;
using System.Text;

namespace Elderberry;

internal sealed class Program {
    private static void Main() {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;

        while (true) {
            Render();

            switch (Console.ReadKey(true).Key) {
                case ConsoleKey.W: Navigator.MoveUp(1); break;
                case ConsoleKey.S: Navigator.MoveDown(1); break;
                case ConsoleKey.A: Navigator.MoveBack(1); Console.Clear(); break;
                case ConsoleKey.D: Navigator.MoveForward(); Console.Clear(); break;
                case ConsoleKey.U: Navigator.Undo(); Console.Clear(); break;
                case ConsoleKey.Tab: Navigator.FilesSelected = !Navigator.FilesSelected; Console.Clear(); break;

                case ConsoleKey.F:
                    var totalInput = new StringBuilder();
                    for (int i = 0; i < 50; i++) { // safety limit
                        var input = Console.ReadKey();
                        if (input.Key == ConsoleKey.Escape || input.Key == ConsoleKey.Enter) break;

                        if (input.Key == ConsoleKey.Backspace) {
                            totalInput.Remove(totalInput.Length - 1, 1);
                        } else {
                            totalInput.Append(input.KeyChar);
                        }
                        
                        int found = FileManager.FindFile(totalInput.ToString(), Navigator.CurrentPath);
                        if (found == -1) continue;
                        Navigator.Selected = found;
                        
                        Render();
                        Console.Write(totalInput);
                    }
                    Console.Clear();
                    break;
            }
        }
    }

    private static void Render() {
        Console.SetCursorPosition(0, 0);

        if (Navigator.FilesSelected) {
            Renderer.FilesSelectedRender(Navigator.GetFSOInfos(), Navigator.Selected);
        } else {
            Renderer.SidebarSelectedRender(Navigator.GetFSOInfos(), Navigator.SidebarSelected);
        }
    }
}