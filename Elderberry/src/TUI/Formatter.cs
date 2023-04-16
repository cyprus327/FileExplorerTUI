namespace Elderberry.TUI;

internal static class Formatter {
    public static string[] FormatVertically(string[] arr, int maxWidth) {
        maxWidth = Math.Max(4, maxWidth);

        // -3 because 1 line for title 1 line for ------ and 1 line at the bottom removed
        string[] output = new string[Console.WindowHeight - 3];

        for (int i = 0; i < output.Length; i++) {
            output[i] = i < arr.Length ?
                arr[i].Length > maxWidth ?
                    string.Concat(arr[i][0..(maxWidth - 3)], "...") :
                    arr[i].PadRight(maxWidth) :
                new string(' ', maxWidth);
        }

        return output;
    }
}