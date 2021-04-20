using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YANMFA.Games.Edgar.FlipFlopFPS
{
	static class Events
	{
		public static void KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
				case Keys.S:
					Auto.player1MoveVect.Y = 0; break;
				case Keys.A:
				case Keys.D:
					Auto.player1MoveVect.X = 0; break;
			}
		}

		public static void KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					Auto.player1MoveVect.Y = Config.playerMoveStep; break;
				case Keys.A:
					Auto.player1MoveVect.X = -Config.playerMoveStep; break;
				case Keys.S:
					Auto.player1MoveVect.Y = -Config.playerMoveStep; break;
				case Keys.D:
					Auto.player1MoveVect.X = Config.playerMoveStep; break;
			}
		}

		public static void MouseClick(object sender, MouseEventArgs e)
		{
			Point digitalP = Tools.ToDigitalPoint(e.X, e.Y);

			for (int i = 0; i < Auto.objects.Length; i++)
				if (Auto.objects[i].IsContained(digitalP))
				{
					Auto.objects[i].TakeDamage(10);
				}



			//objects[0].PosX = digitalP.X;
			//objects[0].PosY = digitalP.Y;
		}

		public static void MouseWheel(object sender, MouseEventArgs e)
		{
			Auto.scaleX *= e.Delta > 0 ? 1f + Config.scaleAccelerator : 1f - Config.scaleAccelerator;
			Auto.scaleY *= e.Delta > 0 ? 1f + Config.scaleAccelerator : 1f - Config.scaleAccelerator;
			if (e.Delta > 0)
			{
				Auto.cameraPosX += (int)((Auto.player1.X - Auto.cameraPosX) * Config.scaleAccelerator);
				Auto.cameraPosY += (int)((Auto.player1.Y - Auto.cameraPosY) * Config.scaleAccelerator);
			}
			//Console.WriteLine(Auto.scaleX.ToString());
		}

	}
}
