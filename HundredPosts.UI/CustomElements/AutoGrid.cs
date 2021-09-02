using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HundredPosts.UI.CustomElements
{
    public class AutoGrid : Grid
    {
        private GridLength cellSizing = new(1, GridUnitType.Star);
        public GridLength CellSizing
        {
            get => cellSizing;
            set
            {
                if (value == cellSizing)
                    return;

                foreach (var row in RowDefinitions)
                {
                    row.Height = value;
                }

                foreach (var column in ColumnDefinitions)
                {
                    column.Width = value;
                }

                cellSizing = value;
            }
        }

        private int columnCount;
        public int ColumnCount
        {
            get => columnCount;
            set
            {
                if (columnCount == value)
                    return;

                if (columnCount < value)
                {
                    for (int i = 0; i < value - columnCount; i++)
                    {
                        ColumnDefinitions.Add(new ColumnDefinition() { Width = CellSizing });
                    }
                }
                else
                {
                    ColumnDefinitions.RemoveRange(value, columnCount - value);
                }

                ResetChildCells();

                columnCount = value;
            }
        }

        private int rowCount;

        public int RowCount
        {
            get => rowCount;
            set
            {
                if (rowCount == value)
                    return;

                if (rowCount < value)
                {
                    for (int i = 0; i < value - rowCount; i++)
                    {
                        RowDefinitions.Add(new RowDefinition() { Height = CellSizing });
                    }
                }
                else
                {
                    RowDefinitions.RemoveRange(value, rowCount - value);
                }

                ResetChildCells();

                rowCount = value;
            }
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (columnCount * rowCount < Children.Count)
                return;

            //rather inefficient
            ResetChildCells();

            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        private void ResetChildCells()
        {
            int rc = RowDefinitions.Count;
            int cc = ColumnDefinitions.Count;

            var e = Children.Cast<UIElement>().GetEnumerator();
            bool exit = false;

            for (int i = 0; i < rc && !exit; i++)
            {
                for (int j = 0; j < cc; j++)
                {
                    if (!e.MoveNext())
                    {
                        exit = true;
                        break;
                    }

                    var current = e.Current;
                    SetCell(current, i, j);
                }
            }
        }

        private static void SetCell(UIElement current, int row, int column)
        {
            SetRow(current, row);
            SetColumn(current, column);
        }
    }
}
