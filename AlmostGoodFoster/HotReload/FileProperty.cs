namespace AlmostGoodFoster.HotReload
{
    public class FileProperty(string filename)
    {
        public string Filename { get; set; } = filename;
        public event EventHandler<FileSystemEventArgs>? Updated;

        internal void OnUpdated(object sender, FileSystemEventArgs args)
        {
            Updated?.Invoke(this, args);
        }
    }
}
