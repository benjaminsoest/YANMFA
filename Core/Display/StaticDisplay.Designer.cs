
namespace YANMFA.Core
{
    partial class StaticDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StaticDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "StaticDisplay";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YANMFA!";
            this.Load += new System.EventHandler(this.StaticDisplay_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StaticDisplay_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StaticDisplay_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StaticDisplay_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StaticDisplay_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StaticDisplay_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StaticDisplay_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.StaticDisplay_MouseWheel);
            this.Resize += new System.EventHandler(this.StaticDisplay_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}