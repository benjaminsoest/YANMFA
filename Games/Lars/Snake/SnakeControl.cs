using System;
using YANMFA.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Lars.Snake
{
    class SnakeControl : IGameInstance
    {
        public string GameName => "Snake";
        public string GameDescription => "Play as a Snake and try to gather as many fruits as possible";

        Rectangle[,] field;
        int[] direction, lastDirection, posBerry, posHead;
        List<Point[]> pRemove;
        List<Rectangle> snake;
        Random rnd;
        int fieldX, fieldY, sqrWidth, sqrHeight, tickCounter;
        //bool finish;
        bool stop;        

        public SnakeControl()
        {
            stop = false;
            snake = new List<Rectangle>();
            rnd = new Random();
            posBerry = new int[2];
            pRemove = new List<Point[]>();
            fieldX = 13;
            fieldY = 11;
            field = new Rectangle[fieldY, fieldX];
            sqrWidth = StaticDisplay.DisplayWidth / fieldX;
            sqrHeight = StaticDisplay.DisplayHeight / fieldY;
            CreateField();
            StartAttributes();
        }

        public Image GetTitleImage()
        {
            return Image.FromFile("./Assets/Lars/Snake/Logo.png");
        }

        public bool IsStopRequested()
        {
            return stop;
        }

        public void Render(Graphics g)
        {
            Brush brLight = new SolidBrush(Color.FromArgb(64, 0, 191, 51));
            Brush brSnake = new SolidBrush(Color.FromArgb(56, 110, 209));
            Brush brBerry = new SolidBrush(Color.FromArgb(242, 61, 61));
            Pen penSnake = new Pen(Color.FromArgb(56, 110, 209));
            Pen penBlack = new Pen(Color.Black);
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
            foreach (var item in snake)
            {
                g.FillRectangle(brSnake, item);
                g.DrawRectangle(penBlack, item);
            }
            for (int i = 0; i < pRemove.Count; i++)
            {
                g.DrawLine(penSnake, pRemove[i][0], pRemove[i][1]);
            }
            g.FillEllipse(brBerry, field[posBerry[0], posBerry[1]]);
            g.DrawEllipse(penBlack, field[posBerry[0], posBerry[1]]);
        }

        public void RenderSplash(Graphics g)
        {
        }

        public void Start()
        {
            StaticKeyboard.AddKeyDownListener(KeyDown);
        }


        private void KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up) && lastDirection[0] != 1)
            {
                direction = new int[] { -1, 0 };
            }
            else if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down) && lastDirection[0] != -1)
            {
                direction = new int[] { 1, 0 };
            }
            else if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && lastDirection[1] != 1)
            {
                direction = new int[] { 0, -1 };
            }
            else if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && lastDirection[1] != -1)
            {
                direction = new int[] { 0, 1 };
            }
            //else if (e.KeyCode == Keys.Space && finish)
            //{
            //    Restart();
            //}
        }
        public void StartAttributes()
        {
            direction = new int[] { 0, -1 };
            lastDirection = direction;
            snake.Add(field[fieldY / 2, fieldX / 2]);
            posHead = new int[] { fieldY / 2, fieldX / 2 };
            SpawnBerry();
            //finish = false;
            //MessageBox.Show("Press Space to start");            
        }

        public void RemoveLine(Rectangle head, bool remove)
        {
            Point[] p = new Point[2];
            int hX = head.X;
            int hY = head.Y;
            if (direction[0] == 1)
            {
                p[0] = new Point(hX, hY);
                p[1] = new Point(hX + sqrWidth, hY);
            }
            else if (direction[0] == -1)
            {
                p[0] = new Point(hX, hY + sqrHeight);
                p[1] = new Point(hX + sqrWidth, hY + sqrHeight);
            }
            else if (direction[1] == 1)
            {
                p[0] = new Point(hX, hY);
                p[1] = new Point(hX, hY + sqrHeight);
            }
            else if (direction[1] == -1)
            {
                p[0] = new Point(hX + sqrWidth, hY);
                p[1] = new Point(hX + sqrWidth, hY + sqrHeight);
            }
            pRemove.Add(p);
            if (remove)
            {
                pRemove.RemoveAt(0);
            }
        }

        public void SpawnBerry()
        {
            bool control = true;
            do
            {
                posBerry[0] = rnd.Next(fieldY);
                posBerry[1] = rnd.Next(fieldX);
                if (!snake.Contains(field[posBerry[0], posBerry[1]]))
                {
                    control = false;
                }
            } while (control);

        }

        public void CreateField()
        {
            for (int i = 0; i < fieldY; i++)
            {
                for (int j = 0; j < fieldX; j++)
                {
                    field[i, j] = new Rectangle(sqrWidth * j, sqrHeight * i, sqrWidth, sqrHeight);
                }
            }
        }

        public void MoveSnake(bool remove)
        {
            Rectangle head = field[posHead[0], posHead[1]];
            head.X += sqrWidth * direction[1];
            head.Y += sqrHeight * direction[0];

            if (posHead[0] + direction[0] >= 0 && posHead[0] + direction[0] < fieldY && posHead[1] + direction[1] >= 0 && posHead[1] + direction[1] < fieldX && (!snake.Contains(head) || snake[snake.Count - 1] == head))
            {
                if (remove)
                {
                    snake.RemoveAt(snake.Count - 1);
                }
                RemoveLine(head, remove);
                snake.Insert(0, head);
                posHead[0] += direction[0];
                posHead[1] += direction[1];
                lastDirection = direction;
            }
            //else
            //{
            //    finish = true;
            //}
        }

        public void Stop()
        {
            StaticKeyboard.RemoveKeyDownListener(KeyDown);
        }

        public void Update()
        {
            tickCounter++;
            if (tickCounter == 15)
            {
                tickCounter = 0;
                if (posHead[0] == posBerry[0] && posHead[1] == posBerry[1])
                {
                    SpawnBerry();
                    MoveSnake(false);
                }
                else
                {
                    MoveSnake(true);
                }
            }
            Console.WriteLine(StaticDisplay.FPSCount);
        }

        public void UpdateSplash()
        {
        }
    }
}
