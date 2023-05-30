using System.Drawing;
using System.Drawing.Configuration;

/*
Nedostaje
    toolstrip
        dugmad za izbor oblika koji se crta, odnosno za selektovanje
        izbor boje

    kontekstni meni
        to front
        to back
*/

namespace grupisanje
{
    public partial class Form1 : Form
    {
        bool dragging;
        bool selecting;
        float mouseX0, mouseY0;
        float mouseX, mouseY;
        List<Sprite> allSprites = new List<Sprite>();
        List<Sprite> selectedSprites = new List<Sprite>();
        public Form1()
        {
            InitializeComponent();
            groupToolStripMenuItem.Enabled = false;
            ungroupToolStripMenuItem.Enabled = false;
            toFrontToolStripMenuItem.Enabled = false;
            toBackToolStripMenuItem.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // u kompletnoj aplikaciji umesto ovoga iz menija bi se birao
            // oblik koji se dodaje misem, boja, alatka za selekciju, gumica i sl.
            allSprites.Add(new CircleShape(100, 100, 30, Color.Red));
            allSprites.Add(new CircleShape(250, 100, 35, Color.Blue));
            allSprites.Add(new RectangleShape(350, 100, 50, 40, Color.Yellow));
            allSprites.Add(new LineShape(100, 300, 150, 370, Color.Green, 3));

            pictureBox1.ContextMenuStrip = contextMenuStrip1;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool mouseIsOverSelectedSprite = false;
                foreach (Sprite s in selectedSprites)
                    if (s.Contains(e.X, e.Y))
                        mouseIsOverSelectedSprite = true;

                if (mouseIsOverSelectedSprite)
                {
                    // start dragging
                    Cursor.Current = Cursors.SizeAll;
                    dragging = true;
                    mouseX = e.X;
                    mouseY = e.Y;
                }
                else
                {
                    // select clicked sprite
                    selectedSprites.Clear();
                    foreach (Sprite sprite in allSprites)
                    {
                        if (sprite.Contains(e.X, e.Y))
                        {
                            selectedSprites.Add(sprite);
                            return;
                        }
                    }
                }
                if (selectedSprites.Count == 0)
                {
                    // start selecting
                    selecting = true;
                    mouseX0 = e.X;
                    mouseY0 = e.Y;
                    mouseX = e.X;
                    mouseY = e.Y;
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                // update dragging
                float dx = e.X - mouseX;
                float dy = e.Y - mouseY;
                foreach (Sprite s in selectedSprites)
                    s.Move(dx, dy);
                mouseX = e.X;
                mouseY = e.Y;
            }
            else if (selecting)
            {
                // update selecting
                mouseX = e.X;
                mouseY = e.Y;
            }
            else
            {
                // set proper cursor shape
                bool mouseIsOverSprite = false;
                foreach (Sprite s in allSprites)
                    if (s.Contains(e.X, e.Y))
                        mouseIsOverSprite = true;

                if (mouseIsOverSprite)
                    Cursor.Current = Cursors.SizeAll;
                else
                    Cursor.Current = Cursors.Default;
            }

            pictureBox1.Invalidate();

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (selecting)
            {
                // finish selecting, put selected sprites into the list 'selectedSprites'
                RectangleF selectedRect = new RectangleF(
                    Math.Min(mouseX, mouseX0),
                    Math.Min(mouseY, mouseY0),
                    Math.Abs(mouseX - mouseX0),
                    Math.Abs(mouseY - mouseY0));

                foreach (Sprite s in allSprites)
                {
                    RectangleF spriteRect = new RectangleF(s.X, s.Y, s.Width, s.Height);
                    if (selectedRect.Contains(spriteRect))
                        selectedSprites.Add(s);
                }
            }

            //update context menu 
            toFrontToolStripMenuItem.Enabled = (selectedSprites.Count > 0);
            toBackToolStripMenuItem.Enabled = (selectedSprites.Count > 0);
            groupToolStripMenuItem.Enabled = (selectedSprites.Count > 1);
            ungroupToolStripMenuItem.Enabled = selectedSprites.Count == 1 &&
                selectedSprites[0] is SpriteGroup;

            dragging = false;
            selecting = false;
            Cursor.Current = Cursors.Default;
            pictureBox1.Invalidate();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Sprite s in allSprites)
                s.Draw(e.Graphics);

            foreach (Sprite s in selectedSprites)
                s.DrawAsSelected(e.Graphics);

            if (selecting)
            {
                float x = Math.Min(mouseX, mouseX0);
                float y = Math.Min(mouseY, mouseY0);
                float w = Math.Abs(mouseX - mouseX0);
                float h = Math.Abs(mouseY - mouseY0);
                e.Graphics.DrawRectangle(new Pen(Color.DarkBlue, 1), x, y, w, h);
            }
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteGroup sg = new SpriteGroup();
            allSprites.Add(sg);
            foreach (Sprite s in selectedSprites)
            {
                sg.AddSprite(s);
                allSprites.Remove(s);
            }
            selectedSprites.Clear();
            selectedSprites.Add(sg);
            pictureBox1.Invalidate();
        }

        private void ungroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSprites == null || selectedSprites.Count == 0)
                return;

            Sprite group = selectedSprites[0];
            allSprites.Remove(group);
            selectedSprites.Clear();
            foreach (Sprite s in group.SubSprites())
            {
                allSprites.Add(s);
                selectedSprites.Add(s);
            }
            pictureBox1.Invalidate();
        }
    }
}