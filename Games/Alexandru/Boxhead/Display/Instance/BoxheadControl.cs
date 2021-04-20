using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using YANMFA.Core;

namespace YANMFA.Games.Alexandru.Boxhead.Game
{
    class BoxheadControl : IGameInstance
    {
        public GameMode GameType => GameMode.SINGLEPLAYER;
        public Characters.Player newplayer = new Characters.Player(); // declaring a new instance for the player

        Keys[] keys = { Keys.Left, Keys.Right, Keys.Up, Keys.Down }; // used for the move method

        public string GameName => "Boxhead"; 

        public string GameDescription => "Singleplayer shooter game inspired by the Flash version Boxhead";

        public Image GameLogo => Image.FromFile("./Assets/Alexandru/Boxhead/boxhead_logo.jpg");

        public Image GetTitleImage() => GameLogo;

        public BoxheadControl()
        {
            newplayer.movable = false;
            newplayer.tickCounter = 0;
            newplayer.velocity = 10;
            newplayer.bulletVelocity = 10;
            
        }
        public bool IsStopRequested()
        {
            return false;
        }

        public void Render(Graphics g)
        {
            // if the player hits a key, the character will move in a desired direction; otherwise, he'll stay
            if (newplayer.movable == true)
            {
                newplayer.Move(g);
            }
            else
            {
                newplayer.Idle(g);
            }
            if (newplayer.shoot == true)
                newplayer.Shoot(g);
        }

        public void RenderSplash(Graphics g)
        {
            newplayer.DrawPlayer(g);
        }

        public void Start(GameMode gameType)
        {
            
        }
        
        public void Stop()
        {
        }

        public void Update()
        {
            
            foreach (var item in keys)
            {
                if (StaticKeyboard.IsKeyDown(item))
                    newplayer.movable = true;
            }
            if (StaticKeyboard.IsKeyDown(Keys.Space))
            {
                newplayer.shoot = true;
                newplayer.bullet.X += newplayer.bulletVelocity;
            }
            if (StaticKeyboard.IsKeyDown(Keys.Left))
            {
                newplayer.player.X -= newplayer.velocity;
            }
            else if (StaticKeyboard.IsKeyDown(Keys.Right))
            {
                newplayer.player.X += newplayer.velocity;
            }
            else if (StaticKeyboard.IsKeyDown(Keys.Up))
            {
                newplayer.player.Y -= newplayer.velocity;
            }
            else if (StaticKeyboard.IsKeyDown(Keys.Down))
            {
                newplayer.player.Y += newplayer.velocity;
            }
            else
            {
                newplayer.movable = false;
            }
            newplayer.tickCounter++;
            
        }

        public void UpdateSplash()
        {
            
        }
    }
}
