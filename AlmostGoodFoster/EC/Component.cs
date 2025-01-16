using Foster.Framework;

namespace AlmostGoodFoster.EC
{
    public abstract class Component()
    {
        /// <summary>
        /// The GUID of the component
        /// </summary>
        public Guid Guid { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// The entity owning the component
        /// </summary>
        public Entity? Entity { get; internal set; }

        /// <summary>
        /// Gets or sets the enabled state of the component
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Automatically called when the component is added to an entity
        /// </summary>
        public virtual void OnAdded()
        {

        }

        /// <summary>
        /// Automatically called when the component is removed from its owner entity
        /// </summary>
        public virtual void OnRemoved()
        {

        }

        /// <summary>
        /// Load the content needed for the component
        /// </summary>
        public virtual void LoadContent()
        {

        }

        /// <summary>
        /// Unload the content of the component
        /// </summary>
        public virtual void UnloadContent()
        {

        }

        /// <summary>
        /// When the scene is loaded
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// Handle inputs
        /// </summary>
        /// <param name="input"></param>
        public virtual void HandleInputs(Input input)
        {

        }

        /// <summary>
        /// Update the component
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void Update(float deltaTime)
        {

        }

        /// <summary>
        /// Update the component since the last frame
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public virtual void FixedUpdate(float fixedDeltaTime)
        {

        }

        /// <summary>
        /// Lately update the component
        /// </summary>
        /// <param name="deltaTime"></param>
        public virtual void LateUpadte(float deltaTime)
        {

        }

        /// <summary>
        /// Render the component
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public virtual void Render(Batcher batcher, float deltaTime)
        {
            
        }

        /// <summary>
        /// Lately render the component
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public virtual void LateRender(Batcher batcher, float deltaTime)
        {

        }

        /// <summary>
        /// Draw GUI
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public virtual void DrawGUI(Batcher batcher, float deltaTime)
        {
            
        }
    }
}
