﻿using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Alex.SpiderFighter.Models.Decorations;
using YANMFA.Games.Alex.SpiderFighter.Models.Items;
using YANMFA.Games.Alex.SpiderFighter.Tools;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Mobs
{
    public class Human : Mob
    {
        private const int maxJumpHeight = 350;
        private const int maxWalkWidth = 70;

        private int _DeltaWalk;
        private int _DeltaJumpHeight;
        private RectangleF _hitbox;
        private WalkDirection _currentWalkDir;
        private WatchDirection _watchDir;

        public override RectangleF Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public Weapon SelectedWeapon { get; set; }

        public Human()
        {
            _hitbox = new RectangleF(new PointF(20F, 30F), new SizeF(50F, 150F));
            Healthpoints = 100;
        }
        
        public override void Render(Graphics g)
        {
            g.DrawImage(CurrentTexture,Hitbox);
        }

        public override void Update()
        {
            if (Healthpoints <= 0)
            {
                MessageBox.Show("Game Over!: You died");
            }
            else
            {
                // Gravity
                if (_hitbox.Y < StaticDisplay.DisplayHeight - Hitbox.Height)
                {
                    _hitbox.Y += 12 * (float)StaticDisplay.FixedDelta;
                }
                
                // Animation on Jumping
                if (_currentWalkDir == WalkDirection.Jump)
                {
                    if (_DeltaJumpHeight < maxJumpHeight)
                    {
                        _DeltaJumpHeight += 27 * (int)StaticDisplay.FixedDelta;
                        _hitbox.Y -= 27 * (float)StaticDisplay.FixedDelta;
                    }
                    else if (_hitbox.Bottom >= StaticDisplay.DisplayHeight - 70 || IsCollision())
                    {
                        _DeltaJumpHeight = 0;
                        _currentWalkDir = WalkDirection.None;
                        return;
                    }

                }
                
                // Animation when running to the right
                if (_currentWalkDir == WalkDirection.Right)
                {
                    
                    if (_DeltaWalk < maxWalkWidth)
                    {
                        Textures.MobBitmaps.TryGetValue("HumanRightWalk", out Bitmap bitmap);
                        {
                            CurrentTexture = bitmap;
                        }
                        _DeltaWalk += 7 * (int)StaticDisplay.FixedDelta;
                        _hitbox.X += 7 * (int)StaticDisplay.FixedDelta;
                    }
                    else
                    {
                        Textures.MobBitmaps.TryGetValue("HumanRight", out Bitmap bitmap);
                        {
                            CurrentTexture = bitmap;
                        }
                        _DeltaWalk = 0;
                        _currentWalkDir = WalkDirection.None;
                    }
                }

                // Animation when running to the left
                if (_currentWalkDir == WalkDirection.Left)
                {

                    if (_DeltaWalk < maxWalkWidth)
                    {
                        Textures.MobBitmaps.TryGetValue("HumanLeftWalk", out Bitmap bitmap);
                        {
                            CurrentTexture = bitmap;
                        }
                        _DeltaWalk += 7 * (int)StaticDisplay.FixedDelta;
                        _hitbox.X -= 7 * (int)StaticDisplay.FixedDelta;
                    }
                    else
                    {
                        Textures.MobBitmaps.TryGetValue("HumanLeft", out Bitmap bitmap);
                        {
                            CurrentTexture = bitmap;
                        }
                        _DeltaWalk = 0;
                        _currentWalkDir = WalkDirection.None;
                    }
                }
            }
        }

        public override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Shoot(new Vector(_hitbox.Right,_hitbox.Y)  | Vector.VectorConverter(new PointF((_hitbox.X - StaticDisplay.DisplayWidth/2)+ e.Location.X, (_hitbox.Y - StaticDisplay.DisplayHeight / 2) + e.Location.Y)),new Vector(e.X,e.Y));
            }
        }

        public override void Resize(int width, int height)
        {
        }

        public override void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                _currentWalkDir = WalkDirection.Left;
                _watchDir = WatchDirection.Left;
                _DeltaJumpHeight = 0;
            }

            if (e.KeyCode == Keys.D)
            {
                _currentWalkDir = WalkDirection.Right;
                _watchDir = WatchDirection.Right;
                _DeltaJumpHeight = 0;
            }

            if (e.KeyCode == Keys.Space && _currentWalkDir == WalkDirection.None)
            {
                _currentWalkDir = WalkDirection.Jump;
            }
        }

        void Shoot(Vector direction, Vector startVec)
        {
            var bullet = new Bullet(direction,startVec,13);
            bullet.StartMoveBullet();
        }

        bool IsCollision()
        {
            foreach (BarrierBlock VARIABLE in Round.CurrentLevel.Items)
            {
                if (VARIABLE.Hitbox.IntersectsWith(_hitbox))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public enum WalkDirection
    {
        None,
        Right,
        Left,
        Jump,
    }

    public enum WatchDirection
    {
        Right,
        Left,
    }
}