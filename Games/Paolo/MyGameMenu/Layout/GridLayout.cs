using System;
using System.Collections.Generic;
using System.Drawing;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GridLayout : GuiComponent
    {

        private readonly List<Rectangle> GridEntries = new List<Rectangle>();

        public int Cols { get; set; }
        public int Rows { get; set; }

        public float PaddingX { get; set; }
        public float PaddingY { get; set; }

        public void Add(GuiComponent item, Rectangle gridEntry)
        {
            GridEntries.Add(gridEntry);
            base.Add(item);
        }

        public new void Add(GuiComponent comp) => throw new InvalidOperationException();
        public new void Remove(GuiComponent comp) => throw new InvalidOperationException();

        public new void RemoveAt(int index)
        {
            GridEntries.RemoveAt(index);
            base.RemoveAt(index);
        }

        public new void Clear()
        {
            GridEntries.Clear();
            base.Clear();
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            float padX = width * PaddingX;
            float padY = height * PaddingY;
            float gridWidth = width / Cols - padX * (1f + 1f / Cols);
            float gridHeight = height / Rows - padY * (1f + 1f / Rows);

            float maxWidth = width;
            float maxHeight = height;
            for(int i = Count - 1; i >= 0; i--)
            {
                Rectangle gridEntry = GridEntries[i];
                float entryWidth = gridWidth * gridEntry.Width + padX * (gridEntry.Width - 1f);
                float entryHeight = gridHeight * gridEntry.Height + padY * (gridEntry.Height - 1f);
                if (entryWidth < 0f || entryHeight < 0f)
                    throw new InvalidOperationException();

                float entryX = padX + (padX + gridWidth) * gridEntry.X;
                float entryY = padY + (padY + gridHeight) * gridEntry.Y;

                GuiComponent comp = this[i];
                comp.SetBounds((int)(x + entryX), (int)(y + entryY), (int)entryWidth, (int)entryHeight);
                if (maxWidth < entryX + comp.Bounds.Width)
                    maxWidth = entryX + comp.Bounds.Width;
                if (maxHeight < entryY + comp.Bounds.Height)
                    maxHeight = entryY + comp.Bounds.Height;
            }

            base.SetBounds(x, y, (int) (maxWidth + padX), (int) (maxHeight + padY));
        }

    }
}
