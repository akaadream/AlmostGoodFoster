using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Components
{
    [Flags]
    public enum Masks
    {
        None = 0,
        Solid = 1 << 0,
        JumpThrough = 1 << 1,
        Player = 1 << 2,
        Enemy = 1 << 3
    }

    public class Actor : Component
    {
        public Vector2 Velocity;
        public bool CollidesWithSolids = true;
        public Masks Masks;

        private Vector2 _remainder = Vector2.Zero;
        private float hitCooldown = 0;

        public bool MovePixel(Point2 sign)
        {
            sign.X = Math.Sign(sign.X);
            sign.Y = Math.Sign(sign.Y);

            if (CollidesWithSolids)
            {
                if (OverlapsAny(sign, Masks.Solid))
                {
                    return false;
                }

                if (sign.Y > 0)
                {
                    return false;
                }
            }

            if (Entity == null)
            {
                return false;
            }

            Entity.Position += sign;
            return true;
        }

        public bool OverlapsAny(Vector2 sign, Masks masks)
        {
            return true;
        }

        public virtual void Move(Vector2 movement)
        {

        }
    }
}
