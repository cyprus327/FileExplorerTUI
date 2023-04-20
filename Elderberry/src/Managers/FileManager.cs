namespace Elderberry.Managers;

internal static class FileManager {
    public static int FindFile(string name, string path) {
        if (path != NavManager.CurrentPath) {
            // TODO handle this case
            return 0;
        }

        int index = -1, i = 0;
        foreach (var item in NavManager.GetCurrentContents()) {
            if (item.ToLower().Contains(name.ToLower())) {
                index = i;
                break;
            }
            i++;
        }

        return index;
    }

    public static void CreateDirectory(string path, string dirName, out string error) {
        try {
            Directory.CreateDirectory(path + Path.PathSeparator + dirName);
        }
        catch (Exception ex) {
            error = ex.Message;
            return;
        }
        error = string.Empty;
    }

    public static void CreateFile(string path, string fileName, out string error) {
        try {
            File.Create(path + Path.PathSeparator + fileName);
        }
        catch (Exception ex) {
            error = ex.Message;
            return;
        }
        error = string.Empty;
    }
}

internal struct FSOInfo {
    public FSOInfo(string name) {
        Name = name;
        DateModified = string.Empty;
        Size = string.Empty;
    }

    public string Name;
    public string DateModified;
    public string Size;

    public string[] All => new string[3] { Name, Size, DateModified };
}