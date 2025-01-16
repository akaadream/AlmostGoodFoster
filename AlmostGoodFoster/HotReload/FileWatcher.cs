using Foster.Framework;

namespace AlmostGoodFoster.HotReload
{
    public static class FileWatcher
    {
        public static Dictionary<string, FileProperty> Files { get; set; } = [];
        public static FileSystemWatcher? Watcher { get; set; }
        public static string RootPath { get; set; } = "Assets/";

        public static void Startup()
        {
            DirectoryInfo? directoryInfo = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
            if (directoryInfo == null ||
                directoryInfo.Parent == null ||
                directoryInfo.Parent.Parent == null ||
                directoryInfo.Parent.Parent.Parent == null)
            {
                Log.Error($"The asset folder cannot be found");
                return;
            }
            RootPath = Path.Join(directoryInfo.Parent.Parent.Parent.FullName, "Assets");
            Watcher = new(RootPath)
            {
                EnableRaisingEvents = true,

                // If true, the watcher will look updates on sub folders of the root directory
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size,
            };

            // Watcher events
            Watcher.Changed += OnFileChanged;
            Watcher.Created += OnFileCreated;
            Watcher.Deleted += OnFileDeleted;
            Watcher.Renamed += OnFileRenamed;
            Watcher.Error += OnError;
        }

        public static void Watch(FileProperty file)
        {
            if (!Files.TryGetValue($"{RootPath}/{file.Filename}", out _))
            {
                Files.Add(file.Filename, file);
            }
        }

        private static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name == null)
            {
                return;
            }

            if (Files.TryGetValue(e.Name, out FileProperty? file))
            {
                file.OnUpdated(sender, e);
            }
        }

        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {

        }

        private static void OnFileDeleted(object sender, FileSystemEventArgs e)
        {

        }

        private static void OnFileRenamed(object sender, FileSystemEventArgs e)
        {

        }

        private static void OnError(object sender, ErrorEventArgs e)
        {

        }

        public static void Shutdown()
        {
            Files.Clear();
        }
    }
}
