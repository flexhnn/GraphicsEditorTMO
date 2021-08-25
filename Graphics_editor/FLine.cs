using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FLine : FObject
    {
        private Point[] arr; //Координаты вершин 

        public const int type = 1; //Код примитива

        private float[] model; //Матрица модели (произведение вышеперечисленных матриц)      
        public Brush pen;
        public Color color;

        public int id;

        public FLine(Point[] arr, Color color)
        {
            this.arr = new Point[arr.Length];
            for (int i = 0; i < arr.Length; i++) this.arr[i] = new Point(arr[i].X, arr[i].Y);
            this.color = color;
            this.pen = new SolidBrush(color);          

            Point center = new Point();
            center.X = (arr[0].X + arr[1].X) / 2;
            center.Y = (arr[0].Y + arr[1].Y) / 2;

            this.model = FMatrix.getTrM(center.X, center.Y);
            float[] otrM = FMatrix.getTrM(-center.X, -center.Y);

            for (int i = 0; i < arr.Length; i++) this.arr[i] = FMatrix.multiply3i(this.arr[i], otrM);          
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            Point[] p = getPoints();
            int x, y, dx, dy, Sx = 0, Sy = 0;
            int F = 0, Fx = 0, dFx = 0, Fy = 0, dFy = 0;

            dx = p[1].X - p[0].X;
            dy = p[1].Y - p[0].Y;

            Sx = Math.Sign(dx);
            Sy = Math.Sign(dy);

            if (Sx > 0) dFx = dy;
            else dFx = -dy;
            if (Sy > 0) dFy = dx;
            else dFy = -dx;

            x = p[0].X;
            y = p[0].Y;
            F = 0;

            int x2 = p[1].X;
            int y2 = p[1].Y;

            if (Func.abs(dx) >= Func.abs(dy))
            {
                do
                {
                    e.FillRectangle(pen, x, y, 1, 1);
                    if (x == x2) break;
                    Fx = F + dFx;
                    F = Fx - dFy;
                    x = x + Sx;
                    if (Func.abs(Fx) < Func.abs(F)) F = Fx;
                    else y = y + Sy;
                } while (true);
            }
            else
            {
                do
                {
                    e.FillRectangle(pen, x, y, 1, 1);
                    if (y == y2) break;
                    Fy = F - dFy;
                    F = Fy + dFx;
                    y = y + Sy;
                    if (Func.abs(Fy) < Func.abs(F)) F = Fy;
                    else x = x + Sx;
                } while (true);
            }
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
            Point[] mm = new Point[] {k[0], k[0]};
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

        public Point getCenter()
        {
            return FMatrix.multiply3i(new Point(0, 0), model);
        }

        public void setColor(Color color)
        {
            this.pen = new SolidBrush(color);
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
