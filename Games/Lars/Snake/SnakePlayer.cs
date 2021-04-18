using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace YANMFA.Games.Lars.Snake
{
    class SnakePlayer
    {
        List<Point> body, berry;
        List<Point[]> removedParts;
        Vector2 direction, lastDirection;
        int fieldLength, fieldHeight;
        string name;

        public SnakePlayer(Point startPoint, Vector2 startDirection, int fieldX, int fieldY, string n)
        {
            body = new List<Point>();
            berry = new List<Point>();
            removedParts = new List<Point[]>();
            body.Add(startPoint);
            direction = startDirection;
            lastDirection = new Vector2();
            fieldLength = fieldX;
            fieldHeight = fieldY;
            name = n;
        }

        public List<Point> Body { get => body; set => body = value; }
        public List<Point> Berry { get => berry; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public Vector2 LastDirection { get => lastDirection; set => lastDirection = value; }
        public List<Point[]> RemovedParts { get => removedParts; set => removedParts = value; }
        public string Name { get => name; }

        public bool MoveSnake(List<Point> p, List<Point>[] combinedBodys)
        {
            Point newHead = body[body.Count - 1];
            newHead.X += (int)direction.X;
            newHead.Y += (int)direction.Y;
            if (newHead.X >= 0 && newHead.X < fieldLength && newHead.Y >= 0 && newHead.Y < fieldHeight && !ControlIntersection(newHead, combinedBodys, true))
            {
                bool newPart = false;
                if (BerryCollision(p) >= 0)
                {
                    newPart = true;
                }
                if (!newPart)
                {
                    body.RemoveAt(0);
                }
                body.Add(newHead);
                RemovePart(newPart);
                lastDirection = direction;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ControlIntersection(Point p, List<Point>[] snakeBodys, bool lastItem)
        {
            foreach (var item in snakeBodys)
            {
                if (lastItem)
                {
                    if (item.Contains(p) && p != item[0])
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.Contains(p))
                    {
                        return true;
                    }
                }               
            }
            return false;
        }

        public void RemovePart(bool newPart)
        {
            int i = body.Count - 1;
            Point[] newLine = new Point[2];
            if (direction.X == 1)//Right
            {
                newLine[0] = body[i];
                newLine[1] = new Point(body[i].X, body[i].Y + 1);
            }
            else if (direction.X == -1)//Left
            {
                newLine[0] = new Point(body[i].X + 1, body[i].Y);
                newLine[1] = new Point(body[i].X + 1, body[i].Y + 1);
            }
            else if (direction.Y == 1)//Down
            {
                newLine[0] = body[i];
                newLine[1] = new Point(body[i].X + 1, body[i].Y);
            }
            else//Up
            {
                newLine[0] = new Point(body[i].X, body[i].Y + 1);
                newLine[1] = new Point(body[i].X + 1, body[i].Y + 1);
            }
            removedParts.Add(newLine);
            if (!newPart)
            {
                removedParts.RemoveAt(0);
            }
        }

        public int BerryCollision(List<Point> p)
        {
            berry = p;
            for (int i = 0; i < p.Count; i++)
            {
                if (body[body.Count - 1] == p[i])
                {
                    berry.RemoveAt(i);
                    return i;
                }
            }
            return -1;
        }
    }
}
