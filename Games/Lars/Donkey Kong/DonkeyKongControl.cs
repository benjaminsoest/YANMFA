using System;
using YANMFA.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace YANMFA.Games.Lars.Donkey_Kong
{
    class DonkeyKongControl : IGameInstance
    {
        public string GameName => "Donkey Kong";
        public string GameDescription => "Try to climbe to the top while avoiding barrels";
        public GameMode GameType => GameMode.SINGLEPLAYER;
        List<Stage> stages;
        List<Barrel> barrels;
        int barrelSpawn, tickCount;
        float gravity;
        Image imgLogo;
        bool bStopRequested;

        public DonkeyKongControl()
        {
            imgLogo = Image.FromFile("./Assets/Lars/Donkey Kong/Logo.png");
        }

        public void Start(GameMode mode)
        {
            bStopRequested = false;
            barrels = new List<Barrel>();
            StaticKeyboard.AddKeyDownListener(KeyDown);
            tickCount = 100;
            barrelSpawn = 200;
            gravity = 0.6F;
            CreateLevel();
        }

        public void Stop()
        {
            StaticKeyboard.RemoveKeyDownListener(KeyDown);
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                bStopRequested = true;
            }
        }

        public void CreateLevel()
        {
            List<Stage> stage = new List<Stage>();
            stage.Add(new Stage(new Vector2(0,30), new Vector2(70,0)));
            stage.Add(new Stage(new Vector2(70, 30), new Vector2(StaticDisplay.DisplayWidth - 80 - 70, 50)));
            stage.Add(new Stage(new Vector2(60, 160), new Vector2(StaticDisplay.DisplayWidth - 60, -40)));
            stages = stage;
        }

        public void Update()
        {
            tickCount++;
            if (tickCount == barrelSpawn)
            {
                tickCount = 0;
                barrels.Add(new Barrel(new RectangleF(71, 0, 20, 20), gravity, stages));
            }
            foreach (var item in barrels)
            {
                item.Move();
            }
        }
        
        public void Render(Graphics g)
        {
            foreach (var item in stages)
            {
                PointF[] p = item.ReturnDrawingPoints();
                g.DrawLine(Pens.Black, p[0], p[1]);
            }
            foreach (var item in barrels)
            {
                g.FillEllipse(Brushes.Brown, item.rPosition);
            }
        }

        public Image GetTitleImage()
        {
            return imgLogo;
        }
        public bool IsStopRequested()
        {
            return bStopRequested;
        }

        public void UpdateSplash()
        {
        }
        public void RenderSplash(Graphics g)
        {
        }
    }
}
