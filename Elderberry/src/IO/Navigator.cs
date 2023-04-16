namespace Elderberry.IO;

internal static class Navigator {
    static Navigator() {
        _dirCache = new Stack<string>();

        CurrentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        directories = Directory.EnumerateDirectories(CurrentPath);
        files = Directory.EnumerateFiles(CurrentPath);
        _currentContents = new HashSet<string>(directories);
        _currentContents.UnionWith(files);
    }

    public static bool FilesSelected { get; set; } = true;
    public static int Selected { get; private set; } = 0;
    public static int SidebarSelected { get; private set; } = 0;

    public static string CurrentPath { get; private set; }

    private static IEnumerable<string> directories;
    private static IEnumerable<string> files;
    private static readonly HashSet<string> _currentContents;

    private static readonly Stack<string> _dirCache;

    public static HashSet<string> GetCurrentContents() => _currentContents;
    public static HashSet<string> GetCurrentContentsTrimmed() {
        var contents = new HashSet<string>();
        foreach (var item in _currentContents) {
            contents.Add(Path.GetFileName(item));
        }
        return contents;
    }

    public static void SetPath(string path, bool addToCache = true) {
        // if the file isn't a directory
        if ((File.GetAttributes(path) & FileAttributes.Directory) != FileAttributes.Directory) return;
        if (path == CurrentPath) return;

        Selected = 0;

        CurrentPath = path;
        try {
            directories = Directory.EnumerateDirectories(CurrentPath);
            files = Directory.EnumerateFiles(CurrentPath);
            _currentContents.Clear();
            _currentContents.UnionWith(directories);
            _currentContents.UnionWith(files);
            Directory.SetCurrentDirectory(CurrentPath);
            if (addToCache) _dirCache.Push(CurrentPath);
        }
        catch (Exception ex) {
            Console.Clear();
            Console.WriteLine(ex.Message);
            Console.ReadKey(true);
        }

    }
    
    public static void MoveForward() {
        if (!FilesSelected) return;

        SetPath(_currentContents.ElementAt(Selected));
    }

    public static void MoveBack(int amount = 1) {
        if (!FilesSelected) return;

        DirectoryInfo? finalDir = Directory.GetParent(CurrentPath), parentDir;

        if (finalDir == null) return;

        for (int i = 0; i < amount; i++) {
            parentDir = Directory.GetParent(CurrentPath);

            if (parentDir == null) break;

            finalDir = parentDir;
        }

        SetPath(finalDir.FullName);
    }

    public static void MoveDown(int amount = 1) {
        if (FilesSelected) {
            // keep as var to handle huge directories
            var dirCount = directories.Count();
            var fileCount = files.Count();

            if (Selected + amount >= dirCount + fileCount) {
                // TODO wrap Selected
                return;
            }

            Selected += amount;
            return;
        }

        // sidebar length of 5 so >= 4
        if (SidebarSelected >= 4) {
            // TODO wrap SidebarSelected
            return;
        }

        SidebarSelected += amount;
    }

    public static void MoveUp(int amount = 1) {
        if (FilesSelected) {
            if (Selected - amount < 0) {
                // TODO wrap Selected
                return;
            }

            Selected -= amount;
            return;
        }

        if (SidebarSelected - amount < 0) {
            // TODO wrap SidebarSelected
            return;
        }

        SidebarSelected -= amount;
    }

    public static void Undo() {
        if (_dirCache.Count == 0) return;

        string pop = _dirCache.Pop();

        while (pop == CurrentPath && _dirCache.Count > 0) {
            pop = _dirCache.Pop();
        }

        SetPath(pop, false);
    }
}
