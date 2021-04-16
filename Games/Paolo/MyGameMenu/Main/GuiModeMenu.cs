using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GuiModeMenu : NavComponent
    {

        private const float PADDING = 0.025f;
        private const int COLUMN_COUNT = 2;
        private const int ROW_COUNT = 1;

        public IGameInstance Game { get; set; }

        private GridLayout GridLayout { get; } = new GridLayout()
        {
            PaddingX = PADDING,
            PaddingY = PADDING,
            Cols = COLUMN_COUNT,
            Rows = ROW_COUNT,

            Visible = true,
            Active = true
        };

        private RelativeLayout GuiLayout { get; } = new RelativeLayout()
        {
            Visible = true,
            Active = true
        };

        private readonly GameMenu Instance;
        public GuiModeMenu(GameMenu instance)
        {
            Instance = instance;
            SetLayout(GuiLayout);

            GuiLayout.Add(GridLayout, new RectangleF(0.25f, 0.25f, 0.5f, 0.5f));
        }

        public override void Show()
        {
            GridLayout.Clear();
            switch (Game.GameType)
            {
                case GameMode.SINGLEPLAYER:
                case GameMode.MULTIPLAYER:
                    GridLayout.Add(new ModeButton(Game, Game.GameType)
                    {
                        Visible = true,
                        Active = true
                    }, new Rectangle(0, 0, 2, 1));
                    break;

                case GameMode.SINGLE_AND_MULTIPLAYER:
                    GridLayout.Add(new ModeButton(Game, GameMode.SINGLEPLAYER)
                    {
                        Visible = true,
                        Active = true
                    }, new Rectangle(0, 0, 1, 1));

                    GridLayout.Add(new ModeButton(Game, GameMode.MULTIPLAYER)
                    {
                        Visible = true,
                        Active = true
                    }, new Rectangle(1, 0, 1, 1));
                    break;

                default: break;
            }

            base.Show();
        }

        public override void KeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Instance.ChangeScreen(Instance.GuiGameMenu);
        }

    }
}
