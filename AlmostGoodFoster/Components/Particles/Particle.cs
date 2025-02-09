using System.Numerics;
using Color = Foster.Framework.Color;

namespace AlmostGoodFoster.Components.Particles
{
    internal class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float RotationSpeed;
        public float Scale;
        public float ScaleSpeed;
        public float CurrentLifetime;
        public float MaxLifetime;
        public Color Color;
        public Color EndColor;
        public bool IsActive;

        public void Update(float deltaTime)
        {
            if (!IsActive)
            {
                return;
            }

            CurrentLifetime += deltaTime;
            if (CurrentLifetime >= MaxLifetime)
            {
                IsActive = false;
                return;
            }

            Position += Velocity * deltaTime;
            Rotation += RotationSpeed * deltaTime;
            Scale += ScaleSpeed * deltaTime;

            float lifePercent = CurrentLifetime / MaxLifetime;
            Color = Color.Lerp(Color, EndColor, lifePercent);
        }
    }
}
