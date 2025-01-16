using AlmostGoodFoster.EC;
using AlmostGoodFoster.Extensions;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Test.Components
{
    public class Player : Component
    {
        public float Speed = 7f;
        public Vector2 Velocity;

        public Player()
        {
            Velocity = Vector2.Zero;
        }

        public override void HandleInputs(Input input)
        {
            Velocity = Vector2.Zero;
            if (input.Keyboard.Down(Keys.Up))
            {
                // Move up
                Velocity.Y -= 1;
            }
            if (input.Keyboard.Down(Keys.Down))
            {
                // Move down
                Velocity.Y += 1;
            }
            if (input.Keyboard.Down(Keys.Left))
            {
                // Move left
                Velocity.X -= 1;
            }
            if (input.Keyboard.Down(Keys.Right))
            {
                // Move right
                Velocity.X += 1;
            }
        }

        public override void Update(float deltaTime)
        {
            if (Entity == null)
            {
                return;
            }

            Velocity = Velocity.Normalized();
            Entity.Transform = new(Entity.Transform.Position.Add(Velocity * Speed), Entity.Transform.Scale, Entity.Transform.Rotation);
        }

        public override void FixedUpdate(float fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
