using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FStar : FObject
    {
        private Point[] arr; //Координаты вершин звезды

        public const int type = 3; //Код примитива

        private float[] model; //Матрица модели (произведение вышеперечисленных матриц)

        public Pen pen; //Пен примитива

        public int id;                    

        public FStar(Point[] CRr, int n, Color color)
        {
            Point[] Rr = CRr;
            int a = 0;

            if (n % 2 != 0)
            {
                a = -90;
            }

            this.pen = new Pen(color, 1);

            this.model = FMatrix.getTrM(Rr[0].X, Rr[0].Y);
            float[] otrM = FMatrix.getTrM(-Rr[0].X, -Rr[0].Y);
            
            buildArr(Rr, a, n);

            for (int i = 0; i < arr.Length; i++) arr[i] = FMatrix.multiply3i(arr[i], otrM);
        }

        private void buildArr(Point[] Rr, int a, int n) //Расчет координат
        {
            this.arr = new Point[2 * n]; //Создание массива подходящего размера

            int dx = Rr[1].X - Rr[0].X;                                         //Расчет внутреннего радиуса
            int dy = Rr[1].Y - Rr[0].Y;                                         //Расчет внутреннего радиуса
            float r = (float) Math.Sqrt((double)(dx * dx) + (double)(dy * dy)); //Расчет внутреннего радиуса

            dx = Rr[2].X - Rr[0].X;                                            //Расчет внешнего радиуса
            dy = Rr[2].Y - Rr[0].Y;                                            //Расчет внешнего радиуса
            float R = (float)Math.Sqrt((double)(dx * dx) + (double)(dy * dy)); //Расчет внешнего радиуса

            for (int i = 0; i < arr.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    this.arr[i].X = (int)(Math.Round(Rr[0].X + r * Math.Cos(a * Math.PI / 180), MidpointRounding.AwayFromZero));
                    this.arr[i].Y = (int)(Math.Round(Rr[0].Y - r * Math.Sin(a * Math.PI / 180), MidpointRounding.AwayFromZero));
                }
                else
                {
                    this.arr[i].X = (int)(Math.Round(Rr[0].X + R * Math.Cos(a * Math.PI / 180), MidpointRounding.AwayFromZero));
                    this.arr[i].Y = (int)(Math.Round(Rr[0].Y - R * Math.Sin(a * Math.PI / 180), MidpointRounding.AwayFromZero));
                }
                a = a + 180 / n;
            }          
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            Point[] vertexList = getPoints();
            Point[] mm = getMinMax();
            int Ymin = mm[0].Y;
            int Ymax = mm[1].Y;
            int pCount = vertexList.Count();

            List<int> Xb = new List<int>();

            for (int Y = Ymin; Y <= Ymax; Y++)
            {
                Xb.Clear();
                int k = 0;
                for (int i = 0; i < pCount; i++)
                {
                    k = i + 1;
                    if (i == pCount - 1)
                    {
                        k = 0;
                    }
                    if (((vertexList[i].Y < Y) && (vertexList[k].Y >= Y)) || ((vertexList[i].Y >= Y) && (vertexList[k].Y < Y)))
                    {
                        Xb.Add(vertexList[i].X + ((Y - vertexList[i].Y) * (vertexList[k].X - vertexList[i].X)) / (vertexList[k].Y - vertexList[i].Y));
                    }
                }
                Xb.Sort();
                for (int i = 0; i < Xb.Count() - 1; i += 2)
                {
                    e.DrawLine(pen, Xb[i], Y, Xb[i + 1], Y);
                }
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
            for (int i = 0; i < arr.Length; i++)
            {
                p[i] = FMatrix.multiply3i(arr[i], model);              
            }
            return p;
        }

        public void setColor(Color color)
        {
            this.pen.Color = color;
        }

        public Point getCenter()
        {
            return FMatrix.multiply3i(new PointF(0, 0), model);
        }

        public Pen getPen()
        {
            return this.pen;
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
