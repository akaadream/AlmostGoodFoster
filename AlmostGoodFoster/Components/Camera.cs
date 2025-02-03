using AlmostGoodFoster.EC;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Components
{
    public class Camera : Component
    {
        public RectInt Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;
                ComputeMatrixes();
            }
        }
        private RectInt _bounds;

        /// <summary>
        /// Shortcut for getting the width of the camera
        /// </summary>
        public int Width { get => _bounds.Width; }

        /// <summary>
        /// Shortcut for getting the height of the camera
        /// </summary>
        public int Height { get => _bounds.Height; }

        /// <summary>
        /// Zoom of the camera
        /// </summary>
        public float Zoom { get; set; }

        public Matrix3x2 View { get; set; }
        public Matrix3x2 Projection { get; set; }
        public Matrix3x2 Screen { get; set; }
        public Matrix3x2 ZoomMatrix { get => Matrix3x2.CreateScale(Zoom); }

        public Vector2 Position
        {
            get
            {
                if (Entity != null)
                {
                    return Entity.Position;
                }

                return Vector2.Zero;
            }
        }

        public SpriteFont Font { get; set; }

        public Camera(GraphicsDevice graphicsDevice, RectInt bounds)
        {
            Bounds = bounds;
            Font = new(graphicsDevice, new Font("Assets/Fonts/Signika-Bold.ttf"), 18);
        }

        public void ComputeMatrixes()
        {
            if (Entity == null)
            {
                Log.Error("The entity should not be null");
                return;
            }

            View = Matrix3x2.CreateTranslation(
                new Vector2(
                    (int)-Entity.Position.X,
                    (int)-Entity.Position.Y)) *
                Matrix3x2.CreateRotation(0f) *
                Matrix3x2.CreateScale(new Vector2(Zoom));
        }

        public override void DrawGUI(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                return;
            }

            batcher.Text(Font, $"Camera: X({Entity.Position.X}), Y({Entity.Position.Y})", new Vector2(10, 40), Color.White * 0.6f);
        }
    }
}
