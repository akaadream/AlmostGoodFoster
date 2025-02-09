using System.Numerics;
using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Components.Particles
{
    public class ParticleEmitter : Component
    {
        private readonly List<Particle> _particles;
        private readonly Random _random;
        private readonly Texture _texture;

        public Vector2 Position;
        public float EmissionRate;
        public float ParticleLifetime;
        public float EmissionRadius;
        public Vector2 MinVelocity;
        public Vector2 MaxVelocity;
        public float MinRotationSpeed;
        public float MaxRotationSpeed;
        public float MinScale;
        public float MaxScale;
        public float MinScaleSpeed;
        public float MaxScaleSpeed;
        public Color StartColor;
        public Color EndColor;

        private float _emissionAccumulator;

        public ParticleEmitter(Texture texture, int maxParticles)
        {
            _texture = texture;
            _particles = new(maxParticles);
            _random = new();

            for (int i = 0; i < maxParticles; i++)
            {
                _particles.Add(new Particle { IsActive = false });
            }

            EmissionRate = 10;
            ParticleLifetime = 2f;
            EmissionRadius = 5f;
            MinVelocity = new Vector2(-50, -50);
            MaxVelocity = new Vector2(50, 50);
            MinRotationSpeed = (float)(-Math.PI / 4);
            MaxRotationSpeed = (float)(Math.PI / 4);
            MinScale = 0.5f;
            MaxScale = 1.0f;
            MinScaleSpeed = -0.1f;
            MaxScaleSpeed = -0.05f;
            StartColor = Color.White;
            EndColor = Color.Transparent;
        }

        public override void Update(float deltaTime)
        {
            foreach (var particle in _particles)
            {
                if (particle.IsActive)
                {
                    particle.Update(deltaTime);
                }
            }

            _emissionAccumulator += deltaTime * EmissionRate;
            while (_emissionAccumulator >= 1.0f)
            {
                EmitParticle();
                _emissionAccumulator -= 1.0f;
            }
        }

        private void EmitParticle()
        {
            Particle? particle = _particles.Find(p => !p.IsActive);
            if (particle == null)
            {
                return;
            }

            float angle = (float)(_random.NextDouble() * (Math.PI * 2));
            float distance = (float)(_random.NextDouble() * EmissionRadius);
            Vector2 offset = new((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance);
            particle.Position = Position + offset;
            particle.Velocity = new Vector2(
                Calc.Lerp(MinVelocity.X, MaxVelocity.X, (float)_random.NextDouble()),
                Calc.Lerp(MinVelocity.Y, MaxVelocity.Y, (float)_random.NextDouble()));
            particle.Rotation = (float)(_random.NextDouble() * Math.PI * 2);
            particle.Scale = Calc.Lerp(MinScale, MaxScale, (float)_random.NextDouble());
            particle.ScaleSpeed = Calc.Lerp(MinScaleSpeed, MaxScaleSpeed, (float)_random.NextDouble());
            particle.CurrentLifetime = 0;
            particle.Color = StartColor;
            particle.EndColor = EndColor;
            particle.IsActive = true;
        }

        public void Draw(Batcher batcher)
        {
            Vector2 origin = new(_texture.Width / 2, _texture.Height / 2);

            foreach (var particle in _particles)
            {
                if (particle.IsActive)
                {
                    batcher.Image(
                        _texture,
                        particle.Position,
                        origin,
                        new Vector2(particle.Scale),
                        particle.Rotation,
                        particle.Color);

                }
            }
        }
    }
}
