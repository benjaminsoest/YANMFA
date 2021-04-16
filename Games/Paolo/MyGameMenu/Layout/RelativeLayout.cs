using System;
using System.Collections.Generic;
using System.Drawing;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class RelativeLayout : GuiComponent
    {

        private readonly List<RectangleF> RelativeEntries = new List<RectangleF>();

        public void Add(GuiComponent item, RectangleF relativeEntry)
        {
            RelativeEntries.Add(relativeEntry);
            base.Add(item);
        }

        public new void Add(GuiComponent comp) => throw new InvalidOperationException();
        public new void Remove(GuiComponent comp) => throw new InvalidOperationException();

        public new void RemoveAt(int index)
        {
            RelativeEntries.RemoveAt(index);
            base.RemoveAt(index);
        }

        public new void Clear()
        {
            RelativeEntries.Clear();
            base.Clear();
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            float maxWidth = width;
            float maxHeight = height;

            for(int i = Count - 1; i >= 0; i--)
            {
                RectangleF relativeEntry = RelativeEntries[i];
                float entryWidth = (int)(width * relativeEntry.Width);
                float entryHeight = (int)(height * relativeEntry.Height);
                if (entryWidth < 0f || entryHeight < 0f)
                    throw new InvalidOperationException();

                float entryX = (int)(width * relativeEntry.X);
                float entryY = (int)(height * relativeEntry.Y);

                GuiComponent comp = this[i];
                comp.SetBounds((int)(x + entryX), (int)(y + entryY), (int)entryWidth, (int)entryHeight);
                if (maxWidth < entryX + comp.Bounds.Width)
                    maxWidth = entryX + comp.Bounds.Width;
                if (maxHeight < entryY + comp.Bounds.Height)
                    maxHeight = entryY + comp.Bounds.Height;
            }

            base.SetBounds(x, y, (int)maxWidth, (int)maxHeight);
        }

    }
}
