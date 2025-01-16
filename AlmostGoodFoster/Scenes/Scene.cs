using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Scenes
{
    public sealed class Scene(string name)
    {
        /// <summary>
        /// The name of the scene
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// True when the scene is already loaded
        /// </summary>
        public bool IsLoaded { get; set; } = false;

        /// <summary>
        /// The list of entities contained in the scene
        /// </summary>
        public List<Entity> Entities { get; set; } = [];

        /// <summary>
        /// Function called when the engine enter the scene
        /// </summary>
        public delegate void OnSceneEnter();

        /// <summary>
        /// Function called when the engine exit the scene
        /// </summary>
        public delegate void OnSceneExit();

        /// <summary>
        /// Load the content of the scene
        /// </summary>
        public void LoadContent()
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.LoadContent();
            }

            GC.Collect();
            IsLoaded = true;
        }

        /// <summary>
        /// Unload the content of the scene
        /// </summary>
        public void UnloadContent()
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.UnloadContent();
            }

            GC.Collect();
            IsLoaded = false;
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.Update(deltaTime);
            }
        }

        /// <summary>
        /// Update the scene with a fixed time step
        /// </summary>
        /// <param name="fixedDeltaTime">Time elapsed since the previous frame</param>
        public void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.FixedUpdate(fixedDeltaTime);
            }
        }

        /// <summary>
        /// Lately update the scene
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public void LateUpdate(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.LateUpdate(deltaTime);
            }
        }

        /// <summary>
        /// Render the scene
        /// </summary>
        /// <param name="batcher">Batcher used to draw the content of the scene</param>
        /// <param name="deltaTime">Time elapsed since the previous frame</param>
        public void Render(Batcher batcher, float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.Render(batcher, deltaTime);
            }
        }
    }
}
