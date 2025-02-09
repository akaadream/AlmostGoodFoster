using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public class UIContainer(int width, int height)
    {
        public int Width { get; set; } = width;

        public int Height { get; set; } = height;

        /// <summary>
        /// Elements contained by this container
        /// </summary>
        public List<UIElement> Elements { get; set; } = [];

        public void OnResized(int width, int height)
        {
            Width = width;
            Height = height;

            foreach (UIElement element in Elements)
            {
                element.OnResized(width, height);
            }
        }

        /// <summary>
        /// Handle inputs of elements
        /// </summary>
        /// <param name="input"></param>
        public void HandleInputs(Input input)
        {
            foreach (UIElement element in Elements)
            {
                element.HandleInputs(input);
            }
        }

        /// <summary>
        /// Update elements
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            foreach (UIElement element in Elements)
            {
                element.Update(deltaTime);
            }
        }

        /// <summary>
        /// Render elements
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public void Render(Batcher batcher, float deltaTime)
        {
            foreach (UIElement element in Elements)
            {
                element.Render(batcher, deltaTime);
            }
        }

        /// <summary>
        /// Right after elements rendering
        /// </summary>
        /// <param name="batcher"></param>
        /// <param name="deltaTime"></param>
        public void OnRendered(Batcher batcher, float deltaTime)
        {
            foreach (UIElement element in Elements)
            {
                element.Render(batcher, deltaTime);
            }
        }
    }
}
