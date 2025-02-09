using Foster.Framework;

namespace AlmostGoodFoster.Components.Collisions
{
    public readonly struct Hitbox
    {
        public enum Shapes
        {
            Rect,
            Grid
        }

        public readonly Shapes Shape;
        private readonly RectInt _rect;
        private readonly bool[,]? _grid;

        public Hitbox(in RectInt value)
        {
            Shape = Shapes.Rect;
            _rect = value;
            _grid = null;
        }

        public Hitbox(in bool[,] value)
        {
            Shape = Shapes.Grid;
            _grid = value;
        }

        public Hitbox()
        {
            Shape = Shapes.Rect;
            _rect = new(0, 0, 0, 0);
            _grid = null;
        }

        public bool Overlaps(in RectInt rect)
        {
            return Overlaps(Point2.Zero, new Hitbox(rect));
        }

        public bool Overlaps(in Hitbox other)
        {
            return Overlaps(Point2.Zero, other);
        }

        public bool Overlaps(in Point2 offset, in Hitbox other)
        {
            switch (Shape)
            {
                case Shapes.Rect:
                    switch (other.Shape)
                    {
                        case Shapes.Rect: return RectToRect(_rect + offset, other._rect);
                        case Shapes.Grid: return RectToGrid(_rect + offset, other._grid!);
                    }
                    break;
                case Shapes.Grid:
                    switch (other.Shape)
                    {
                        case Shapes.Rect: return RectToGrid(other._rect - offset, _grid!);
                        case Shapes.Grid: throw new NotImplementedException($"Grid to Grid overlap detection not implemented!");
                    }
                    break;
            }

            throw new NotImplementedException();
        }

        private static bool RectToRect(in RectInt a, in RectInt b)
        {
            return a.Overlaps(b);
        }

        private static bool RectToGrid(in RectInt a, in bool[,] grid)
        {
            int left = Calc.Clamp((int)Math.Floor(a.Left / (float)Settings.TileSize), 0, grid.GetLength(0));
            int right = Calc.Clamp((int)Math.Ceiling(a.Right / (float)Settings.TileSize), 0, grid.GetLength(0));
            int top = Calc.Clamp((int)Math.Floor(a.Top / (float)Settings.TileSize), 0, grid.GetLength(1));
            int bottom = Calc.Clamp((int)Math.Ceiling(a.Bottom / (float)Settings.TileSize), 0, grid.GetLength(1));

            for (int x = left; x < right; x++)
            {
                for (int y = top; y < bottom; y++)
                {
                    if (grid[x, y])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Render(Batcher batcher, Point2 offset, Color color)
        {
            batcher.PushMatrix(offset);

            if (Shape == Shapes.Rect)
            {
                batcher.RectLine(_rect + offset, 1, color);
            }
            else if (Shape == Shapes.Grid && _grid != null)
            {
                for (int x = 0; x < _grid.GetLength(0); x++)
                {
                    for (int y = 0; y < _grid.GetLength(1); y++)
                    {
                        if (_grid[x, y])
                        {
                            batcher.RectLine(new RectInt(x, y, 1, 1) * Settings.TileSize, 1, color);
                        }
                    }
                }
            }

            batcher.PopMatrix();
        }
    }
}
