using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FBezier : FObject
    {
        private Point[] arr; //Координаты вершин треугольника

        public const int type = 2; //Код примитива

        private float[] model; //Матрица модели (произведение вышеперечисленных матриц)

        public Pen pen; //Пен примитива

        public int id;

        private double dt; //Шаг

        private int n; //Степень

        public FBezier(Point[] arr, Color color, int countPoints)
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(arr[0].X, arr[0].Y));

            this.pen = new Pen(color, 1);

            this.dt = 0.01;

            Point[] mm = new Point[2];
            mm[0] = new Point(arr[0].X, arr[0].Y);
            mm[1] = new Point(arr[0].X, arr[0].Y);

            this.n = countPoints - 1;

            double factN = FMatrix.fa[n];
            double t = dt;
            double dth = 1 + dt / 2;
            int Xprev = arr[0].X;
            int Yprev = arr[0].Y;
            double J;

            while (t < dth)
            {
                double xt = 0, yt = 0;
                int i = 0;
                while (i <= n)
                {
                    J = (FMatrix.pw(t, i) * FMatrix.pw(1 - t, n - i));
                    J *= (factN / (FMatrix.fa[i] * FMatrix.fa[n - i]));
                    xt = (xt + arr[i].X * J);
                    yt = (yt + arr[i].Y * J);
                    ++i;
                }
                list.Add(new Point((int)Math.Round(xt, MidpointRounding.AwayFromZero), (int)Math.Round(yt,MidpointRounding.AwayFromZero)));
                if (Xprev > mm[1].X) mm[1].X = Xprev;
                if (Yprev > mm[1].Y) mm[1].Y = Yprev;
                if (Xprev < mm[0].X) mm[0].X = Xprev;
                if (Yprev < mm[0].Y) mm[0].Y = Yprev;
                t += dt;
                Xprev = (int)xt;
                Yprev = (int)yt;
            }

            this.arr = new Point[list.Count()];
            Point center = new Point();
            center.X = (mm[0].X + mm[1].X) / 2;
            center.Y = (mm[0].Y + mm[1].Y) / 2;
            model = FMatrix.getTrM(center.X, center.Y);

            float[] otrm = FMatrix.getTrM(-center.X, -center.Y);
            for (int i = 0; i < list.Count(); i++)
            {
                this.arr[i] = FMatrix.multiply3i(list[i], otrm);
            }
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            e.DrawLines(pen, getPoints());
        }

        public void move(float dx, float dy)
        {
            model = FMatrix.multiply3fm(model, FMatrix.getTrM(dx, dy));
        }

        public void rotate(Point p, float angle)
        {
            model = FMatrix.multiply3fm(model, FMatrix.getRtM(p, angle));
        }

        public void scale(Point p, float sx, float sy)
        {
            model = FMatrix.multiply3fm(model, FMatrix.getMtM2(p, sx, sy));
        }

        public Point[] getMinMax()
        {
            Point[] k = getPoints();
            Point[] mm = new Point[] { k[0], k[0] };
            for (int i = 0; i < k.Length; i++)
            {
                if (k[i].X < mm[0].X) mm[0].X = k[i].X;
                if (k[i].Y < mm[0].Y) mm[0].Y = k[i].Y;
                if (k[i].X > mm[1].X) mm[1].X = k[i].X;
                if (k[i].Y > mm[1].Y) mm[1].Y = k[i].Y;
            }
            return mm;
        }

        public Point[] getPoints()
        {
            Point[] p = new Point[this.arr.Length];
            for (int i = 0; i < arr.Length; i++) p[i] = FMatrix.multiply3i(arr[i], model);
            return p;
        }

        public void setColor(Color color)
        {
            this.pen.Color = color;
        }

        public Point getCenter()
        {
            return FMatrix.multiply3i(new Point(0, 0), model);
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }
    }
}
