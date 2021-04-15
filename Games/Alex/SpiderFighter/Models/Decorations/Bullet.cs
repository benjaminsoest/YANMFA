using System;
using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Alex.SpiderFighter.Tools;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class Bullet : Item
    {
        private RectangleF _hitbox;
        private bool _isMoving = false;
        
        public int FireSpeed { get; set; }
        public Vector ConnectVec { get; set; }
        public Vector StartVec { get; set; }

        public Bullet(Vector connectVec, Vector startVec, int firestpeed)
        {
            ConnectVec = connectVec;
            StartVec = startVec;
            FireSpeed = firestpeed;
        }
        
        public override RectangleF Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(Brushes.Goldenrod, _hitbox);
        }

        public void StartMoveBullet()
        {
            ConnectVec = Vector.ConvertUnitVector(ConnectVec, FireSpeed);
            _isMoving = true;
        }
        
        public override void Update()
        {
            ProofLocationValidity();
            if ((_hitbox.X < StaticDisplay.DisplayWidth + StartVec.X) && (_hitbox.X > 0) && (_hitbox.Y > 0) && (_hitbox.Y < StaticDisplay.DisplayHeight))
            {
                _hitbox.X += _hitbox.X * (float)StaticDisplay.FixedDelta;
                _hitbox.Y += _hitbox.Y * (float)StaticDisplay.FixedDelta;
            }
        }
        
        public override void Resize(int width, int height)
        {
        }

        void ProofLocationValidity()
        {
            foreach (BarrierBlock VARIABLE in Round.CurrentLevel.Items)
            {
                if (VARIABLE.Hitbox.IntersectsWith(_hitbox))
                {
                    NeutralizeBullet();
                    return;
                }
            }
        }

        void NeutralizeBullet()
        {
            _hitbox.Width = 0;
            _hitbox.Height = 0;
            DeleteItem();
        }
    }
}