using AlmostGoodFoster.Scenes;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.EC
{
    public class Entity(Scene scene)
    {
        /// <summary>
        /// The GUID of the entity
        /// </summary>
        public Guid Guid { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// The position of the entity
        /// </summary>
        public Point2 Position;

        /// <summary>
        /// The scale of the entity
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// The rotation of the entity
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// The list of components attached to this entity
        /// </summary>
        public List<Component> Components = [];

        /// <summary>
        /// The list of children entities attached to this entity
        /// </summary>
        public List<Entity> Children = [];

        /// <summary>
        /// Gets or sets the actives state of the entity
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// True when the entity gets loaded
        /// </summary>
        private bool _isLoaded = false;

        /// <summary>
        /// The scene that contains this entity
        /// </summary>
        public Scene Scene { get; private set; } = scene;

        /// <summary>
        /// Load content of entity's components
        /// </summary>
        public void LoadContent()
        {
            foreach (var component in Components)
            {
                component?.LoadContent();
            }
        }

        /// <summary>
        /// Unload content of entity's components
        /// </summary>
        public void UnloadContent()
        {
            foreach (var component in Components)
            {
                component?.UnloadContent();
            }
        }

        /// <summary>
        /// Register the given component to this entity
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public Entity Register(Component component)
        {
            if (component == null || Components.Contains(component))
            {
                // TODO: throw an Exception
                return this;
            }

            Components.Add(component);
            component.Entity = this;
            component.OnAdded();
            return this;
        }

        /// <summary>
        /// Remove the given component from this entity
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool Remove(Component component)
        {
            if (component == null)
            {
                return false;
            }

            int index = Components.IndexOf(component);
            if (index == -1)
            {
                return false;
            }

            Components.Remove(component);
            component.Entity = null;
            component.OnRemoved();
            return true;
        }

        /// <summary>
        /// Handle inputs
        /// </summary>
        /// <param name="input"></param>
        internal void HandleInputs(Input input)
        {
            foreach (var component in Components)
            {
                if (!_isLoaded)
                {
                    component?.Start();
                }
                component?.HandleInputs(input);
            }

            if (!_isLoaded)
            {
                _isLoaded = true;
            }
        }

        /// <summary>
        /// Update the entity's components
        /// </summary>
        /// <param name="deltaTime"></param>
        internal void Update(float deltaTime)
        {
            foreach (var component in Components)
            {
                if (!_isLoaded)
                {
                    component?.Start();
                }
                component?.Update(deltaTime);
            }

            if (!_isLoaded)
            {
                _isLoaded = true;
            }
        }

        /// <summary>
        /// Update the entity with a fixed time step
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        internal void FixedUpdate(float fixedDeltaTime)
        {
            foreach (var component in Components)
            {
                component?.FixedUpdate(fixedDeltaTime);
            }
        }

        /// <summary>
        /// Lately update the entity
        /// </summary>
        /// <param name="deltaTime"></param>
        internal void LateUpdate(float deltaTime)
        {
            foreach (var component in Components)
            {
                component?.LateUpdate(deltaTime);
            }
        }

        /// <summary>
        /// Render the entity's components
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        internal void Render(Batcher batcher, float deltaTime)
        {
            foreach (var component in Components)
            {
                component?.Render(batcher, deltaTime);
            }
        }

        /// <summary>
        /// Lately render the entity's components
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        internal void LateRender(Batcher batcher, float deltaTime)
        {
            foreach (var component in Components)
            {
                component?.Render(batcher, deltaTime);
            }
        }

        /// <summary>
        /// Draw the GUI of entity's components
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        internal void DrawGUI(Batcher batcher, float deltaTime)
        {
            foreach (var component in Components)
            {
                component?.DrawGUI(batcher, deltaTime);
            }
        }

        /// <summary>
        /// Find the first component corresponding with the given type.
        /// If an instance of T is given as a parameter, the function will find the first
        /// component which is not the given instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T? Find<T>(T? instance = null) where T : Component
        {
            foreach (var component in Components)
            {
                if (instance != null && component == instance)
                {
                    continue;
                }

                if (component is T t && t != null)
                {
                    return t;
                }
            }

            return default;
        }

        /// <summary>
        /// Find all the component corresponding with the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> FindAll<T>(T? instance = null) where T : Component
        {
            List<T> components = [];
            foreach (var component in Components)
            {
                if (instance != null && component == instance)
                {
                    continue;
                }

                if (component is T t && t != null)
                {
                    components.Add(t);
                }
            }
            return components;
        }

        /// <summary>
        /// Return true if the entity got a component of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>() where T : Component => Find<T>() != null;

        /// <summary>
        /// Return the number of components of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public int Count<T>() where T : Component => FindAll<T>().Count;
    }
}
