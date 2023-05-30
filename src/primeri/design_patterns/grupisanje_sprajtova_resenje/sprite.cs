using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupisanje
{
    public abstract class Sprite
    {
        protected Pen selPen = new Pen(Color.LightGray, 1);
        public abstract float X { get; }
        public abstract float Y { get; }
        public abstract float Width { get; }
        public abstract float Height { get; }
        public abstract IEnumerable<Sprite> SubSprites();
        public abstract bool Contains(float x, float y);
        public abstract void Move(float dx, float dy);
        public abstract void Draw(Graphics g);
        public void DrawAsSelected(Graphics g)
        {
            Draw(g);
            g.DrawRectangle(selPen, X, Y, Width, Height);
        }
    }
}
