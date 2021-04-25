using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Lars.Donkey_Kong
{
    class Stage
    {
        public Vector2 SupportV { get; set; }
        public Vector2 DirectionV { get; set; }

        public Stage(Vector2 s, Vector2 d)
        {
            SupportV = s;
            DirectionV = d;
        }

        public PointF[] ReturnDrawingPoints()
        {
            return new PointF[2] { new PointF(SupportV.X, SupportV.Y), new PointF(SupportV.X + DirectionV.X, SupportV.Y + DirectionV.Y) };
        }

        public bool Collision(Vector2 sv, Vector2 dv)
        {
            float valueRectangle = (DirectionV.X * (sv.Y - SupportV.Y) + DirectionV.Y * (SupportV.X - sv.X)) / (dv.X * DirectionV.Y - dv.Y * DirectionV.X);
            float valueStage = (dv.X * ( SupportV.Y - sv.Y) + dv.Y * (sv.X - SupportV.X)) / (DirectionV.X * dv.Y - DirectionV.Y * dv.X);
            if (valueRectangle <= 1 && valueRectangle >= 0 && valueStage <= 1 && valueStage >= 0)
            {
                return true;
            }
            return false;
        }

        public float GetYValue(float xValue)
        {
            return SupportV.Y + ((xValue - SupportV.X) / DirectionV.X) * DirectionV.Y;
        }

        public float GetAngle()
        {      
            return (float)((180 / Math.PI) * Math.Acos(DirectionV.X / (Math.Sqrt(DirectionV.X * DirectionV.X + DirectionV.Y * DirectionV.Y))));
        }
    }
}
