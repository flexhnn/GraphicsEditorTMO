using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_editor
{
    public class EdgeF
    {
        public PointF a;
        public PointF b;
        public float dx;
        public float dy;
        public float tg;
        public int flag;
        public int flag2;
        
        public EdgeF(PointF a, PointF b)
        {
            this.a = a;
            this.b = b;
            if (a.Y > b.Y)
            {
                PointF buf = a;
                this.a = b;
                this.b = buf;
            }
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
            this.flag = 0;
            this.flag2 = 0;
        }
    }
}
