
namespace GraficiOdabranihFunkcija
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.functionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cosineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tangentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotangentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exponentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // functionToolStripMenuItem
            // 
            this.functionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sineToolStripMenuItem,
            this.cosineToolStripMenuItem,
            this.tangentToolStripMenuItem,
            this.cotangentToolStripMenuItem,
            this.squareToolStripMenuItem,
            this.squareRootToolStripMenuItem,
            this.exponentialToolStripMenuItem,
            this.logToolStripMenuItem});
            this.functionToolStripMenuItem.Name = "functionToolStripMenuItem";
            this.functionToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.functionToolStripMenuItem.Text = "Функција";
            // 
            // sineToolStripMenuItem
            // 
            this.sineToolStripMenuItem.Name = "sineToolStripMenuItem";
            this.sineToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.sineToolStripMenuItem.Text = "Синус";
            this.sineToolStripMenuItem.Click += new System.EventHandler(this.sineToolStripMenuItem_Click);
            // 
            // cosineToolStripMenuItem
            // 
            this.cosineToolStripMenuItem.Name = "cosineToolStripMenuItem";
            this.cosineToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.cosineToolStripMenuItem.Text = "Косинус";
            this.cosineToolStripMenuItem.Click += new System.EventHandler(this.cosineToolStripMenuItem_Click);
            // 
            // tangentToolStripMenuItem
            // 
            this.tangentToolStripMenuItem.Name = "tangentToolStripMenuItem";
            this.tangentToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tangentToolStripMenuItem.Text = "Тангенс";
            this.tangentToolStripMenuItem.Click += new System.EventHandler(this.tangentToolStripMenuItem_Click);
            // 
            // cotangentToolStripMenuItem
            // 
            this.cotangentToolStripMenuItem.Name = "cotangentToolStripMenuItem";
            this.cotangentToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.cotangentToolStripMenuItem.Text = "Котангенс";
            this.cotangentToolStripMenuItem.Click += new System.EventHandler(this.cotangentToolStripMenuItem_Click);
            // 
            // squareToolStripMenuItem
            // 
            this.squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            this.squareToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.squareToolStripMenuItem.Text = "Квадрат";
            this.squareToolStripMenuItem.Click += new System.EventHandler(this.squareToolStripMenuItem_Click);
            // 
            // squareRootToolStripMenuItem
            // 
            this.squareRootToolStripMenuItem.Name = "squareRootToolStripMenuItem";
            this.squareRootToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.squareRootToolStripMenuItem.Text = "Квадратни корен";
            this.squareRootToolStripMenuItem.Click += new System.EventHandler(this.squareRootToolStripMenuItem_Click);
            // 
            // exponentialToolStripMenuItem
            // 
            this.exponentialToolStripMenuItem.Name = "exponentialToolStripMenuItem";
            this.exponentialToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.exponentialToolStripMenuItem.Text = "Експоненцијална";
            this.exponentialToolStripMenuItem.Click += new System.EventHandler(this.exponentialToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.logToolStripMenuItem.Text = "Природни логаритам";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem functionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cosineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tangentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotangentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exponentialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
    }
}

