using System.Numerics;
using AlmostGoodFoster.Components.Collisions;
using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Components
{
    public class Actor : Component
    {
        public Vector2 Velocity;
        public Vector2 Squish;
        public bool CollidesWithSolids = true;
        public float IFrameTime = 0.5f;
        public ActorMasks Masks;
        public Hitbox Hitbox;

        private Vector2 _remainder = Vector2.Zero;
        private float hitCooldown = 0;

        public bool MovePixel(Point2 sign)
        {
            sign.X = Math.Sign(sign.X);
            sign.Y = Math.Sign(sign.Y);

            if (CollidesWithSolids)
            {
                if (OverlapsAny(sign, ActorMasks.Solid))
                {
                    return false;
                }

                if (sign.Y > 0 && Grounded())
                {
                    return false;
                }
            }

            Entity.Position += sign;
            return true;
        }

        public bool OverlapsAny(ActorMasks mask)
        {
            return OverlapsAny(Point2.Zero, mask);
        }

        public bool OverlapsAny(Point2 offset, ActorMasks mask)
        {
            List<Actor> others = Entity!.Scene.FindAllExcept(this);

            foreach (var other in others)
            {
                if (other.Masks.Has(mask) && OverlapsAny(offset, other))
                {
                    return true;
                }
            }

            return false;
        }

        public bool OverlapsAny(Point2 offset, Actor other)
        {
            return Hitbox.Overlaps(Entity.Position + offset, other.Hitbox);
        }

        public Actor? OverlapsFirst(Point2 offset, ActorMasks mask)
        {
            List<Actor> others = Entity!.Scene.FindAllExcept(this);
            
            foreach (var other in others)
            {
                if (other.Masks.Has(mask) && OverlapsAny(offset, other))
                {
                    return other;
                }
            }

            return null;
        }

        public Actor? OverlapsFirst(ActorMasks mask)
        {
            return OverlapsFirst(Point2.Zero, mask);
        }

        public bool Grounded()
        {
            List<Actor> others = Entity!.Scene.FindAllExcept(this);

            foreach (var other in others)
            {
                if (!other.Masks.Has(ActorMasks.Solid | ActorMasks.JumpThrough))
                {
                    continue;
                }

                if (!OverlapsAny(Point2.Down, other))
                {
                    continue;
                }

                if (other.Masks.Has(ActorMasks.JumpThrough) && OverlapsAny(Point2.Zero, other))
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public void Move(Vector2 value)
        {
            _remainder += value;
            Point2 move = (Point2)_remainder;
            _remainder -= move;

            while (move.X != 0)
            {
                var sign = Math.Sign(move.X);
                if (!MovePixel(Point2.UnitX * sign))
                {
                    OnCollideX();
                    break;
                }
                else
                {
                    move.X -= sign;
                }
            }
        }

        public void Stop()
        {
            Velocity = Vector2.Zero;
            _remainder = Vector2.Zero;
        }

        public void StopX()
        {
            Velocity.X = 0;
            _remainder.X = 0;
        }

        public void StopY()
        {
            Velocity.Y = 0;
            _remainder.Y = 0;
        }

        public bool Hit(Actor actor)
        {
            if (actor.hitCooldown <= 0)
            {
                actor.hitCooldown = actor.IFrameTime;
                actor.OnWasHit(this);
                OnPerformHit(actor);
                return true;
            }
            return false;
        }

        public virtual void OnCollideX()
        {
            StopX();
        }

        public virtual void OnCollideY()
        {
            StopY();
        }

        public virtual void OnWasHit(Actor by)
        {

        }

        public virtual void OnPerformHit(Actor hitting)
        {

        }

        public override void Update(float deltaTime)
        {
            if (Velocity != Vector2.Zero)
            {
                Move(Velocity * deltaTime);
            }

            Squish = Calc.Approach(Squish, Vector2.One, deltaTime * 4.0f);
            hitCooldown = Calc.Approach(hitCooldown, 0, deltaTime);
        }
    }
}
