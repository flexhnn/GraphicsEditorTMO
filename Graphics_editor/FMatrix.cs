using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FMatrix
    {
        public static double[] fa;

        public static double[] getFactArray(int count)
        {
            double[] factArray = new double[count];
            factArray[0] = 1;
            factArray[1] = 1;
            for (int i = 2; i < factArray.Length; i++)
            {
                factArray[i] = factArray[i - 1] * i;
            }
            return factArray;
        }

        public static double pw(double a, int b)
        {
            double res = 1;
            for (int i = 0; i < b; i++) res *= a;
            return res;
        }

        public static float[] multiply3fm(float[] m1, float[] m2)
        {
            float[] res = new float[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j += 3)
                {
                    for (int k = 0, d = 0; k < 3; k++, d += 3)
                    {
                        res[i + j] += m1[k + j] * m2[i + d];
                    }
                }
            }
            return res;
        }

        public static PointF multiply3f(PointF p, float[] m2)
        {
            float[] m1 = new float[] {p.X, p.Y, 1};
            float[] res = new float[3];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0, k = 0; l < 3; l++, k += 3)
                {
                    res[i] += m1[l] * m2[k + j];
                }
                ++j;
            }
            return new PointF(res[0], res[1]);
        }

        public static Point multiply3i(PointF p, float[] m2)
        {
            float[] m1 = new float[] { p.X, p.Y, 1 };
            int[] res = new int[3];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0, k = 0; l < 3; l++, k += 3)
                {
                    res[i] += (int)Math.Round(m1[l] * m2[k + j], MidpointRounding.AwayFromZero);
                }
                ++j;
            }
            return new Point(res[0], res[1]);
        }

        public static Point multiply3i(Point p, float[] m2)
        {
            int[] m1 = new int[] { p.X, p.Y, 1 };
            int[] res = new int[3];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0, k = 0; l < 3; l++, k += 3)
                {
                    res[i] += (int)Math.Round(m1[l] * m2[k + j], MidpointRounding.AwayFromZero);
                }
                ++j;
            }
            return new Point(res[0], res[1]);
        }

        public static Point rotate(Point p, float angle, Point c)
        {
            return multiply3i(p, getRtM(c, angle));
        }

        public static float[] getTrM(float x, float y)
        {
            return new float[] { 1, 0, 0, 0, 1, 0, x, y, 1 };
        }

        public static float[] getMtM(float x, float y)
        {
            return new float[] { x, 0, 0, 0, y, 0, 0, 0, 1 };
        }

        public static float[] getMtM2(float x, float y, Point c)
        {
            return new float[] { x, 0, 0, 0, y, 0, (1-x)*c.X, (1-y)*c.Y, 1 };
        }

        public static float[] getMtM2(Point p, float sx, float sy)
        {
            return new float[] { sx, 0, 0, 0, sy, 0, (1 - sx) * p.X, (1 - sy) * p.Y, 1 };
        }

        public static float[] getRtM(float x, float y, float angle)
        {
            double ang = angle * Math.PI / 180;
            double cosa = Math.Cos(ang);
            double sina = Math.Sin(ang);
            return new float[] { (float)cosa, (float)sina, 0, (float)-sina, (float)cosa, 0, (float)(-x * cosa + y * sina + x), (float)(-x * sina - y * cosa + y), 1 };
        }

        public static float[] getRtM(Point p, float angle)
        {
            double ang = angle * Math.PI / 180;
            double cosa = Math.Cos(ang);
            double sina = Math.Sin(ang);
            return new float[] { (float)cosa, (float)sina, 0, (float)-sina, (float)cosa, 0, (float)(-p.X * cosa + p.Y * sina + p.X), (float)(-p.X * sina - p.Y * cosa + p.Y), 1 };
        }
    }
}
