using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    public class line
    {
        public double a, b, c;
        public line() 
        { 
        
        }
        public line(PointF p, PointF q)
        {
            a = p.Y - q.Y;
            b = q.X - p.X;
            c = -a * p.X - b * p.Y;
            norm();
        }

        public void norm()
        {
            double z = Math.Sqrt(a * a + b * b);
            if (Func.abs(z) > Func.EPS)
            {
                a /= z;
                b /= z;
                c /= z;
            }
        }

        public double dist(PointF p)
        {
            return a * p.X + b * p.Y + c;
        }
    }
}
