using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace StanjeOblika
{
    abstract class Shape
    {
        abstract public void Draw(Graphics g);
        abstract protected bool ContainsPoint(int x, int y);

        interface State
        {
            public void Init();
            public void Update();
        }
        class SteadyState : State
        {
            public void Init()
            {
            }
            public void Update()
            {
            }
        }
        class ColorChangingState : State
        {
            private Shape shape;
            private Random rnd = new Random();
            private Color startingColor;
            private int ticks = 0;
            const int DURATION_IN_TICKS = 30;
            public ColorChangingState(Shape s)
            {
                shape = s;
                startingColor = s.color;
            }
            public void Init()
            {
                ticks = 0;
            }
            public void Update()
            {
                shape.color = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
                if (++ticks > DURATION_IN_TICKS)
                {
                    shape.color = startingColor;
                    shape.SetState(shape.STEADY_STATE);
                }
            }
        }
        class ShakingState : State
        {
            private Shape shape;
            private Random rnd = new Random();
            private int ticks = 0;
            const int DURATION_IN_TICKS = 30;
            public ShakingState(Shape s)
            {
                shape = s;
            }

            float randomBetween(float a, float b)
            {
                return a + rnd.NextSingle() * (b - a);
            }

            public void Init()
            {
                ticks = 0;
            }

            public void Update()
            {
                shape.x += randomBetween(-5, 5);
                shape.y += randomBetween(-5, 5);
                if (++ticks > DURATION_IN_TICKS)
                    shape.SetState(shape.STEADY_STATE);
            }
        }

        private State STEADY_STATE;
        private State COLOR_CHANGING_STATE;
        private State SHAKING_STATE;
        private State state = null!;
        protected Color color;
        protected float x, y;

        public Shape(float x, float y, Color c)
        {
            this.x = x;
            this.y = y;
            color = c;
            STEADY_STATE = new SteadyState();
            COLOR_CHANGING_STATE = new ColorChangingState(this);
            SHAKING_STATE = new ShakingState(this);
            SetState(STEADY_STATE);
        }
        public void Update()
        {
            state.Update();
        }
        public void OnClick(int button, int x, int y)
        {
            if (ContainsPoint(x, y))
            {
                State newState = STEADY_STATE;
                if (button == 1) newState = COLOR_CHANGING_STATE;
                if (button == 2) newState = SHAKING_STATE;
                SetState(newState);
            }
        }
        private void SetState(State st)
        {
            st.Init();
            this.state = st;
        }
    }
}
