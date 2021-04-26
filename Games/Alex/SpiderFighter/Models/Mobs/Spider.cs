using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Alex.SpiderFighter.Models.Decorations;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Mobs
{
    public class Spider : Mob
    {
        public Spider(RectangleF hitbox)
        {
            _hitbox = hitbox;
            _healthVisual = new RectangleF(Hitbox.X, Hitbox.Y + 20, Hitbox.Width, 20);
            Healthpoints = 100;
            AddMob();
        }

        #region Fields
        private RectangleF _hitbox; // Hitbox 
        private RectangleF _healthVisual; // Health row
        private const int _atractSpeed = 2; // Atract Speed 
        private int _deltaHeight; // difference height
        private const int maxJumpHeight = 350; // maximal jump height
        private WatchDirection _watchDirection;
        private WalkDirection _walkDirection;
        private const int _maxDistance = 600;
        private const int _gravity = 3;
        private int _hitCooldown;
        private const int _maxHitCooldown = 700;
        #endregion

        public override RectangleF Hitbox { get => _hitbox; set => _hitbox = value; }

        public override void Update()
        {
            ProofInteractionWithBullets();

            _hitCooldown++;

            _hitbox.Y += _gravity;

            if (IsCollision() && _hitbox.Y >= Round.PlayerOne.Hitbox.Y)
            {
                _hitbox.Y -= 80;
            }

            // Procedure when the spider jumps
            if (_deltaHeight < maxJumpHeight)
            {
                _deltaHeight += 25;
                _hitbox.Y -= 25;
            }

            // End the Jump procedure
            else if(_hitbox.Bottom >= StaticDisplay.DisplayHeight + Round.PlayerOne.Hitbox.Y/2)
            {
                _deltaHeight = 0;                   
            }

            // The spider will be atracted in near of the Player when it is positioned on players right side
            if (Hitbox.X < Round.PlayerOne.Hitbox.X && (Round.PlayerOne.Hitbox.X - Hitbox.X) < _maxDistance)
            {
                _hitbox.X += _atractSpeed;
            }

            // The spider will be atracted in near of the Player when it is positioned on players left side
            else if (Hitbox.X > Round.PlayerOne.Hitbox.X && (Hitbox.X - Round.PlayerOne.Hitbox.X) < _maxDistance)
            {
                _hitbox.X -= _atractSpeed;
            }

            // Starts the knockback procedure when the spider intersects with the players hitbox
            if (Round.PlayerOne.Hitbox.IntersectsWith(_hitbox))
            {
                if (_hitbox.X > Round.PlayerOne.Hitbox.X)
                {
                    if (_hitCooldown >= _maxHitCooldown)
                        Round.PlayerOne.Knockback(AttackDirection.Right);
                    
                    _hitbox.X += 30;
                    _walkDirection = WalkDirection.Right;
                    
                }
                else if(_hitbox.X < Round.PlayerOne.Hitbox.X)
                {
                    if (_hitCooldown >= _maxHitCooldown)
                       Round.PlayerOne.Knockback(AttackDirection.Left);
                    
                    _hitbox.X -= 30;
                    _walkDirection = WalkDirection.Left;
                }
                if (_hitCooldown >= _maxHitCooldown)
                {
                    Round.PlayerOne.Healthpoints -= 10;
                    _hitCooldown = 0;
                }
            }
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Red, Hitbox);
            g.FillRectangle(Brushes.Green, _healthVisual);
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

        // Proofs Health and Intersection with Bulltet and the Spider
        void ProofInteractionWithBullets()
        {
            foreach (var item in Round.Bullets)
            {
                if (Hitbox.IntersectsWith(item.Hitbox))
                {
                    Healthpoints -= 5;
                    item.DeleteItem();
                }
            }

            _healthVisual = new RectangleF(Hitbox.X, Hitbox.Y - 20, _hitbox.Width / 100 * Healthpoints, 10);

            if (Healthpoints <= 0)
            {
                base.DeleteMob();
            }
        }

        
    }
}