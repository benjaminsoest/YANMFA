using GDI_GameEngine.GameEngine;
using System.Drawing;
using System.Windows.Forms;

namespace GDI_GameEngine.Paolo.MyGameMenu
{
    class GuiMainMenu : NavComponent
    {

        private const float PADDING = 0.025f;
        private const int COLUMN_COUNT = 4;
        private const int ROW_COUNT = 4;

        private GridLayout GridLayout { get; } = new GridLayout()
        {
            Cols = COLUMN_COUNT,
            Rows = ROW_COUNT,
            PaddingX = PADDING,
            PaddingY = PADDING,

            Visible = true,
            Active = true
        };

        public GuiMainMenu() => SetLayout(GridLayout);

        public override void Show()
        {
            GridLayout.Clear();
            for (int i = 0; i < StaticEngine.Games.Count; i++)
            {
                GridLayout.Add(new GameButton(StaticEngine.Games[i])
                {
                    Visible = true,
                    Active = true
                }, new Rectangle(i % COLUMN_COUNT, i / ROW_COUNT, 1, 1));
            }

            base.Show();
        }

        public override void Hide()
        {
            GridLayout.Clear();
            base.Hide();
        }

        public override void Render(Graphics g)
        {
            base.Render(g);

            {
                string text = $"Framerate: {StaticDisplay.FPSCount}/{StaticDisplay.FPSCap}\n" +
                                $"Delta time: {StaticDisplay.FixedDelta}/1.0";
                Size size = TextRenderer.MeasureText(text, SystemFonts.StatusFont);
                g.DrawString(text, SystemFonts.StatusFont, Brushes.Black, 5f, StaticDisplay.DisplayHeight - size.Height - 5f);
            }
            {
                string text = $"Paolo V. ~ Work in progress!";
                Size size = TextRenderer.MeasureText(text, SystemFonts.StatusFont);
                g.DrawString(text, SystemFonts.StatusFont, Brushes.Black, StaticDisplay.DisplayWidth - size.Width - 5f, StaticDisplay.DisplayHeight - size.Height - 5f);
            }
        }

    }
}
