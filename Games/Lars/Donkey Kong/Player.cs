using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Lars.Donkey_Kong
{
    class Player
    {
        public RectangleF RBody { get; set; }
        public RectangleF RBodyOld { get; set; }
        public Vector2 Direction { get; set; }
        public bool MidAir { get; set; }
        float gravitation;
        List<Stage> stages;

        public Player(RectangleF r, float g, List<Stage> ls)
        {
            RBody = r;
            gravitation = g;
            stages = ls;
        }

        public void Gravitation()
        {
            if (Direction.Y < 20 && MidAir)
            {
                Direction = new Vector2(Direction.X, Direction.Y + gravitation);
            }
        }

        public void Move()
        {
            Gravitation();
            RBodyOld = RBody;
            RBody = new RectangleF(RBody.X + Direction.X, RBody.Y + Direction.Y, RBody.Width, RBody.Height);
        }
    }
}
