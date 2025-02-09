using Foster.Framework;

namespace AlmostGoodFoster.Scenes
{
    public class SceneManager()
    {
        internal Dictionary<string, Scene> Scenes { get; private set; } = [];
        public string CurrentSceneName { get; private set; } = "";
        public Scene? CurrentScene
        {
            get
            {
                if (Scenes.TryGetValue(CurrentSceneName, out var scene))
                {
                    return scene;
                }
                return null;
            }
        }

        /// <summary>
        /// Register a new scene
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public bool AddScene(Scene scene)
        {
            if (scene == null || AlreadyExists(scene.Name))
            {
                return false;
            }

            Scenes.Add(scene.Name, scene);
            if (CurrentScene == null)
            {
                CurrentSceneName = scene.Name;
            }
            return true;
        }

        /// <summary>
        /// Define a new active scene
        /// </summary>
        /// <param name="name">The name of the scene you want to load</param>
        /// <returns>True if a new scene is loaded</returns>
        public bool SetActive(string name)
        {
            if (!AlreadyExists(name) || CurrentSceneName == name)
            {
                return false;
            }

            CurrentScene?.UnloadContent();

            // TODO: scene transition

            CurrentSceneName = name;
            CurrentScene?.LoadContent();
            return true;
        }

        /// <summary>
        /// Start the running process of the scene manager
        /// </summary>
        public void Startup()
        {
            CurrentScene?.LoadContent();
        }

        /// <summary>
        /// Shutdown the running process of the scene manager
        /// </summary>
        public void Shutdown()
        {
            CurrentScene?.UnloadContent();
            Scenes?.Clear();
        }

        public void OnResized(int width, int height)
        {
            CurrentScene?.OnResized(width, height);
        }

        /// <summary>
        /// Know if a scene using the given name already exists
        /// </summary>
        /// <param name="name">The name of the researched scene</param>
        /// <returns>True if the scene with this name already exists or false if the scene doesn't exists</returns>
        public bool AlreadyExists(string name) => Scenes.TryGetValue(name, out _);

        /// <summary>
        /// Handle inputs inside the current loaded scene
        /// </summary>
        /// <param name="inputState"></param>
        public void HandleInputs(Input input) => CurrentScene?.HandleInputs(input);

        /// <summary>
        /// Update the current loaded scene
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public void Update(float deltaTime) => CurrentScene?.Update(deltaTime);

        /// <summary>
        /// Update the current loaded scene with a fixed time step
        /// </summary>
        /// <param name="fixedDeltaTime">Time elapsed since the previous frame</param>
        public void FixedUpdate(float fixedDeltaTime) => CurrentScene?.FixedUpdate(fixedDeltaTime);

        /// <summary>
        /// Lately update the current loaded scene
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public void LateUpdate(float deltaTime) => CurrentScene?.LateUpdate(deltaTime);

        /// <summary>
        /// Render the current loaded scene
        /// </summary>
        /// <param name="batcher">Batcher used to draw the scene's content</param>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public void Render(Batcher batcher, float deltaTime) => CurrentScene?.Render(batcher, deltaTime);

        /// <summary>
        /// Right after the rendering of the current scene
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public void OnRendered(Batcher batcher, float deltaTime) => CurrentScene?.OnRendered(batcher, deltaTime);

        /// <summary>
        /// ImGUI rendering
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public void ImGUIRender(Batcher batcher, float deltaTime) => CurrentScene?.ImGUIRender(batcher, deltaTime);
    }
}
