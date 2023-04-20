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
                case ConsoleKey.W: NavManager.MoveUp(1); break;
                case ConsoleKey.S: NavManager.MoveDown(1); break;

                case ConsoleKey.A: NavManager.MoveBack(1); Console.Clear(); break;
                case ConsoleKey.D: NavManager.MoveForward(); Console.Clear(); break;
                
                case ConsoleKey.U: NavManager.Undo(); Console.Clear(); break;
                
                case ConsoleKey.Tab: NavManager.FilesSelected = !NavManager.FilesSelected; Console.Clear(); break;

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
                        
                        int found = FileManager.FindFile(totalInput.ToString(), NavManager.CurrentPath);
                        if (found == -1) continue;
                        NavManager.Selected = found;
                        
                        Render();
                        Console.Write($"Searching for: {totalInput}");
                    }
                    Console.Clear();
                    break;
            }
        }
    }

    private static void Render(bool setCursorPos = true) {
        if (setCursorPos) Console.SetCursorPosition(0, 0);

        if (NavManager.FilesSelected) {
            Renderer.FilesSelectedRender(NavManager.GetFSOInfos(), NavManager.Selected);
        }
        else {
            Renderer.SidebarSelectedRender(NavManager.GetFSOInfos(), NavManager.SidebarSelected);
        }
    }
}