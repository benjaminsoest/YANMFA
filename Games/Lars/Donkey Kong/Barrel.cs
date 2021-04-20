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
        #region variables
        private RectangleF barrelObject, barrelDraw;
        private int counter, line;
        private float position, gravitation;
        #endregion

        public Barrel(Point start, int width, float pos)
        {
            barrelObject = new RectangleF(start.X, start.Y, width, width);
            line = 1;
            barrelDraw = barrelObject;
            position = pos;
        }

        #region accessors
        public int Line { get => line; set => line = value; }
        public float Gravitation { get => gravitation; set => gravitation = value; }
        public int Counter { get => counter; set => counter = value; }
        public RectangleF BarrelObject { get => barrelObject; set => barrelObject = value; }
        public RectangleF BarrelDraw { get => barrelDraw; }
        public float Position { get => position; set => position = value; }
        #endregion

        public void BarrelMovement(Vector2 direction, float time)// Move the barrel along the direction of the ground it is on top. Extra vertical movement is added if the barrel is in the air
        {
            counter++;
            barrelDraw = new RectangleF((float)(barrelObject.X + (counter / time) * direction.X), (float)(barrelObject.Y + (counter / time) * direction.Y + 3 * gravitation), barrelObject.Width, barrelObject.Height);
        }
    }
}
