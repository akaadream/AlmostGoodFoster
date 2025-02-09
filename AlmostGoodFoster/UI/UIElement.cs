using System.Numerics;
using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public abstract class UIElement(UIElement? parent, UIContainer container)
    {
        /// <summary>
        /// Container where this element is located
        /// </summary>
        public UIContainer Container { get; set; } = container;

        /// <summary>
        /// Parent of this element
        /// </summary>
        public UIElement? Parent { get; set; } = parent;

        /// <summary>
        /// The X position of the UIElement
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// The Y position of the UIElement
        /// </summary>
        public int Y { get; set; } = 0;

        /// <summary>
        /// The anchor of the element
        /// </summary>
        public Anchor Anchor { get; set; } = Anchor.TopLeft;

        /// <summary>
        /// Children elements of this element
        /// </summary>
        private List<UIElement> _children = [];

        /// <summary>
        /// If the element should be auto sized
        /// </summary>
        public bool AutoSize { get; set; }

        /// <summary>
        /// The width of the element
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (AutoSize)
                {
                    return;
                }

                _width = value;
            }
        }
        protected int _width;

        /// <summary>
        /// The height of the element
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (AutoSize)
                {
                    return;
                }

                _height = value;
            }
        }
        protected int _height;

        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public int Padding { get; set; } = 0;

        public Color BackgroundColor { get; set; }
        public Color HoverBackgroundColor { get; set; }

        public float Opacity { get; set; } = 1.0f;

        public bool IsHovered = false;
        public bool IsClicked = false;

        public virtual void AddChild(UIElement child)
        {
            _children.Add(child);
        }

        public virtual void HandleInputs(Input input)
        {
            foreach (UIElement child in _children)
            {
                child.HandleInputs(input);
            }

            if (MouseHovered(input))
            {
                IsHovered = true;
                IsClicked = input.Mouse.LeftPressed;
            }
            else
            {
                IsHovered = false;
            }
        }

        private bool MouseHovered(Input input)
        {
            var position = GetFinalPosition();
            return input.Mouse.Position.X >= position.X &&
                input.Mouse.Position.X < position.X + Width &&
                input.Mouse.Position.Y >= position.Y &&
                input.Mouse.Position.Y < position.Y + Height;
        }

        public virtual void OnResized(int width, int height)
        {

        }

        public virtual void Update(float deltaTime)
        {
            foreach (UIElement child in _children)
            {
                child.Update(deltaTime);
            }
        }

        public virtual void Render(Batcher batcher, float deltaTime)
        {
            foreach (UIElement child in _children)
            {
                child.Render(batcher, deltaTime);
            }
        }

        public virtual void OnRendered(Batcher batcher, float deltaTime)
        {
            foreach (UIElement child in _children)
            {
                child.OnRendered(batcher, deltaTime);
            }
        }

        public virtual void ComputeAutoSize()
        {

        }

        /// <summary>
        /// Get the final position
        /// </summary>
        /// <returns></returns>
        protected Vector2 GetFinalPosition()
        {
            switch (Anchor)
            {
                case Anchor.TopLeft:
                    break;
                case Anchor.TopCenter:
                    X = GetCenterX();
                    break;
                case Anchor.TopRight:
                    X = GetRightX();
                    break;
                case Anchor.MiddleLeft:
                    Y = GetMiddleY();
                    break;
                case Anchor.MiddleCenter:
                    X = GetCenterX();
                    Y = GetMiddleY();
                    break;
                case Anchor.MiddleRight:
                    X = GetRightX();
                    Y = GetMiddleY();
                    break;
                case Anchor.BottomLeft:
                    Y = GetBottomY();
                    break;
                case Anchor.BottomCenter:
                    X = GetCenterX();
                    Y = GetBottomY();
                    break;
                case Anchor.BottomRight:
                    X = GetRightX();
                    Y = GetBottomY();
                    break;
            }

            return new Vector2(X, Y);
        }

        private int GetCenterX() => Container.Width / 2 - Width / 2 + X;

        private int GetRightX() => Container.Width - Width - X;

        private int GetMiddleY() => Container.Height / 2 - Height / 2 + Y;

        private int GetBottomY() => Container.Height - Height - Y;
    }
}
