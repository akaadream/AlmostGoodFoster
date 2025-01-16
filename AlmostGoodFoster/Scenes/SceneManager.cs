﻿using Foster.Framework;

namespace AlmostGoodFoster.Scenes
{
    public static class SceneManager
    {
        internal static Dictionary<string, Scene> Scenes { get; private set; } = [];
        public static string CurrentSceneName { get; private set; } = "";
        public static Scene? CurrentScene
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
        public static bool AddScene(Scene scene)
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
        public static bool SetActive(string name)
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
        public static void Startup()
        {
            CurrentScene?.LoadContent();
        }

        /// <summary>
        /// Shutdown the running process of the scene manager
        /// </summary>
        public static void Shutdown()
        {
            CurrentScene?.UnloadContent();
            Scenes?.Clear();
        }

        /// <summary>
        /// Know if a scene using the given name already exists
        /// </summary>
        /// <param name="name">The name of the researched scene</param>
        /// <returns>True if the scene with this name already exists or false if the scene doesn't exists</returns>
        public static bool AlreadyExists(string name) => Scenes.TryGetValue(name, out _);

        /// <summary>
        /// Update the current loaded scene
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public static void Update(float deltaTime) => CurrentScene?.Update(deltaTime);

        /// <summary>
        /// Update the current loaded scene with a fixed time step
        /// </summary>
        /// <param name="fixedDeltaTime">Time elapsed since the previous frame</param>
        public static void FixedUpdate(float fixedDeltaTime) => CurrentScene?.FixedUpdate(fixedDeltaTime);

        /// <summary>
        /// Lately update the current loaded scene
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public static void LateUpdate(float deltaTime) => CurrentScene?.LateUpdate(deltaTime);

        /// <summary>
        /// Render the current loaded scene
        /// </summary>
        /// <param name="batcher">Batcher used to draw the scene's content</param>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public static void Render(Batcher batcher, float deltaTime) => CurrentScene?.Render(batcher, deltaTime);
    }
}
