using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Lars.Donkey_Kong
{
    class Barrel
    {
        public RectangleF rPosition { get; set; }
        public Vector2 Direction { get; set; }
        public bool MidAir { get; set; }
        float gravitation;
        List<Stage> stages;
        List<bool> firstCollision;

        public Barrel(RectangleF r, float g, List<Stage> ls)
        {
            rPosition = r;
            gravitation = g;
            stages = ls;
            MidAir = true;
            firstCollision = new List<bool>();
            foreach (var item in ls)
                firstCollision.Add(false);
        }

        public void Gravitation()
        {
            if (Direction.Y < 10 && MidAir)
            {
                Direction = new Vector2(Direction.X, Direction.Y + gravitation);
            }
        }

        public void Move()
        {
            Gravitation();
            rPosition = new RectangleF(rPosition.X + Direction.X, rPosition.Y + Direction.Y, rPosition.Width, rPosition.Height);
            Vector2 pos = new Vector2(rPosition.X, rPosition.Y);
            Vector2 side = new Vector2(0, rPosition.Height);
            for (int j = 0; j < stages.Count; j++)
            {
                if (stages[j].Collision(pos, side) || stages[j].Collision(new Vector2(pos.X + rPosition.Width, pos.Y), side))
                {
                    rPosition = new RectangleF(rPosition.X, stages[j].GetYValue(rPosition.X) - rPosition.Height, rPosition.Width, rPosition.Height);          
                    MidAir = false;
                    if (!firstCollision[j])
                    {
                        firstCollision[j] = true;
                        Direction = new Vector2(0, 0);
                    }
                    //Console.WriteLine(rPosition.X + "   " + (stages[j].GetYValue(rPosition.X) - rPosition.Height) + "   " + rPosition.Width + "   " + rPosition.Height + "   " + firstCollision[2]);
                    Direction = new Vector2(Direction.X + (float)Math.Sin(90 - stages[j].GetAngle()) * gravitation * (float)Math.Sin(stages[j].GetAngle()), Direction.Y + (float)Math.Cos(90 - stages[j].GetAngle()) * gravitation * (float)Math.Sin(stages[j].GetAngle()));

                    int s = 9;
                    if (Direction.Y >= s)
                        Direction = new Vector2(Direction.X, s);
                    if (Direction.X >= s)
                        Direction = new Vector2(s, Direction.Y);
                    break;
                }
                MidAir = true;
            }
        }
    }
}
