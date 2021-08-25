using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FTrg : FObject
    {
        private Point[] fin; //Координаты вершин треугольника
       
        public const int type = 4; //Код примитива

        private float[] model; //Матрица модели (произведение вышеперечисленных матриц)
         
        public Pen pen; //Пен примитива

        public int id;

        public FTrg(Point[] arr, Color color)
        {
            this.fin = new Point[3];

            this.fin[0] = arr[0];
            this.fin[1] = arr[1];
            this.pen = new Pen(color, 1);                                

            refact();
            buildThree(arr);

            Point max = new Point(fin[0].X, fin[0].Y);
            Point min = new Point(fin[0].X, fin[0].Y);
            for (int i = 0; i < 3; i++)
            {
                if (fin[i].X > max.X) max.X = (int)fin[i].X;
                if (fin[i].Y > max.Y) max.Y = (int)fin[i].Y;
                if (fin[i].X < min.X) min.X = (int)fin[i].X;
                if (fin[i].Y < min.Y) min.Y = (int)fin[i].Y;
            }
            Point center = new Point(((max.X + min.X) / 2), ((max.Y + min.Y) / 2));

            this.model = FMatrix.getTrM(center.X, center.Y);          
            float[] otrM = FMatrix.getTrM(-center.X, -center.Y);

            for (int i = 0; i < fin.Length; i++) fin[i] = FMatrix.multiply3i(fin[i], otrM);
        }

        private void refact() //Упорядочивание вершин основания
        {
            if (fin[0].X > fin[1].X)
            {
                Point buf = fin[0];
                fin[0] = fin[1];
                fin[1] = buf;
            }
        }

        private void buildThree(Point[] arr) //Расчет третей вершины
        {
            Point wcenter = new Point();
            wcenter.X = (fin[0].X + fin[1].X) / 2;
            wcenter.Y = (fin[0].Y + fin[1].Y) / 2;
            Point p;

            float ddx = arr[2].X - wcenter.X;
            float ddy = arr[2].Y - wcenter.Y;
            double ar2l = Math.Sqrt((ddx * ddx) + (ddy * ddy));

            float rdx = fin[1].X - fin[0].X;
            float rdy = fin[1].Y - fin[0].Y;
            
            if (Math.Abs(rdx) >= Math.Abs(rdy))
            {
                if (arr[2].Y > wcenter.Y)
                {
                    p = FMatrix.rotate(fin[1], 90, new Point(wcenter.X, wcenter.Y));
                }
                else
                {
                    p = FMatrix.rotate(fin[0], 90, new Point(wcenter.X, wcenter.Y));
                }
            }
            else
            {
                if (fin[0].Y > fin[1].Y)
                {
                    Point buf = fin[0];
                    fin[0] = fin[1];
                    fin[1] = buf;
                }
                if (arr[2].X > wcenter.X)
                {
                    p = FMatrix.rotate(fin[0], 90, new Point(wcenter.X, wcenter.Y));
                }
                else
                {
                    p = FMatrix.rotate(fin[1], 90, new Point(wcenter.X, wcenter.Y));
                }
            }           
            float dx = p.X - wcenter.X;
            float dy = p.Y - wcenter.Y;
            float le = (float)Math.Sqrt((dx * dx) + (dy * dy));
            float dx1 = dx / le;
            float dy1 = dy / le;
            dx1 *= (float) ar2l;
            dy1 *= (float) ar2l;                    
            fin[2].X = (int)Math.Round(wcenter.X + dx1, MidpointRounding.AwayFromZero);
            fin[2].Y = (int)Math.Round(wcenter.Y + dy1, MidpointRounding.AwayFromZero);           
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
            Point[] p = new Point[this.fin.Length];
            for (int i = 0; i < fin.Length; i++) p[i] = FMatrix.multiply3i(fin[i], model);
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
