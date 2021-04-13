namespace GDI_GameEngine.Paolo.MyGameMenu
{
    class NavComponent : GuiComponent
    {

        public GuiComponent Layout { get; private set; }

        public void Refresh() => SetBounds(Bounds);

        public override void Resize(int width, int height)
        {
            SetBounds(0, 0, width, height);
            base.Resize(width, height);
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            base.SetBounds(x, y, width, height);
            Layout?.SetBounds(x, y, width, height);
        }

        public void SetLayout(GuiComponent layout)
        {
            Remove(Layout);
            Layout = layout;
            Add(Layout);
        }

    }
}
