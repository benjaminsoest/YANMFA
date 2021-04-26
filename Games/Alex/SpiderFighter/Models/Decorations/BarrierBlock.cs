using System.Drawing;
using System.Windows.Forms;
using YANMFA.Games.Alex.SpiderFighter.Models.Mobs;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class BarrierBlock : Item
    {
        private RectangleF _hitbox;

        public BarrierBlock(RectangleF rec, Bitmap texture)
        {
            CurrentTexture = texture;
            Hitbox = rec;
        }

        public override RectangleF Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public override void Render(Graphics g)
        {
            g.DrawImage(CurrentTexture,_hitbox);
        }

        public override void Update()
        {
            MobResistance();
        }

        public override void Resize(int width, int height)
        {

        }             
    }
}