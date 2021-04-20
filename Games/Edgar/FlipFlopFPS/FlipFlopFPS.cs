
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Edgar.FlipFlopFPS;

namespace YANMFA.Games.Edgar.FlipFlopFPS
{
	class FlipFlopFPS : IGameInstance
	{
		public string GameName => "FlipFlopFPS";

		public string GameDescription => "Survive and fight against enemies in an ever switching manner!";

		public static Image titleImage = Image.FromFile("./Assets/Edgar/FlipFlopFPS/test image.png");
		public Image GetTitleImage()
		{
			return titleImage;
		}

		public bool IsStopRequested()
		{
			return false;
		}

		public void Render(Graphics g)
		{

			// vars
			Auto.windX = StaticDisplay.DisplayWidth; Auto.windY = StaticDisplay.DisplayHeight;
			int objSize = Config.objSize;
			ObjBox[] objects = Auto.objects;

			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;


			SolidBrush sb = new SolidBrush(Color.Empty);
			// draw all objects
			for (int i = 0; i < objects.Length; i++)
			{
				g.FillRectangle(Brushes.Black,
					Tools.ToScreenRect(objects[i].PosX, objects[i].PosY, new Size(objects[i].SizeX, objects[i].SizeY))
					);
				// if recently took damage --> display health
				if (objects[i].SincetHit.TotalMilliseconds < Config.objHealthFadeTime + Config.objHealthFadeStart
					&& Tools.GetVectLength(objects[i].PosX - Auto.player1.X, objects[i].PosY - Auto.player1.Y) < Config.objHealthDisplayDistance)
				{
					if (objects[i].SincetHit.TotalMilliseconds > Config.objHealthFadeStart)
						sb.Color = Color.FromArgb(
							(int)(255 * (1 -
								((double)objects[i].SincetHit.TotalMilliseconds - Config.objHealthFadeStart)
								/ Config.objHealthFadeTime)),
							Config.objHealthColor_R, Config.objHealthColor_G, Config.objHealthColor_B);
					else sb.Color = Color.FromArgb(255, Config.objHealthColor_R, Config.objHealthColor_G, Config.objHealthColor_B);

					g.FillRectangle(sb,
						Tools.ToScreenRect(
							objects[i].PosX - objects[i].SizeX / 2, objects[i].PosY,
							new Size((int)(objects[i].SizeX * objects[i].GetHealthPercent()), Config.objHealthWidth),
							false, true)
						);
				}
			}
			Rectangle rect = Tools.ToScreenRect(Auto.player1.X, Auto.player1.Y, new Size(Config.playerSize, Config.playerSize));
			g.FillRectangle(Brushes.Black, rect);

			// shadows
			Point[] corners = new Point[4];
			Pen pen = new Pen(Color.FromArgb(180, 0, 0, 0));
			for (int i = 0; i < objects.Length; i++)
			{
				double len = Tools.GetVectLength(objects[i].PosX - Auto.player1.X, objects[i].PosY - Auto.player1.Y);
				if (len >= Config.LightRadius) continue;
				if (len <= Config.playerSize / 2) continue;
				corners[0] = new Point(objects[i].PosX - objects[i].SizeX / 2, objects[i].PosY - objects[i].SizeY / 2);
				corners[1] = new Point(objects[i].PosX - objects[i].SizeX / 2, objects[i].PosY + objects[i].SizeY / 2);
				corners[2] = new Point(objects[i].PosX + objects[i].SizeX / 2, objects[i].PosY + objects[i].SizeY / 2);
				corners[3] = new Point(objects[i].PosX + objects[i].SizeX / 2, objects[i].PosY - objects[i].SizeY / 2);

				/*
				 * old
				for (int i2 = 0; i2 < 4; i2++)
				{
					Point direction = new Point(corners[i2].X - Auto.player1.X, corners[i2].Y - Auto.player1.Y);
					Point endPoint = new Point(
						//(int)(direction.X * 0.8 + corners[i2].X),
						//(int)(direction.Y * 0.8 + corners[i2].Y)
						(int)(Math.Abs(direction.X) * direction.X * 0.02 + corners[i2].X),
						(int)(Math.Abs(direction.Y) * direction.Y * 0.02 + corners[i2].Y)
						);
					//G.DrawLine(pen, ToScreenPoint(corners[i2].X, corners[i2].Y), ToScreenPoint(endPoint.X, endPoint.Y));
					corners[i2] = ToScreenPoint(endPoint.X, endPoint.Y);
				}
				if (len == 0) len = 0.00000001;
				sb = new SolidBrush(Color.FromArgb((int)(255 * (1-len/Math.Pow(len,1.2))), 0, 0, 0));
				G.FillPolygon(sb, corners);
				*/

				int total = 10;
				int baseShadowStep = 1;

				// NOTE: auf normalform bringen -->--<--

				Point direction = new Point(objects[i].PosX - Auto.player1.X, objects[i].PosY - Auto.player1.Y);
				sb = new SolidBrush(Color.Empty);
				for (int i2 = 1; i2 < total; i2++)
				{
					double factor = i2 * 0.2;
					double transValue = i2 * len / 100.0;
					sb.Color = Color.FromArgb((int)transValue > 255 ? 255 : (int)transValue < 0 ? 0 : (int)transValue, 0, 0, 0);
					// Color.FromArgb((int)(255 * (1 - len / Math.Pow(len, 1.2))));
					g.FillRectangle(sb,
						Tools.ToScreenRect((int)(objects[i].PosX + direction.X * factor), (int)(objects[i].PosY + direction.Y * factor),
						new Size((int)(objects[i].SizeX + objects[i].SizeX * (factor - 0.1)), (int)(objects[i].SizeY + objects[i].SizeY * (factor - 0.1))))
					);

				}






			}



			// fog
			int circleBaseWidth = 40; // look into the code; its not intuitive
			int layers = 10;
			int startradius = Config.LightRadius;
			int currentRadius = startradius;
			pen = new Pen(Color.Empty);
			for (int i = 1; i < layers + 1; i++)
			{
				pen.Color = Color.FromArgb((int)(255 * (float)i / layers), 0, 0, 0);
				int penWidth = circleBaseWidth / layers * i + (i == layers ? 10000 : 0);
				pen.Width = Tools.ToScreenSizeF(penWidth).Width;
				currentRadius += penWidth;
				g.DrawEllipse(pen,
					Tools.ToScreenRectF(Auto.player1.X, Auto.player1.Y,
						new Size(currentRadius, currentRadius)
						)
					);
				currentRadius += (int)penWidth - (i / 3); // the subtraction part is because of the way circles are rendered on pixel-based screens
			}


			// crossline
			//G.DrawLine(Pens.Black, Auto.windX / 2, 0, Auto.windX / 2, Auto.windY);
			//G.DrawLine(Pens.Black, 0, Auto.windY / 2, Auto.windX, Auto.windY / 2);

			// meta display
			string displayText =
				"scale(" + Auto.scaleX.ToString() + "|" + Auto.scaleY.ToString() + ")"
				+ "fps(" + (Auto.fpsReal.ToString()) + ")"
				+ "updatesign:(" + DateTime.Now.Millisecond.ToString() + ")";
			
			
			
			
			
			SizeF displayTextSize = g.MeasureString(displayText, SystemFonts.DefaultFont);
			g.FillRectangle(Brushes.LightGray, 0, 0, (int)displayTextSize.Width + 1, (int)displayTextSize.Height + 1);
			g.DrawString(displayText, SystemFonts.DefaultFont, Brushes.Black, new Point(0, 0));




			// fps counting related
			Auto.fpsFrames += 1;
			if (DateTime.Now.Second != Auto.fpsSecond)
			{
				Auto.fpsReal = Auto.fpsFrames;
				Auto.fpsFrames = 0;
				Auto.fpsSecond = DateTime.Now.Second;
			}
		}

		public void RenderSplash(Graphics g)
		{
			
		}

		public void Start()
		{
			Auto._globalStepFactor = (double)Config.gameTickBase / Config.gameTicksPS;

			Auto.objects = new ObjBox[Config.maxObjs];

			Auto.player1 = new Point(0, 0);
			for (int i = 0; i < Auto.objects.Length; i++)
			{
				Auto.objects[i] = new ObjBox(Config.rand.Next(-500, 500), Config.rand.Next(-500, 500));
			}


			StaticKeyboard.AddKeyUpListener(Events.KeyUp);
			StaticKeyboard.AddKeyDownListener(Events.KeyDown);
			StaticMouse.AddMouseDownListener(Events.MouseClick);
			StaticMouse.AddMouseWheelListener(Events.MouseWheel);
		}

		public void Stop()
		{
			StaticKeyboard.RemoveKeyUpListener(Events.KeyUp);
			StaticKeyboard.RemoveKeyDownListener(Events.KeyDown);
			StaticMouse.RemoveMouseDownListener(Events.MouseClick);
			StaticMouse.RemoveMouseWheelListener(Events.MouseWheel);
		}

		public void Update()
		{
			Auto.player1.X += (int)(Auto.player1MoveVect.X * Auto._globalStepFactor);
			Auto.player1.Y += (int)(Auto.player1MoveVect.Y * Auto._globalStepFactor);


			Rectangle windowFreeWalkArea = new Rectangle(
				(int)(Auto.windX * Config.windowFreeWalkAreaPerc / 100),
				(int)(Auto.windY * Config.windowFreeWalkAreaPerc / 100),
				(int)(Auto.windX - Auto.windX * Config.windowFreeWalkAreaPerc / 100 * 2),
				(int)(Auto.windY - Auto.windY * Config.windowFreeWalkAreaPerc / 100 * 2)
				);

			Point center = new Point(windowFreeWalkArea.X + windowFreeWalkArea.Width / 2, windowFreeWalkArea.Y + windowFreeWalkArea.Height / 2);
			Rectangle rect = Tools.ToScreenRect(Auto.player1.X, Auto.player1.Y, new Size());
			if (!windowFreeWalkArea.Contains(new Point(rect.Location.X, center.Y)))
				Auto.cameraPosX += (int)(Auto.player1MoveVect.X * Auto._globalStepFactor);
			if (!windowFreeWalkArea.Contains(new Point(center.X, rect.Location.Y)))
				Auto.cameraPosY += (int)(Auto.player1MoveVect.Y * Auto._globalStepFactor);
		}

		public void UpdateSplash()
		{
			
		}

		
	}
	
}
