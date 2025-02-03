using AlmostGoodFoster.HotReload;
using AlmostGoodFoster.Scenes;
using Foster.Framework;

namespace AlmostGoodFoster
{
    public class AlmostGoodGame : App
    {
        public Batcher Batcher { get; private set; }
        public Target Target { get; private set; }

        public int FPS { get; private set; }

        #region Fixed time step parameters

        private float _accumulator = 0f;
        private float _previousTime = 0f;
        private const float _maxFrameTime = 250f;
        private const float _fixedDeltaTimeTarget = (int)(1000 / (float)60);
        private float _fixedDeltaTime = 0f;

        #endregion

        private int _fpsCount;
        private TimeSpan _elapsed;
        private TimeSpan _last;

        public AlmostGoodGame(string name = "Almost Good Game", int width = 1280, int height = 720):
            base(name, width, height)
        {
            // Instanciation
            Batcher = new(GraphicsDevice);
            Target = new(GraphicsDevice, width, height);


            UpdateMode = UpdateMode.UnlockedStep();
            GraphicsDevice.VSync = true;
        }

        protected virtual void LoadContent()
        {

        }

        /// <summary>
        /// Startup the game and load the game's content
        /// </summary>
        protected override void Startup()
        {
            LoadContent();

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
            Window.Title = $"{Name} | FPS: {FPS} - {(GC.GetTotalMemory(false) / 1_048_576f).ToString("F")} MB";

            FPSCalculation();
            UpdateFixedTime();

            SceneManager.HandleInputs(Input);

            if (Input.Keyboard.Pressed(Keys.S))
            {
                Log.Info("Saved");
            }

            // Fixed update
            while (_accumulator >= _fixedDeltaTimeTarget)
            {
                SceneManager.FixedUpdate(_fixedDeltaTime);
                _accumulator -= _fixedDeltaTimeTarget;
            }

            SceneManager.Update(Time.Delta);
            SceneManager.LateUpdate(Time.Delta);
        }

        /// <summary>
        /// Compute the fixed time step
        /// </summary>
        private void UpdateFixedTime()
        {
            if (_previousTime == 0f)
            {
                _previousTime = Time.Delta;
            }
            float now = Time.Delta;
            if (_fixedDeltaTime > _maxFrameTime)
            {
                _fixedDeltaTime = _maxFrameTime;
            }
            _previousTime = now;
            _accumulator += _fixedDeltaTime;
        }

        private void FPSCalculation()
        {
            _fpsCount++;
            _elapsed = Time.Elapsed;
            if (_elapsed - _last >= TimeSpan.FromSeconds(1))
            {
                FPS = _fpsCount;
                _fpsCount = 0;
                _last = _elapsed;
                _elapsed -= TimeSpan.FromSeconds(1);
            }
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
}
