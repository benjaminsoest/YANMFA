using System.Drawing;
using YANMFA.Core;
using YANMFA.Games.Alex.SpiderFighter.Models.Decorations;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Mobs
{
    public class Spider : Mob
    {
        public Spider(RectangleF hitbox)
        {
            _hitbox = hitbox;
            AddMob();
        }

        private RectangleF _hitbox;
        private const int _atractSpeed = 2;
        private int _deltaHeight;
        private const int maxJumpHeight = 350;
        private WatchDirection _watchDirection;
        private WalkDirection _walkDirection;
        private const int _maxDistance = 300;
        private const int _gravity = 3;
        private const int _knockbackWidth = 300;
        private int _deltaKnockback;

        public override RectangleF Hitbox { get => _hitbox; set => _hitbox = value; }

        public override void Update()
        {
            _hitbox.Y += _gravity * (float)StaticDisplay.FixedDelta;

            if (_deltaKnockback > 0 && _walkDirection == WalkDirection.Right)
            {
                _deltaKnockback += 30;
                _hitbox.X += 30;
            }
            else if (_deltaKnockback > 0 && _walkDirection == WalkDirection.Left)
            {
                _deltaKnockback += 30;
                _hitbox.X -= 30;

            }
            if (_deltaKnockback >= _knockbackWidth)
            {
                _deltaKnockback = 0;
                _walkDirection = WalkDirection.None;
            }

            if (_deltaHeight < maxJumpHeight)
            {
                _deltaHeight += 25;
                _hitbox.Y -= 25;
            }
            else if(_hitbox.Bottom >= StaticDisplay.DisplayHeight + Round.PlayerOne.Hitbox.Y/2)
            {
                _deltaHeight = 0;                   
            }

            if (Hitbox.X < Round.PlayerOne.Hitbox.X && (Round.PlayerOne.Hitbox.X - Hitbox.X) < _maxDistance)
            {
                _hitbox.X += _atractSpeed;
            }
            else if (Hitbox.X > Round.PlayerOne.Hitbox.X && (Hitbox.X - Round.PlayerOne.Hitbox.X) < _maxDistance)
            {
                _hitbox.X -= _atractSpeed;
            }
            else if (Hitbox.IntersectsWith(Round.PlayerOne.Hitbox))
            {
                if (_hitbox.X > Hitbox.X)
                {
                    _deltaKnockback += 30;
                    _hitbox.X += 30;
                    _walkDirection = WalkDirection.Right;
                }
                else
                {
                    _deltaKnockback += 30;
                    _hitbox.X -= 30;
                    _walkDirection = WalkDirection.Left;
                }
            }
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Red, Hitbox);
        }

        bool IsCollision()
        {
            foreach (Item VARIABLE in Round.CurrentLevel.Items)
            {
                if ((VARIABLE is BarrierBlock || VARIABLE is Door) && VARIABLE.Hitbox.IntersectsWith(_hitbox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}