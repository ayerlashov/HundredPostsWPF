using System;
using System.Collections.Generic;
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

        private Dictionary<UIElement, LinkedListNode<(int index, UIElement uiElement)>> UIElementNodeDict { get; set; } = new();
        private LinkedList<(int index, UIElement uiElement)> ChildrenMirror { get; set; } = new();

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded is UIElement uiElement)
                ProcessAdded(uiElement);

            if (visualRemoved is UIElement uIElement)
                ProcessRemoved(uIElement);

            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        private void ProcessRemoved(UIElement uIElement)
        {
            var node = UIElementNodeDict[uIElement];

            int removedIndex = node.Value.index;
            int rowIndex = Math.DivRem(removedIndex, columnCount, out int columnIndex);

            var currentNode = node.Next;
            while (currentNode != null)
            {
                ref var currentValue = ref currentNode.ValueRef;

                currentValue.index--;

                if (rowIndex < RowCount)
                {
                    SetCell(currentValue.uiElement, rowIndex, columnIndex);
                }

                columnIndex++;

                if (columnIndex >= ColumnCount)
                {
                    columnIndex = 0;
                    rowIndex++;
                }

                currentNode = currentNode.Next;
            }

            ChildrenMirror.Remove(node);
            _ = UIElementNodeDict.Remove(uIElement);
        }

        private void ProcessAdded(UIElement uiElement)
        {
            int childIndex = ChildrenMirror.Count;
            var node = new LinkedListNode<(int, UIElement)>((childIndex, uiElement));

            ChildrenMirror.AddLast(node);

            UIElementNodeDict[uiElement] = node;

            if (childIndex >= rowCount * columnCount)
                return;

            int rowNumber = Math.DivRem(childIndex, ColumnCount, out int columnNumber);

            SetCell(uiElement, rowNumber, columnNumber);
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

                    if(current != null)
                        SetCell(current, i, j);
                }
            }
        }

        private static void SetCell(UIElement element, int row, int column)
        {
            SetRow(element, row);
            SetColumn(element, column);
        }

        private static (int row, int column) GetCell(UIElement element) => (GetRow(element), GetColumn(element));
    }
}
