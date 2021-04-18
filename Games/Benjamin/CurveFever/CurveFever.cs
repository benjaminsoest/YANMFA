﻿using System;
using System.Drawing;
using System.Linq;
using YANMFA.Core;
using YANMFA.Games.Benjamin.Utils;

namespace YANMFA.Games.Benjamin.CurveFever
{
	class CurveFever : Game
	{
		public static Style CurveFeverStyle = new Style(Color.LightGray, Color.Black, Color.White, Color.Black);

		public override string AssetDirectory => "";
		public override string GameName => "Curve Fever";
		public override string GameDescription => "";
		public override GameMode GameType => GameMode.MULTIPLAYER;
		public override Style Style => CurveFeverStyle;

		public override void Render(Graphics g)
		{
			base.Render(g);
		}

		public override void Start(GameMode mode)
		{
			base.Start(mode);
		}

		public override void Stop()
		{
			base.Stop();
		}

		public override void Update()
		{
			base.Update();
		}
	}
}
