using System.Drawing;
using System.Linq;
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
        private AttackDirection _attackDirection;

        private int _deltaKnockback;
        private const int _knockbackWidth = 300;



        public override RectangleF Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public int Gravity { get; set; }

        public Weapon SelectedWeapon { get; set; }

        public Human()
        {
            _hitbox = new RectangleF(new PointF(20F, 30F), new SizeF(50F, 150F));
            Textures.MobBitmaps.TryGetValue("HumanRight", out Bitmap img);
            CurrentTexture = img;
            Healthpoints = 100;
            Gravity = 12;
        }
        
        public override void Render(Graphics g)
        {           
            g.DrawImage(CurrentTexture,Hitbox);                
        }

        public override void Update()
        {
            if (Hitbox.X <= -1 && Round.CurrentLevel.Index.Item1 != 0)
            {
                var levels = Round.Levels.Where(l => l.Index.Item1 == Round.CurrentLevel.Index.Item1 - 1 && l.Index.Item2 == Round.CurrentLevel.Index.Item2).ToList();
                Round.CurrentLevel = levels[0];
                _hitbox.X = 1900;
                _hitbox.Y = 100;
            }
            if (Hitbox.X >= 1999)
            {
                var levels = Round.Levels.Where(l => l.Index.Item1 == Round.CurrentLevel.Index.Item1 + 1 && l.Index.Item2 == Round.CurrentLevel.Index.Item2).ToList();
                Round.CurrentLevel = levels[0];
                _hitbox.X = 10;
                _hitbox.Y = 100;
            }

            if (_attackDirection != AttackDirection.None)
            {
                if (_deltaKnockback >= _knockbackWidth)
                {
                    _deltaKnockback = 0;
                    _attackDirection = AttackDirection.None;
                }
                else if (_attackDirection == AttackDirection.Right)
                {
                    _deltaKnockback += 7;
                    _hitbox.X -= 7;
                }
                else if (_attackDirection == AttackDirection.Left)
                {
                    _deltaKnockback += 7;
                    _hitbox.X += 7;
                }
            }

            if (Healthpoints <= 0)
            {
                MessageBox.Show("Game Over!: You died");
            }
            else
            {   
                if (StaticKeyboard.IsKeyDown(Keys.W) && _currentWalkDir == WalkDirection.None)
                {
                    _currentWalkDir = WalkDirection.Jump;
                }            
                if (StaticKeyboard.IsKeyDown(Keys.A))
                {
                    _currentWalkDir = WalkDirection.Left;
                    _watchDir = WatchDirection.Left;
                    _DeltaJumpHeight = 0;
                }
                if (StaticKeyboard.IsKeyDown(Keys.D))
                {
                    _currentWalkDir = WalkDirection.Right;
                    _watchDir = WatchDirection.Right;
                    _DeltaJumpHeight = 0;
                }
                               
                // Gravity
                if (_hitbox.Y < Hitbox.Y + StaticDisplay.DisplayHeight/2)
                {
                    _hitbox.Y += Gravity;
                }
                
                // Animation on Jumping
                if (_currentWalkDir == WalkDirection.Jump)
                {
                    Gravity = 12;
                    if (_DeltaJumpHeight < maxJumpHeight)
                    {
                        _DeltaJumpHeight += 25;
                        _hitbox.Y -= 25;
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
                        _DeltaWalk += 7;
                        _hitbox.X += 7;
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
                        _DeltaWalk += 7;
                        _hitbox.X -= 7;
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
                Shoot(new Vector(Hitbox.Right,Hitbox.Y)  | Vector.VectorConverter(new PointF((_hitbox.X - StaticDisplay.DisplayWidth/2)+ e.Location.X, (_hitbox.Y - StaticDisplay.DisplayHeight / 2) + e.Location.Y)),new Vector(Hitbox.X,Hitbox.Y));
            }
        }

        public override void Resize(int width, int height)
        {
            _hitbox.Width *= width / Round.StartWidth;
            _hitbox.Height *= height / Round.StartHeight;
            _hitbox.X *= width / Round.StartWidth;
            _hitbox.Y *= height / Round.StartHeight;
        }

        public override void KeyDown(KeyEventArgs e)
        {
                        
        }

        void Shoot(Vector direction, Vector startVec)
        {
            var bullet = new Bullet(direction,startVec,13);
            bullet.StartMoveBullet();
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

        public void Knockback(AttackDirection attack)
        {
            _attackDirection = attack;
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

    public enum AttackDirection
    {
        None,
        Top,
        Bottom,
        Right,
        Left,
    }
}