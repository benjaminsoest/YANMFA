using System;
using System.Collections.Generic;
using System.Drawing;

namespace YANMFA.Games.Lars.Snake
{
    class SnakeBerry
    {
        List<Point> berryPos;
        int width, height;

        public SnakeBerry(int x, int y)
        {
            berryPos = new List<Point>();
            width = x;
            height = y;
        }

        public List<Point> BerryPos { get => berryPos; }

        public void SpawnBerry(SnakePlayer[] player, List<Point>[] snakeBodys)
        {
            Random rnd = new Random();
            Point newBerry = new Point();
            bool control;
            do
            {
                control = true;
                newBerry.X = rnd.Next(width);
                newBerry.Y = rnd.Next(height);
                if (player[0].ControlIntersection(newBerry, snakeBodys))
                {
                        control = false;
                }              
                if (berryPos.Contains(newBerry))
                {
                    control = false;
                }
            } while (!control);
            berryPos.Add(newBerry);
        }
    }
}
