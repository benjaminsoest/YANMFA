using YANMFA.Core;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GuiGameMenu : NavComponent
    {

        private const float PADDING = 0.025f;
        private const int COLUMN_COUNT = 4;
        private const int ROW_COUNT = 4;

        private GridLayout GuiLayout { get; } = new GridLayout()
        {
            PaddingX = PADDING,
            PaddingY = PADDING,
            Cols = COLUMN_COUNT,
            Rows = ROW_COUNT,

            Visible = true,
            Active = true
        };

        private readonly GameMenu Instance;
        public GuiGameMenu(GameMenu instance)
        {
            Instance = instance;
            SetLayout(GuiLayout);
        }

        public override void Show()
        {
            GuiLayout.Clear();
            for (int i = 0; i < StaticEngine.Games.Count; i++)
            { // TODO: Change
                GuiLayout.Add(new GameButton(Instance, StaticEngine.Games[i])
                {
                    Visible = true,
                    Active = true
                }, new Rectangle((i - 0) % COLUMN_COUNT, (i - 0) / ROW_COUNT, 1, 1));
            }

            base.Show();
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
