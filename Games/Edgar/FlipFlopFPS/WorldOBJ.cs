using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Edgar.FlipFlopFPS
{
	static class WorldOBJ
	{
		public interface IPosition
		{

		}

		public interface IHumanoid
		{


		}
		public interface IPart
		{


		}

		public class BaseOBJ
		{
			protected int posX, posY, sizeX, sizeY, health;
		}
		public class BaseEntity
		{

		}


		public class Humanoid : BaseOBJ
		{



		}
		public class Part : BaseOBJ
		{

		}


	}

	class ObjBox : WorldOBJ.Part
	{

		public enum Material
		{
			Plastic = 140,
			Metall = 7000,
			Constant = 1000000000
		}
		int maxHealth;
		int health;
		bool cleanupFlag;
		DateTime sinceHit;
		public ObjBox(int x, int y, int sizeX = 30, int sizeY = 30, Material material = Material.Plastic)
		{
			this.posX = x; this.posY = y;
			this.sizeX = sizeX; this.sizeY = sizeY;
			health = (int)material;
			maxHealth = health;
			cleanupFlag = false;
			sinceHit = new DateTime(0);

		}
		public int PosX { get { return posX; } set { posX = value; } }
		public int PosY { get { return posY; } set { posY = value; } }
		public int SizeX { get { return sizeX; } set { sizeX = value; } }
		public int SizeY { get { return sizeY; } set { sizeY = value; } }
		public int Health { get; }
		public bool CleanupFlag { get; }
		public TimeSpan SincetHit { get { return DateTime.Now - sinceHit; } }
		public void TakeDamage(int hp)
		{
			health -= Math.Abs(hp);
			sinceHit = DateTime.Now;
			if (health <= 0)
			{
				cleanupFlag = true;
				health = 0;
			}
		}
		// note: "on the frame" is seen as "contained"
		public bool IsContained(Point p)
		{
			return p.X <= PosX + (double)sizeX / 2 && p.X >= PosX - (double)sizeX / 2
				&& p.Y <= PosY + (double)sizeY / 2 && p.Y >= PosY - (double)sizeY / 2;
		}
		public float GetHealthPercent()
		{
			return (float)health / maxHealth;
		}
	}
	public static class Tools
    {
		public static Rectangle ToScreenRect(int posX, int posY, Size size, bool sizeX_IsCentralized = true, bool sizeY_IsCentralized = true)
		{
			double digitalX = Auto.cameraPosX - posX, digitalY = Auto.cameraPosY - posY;
			double
				endSizeX = size.Width * Auto.scaleX,
				endSizeY = size.Height * Auto.scaleY;
			double
				endPosX = Auto.windX / 2 - digitalX * Auto.scaleX,
				endPosY = Auto.windY / 2 + digitalY * Auto.scaleY;
			return new Rectangle(
				(int)(endPosX - (sizeX_IsCentralized ? endSizeX / 2 : 0)),
				(int)(endPosY - (sizeY_IsCentralized ? endSizeY / 2 : 0)),
				(int)endSizeX, (int)endSizeY
				);
		}
		public static RectangleF ToScreenRectF(int posX, int posY, Size size, bool sizeX_IsCentralized = true, bool sizeY_IsCentralized = true)
		{
			double digitalX = Auto.cameraPosX - posX, digitalY = Auto.cameraPosY - posY;
			double
				endSizeX = size.Width * Auto.scaleX,
				endSizeY = size.Height * Auto.scaleY;
			double
				endPosX = Auto.windX / 2 - digitalX * Auto.scaleX,
				endPosY = Auto.windY / 2 + digitalY * Auto.scaleY;
			return new RectangleF(
				(float)(endPosX - (sizeX_IsCentralized ? endSizeX / 2 : 0)),
				(float)(endPosY - (sizeY_IsCentralized ? endSizeY / 2 : 0)),
				(float)endSizeX, (float)endSizeY
				);
		}
		public static Size ToScreenSize(double sizeX = 0, double sizeY = 0)
		{
			return new Size((int)(sizeX * Auto.scaleX), (int)(sizeY * Auto.scaleY));
		}
		public static SizeF ToScreenSizeF(double sizeX = 0, double sizeY = 0)
		{
			return new SizeF((float)sizeX * Auto.scaleX, (float)sizeY * Auto.scaleY);
		}
		// same thing as ToScreenRect, but with removed size parameter
		public static Point ToScreenPoint(int posX, int posY)
		{
			double digitalX = Auto.cameraPosX - posX, digitalY = Auto.cameraPosY - posY;
			double
				endPosX = Auto.windX / 2 - digitalX * Auto.scaleX,
				endPosY = Auto.windY / 2 + digitalY * Auto.scaleY;
			return new Point(
				(int)(endPosX),
				(int)(endPosY)
				);
		}
		public static Point ToDigitalPoint(int posX, int posY)
		{
			double
				endPosX = (-Auto.windX / 2 + posX) / Auto.scaleX + Auto.cameraPosX,
				endPosY = (-Auto.windY / 2 + posY) / (-Auto.scaleY) + Auto.cameraPosY;
			return new Point(
				(int)endPosX,
				(int)endPosY
				);
		}
		public static double GetVectLength(Point a)
		{
			return Math.Sqrt(a.X * a.X + a.Y * a.Y);
		}
		public static double GetVectLength(double x, double y, double z = 0)
		{
			return Math.Sqrt(x * x + y * y + z * z);
		}
	}
	


	
	static class Config
	{
		public static int
			maxObjs = 120,
			objSize = 30, // discontinued
			objSizeVariation = 50,
			objHealthDisplayDistance = 250,
			objHealthFadeTime = 1200, // in ms
			objHealthFadeStart = 500, // in ms
			objHealthWidth = 3,
			objHealthColor_R = 20,
			objHealthColor_G = 180,
			objHealthColor_B = 0,

			LightRadius = 350,

			playerSize = 50,
			targetFPS = 100, // standard 40
			gameTicksPS = 60, // standard 60 | influences the smoothness of steps (lower = less smooth ; higher = more smooth & maybe more CPU)
			gameTickBase = 30, // the base speed of the game
			playerMoveStep = 10,

			// TODO --> Add vertical and horizontal percent / or pxl or smth like that

			windowFreeWalkAreaPerc = 30; // size of the edge area in percent of the width & height



		public static float
			scaleAccelerator = 0.1f;

		public static Random rand = new Random(1337);

	}
	static class Auto
	{
		public static double _globalStepFactor = 1; // constant after load
		public static float scaleX = 1f, scaleY = 1f, fpsReal = 0;
		public static int fpsFrames = 0, fpsSecond = 0;
		public static int windX = 0, windY = 0;
		public static int cameraPosX = 0, cameraPosY = 0;
		public static Point player1MoveVect = new Point(0, 0);
		public static Point player1 = new Point(0, 0), player2 = new Point(0, 0);

		public static Random rand = new Random(0);
		public static ObjBox[] objects;

	}
}
