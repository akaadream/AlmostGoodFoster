using System.Numerics;
using AlmostGoodFoster.Fonts;
using AlmostGoodFoster.HotReload;
using AlmostGoodFoster.Scenes;
using Foster.Framework;

namespace AlmostGoodFoster
{
    public class AlmostGoodGame : App
    {
        public Batcher Batcher { get; private set; }
        public Target? Target { get; private set; }

        protected SceneManager SceneManager { get; private set; }

        public int FPS { get; private set; }

        public RectInt Viewport { get; set; }

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

        public static Time CurrentTime { get; private set; }

        public AlmostGoodGame():
            base(Settings.Title, Settings.Width, Settings.Height)
        {
            // Instanciation
            Batcher = new(GraphicsDevice);

            SceneManager = new();

            GraphicsDevice.VSync = true;
            Window.OnResize += OnWindowResized;   
        }

        private void OnWindowResized()
        {
            SceneManager.OnResized(Window.WidthInPixels, Window.HeightInPixels);
        }

        private void LoadSettings()
        {
            if (Settings.UseCustomResolution)
            {
                Target = new(GraphicsDevice, Settings.ResolutionWidth, Settings.ResolutionHeight);
                Viewport = (RectInt)new Rect(0, 0, Window.WidthInPixels / Settings.Scale, Window.HeightInPixels / Settings.Scale).Inflate(Settings.Padding);
            }
        }

        public void DisableDefaultLogs()
        {
            Log.Fn onInfoFn = new(OnInfo);
            Log.Fn onErrorFn = new(OnInfo);
            Log.Fn onWarnFn = new(OnInfo);
            Log.SetCallbacks(onInfoFn, onWarnFn, onErrorFn);
        }

        private void OnInfo(ReadOnlySpan<char> text)
        {

        }

        private void OnErrorInfo(ReadOnlySpan<char> text)
        {

        }

        private void OnWarnInfo(ReadOnlySpan<char> text)
        {

        }

        protected virtual void RegisterScene(Scene scene) => SceneManager.AddScene(scene);

        protected virtual void LoadContent()
        {

        }

        /// <summary>
        /// Startup the game and load the game's content
        /// </summary>
        protected override void Startup()
        {
            FontManager.Startup(GraphicsDevice);
            FontManager.Register("default", new Font("Assets/Fonts/OpenSans-Bold.ttf"));

            LoadSettings();
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
            CurrentTime = Time;
#if DEBUG
            Window.Title = $"{Name} | FPS: {FPS} - {(GC.GetTotalMemory(false) / 1_048_576f).ToString("F")} MB";
#else
            Window.Title = $"{Name}";
#endif

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

            if (Target != null)
            {
                RenderUsingTarget();
            }
            else
            {
                RenderUsingWindow();
            }
        }

        private void RenderUsingTarget()
        {
            if (Target == null)
            {
                return;
            }

            Batcher.Render(Window);
            RenderDrawableTarget(Target);
        }

        private void RenderUsingWindow()
        {
            RenderDrawableTarget(Window);
        }

        private void RenderDrawableTarget(IDrawableTarget target)
        {
            SceneManager.Render(Batcher, Time.Delta);
            Batcher.Render(target);
            Batcher.Clear();

            if (Target != null)
            {
                var size = Viewport.Size;
                var center = Viewport.Center;
                var scale = Calc.Min(size.X / (float)Target.Width, size.Y / (float)Target.Height);

                Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp, TextureWrap.Clamp));
                Batcher.Image(Target, center, Target.Bounds.Size / 2, Vector2.One * scale, 0, Color.White);
                Batcher.PopSampler();
                Batcher.Render(Window);
                Batcher.Clear();
            }

            SceneManager.OnRendered(Batcher, Time.Delta);
            Batcher.Render(Window);
            SceneManager.ImGUIRender(Batcher, Time.Delta);
            Batcher.Clear();
        }
    }
}
