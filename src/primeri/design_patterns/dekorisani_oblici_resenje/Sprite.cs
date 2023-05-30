using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DekorisaniOblici
{
    interface Sprite
    {
        void Draw(Graphics g);
        void Update();

        float X { get; set;  }
        float Y { get; set;  }
    }
    class Circle : Sprite
    {
        private float x, y;
        private float r;
        private Brush brush;

        public Circle(float x, float y, float r, Color color)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            this.brush = new SolidBrush(color);
        }
        public float X
        {
            get { return x; }   
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(brush, x - r, y - r, 2 * r, 2 * r);
        }

        public void Update()
        {
        }
    }

    class Rectangle : Sprite
    {
        private float x, y;
        private float width, height;
        Brush brush;

        public Rectangle(float x, float y, float width, float height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.brush = new SolidBrush(color);
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(brush, x - width/2, y - height/2, width, height);
        }

        public void Update()
        {
        }
    }
}
