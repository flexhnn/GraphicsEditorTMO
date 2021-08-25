using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_editor
{
    class FTMObjectEdge : FObject
    {
        private Edge[] arr;

        public const int type = 6;

        private float[] model; //Матрица модели (произведение вышеперечисленных матриц)      
        public Pen pen;
        public Color color;

        public int id;

        public FTMObjectEdge(List<Edge> k, Color color)
        {
            this.color = color;
            this.pen = new Pen(color);
            Point[] mm = new Point[]{k[0].a, k[0].a};
            for (int i = 0; i < k.Count(); i++)
            {
                if (k[i].a.X < mm[0].X) mm[0].X = k[i].a.X;
                if (k[i].b.X < mm[0].X) mm[0].X = k[i].b.X;
                if (k[i].a.Y < mm[0].Y) mm[0].Y = k[i].a.Y;
                if (k[i].b.Y < mm[0].Y) mm[0].Y = k[i].b.Y;
                if (k[i].a.X > mm[1].X) mm[1].X = k[i].a.X;
                if (k[i].b.X > mm[1].X) mm[1].X = k[i].b.X;
                if (k[i].a.Y > mm[1].Y) mm[1].Y = k[i].a.Y;
                if (k[i].b.Y > mm[1].Y) mm[1].Y = k[i].b.Y;
            }

            float cx = (mm[0].X + mm[1].X) / 2;
            float cy = (mm[0].Y + mm[1].Y) / 2;

            this.model = FMatrix.getTrM(cx, cy);
            float[] otrm = FMatrix.getTrM(-cx, -cy);

            this.arr = new Edge[k.Count()];
            for (int i = 0; i < k.Count(); i++)
            {
                this.arr[i] = new Edge(FMatrix.multiply3i(k[i].a, otrm), FMatrix.multiply3i(k[i].b, otrm));
            }
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            int yNext;

            EdgeF[] edges = getEdgesF();

            Func.fsort(edges);
            List<EdgeF> tar = new List<EdgeF>();
            PointF[] mm = getMinMaxF();
            int Ymin = (int)Math.Round(mm[0].Y, MidpointRounding.AwayFromZero);
            int Ymax = (int)Math.Round(mm[1].Y, MidpointRounding.AwayFromZero);
            int yCur = Ymin;
            yCur = refact(tar, edges, yCur);
            yNext = getYnext(tar, edges);
            Func.fsortx(tar);
            yCur = Ymin;
            while (yCur != Ymax)
            {
                for (int i = yCur; i < yNext; i++)
                {
                    for (int j = 0; j < tar.Count(); j += 2)
                    {
                        if (tar.Count() % 2 != 0)
                        {
                            return;
                        }
                        e.DrawLine(pen, tar[j].a.X, i, tar[j + 1].a.X, i);
                        tar[j].a.X += tar[j].tg;
                        tar[j + 1].a.X += tar[j + 1].tg;
                    }
                    yCur = refact(tar, edges, yCur);
                    ++yCur;
                }
                yCur = refact(tar, edges, yCur);
                if (tar.Count() != 0)
                {
                    yNext = getYnext(tar, edges);
                }
                else
                {
                    break;
                }
            }
        }

        private int getYnext(List<EdgeF> tar, EdgeF[] edges)
        {
            int yNext = (int)Math.Round(tar[0].b.Y, MidpointRounding.AwayFromZero);
            for (int i = 1; i < tar.Count(); i++)
            {
                if (yNext > tar[i].b.Y)
                {
                    yNext = (int)Math.Round(tar[i].b.Y, MidpointRounding.AwayFromZero);
                }
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].flag == 0 && yNext > edges[i].a.Y)
                {
                    yNext = (int)Math.Round(edges[i].a.Y, MidpointRounding.AwayFromZero);
                }
            }
            return yNext;
        }

        public static int refact(List<EdgeF> tar, EdgeF[] edges, int yCur)
        {
            for (int i = 0; i < tar.Count(); i++)
            {
                if ((int)Math.Round(tar[i].b.Y, MidpointRounding.AwayFromZero) == yCur)
                {
                    tar.RemoveAt(i);
                    --i;
                }
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].flag == 0 && (int)Math.Round(edges[i].a.Y, MidpointRounding.AwayFromZero) == yCur)
                {
                    tar.Add(edges[i]);
                    edges[i].flag = 1;
                }
            }
            if (tar.Count() == 0)
            {
                List<EdgeF> buf = new List<EdgeF>();
                for (int i = 0; i < edges.Length; i++)
                {
                    if (edges[i].flag == 0)
                    {
                        buf.Add(edges[i]);
                    }
                }
                if (buf.Count() != 0)
                {
                    EdgeF[] nw = new EdgeF[buf.Count()];
                    for (int i = 0; i < buf.Count(); i++)
                    {
                        nw[i] = buf[i];
                    }
                    edges = nw;
                    Func.fsort(nw);
                    yCur = (int)Math.Round(nw[0].a.Y, MidpointRounding.AwayFromZero);
                    refact(tar, edges, yCur);
                }
            }
            Func.fsortx(tar);
            return yCur;
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
            Edge[] k = getEdges();
            Point[] mm = new Point[] { k[0].a, k[0].a };
            for (int i = 0; i < k.Count(); i++)
            {
                if (k[i].a.X < mm[0].X) mm[0].X = k[i].a.X;
                if (k[i].b.X < mm[0].X) mm[0].X = k[i].b.X;
                if (k[i].a.Y < mm[0].Y) mm[0].Y = k[i].a.Y;
                if (k[i].b.Y < mm[0].Y) mm[0].Y = k[i].b.Y;
                if (k[i].a.X > mm[1].X) mm[1].X = k[i].a.X;
                if (k[i].b.X > mm[1].X) mm[1].X = k[i].b.X;
                if (k[i].a.Y > mm[1].Y) mm[1].Y = k[i].a.Y;
                if (k[i].b.Y > mm[1].Y) mm[1].Y = k[i].b.Y;
            }
            return mm;
        }

        public PointF[] getMinMaxF()
        {
            EdgeF[] k = getEdgesF();
            PointF[] mm = new PointF[] { k[0].a, k[0].a };
            for (int i = 0; i < k.Count(); i++)
            {
                if (k[i].a.X < mm[0].X) mm[0].X = k[i].a.X;
                if (k[i].b.X < mm[0].X) mm[0].X = k[i].b.X;
                if (k[i].a.Y < mm[0].Y) mm[0].Y = k[i].a.Y;
                if (k[i].b.Y < mm[0].Y) mm[0].Y = k[i].b.Y;
                if (k[i].a.X > mm[1].X) mm[1].X = k[i].a.X;
                if (k[i].b.X > mm[1].X) mm[1].X = k[i].b.X;
                if (k[i].a.Y > mm[1].Y) mm[1].Y = k[i].a.Y;
                if (k[i].b.Y > mm[1].Y) mm[1].Y = k[i].b.Y;
            }
            return mm;
        }

        public Point[] getPoints()
        {
            Point[] p = new Point[arr.Length * 2];
            for (int i = 0, j = 0; i < p.Length; i += 2, j++)
            {
                p[i] = FMatrix.multiply3i(arr[j].a, model);
                p[i + 1] = FMatrix.multiply3i(arr[j].b, model);
            }
            return p;
        }

        public EdgeF[] getEdgesF()
        {
            EdgeF[] eds = new EdgeF[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                eds[i] = new EdgeF(FMatrix.multiply3f(arr[i].a, model), FMatrix.multiply3f(arr[i].b, model));
            }
            return eds;
        }

        public Edge[] getEdges()
        {
            Edge[] eds = new Edge[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                eds[i] = new Edge(FMatrix.multiply3i(arr[i].a, model), FMatrix.multiply3i(arr[i].b, model));
            }
            return eds;
        }

        public Point getCenter()
        {
            return FMatrix.multiply3i(new Point(0, 0), model);
        }

        public void setColor(Color color)
        {
            this.pen.Color = color;
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
