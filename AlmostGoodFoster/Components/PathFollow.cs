using AlmostGoodFoster.EC;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Components
{
    public class PathFollow : Component
    {
        /// <summary>
        /// Value between 0 and 1 representing the position in the path
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// True if the path may loop
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Points defining the shape of the path
        /// </summary>
        protected List<Vector2> Points { get; private set; }

        private List<float> _distances;

        private float _startValue;
        private bool _goingOntoNext;
        private float _nextValue;
        private float _transitionTimer;
        private float _transitionDuration;

        public PathFollow()
        {
            Value = 0;
            Points = [];
            _distances = [];
        }

        public Vector2 GetAtValue()
        {
            float totalDistance = TotalDistance();
            float distance = Value * totalDistance;
            int index = FindDistanceIndex(distance);
            float distanceUntil = GetDistanceUntil(index);
            float previousDistance = PreviousDistanceUntil(index);

            float percentage = float.Clamp((distance - previousDistance) / (distanceUntil - previousDistance), 0f, 1f);

            if (index == -1)
            {
                return Points[0];
            }

            if (index == _distances.Count - 1 && Loop)
            {
                return Points[index] + (Points[0] - Points[index]) * percentage;
            }
            return Points[index] + (Points[index + 1] - Points[index]) * percentage;
        }

        public float PreviousDistanceUntil(int index)
        {
            return GetDistanceUntil(index - 1);
        }

        public float GetDistanceUntil(int index)
        {
            float distance = 0f;
            for (int i = 0; i <= index; i++)
            {
                distance += _distances[i];
            }

            return distance;
        }

        public int FindDistanceIndex(float distance)
        {
            float previousDistances = 0f;
            for (int i = 0; i < _distances.Count; i++)
            {
                if (distance >= previousDistances&&
                    distance < _distances[i] + previousDistances)
                {
                    return i;
                }

                previousDistances += _distances[i];
            }

            return -1;
        }

        public int ValueToIndex()
        {
            return ValueToIndex(Value);
        }

        public int ValueToIndex(float value)
        {
            return FindDistanceIndex(Value * TotalDistance());
        }

        public float IndexToValue(int index)
        {
            return float.Clamp(PreviousDistanceUntil(index) / TotalDistance(), 0, 1);
        }

        public void GoToNextPoint(float duration = 0.5f)
        {
            if (_goingOntoNext)
            {
                return;
            }
            _transitionTimer = 0f;
            _transitionDuration = duration;
            _startValue = Value;
            bool restart = false;
            if (Value == 1f)
            {
                if (Loop)
                {
                    restart = true;
                    _startValue = 0;
                }
                else
                {
                    return;
                }
            }
            int index = restart ? 1 : ValueToIndex() + 1;
            _nextValue = IndexToValue(index);
            _goingOntoNext = true;
        }

        public void AddPoint(Vector2 point)
        {
            Points.Add(point);
            ComputeDistances();
        }

        private void RemovePoint(Vector2 point)
        {
            Points.Remove(point);
            ComputeDistances();
        }

        private void RemovePoint(int index)
        {
            Points.RemoveAt(index);
            ComputeDistances();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //Value += deltaTime * 0.2f;
            //if (Value > 1f)
            //{
            //    Value -= 1f;
            //}
            if (_goingOntoNext)
            {
                if (_transitionTimer >= _transitionDuration)
                {
                    // Compute new Value
                    _goingOntoNext = false;
                    Value = _nextValue;
                    if (Value == 1f && Loop)
                    {
                        Value = 0f;
                    }
                }
                else
                {
                    _transitionTimer += deltaTime;
                    float t = float.Clamp(_transitionTimer / _transitionDuration, 0f, 1f);
                    Value = float.Lerp(_startValue, _nextValue, t);
                }
            }
        }

        public void ComputeDistances()
        {
            _distances.Clear();
            for (int i = 0; i < Points.Count - 1; i++)
            {
                _distances.Add(Vector2.Distance(Points[i], Points[i + 1]));
                if (i == Points.Count - 2 && Loop)
                {
                    _distances.Add(Vector2.Distance(Points[i + 1], Points[0]));
                }
            }
        }

        public float TotalDistance()
        {
            return _distances.Sum();
        }

        public override void DrawGUI(Batcher batcher, float deltaTime)
        {
            if (Points.Count < 2)
            {
                return;
            }

            // Draw path lines
            for (int i = 0; i < Points.Count - 1; i++)
            {
                batcher.LineDashed(Points[i], Points[i + 1], 3f, Color.White, 5f, 1f);
                if (i == Points.Count - 2 && Loop)
                {
                    batcher.LineDashed(Points[i + 1], Points[0], 3f, Color.White, 5f, 1f);
                }
            }

            // Draw point at value
            batcher.Circle(new Circle(GetAtValue(), 10f), 20, Color.Black * 0.8f);
        }
    }
}
