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
        #region variables
        private Point pLeft, pRight;
        private Vector2 directionVector;
        private float gradient, yAchsis, collisionValue, yValue, length;
        #endregion

        public Stage(Point p1, Point p2)
        {
            pLeft = p1;
            pRight = p2;
            gradient = (float)(pRight.Y - pLeft.Y) / (float)(pRight.X - pLeft.X);
            yAchsis = (float)(pLeft.Y - (gradient * pLeft.X));
            directionVector = new Vector2();
            CalculateDirectionVector();
            length = (float)Math.Sqrt(directionVector.X * directionVector.X + directionVector.Y * directionVector.Y);
        }

        #region accessors
        public Point PLeft { get => pLeft; }
        public Point PRight { get => pRight; }
        public Vector2 DirectionVector { get => directionVector; }
        public float CollisionValue { get => collisionValue; }
        public float YValue { get => yValue; }
        public float Length { get => length; }
        #endregion

        public void CalculateDirectionVector()//Calculationg the Vector between the two points that creates the line the barrels move on
        {
            //Left to right
            directionVector.X = PRight.X - PLeft.X;
            directionVector.Y = PRight.Y - PLeft.Y;
            if (pLeft.Y > pRight.Y)//Right to left
            {
                directionVector.X *= -1;
                directionVector.Y *= -1;
            }
        }

        public bool CollisionDetection(float barrelBottom, float barrelCenter, float time)
        {
            yValue = gradient * barrelCenter + yAchsis;//The yValue of the line at the X-location of the center of the barrel so at the spot it touches the line
            if (yValue <= barrelBottom)
            {
                if (pLeft.Y < pRight.Y) //right direction
                {
                    collisionValue = (float)(time * ((barrelCenter - PLeft.X) / directionVector.X)); //How far the barrel progressed so far without the counter increasing
                }
                else //left direction
                {
                    collisionValue = (float)(time * ((barrelCenter - PRight.X) / directionVector.X));
                }
                return true;
            }
            return false;
        }
    }
}
