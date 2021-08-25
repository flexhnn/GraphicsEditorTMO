using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class Geom2_0
    {
        public const double EPS = 1E-9;
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static bool intersect(Edge ed1, Edge ed2, FPoint left, FPoint right)
        {
            PointF a = new PointF(ed1.a.X, ed1.a.Y);
            PointF b = new PointF(ed1.b.X, ed1.b.Y);
            PointF c = new PointF(ed2.a.X, ed2.a.Y);
            PointF d = new PointF(ed2.b.X, ed2.b.Y);

            if (!intersect_1d(a.X, b.X, c.X, d.X) || !intersect_1d(a.X, b.X, c.X, d.X))
            {
                return false;
            }
            line m = new line(a, b);
            line n = new line(c, d);
            double zn = det(m.a, m.b, n.a, n.b);
            if (Func.abs(zn) < EPS)
            {
                if (Func.abs(m.dist(c)) > EPS || Func.abs(n.dist(a)) > EPS)
                {
                    return false;
                }
                if (isAmin(b, a))
                {
                    PointF q;
                    q = a;
                    a = b;
                    b = q;
                }
                if (isAmin(d, c))
                {
                    PointF q;
                    q = c;
                    c = d;
                    d = q;
                }
                PointF buf1 = max(a, c);
                PointF buf2 = min(b, d);
                left.X = buf1.X;
                left.Y = buf1.Y;
                right.X = buf2.X;
                right.Y = buf2.Y;
                return true;
            }
            else
            {
                left.X = right.X = (float)(-det(m.c, m.b, n.c, n.b) / zn);
                left.Y = right.Y = (float)(-det(m.a, m.c, n.a, n.c) / zn);
                return betw(a.X, b.X, left.X) && betw(a.Y, b.Y, left.Y) && betw(c.X, d.X, left.X) && betw(c.Y, d.Y, left.Y);
            }
        }

        static double det(double a, double b, double c, double d)
        {
            return (a * d - b * c);
        }

        static bool betw(double l, double r, double x)
        {
            return Math.Min(l, r) <= x + EPS && x <= Math.Max(l, r) + EPS;
        }

        static bool intersect_1d(double a, double b, double c, double d)
        {
            if (a > b)
            {
                double q = a;
                a = b;
                b = q;
            }
            if (c > d)
            {
                double q = c;
                c = d;
                d = q;
            }
            return Math.Max(a, c) <= Math.Min(b, d) + EPS;
        }

        public static PointF max(PointF a, PointF b)
        {
            if (isAmin(a, b))
            {
                return b;
            }
            return a;
        }

        public static PointF min(PointF a, PointF b)
        {
            if (isAmin(a, b))
            {
                return a;
            }
            return b;
        }

        public static bool isAmin(PointF a, PointF b)
        {
            return a.X < b.X - EPS || Func.abs(a.X - b.X) < EPS && a.Y < b.Y - EPS;
        }
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static bool isInside(FObject fo, Point p)
        {
            Edge[] edges = Func.getEdgesOfFObject(fo);
            EdgeF[] eds = Func.edgeToEdge2(edges);
            List<EdgeF> eds2 = new List<EdgeF>();
            for (int i = 0; i < eds.Length; i++)
            {
                if (p.Y >= eds[i].a.Y && p.Y < eds[i].b.Y)
                {
                    eds2.Add(new EdgeF(new PointF(eds[i].a.X, eds[i].a.Y), new PointF(eds[i].b.X, eds[i].b.Y)));
                }
            }
            for (int i = 0; i < eds2.Count(); i++)
            {
                while ((int)Math.Round(eds2[i].a.Y, MidpointRounding.AwayFromZero) != p.Y)
                {
                    if (eds2[i].a.Y < p.Y)
                    {
                        eds2[i].a.X += eds2[i].tg;
                        ++eds2[i].a.Y;
                    }                    
                }
            }
            Func.fsortx(eds2);
            for (int i = 0; i < eds2.Count(); i += 2)
            {
                if (p.X > eds2[i].a.X && p.X < eds2[i + 1].a.X)
                {
                    return true;
                }
            }
            return false;
        }
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static bool isInside(FObject fo, PointF p)
        {
            Edge[] edges = Func.getEdgesOfFObject(fo);
            EdgeF[] eds = Func.edgeToEdge2(edges);
            List<EdgeF> eds2 = new List<EdgeF>();
            for (int i = 0; i < eds.Length; i++)
            {
                if (p.Y >= eds[i].a.Y && p.Y < eds[i].b.Y)
                {
                    eds2.Add(new EdgeF(new PointF(eds[i].a.X, eds[i].a.Y), new PointF(eds[i].b.X, eds[i].b.Y)));
                }
            }
            for (int i = 0; i < eds2.Count(); i++)
            {
                while ((int)Math.Round(eds2[i].a.Y, MidpointRounding.AwayFromZero) != (int)Math.Round(p.Y, MidpointRounding.AwayFromZero))
                {
                    if (eds2[i].a.Y < p.Y)
                    {
                        eds2[i].a.X += eds2[i].tg;
                        ++eds2[i].a.Y;
                    }  
                }
            }
            Func.fsortx(eds2);
            for (int i = 0; i < eds2.Count(); i += 2)
            {
                if (p.X > eds2[i].a.X && p.X < eds2[i + 1].a.X)
                {
                    return true;
                }
            }
            return false;
        }
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static bool intersection(Edge ed1, Edge ed2, FPoint k)
        {
            PointF dir1 = new PointF(ed1.dx, ed1.dy);
            PointF dir2 = new PointF(ed2.dx, ed2.dy);

            float a1 = -dir1.Y;
            float b1 = +dir1.X;
            float d1 = -(a1 * ed1.a.X + b1 * ed1.a.Y);

            float a2 = -dir2.Y;
            float b2 = +dir2.X;
            float d2 = -(a2 * ed2.a.X + b2 * ed2.a.Y);

            float seg1_line2_start = a2 * ed1.a.X + b2 * ed1.a.Y + d2;
            float seg1_line2_end = a2 * ed1.b.X + b2 * ed1.b.Y + d2;

            float seg2_line1_start = a1 * ed2.a.X + b1 * ed2.a.Y + d1;
            float seg2_line1_end = a1 * ed2.b.X + b1 * ed2.b.Y + d1;

            if (seg1_line2_start * seg1_line2_end >= 0 || seg2_line1_start * seg2_line1_end >= 0)
            {
                return false;
            }

            float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
            k.X = ed1.a.X + u * dir1.X;
            k.Y = ed1.a.Y + u * dir1.Y;

            if (k.X < 0)
            {
                k.X *= -1;
            }
            if (k.Y < 0)
            {
                k.Y *= -1;
            }

            return true;
        }
    }
}
