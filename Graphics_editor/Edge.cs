using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_editor
{
    public class Edge
    {
        public Point a;
        public Point b;
        public int dx;
        public int dy;
        public float tg;
        public int flag2;

        public Edge(Point a, Point b)
        {
            this.a = a;
            this.b = b;
            this.dx = b.X - a.X;
            this.dy = b.Y - a.Y;
            if (dx == 0)
            {
                this.tg = 0;
            }
            else
            {
                if (dy == 0)
                {
                    this.tg = 1;
                }
                else
                {
                    this.tg = dx / dy;
                }
            }
            this.flag2 = 0;
        }
    }
}
