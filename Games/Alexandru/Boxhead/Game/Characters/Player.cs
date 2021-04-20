using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Alexandru.Boxhead.Game.Characters
{
    class Player
    {
        public bool movable = false; // decides whether the player can move or not
        public int move = 0; // used for the image array to call an exact animation at a desired tick
        public int tickCounter = 0; // used for animations
        public int velocity = 0; // depending on the desired difficulty, from 5 to 15 for zombies (lower is easier)

        // create a constructor
        public Rectangle player = new Rectangle(StaticDisplay.DisplayWidth / 10,
        StaticDisplay.DisplayHeight - StaticDisplay.DisplayHeight / 2, 45, 45); // sets the default coordinates of the player for the render 
        public Rectangle bullet;
        public int bulletVelocity;
        public bool shoot;


        Image[] movement_player = {Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_idle without gun_0.png")),
            Image.FromFile("./Assets/Alexandru/Boxhead/Cowboy2_idle without gun_1.png"),
            Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_idle without gun_2.png")),
            Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_idle without gun_3.png")),
            Image.FromFile("./Assets/Alexandru/Boxhead/Cowboy2_walk with gun_0.png"),
            Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_walk with gun_1.png")),
            Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_walk with gun_2.png")),
            Image.FromFile(("./Assets/Alexandru/Boxhead/Cowboy2_walk with gun_3.png"))
        }; // here will be saved the idle and movement animations of the characters

        public void DrawPlayer(Graphics g)
        {
            g.FillRectangle(new TextureBrush(Image.FromFile("./Assets/Alexandru/Boxhead/Cowboy2_idle with gun_0.png")), player); // default character sprite
        }



        public void Idle(Graphics g)
        {
            if (tickCounter % 2 == 0) // to avoid flickering, the animation will change every second tick, preserving the current animation for two ticks in a row
            {
                move++;
            }
            if (move > 3) 
            {
                move = 0;
            }
            g.DrawImage(movement_player[move], player); // depending on the value of move (max 3, 4th pos. of the image array),
                                                        // every two ticks a part of the idle animation will be drawn

        }
        public void Move(Graphics g)
        {
            if (tickCounter % 2 == 0) // same as by idle
            {
                move++;
            }
            if (move < 3 || move > 7)
            {
                move = 4;
            }
           
            g.DrawImage(movement_player[move], player); // same as by idle, but with values of variable move going from 4 to 7 
        }

        public void Shoot(Graphics g)
        {
            SolidBrush bulletColor = new SolidBrush(Color.Lavender);
            bullet = new Rectangle(player.X + player.Width, player.Y + player.Height / 2, 10, 10);
            g.FillRectangle(bulletColor, bullet);
            bullet.Offset(StaticDisplay.DisplayWidth - player.X, 50);
            // de terminat metoda de tras
            
        }

        }

    }
    

