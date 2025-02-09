using AlmostGoodFoster.Components;
using AlmostGoodFoster.EC;
using AlmostGoodFoster.UI;
using Foster.Framework;

namespace AlmostGoodFoster.Scenes
{
    public class Scene(string name)
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
        /// The main camera of the scene
        /// </summary>
        public Camera? MainCamera { get; private set; }

        /// <summary>
        /// Function called when the engine enter the scene
        /// </summary>
        public delegate void OnSceneEnter();

        /// <summary>
        /// Function called when the engine exit the scene
        /// </summary>
        public delegate void OnSceneExit();

        /// <summary>
        /// Ui container
        /// </summary>
        public UIContainer UIContainer { get; private set; } = new(1280, 720);

        /// <summary>
        /// Load the content of the scene
        /// </summary>
        public virtual void LoadContent()
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.LoadContent();

                var camera = entity.Find<Camera>();
                if (camera != null)
                {
                    MainCamera = camera;
                }
            }

            GC.Collect();
            IsLoaded = true;
        }

        /// <summary>
        /// Unload the content of the scene
        /// </summary>
        public virtual void UnloadContent()
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

        public virtual void OnResized(int width, int height)
        {
            UIContainer.OnResized(width, height);
        }

        /// <summary>
        /// Handle entity's components inputs
        /// </summary>
        /// <param name="input"></param>
        public virtual void HandleInputs(Input input)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.HandleInputs(input);
            }

            UIContainer.HandleInputs(input);
        }

        /// <summary>
        /// Update the scene
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                entity.Update(deltaTime);
            }

            UIContainer.Update(deltaTime);
        }

        /// <summary>
        /// Update the scene with a fixed time step
        /// </summary>
        /// <param name="fixedDeltaTime">Time elapsed since the previous frame</param>
        public virtual void FixedUpdate(float fixedDeltaTime)
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
        public virtual void LateUpdate(float deltaTime)
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
        public virtual void Render(Batcher batcher, float deltaTime)
        {
            foreach (var entity in Entities)
            {
                if (entity is null || !entity.IsActive)
                {
                    continue;
                }

                if (MainCamera != null)
                {
                    batcher.PushMatrix(MainCamera.Position);
                }
                entity.Render(batcher, deltaTime);
                if (MainCamera != null)
                {
                    batcher.PopMatrix();
                }
                entity.DrawGUI(batcher, deltaTime);
            }

            UIContainer.Render(batcher, deltaTime);
        }

        /// <summary>
        /// Right after the rendering of the scene
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public virtual void OnRendered(Batcher batcher, float deltaTime)
        {
            UIContainer.OnRendered(batcher, deltaTime);
        }

        /// <summary>
        /// Render the imgui interface
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public virtual void ImGUIRender(Batcher batcher, float deltaTime)
        {
            
        }

        /// <summary>
        /// Add the given entity into the scene
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Create an entity and add it to the scene
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            var entity = new Entity(this);
            AddEntity(entity);
            return entity;
        }

        /// <summary>
        /// Find components of the given type except the given instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        /// <returns></returns>
        public List<T> FindAllExcept<T>(T? exception) where T : Component
        {
            List<T> components = [];

            foreach (var entity in Entities)
            {
                if (entity == null)
                {
                    continue;
                }

                List<T> ts = entity.FindAll(exception);
                if (ts.Count > 0)
                {
                    components.AddRange(ts);
                }
            }

            return components;
        }

        /// <summary>
        /// Find all the component of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> FindAll<T>() where T : Component
        {
            return FindAllExcept<T>(default);
        }

        /// <summary>
        /// Find the first component of the given type except the given instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exception"></param>
        /// <returns></returns>
        public T? FindFirstExcept<T>(T? exception) where T : Component
        {
            foreach (var entity in Entities)
            {
                T? t = entity.Find(exception);
                if (t != null)
                {
                    return t;
                }
            }

            return default;
        }

        /// <summary>
        /// Find the first intance of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? FindFirst<T>() where T : Component
        {
            return FindFirstExcept<T>(default);
        }
    }
}
