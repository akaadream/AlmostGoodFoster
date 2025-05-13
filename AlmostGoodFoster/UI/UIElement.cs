using System.Numerics;
using AlmostGoodFoster.Fonts;
using AlmostGoodFoster.UI.Containers;
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
        public int X { get; private set; } = 0;

        /// <summary>
        /// The Y position of the UIElement
        /// </summary>
        public int Y { get; private set; } = 0;

        /// <summary>
        /// The anchor of the element
        /// </summary>
        public Anchor Anchor
        {
            get => _anchor;
            set
            {
                _anchor = value;
                ComputePosition();
            }
        }
        private Anchor _anchor = Anchor.TopLeft;


        /// <summary>
        /// Children elements of this element
        /// </summary>
        protected List<UIElement> Children = [];

        /// <summary>
        /// If the element should be auto sized
        /// </summary>
        public bool AutoSize
        {
            get => _autoSize;
            set
            {
                _autoSize = value;
                ComputeAutoSize();
                ComputePosition();
            }
        }
        private bool _autoSize = false;

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
                ComputePosition();
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
                ComputePosition();
            }
        }
        protected int _height;

        public int Top
        {
            get => _top;
            set
            {
                _top = value;
                ComputePosition();
            }
        }
        private int _top = 0;
        public int Bottom
        {
            get => _bottom;
            set
            {
                _bottom = value;
                ComputePosition();
            }
        }
        private int _bottom = 0;
        public int Left
        {
            get => _left;
            set
            {
                _left = value;
                ComputePosition();
            }
        }
        private int _left = 0;
        public int Right
        {
            get => _right;
            set
            {
                _right = value;
                ComputePosition();
            }
        }
        private int _right = 0;

        public int Padding
        {
            get => _padding;
            set
            {
                _padding = value;
                ComputePosition();
            }
        }
        private int _padding = 0;

        public Color BackgroundColor { get; set; }
        public Color HoverBackgroundColor { get; set; }

        public float Opacity { get; set; } = 1.0f;
        public float Radius { get; set; } = 0.0f;

        public bool IsHovered = false;
        public bool IsClicked = false;

        #region Text settings

        public string Text { get; set; } = "";
        public int FontSize { get; set; } = 12;

        protected SpriteFont Font { get; set; } = FontManager.Get("default", 12);

        public Color TextColor { get; set; } = Color.White;
        public Color HoverTextColor { get; set; } = Color.White;

        #endregion

        public virtual void AddChild(UIElement child)
        {
            Children.Add(child);
        }

        public virtual void HandleInputs(Input input)
        {
            foreach (UIElement child in Children)
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
            return input.Mouse.Position.X >= X &&
                input.Mouse.Position.X < X + Width &&
                input.Mouse.Position.Y >= Y &&
                input.Mouse.Position.Y < Y + Height;
        }

        public virtual void OnResized(int width, int height)
        {

        }

        public virtual void Update(float deltaTime)
        {
            foreach (UIElement child in Children)
            {
                child.Update(deltaTime);
            }
        }

        public virtual void Render(Batcher batcher, float deltaTime)
        {
            foreach (UIElement child in Children)
            {
                child.Render(batcher, deltaTime);
            }
        }

        public virtual void OnRendered(Batcher batcher, float deltaTime)
        {
            foreach (UIElement child in Children)
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
        protected Vector2 ComputePosition()
        {
            switch (Anchor)
            {
                case Anchor.TopLeft:
                    X = Left;
                    Y = Top;
                    break;
                case Anchor.TopCenter:
                    X = GetCenterX();
                    Y = Top;
                    break;
                case Anchor.TopRight:
                    X = GetRightX();
                    Y = Top;
                    break;
                case Anchor.MiddleLeft:
                    X = Left;
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
                    X = Left;
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

        private int GetCenterX() => Container.Width / 2 - Width / 2 + Left;

        private int GetRightX() => Container.Width - Width - Left;

        private int GetMiddleY() => Container.Height / 2 - Height / 2 + Top;

        private int GetBottomY() => Container.Height - Height - Top;
    }
}
