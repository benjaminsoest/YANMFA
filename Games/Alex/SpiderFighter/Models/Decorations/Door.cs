using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class Door : Item
    {
        public bool OpenState { get; private set; }
        
        public override void Render(Graphics graphics)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void MouseDown(MouseEventArgs mouseEventArgs)
        {
        }

        public override void Resize(int width, int height)
        {

        }

        public override void KeyDown(KeyEventArgs keyEventArgs)
        {
        }

        public void OpenDoor()
        {
            
        }

        public void CloseDoor()
        {
            
        }
    }
}