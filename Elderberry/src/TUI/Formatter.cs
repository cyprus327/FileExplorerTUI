namespace Elderberry.TUI;

internal static class Formatter {
    public static string[] FormatVertically(string[] arr, int maxWidth, int additionalSpace = 0) {
        maxWidth = Math.Max(4, maxWidth);

        // -3 because 1 line for title 1 line for ------ and 1 line at the bottom removed
        string[] output = new string[Console.WindowHeight - 3];

        for (int i = 0; i < output.Length; i++) {
            output[i] = i < arr.Length ?
                FormatString(arr[i], maxWidth, additionalSpace) :
                new string(' ', maxWidth);
        }

        return output;
    }

    public static string FormatString(string str, int maxWidth, int addtionalSpace = 0) {
        maxWidth = Math.Max(4, maxWidth);

        return str.Length > maxWidth ?
            string.Concat(str[0..(maxWidth - 3)], "...", new string(' ', addtionalSpace)) :
            str.PadRight(maxWidth + addtionalSpace);
    }
}