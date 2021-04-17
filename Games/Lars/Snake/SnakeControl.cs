using System;
using YANMFA.Core;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace YANMFA.Games.Lars.Snake
{
    class SnakeControl : IGameInstance
    {
        public string GameName => "Snake";
        public string GameDescription => "Play as a snake and try to gather as many fruits as possible";
        public GameMode GameType => GameMode.SINGLE_AND_MULTIPLAYER;
        Image imgStart;
        Rectangle[,] field;
        SnakePlayer[] player;
        SnakeBerry berryList;
        int fieldCountX, fieldCountY, tickCounter, berrySpawnCounter;
        bool stopRequested, gameOver, multiplayer;

        public SnakeControl()
        {
            imgStart = Image.FromFile("./Assets/Lars/Snake/Logo.png");
            fieldCountX = 13;
            fieldCountY = 11;
            field = new Rectangle[fieldCountX, fieldCountY];
            berrySpawnCounter = 3;
        }

        public void CreateField()
        {
            float sqrWidth = StaticDisplay.DisplayWidth / fieldCountX;
            float sqrHeight = StaticDisplay.DisplayHeight / fieldCountY;
            for (int i = 0; i < fieldCountX; i++)
            {
                for (int j = 0; j < fieldCountY; j++)
                {
                    field[i, j] = new Rectangle(Convert.ToInt32(sqrWidth * i), Convert.ToInt32(sqrHeight * j), Convert.ToInt32(sqrWidth), Convert.ToInt32(sqrHeight));
                }
            }
        }

        public void CreatePlayer()
        {
            if (multiplayer)
            {
                player = new SnakePlayer[2];
                player[0] = new SnakePlayer(new Point(2, 3), new Vector2(0, -1), fieldCountX, fieldCountY, "Player 1");
                player[1] = new SnakePlayer(new Point(10, 8), new Vector2(0, -1), fieldCountX, fieldCountY, "Player 2");
            }
            else
            {
                player = new SnakePlayer[1];
                player[0] = new SnakePlayer(new Point(fieldCountX / 2, fieldCountY / 2), new Vector2(-1, 0), fieldCountX, fieldCountY, "Player");
            }
        }

        public Image GetTitleImage()
        {
            return imgStart;
        }
        public bool IsStopRequested()
        {
            return stopRequested;
        }

        public void Start(GameMode mode)
        {
            if (mode == GameMode.SINGLEPLAYER)
            {
                multiplayer = false;
            }
            else
            {
                multiplayer = true;
            }
            StaticDisplay.AddResizeListener(Resize);
            StaticKeyboard.AddKeyDownListener(KeyDown);
            stopRequested = false;
            Restart();            
        }

        public void IsKeyDown()
        {
            if (StaticKeyboard.IsKeyDown(Keys.W) && player[0].LastDirection.Y != 1)
            {
                player[0].Direction = new Vector2(0, -1);
            }
            else if (StaticKeyboard.IsKeyDown(Keys.S) && player[0].LastDirection.Y != -1)
            {
                player[0].Direction = new Vector2(0, 1);
            }
            else if (StaticKeyboard.IsKeyDown(Keys.A) && player[0].LastDirection.X != 1)
            {
                player[0].Direction = new Vector2(-1, 0);
            }
            else if (StaticKeyboard.IsKeyDown(Keys.D) && player[0].LastDirection.X != -1)
            {
                player[0].Direction = new Vector2(1, 0);
            }
            else if (multiplayer)
            {
                if (StaticKeyboard.IsKeyDown(Keys.Up) && player[1].LastDirection.Y != 1)
                {
                    player[1].Direction = new Vector2(0, -1);
                }
                else if (StaticKeyboard.IsKeyDown(Keys.Down) && player[1].LastDirection.Y != -1)
                {
                    player[1].Direction = new Vector2(0, 1);
                }
                else if (StaticKeyboard.IsKeyDown(Keys.Left) && player[1].LastDirection.X != 1)
                {
                    player[1].Direction = new Vector2(-1, 0);
                }
                else if (StaticKeyboard.IsKeyDown(Keys.Right) && player[1].LastDirection.X != -1)
                {
                    player[1].Direction = new Vector2(1, 0);
                }
            }
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                stopRequested = true;
            }
            else if (e.KeyCode == Keys.Space && gameOver)
            {
                Restart();
            }
        }

        public void Stop()
        {
            StaticDisplay.RemoveResizeListener(Resize);
            StaticKeyboard.RemoveKeyDownListener(KeyDown);
            stopRequested = false;
        }

        public void Resize(object sender, EventArgs e)
        {
            CreateField();
        }

        public void Render(Graphics g)
        {
            Brush brLight = new SolidBrush(Color.FromArgb(64, 0, 191, 51));
            Brush brSnake;
            Brush brBerry = new SolidBrush(Color.FromArgb(242, 61, 61));
            Pen penSnake;
            Pen penBlack = new Pen(Color.Black);
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (i % 2 != j % 2)
                    {
                        g.FillRectangle(brLight, field[i, j]);
                    }
                }
            }
            for (int i = 0; i < player.Length; i++)
            {
                if (i == 0)
                {
                    brSnake= new SolidBrush(Color.FromArgb(56, 110, 209));
                    penSnake = new Pen(Color.FromArgb(56, 110, 209));
                }
                else
                {
                    brSnake = new SolidBrush(Color.FromArgb(204, 7, 230));
                    penSnake = new Pen(Color.FromArgb(204, 7, 230));
                }
                for (int j = 0; j < player[i].Body.Count; j++)
                {
                    g.FillRectangle(brSnake, field[player[i].Body[j].X, player[i].Body[j].Y]);
                    g.DrawRectangle(penBlack, field[player[i].Body[j].X, player[i].Body[j].Y]);                   
                }
                foreach (var item in player[i].RemovedParts)
                {
                    g.DrawLine(penSnake, new Point(field[0, 0].Width * item[0].X, field[0, 0].Height * item[0].Y), new Point(field[0, 0].Width * item[1].X, field[0, 0].Height * item[1].Y));
                }
            }
            foreach (var item in berryList.BerryPos)
            {
                g.FillEllipse(brBerry, field[item.X, item.Y]);
                g.DrawEllipse(penBlack, field[item.X, item.Y]);
            }
        }

        public void Update()
        {
            IsKeyDown();
            tickCounter++;
            if (tickCounter > 17 && !gameOver)
            {                
                tickCounter = 0;
                for(int i = 0; i < player.Length; i++)
                {
                    if (!player[i].MoveSnake(berryList.BerryPos, MergeSnakes()))
                    {
                        gameOver = true;
                        MessageBox.Show(player[i].Name + " lost. Press Space to restart");
                    }
                }
                if (SpawnBerryReady())
                {
                    berryList.SpawnBerry(player, MergeSnakes());
                }
            }
        }

        public List<Point>[] MergeSnakes()
        {
            List<Point>[] snakeBody = new List<Point>[player.Length];
            for (int i = 0; i < snakeBody.Length; i++)
            {
                snakeBody[i] = player[i].Body;
            }
            return snakeBody;
        }

        public bool SpawnBerryReady()
        {
            int occupied = 0;
            for (int i = 0; i < player.Length; i++)
            {
                occupied += player[i].Body.Count;
            }
            if (berrySpawnCounter >= (fieldCountX * fieldCountY) - occupied)
            {
                berrySpawnCounter = (fieldCountX * fieldCountY) - occupied;
                if (berrySpawnCounter == 0)
                {
                    gameOver = true;
                    MessageBox.Show("Congratulations you won the game. Press Space to restart");
                }
            }           
            if (!gameOver && berryList.BerryPos.Count < berrySpawnCounter)
            {
                return true;
            }
            return false;
        }

        public void Restart()
        {
            CreatePlayer();
            CreateField();
            tickCounter = 0;
            berryList = new SnakeBerry(fieldCountX, fieldCountY);
            berryList.SpawnBerry(player,MergeSnakes());
            gameOver = false;
        }

        public void RenderSplash(Graphics g)
        {
        }
        public void UpdateSplash()
        {
        }
    }
}
