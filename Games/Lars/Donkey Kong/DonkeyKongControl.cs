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
        public string GameDescription => "Try to climb to the top while avoiding the barrels";
        public GameMode GameType => GameMode.SINGLE_AND_MULTIPLAYER;
        Image imgLogo;
        bool stopRequested;
        List<Stage> stages;
        List<Barrel> barrels;
        //Player player;
        int spawnTime, spawner, width, height;
        float time;

        public DonkeyKongControl()
        {
            imgLogo = Image.FromFile("./Assets/Lars/Donkey Kong/Logo.png");
        }


        public void Update()
        {
            spawner++;
            if (spawnTime == spawner)//Spawn a new Barrel
            {
                spawner = 0;
                barrels.Add(new Barrel(new Point(70, 15), 15, 0));
            }
            foreach (var item in barrels)//Every Barrel gets checked
            {
                if (item.Counter <= time - item.Position) //The Barrel is on the ground/line
                {
                    if (stages[item.Line].DirectionVector.Y != 0)
                    {
                        item.BarrelMovement(stages[item.Line].DirectionVector, time);
                    }
                    else
                    {
                        Vector2 v2 = new Vector2(stages[item.Line - 1].DirectionVector.X, 0);
                        item.BarrelMovement(v2, time);
                    }
                }
                else if (item.BarrelDraw.X >= stages[item.Line].PRight.X || item.BarrelDraw.Right <= stages[item.Line].PLeft.X)//The Barrel is in the "air"
                {
                    item.Gravitation += (float)0.8;
                    item.BarrelMovement(stages[item.Line].DirectionVector, time);//The barrel moves on but with extra vertical movement
                    if (item.BarrelDraw.Right >= width || item.BarrelDraw.X <= 0 || stages[item.Line + 1].CollisionDetection(item.BarrelDraw.Bottom, item.BarrelDraw.X + item.BarrelDraw.Width / 2, time))// The barrel reached the right or left side of the window, or reached the next platform
                    {
                        item.Line++;
                        item.Counter = 0; //Start at 0 again
                        item.Gravitation = 0; //No extra vertical movement
                        item.BarrelObject = new RectangleF(item.BarrelDraw.X, stages[item.Line].YValue - (item.BarrelObject.Width + (float)0.5), item.BarrelObject.Width, item.BarrelObject.Width);//The "start" position of the barrel changes to a position on top of the new stage so that the barrel is always on top 
                        item.Position = stages[item.Line].CollisionValue; // The position of the collision gets saved
                    }
                }
                else // The barrels gets stuck sometimes due to rounding errors so the barrel has to be moved a bit to continue
                {
                    item.BarrelMovement(stages[item.Line].DirectionVector, time);
                }
            }
            //if (!CollisionPlayerBarrel())
            //{
            //    MessageBox.Show("Hit");
            //}
            for (int i = 0; i < barrels.Count; i++)
            {
                if (barrels[i].Line + 1 == stages.Count && !(barrels[i].Counter <= time - barrels[i].Position))// If the barrel is at the end of the last track it gets removed to prevent chrashes
                {
                    barrels.RemoveAt(i);
                }
                else if (barrels[i].BarrelDraw.Right <= 0 || barrels[i].BarrelDraw.X >= width)
                {
                    barrels.RemoveAt(i);
                }
            }
        }

        public void Render(Graphics g)
        {
            Pen penBlack = new Pen(Color.Black);
            Brush brBrown = new SolidBrush(Color.Brown);
            Brush brGreen = new SolidBrush(Color.Green);            
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (var item in stages)
            {
                g.DrawLine(penBlack, item.PLeft, item.PRight);
            }
            foreach (var item in barrels)
            {
                g.FillEllipse(brBrown, item.BarrelDraw);
            }
        }

        public void Start(GameMode mode)
        {
            stopRequested = false;
            stages = new List<Stage>();
            barrels = new List<Barrel>();
            //player = new Player(new Point(0, 290), new Size(20, 30), stages);            
            time = 150;
            spawnTime = 200;
            spawner = 140;
            width = StaticDisplay.DisplayWidth;
            height = StaticDisplay.DisplayHeight;
            StaticKeyboard.AddKeyDownListener(KeyDown);
            StaticDisplay.AddResizeListener(Resize);
            LinesAdd();
        }

        public void LinesAdd()
        {
            stages.Add(new Stage(new Point(0, 30), new Point(70, 30)));
            stages.Add(new Stage(new Point(70, 30), new Point(width - 80, 80)));
            stages.Add(new Stage(new Point(70, 160), new Point(width, 110)));
            stages.Add(new Stage(new Point(0, 190), new Point(width - 80, 240)));
            stages.Add(new Stage(new Point(70, 320), new Point(width, 270)));
            stages.Add(new Stage(new Point(0, 320), new Point(70, 320)));
        }

        public void Stop()
        {
            StaticKeyboard.RemoveKeyDownListener(KeyDown);
            StaticDisplay.RemoveResizeListener(Resize);
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                stopRequested = true;
            }
        }

        public void Resize(object sender, EventArgs e)
        {
            
        }

        public Image GetTitleImage()
        {
            return imgLogo;
        }

        public bool IsStopRequested()
        {
            return stopRequested;
        }

        public void RenderSplash(Graphics g)
        {
        }    
        public void UpdateSplash()
        {
        }
    }
}
