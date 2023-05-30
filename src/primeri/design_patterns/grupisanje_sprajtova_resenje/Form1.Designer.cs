namespace grupisanje
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
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            groupToolStripMenuItem = new ToolStripMenuItem();
            ungroupToolStripMenuItem = new ToolStripMenuItem();
            toFrontToolStripMenuItem = new ToolStripMenuItem();
            toBackToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 450);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { groupToolStripMenuItem, ungroupToolStripMenuItem, toFrontToolStripMenuItem, toBackToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(122, 92);
            // 
            // groupToolStripMenuItem
            // 
            groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            groupToolStripMenuItem.Size = new Size(121, 22);
            groupToolStripMenuItem.Text = "Group";
            groupToolStripMenuItem.Click += groupToolStripMenuItem_Click;
            // 
            // ungroupToolStripMenuItem
            // 
            ungroupToolStripMenuItem.Name = "ungroupToolStripMenuItem";
            ungroupToolStripMenuItem.Size = new Size(121, 22);
            ungroupToolStripMenuItem.Text = "Ungroup";
            ungroupToolStripMenuItem.Click += ungroupToolStripMenuItem_Click;
            // 
            // toFrontToolStripMenuItem
            // 
            toFrontToolStripMenuItem.Name = "toFrontToolStripMenuItem";
            toFrontToolStripMenuItem.Size = new Size(121, 22);
            toFrontToolStripMenuItem.Text = "To Front";
            // 
            // toBackToolStripMenuItem
            // 
            toBackToolStripMenuItem.Name = "toBackToolStripMenuItem";
            toBackToolStripMenuItem.Size = new Size(121, 22);
            toBackToolStripMenuItem.Text = "To Back";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Груписање спрајтова";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem groupToolStripMenuItem;
        private ToolStripMenuItem ungroupToolStripMenuItem;
        private ToolStripMenuItem toFrontToolStripMenuItem;
        private ToolStripMenuItem toBackToolStripMenuItem;
    }
}