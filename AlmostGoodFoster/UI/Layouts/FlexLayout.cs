using AlmostGoodFoster.UI.Containers;

namespace AlmostGoodFoster.UI.Layouts
{
    public enum FlexDirection
    {
        Row,
        Column
    }

    public enum JustifyContent
    {
        Start = 0,
        Center,
        SpaceBetween,
        SpaceAround,
        End
    }

    public enum AlignItems
    {
        Start = 0,
        Center,
        Stretch,
        End
    }

    public class FlexLayout(UIElement? parent, UIContainer container) : Layout(parent, container)
    {
        private int _nextX = 0;
        private int _nextY = 0;

        public JustifyContent JustifyContent { get; set; } = JustifyContent.Start;

        public AlignItems AlignItems { get; set; } = AlignItems.Start;

        public FlexDirection Direction { get; set; } = FlexDirection.Row;

        public override void AddChild(UIElement child)
        {
            base.AddChild(child);



            // Manage child position / size
            foreach (var element in Children)
            {
                element.Left = _nextX;
                element.Top = _nextY;

                switch (Direction)
                {
                    case FlexDirection.Row:
                        _nextX += element.Width;
                        break;
                    case FlexDirection.Column:
                        _nextY += element.Height;
                        break;
                }
            }
        }

        private int GetPivotX()
        {
            switch (JustifyContent)
            {
                case JustifyContent.Start:
                    break;
                case JustifyContent.Center:
                    break;
                case JustifyContent.SpaceBetween:
                    break;
                case JustifyContent.SpaceAround:
                    break;
                case JustifyContent.End:
                    break;
            }

            return X;
        }

        private int GetPivotY()
        {
            switch (AlignItems)
            {
                case AlignItems.Start:
                    break;
                case AlignItems.Center:
                    break;
                case AlignItems.End:
                    break;
            }
            return Y;
        }
    }
}
