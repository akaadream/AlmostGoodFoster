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

        public Camera(RectInt bounds)
        {
            Bounds = bounds;
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
                    (int)-Entity.Transform.X,
                    (int)-Entity.Transform.Y)) *
                Matrix3x2.CreateRotation(0f) *
                Matrix3x2.CreateScale(new Vector2(Zoom));
        }
    }
}
