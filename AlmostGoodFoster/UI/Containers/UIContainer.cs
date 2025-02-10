using AlmostGoodFoster.UI.Window;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Containers
{
    public class UIContainer(int width, int height)
    {
        /// <summary>
        /// Width of the container
        /// </summary>
        public int Width { get; set; } = width;

        /// <summary>
        /// Height of the container
        /// </summary>
        public int Height { get; set; } = height;

        /// <summary>
        /// Elements contained by this container
        /// </summary>
        private readonly List<UIElement> _elements = [];
        private readonly List<SubWindow> _windows = [];

        public SubWindow? FocusedWindow = null;
        
        public void AddChild(UIElement element)
        {
            if (element is SubWindow window)
            {
                _windows.Add(window);
                return;
            }

            _elements.Add(element);
        }

        public void OnResized(int width, int height)
        {
            Width = width;
            Height = height;

            foreach (UIElement element in _elements)
            {
                element.OnResized(width, height);
            }

            foreach (UIElement element in _windows)
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
            foreach (UIElement element in _elements)
            {
                element.HandleInputs(input);
            }

            foreach (SubWindow window in _windows)
            {
                window.HandleInputs(input);

                // TODO: WINDOW FOCUS
                if (window.IsClicked)
                {
                    
                }
            }
        }

        /// <summary>
        /// Update elements
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Update(float deltaTime)
        {
            foreach (UIElement element in _elements)
            {
                element.Update(deltaTime);
            }

            foreach (UIElement element in _windows)
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
            foreach (UIElement element in _elements)
            {
                element.Render(batcher, deltaTime);
            }

            foreach (UIElement element in _windows)
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
            foreach (UIElement element in _elements)
            {
                element.Render(batcher, deltaTime);
            }

            foreach (UIElement element in _windows)
            {
                element.Render(batcher, deltaTime);
            }
        }
    }
}
