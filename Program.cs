using YANMFA.Core;

using System;
using System.Windows.Forms;

namespace YANMFA
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StaticDisplay());
        }

    }
}
