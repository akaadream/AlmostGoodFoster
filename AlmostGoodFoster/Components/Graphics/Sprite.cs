using AlmostGoodFoster.EC;
using AlmostGoodFoster.HotReload;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Components.Graphics
{
    public class Sprite : Component
    {
        public Texture Texture { get; set; }
        public string Filename { get; set; }
        public float Scale { get; set; } = 1f;
        public bool Watched { get; set; } = false;

        private FileProperty _asset;
        private GraphicsDevice _graphicsDevice;

        public Sprite(GraphicsDevice graphicsDevice, string filename)
        {

            Filename = filename;
            _graphicsDevice = graphicsDevice;
#if DEBUG
            _asset = new(Filename);
            _asset.Updated += OnAssetUpdated;
            FileWatcher.Watch(_asset);
            Watched = true;
#endif
        }

        private void OnAssetUpdated(object? sender, FileSystemEventArgs e)
        {
            Thread.Sleep(100);
            using FileStream stream = new(e.FullPath, FileMode.Open);
            try
            {
                var image = new Image(stream);
                Texture = new Texture(_graphicsDevice, image);
            }
            catch (Exception)
            {
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Texture = new Texture(graphicsDevice, new Image($"Assets/{filename}"));
        }

        public override void UnloadContent()
        {
#if DEBUG
            if (Watched)
            {
                _asset.Updated -= OnAssetUpdated;
            }
#endif
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                Log.Error($"The component (GUID: {Guid}) is not attached to an entity");
                return;
            }
            batcher.Image(Texture, Entity.Position, Vector2.Zero, Entity.Scale * Scale, 0f, Color.White);
        }
    }
}
