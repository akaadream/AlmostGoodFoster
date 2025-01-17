using System.Diagnostics;
using AlmostGoodFoster.HotReload;
using AlmostGoodFoster.Scenes;
using Foster.Framework;

namespace AlmostGoodFoster
{
    public class AlmostGoodGame : App
    {
        public Batcher Batcher { get; private set; }
        public Target Target { get; private set; }

        private FrameCounter _frameCounter;

        public AlmostGoodGame(string name = "Almost Good Game", int width = 1280, int height = 720):
            base(name, width, height)
        {
            // Instanciation
            Batcher = new(GraphicsDevice);
            Target = new(GraphicsDevice, width, height);

            _frameCounter = new FrameCounter();

            GraphicsDevice.VSync = true;
        }

        /// <summary>
        /// Startup the game and load the game's content
        /// </summary>
        protected override void Startup()
        {
            // Start everthing
            SceneManager.Startup();

#if DEBUG
            FileWatcher.Startup();
#endif
        }

        /// <summary>
        /// Shutdown the game and unload the game's content
        /// </summary>
        protected override void Shutdown()
        {
            // Shutdown
            SceneManager.Shutdown();

#if DEBUG
            FileWatcher.Shutdown();
#endif
        }

        /// <summary>
        /// Update routine of the game
        /// </summary>
        protected override void Update()
        {
            _frameCounter.Update();
            Window.Title = $"{Name} | FPS: {_frameCounter.FPS} - {(GC.GetTotalMemory(false) / 1_048_576f).ToString("F")} MB";

            SceneManager.HandleInputs(Input);
            SceneManager.Update(Time.Delta);
            SceneManager.LateUpdate(Time.Delta);
        }

        /// <summary>
        /// Render the game
        /// </summary>
        protected override void Render()
        {
            Window.Clear(0x897897);

            // Render
            SceneManager.Render(Batcher, Time.Delta);
            Batcher.Render(Window);
            Batcher.Clear();
        }
    }

    public class FrameCounter
    {
        public int FPS = 0;
        public int Frames = 0;
        public Stopwatch sw = Stopwatch.StartNew();

        public void Update()
        {
            Frames++;
            var elapsed = sw.Elapsed.TotalSeconds;
            if (elapsed > 1)
            {
                sw.Restart();
                FPS = Frames;
                Frames = 0;
            }
        }
    }

}
