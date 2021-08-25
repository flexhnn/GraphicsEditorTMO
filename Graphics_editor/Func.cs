using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class Func
    {
        public const double EPS = 1E-9;

        public static int abs(int i)
        {
            if (i < 0) i *= -1;
            return i;
        }

        public static double abs(double i)
        {
            if (i < 0) i *= -1;
            return i;
        }

        public static void fsortx(List<EdgeF> a)
        {
            int i = 0;
            bool t = true;
            while (t)
            {
                t = false;
                for (int j = 0; j < a.Count() - i - 1; j++)
                {
                    if (a[j].a.X > a[j + 1].a.X)
                    {
                        EdgeF buf = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = buf;
                        t = true;
                    }
                }
                i = i + 1;
            }
        }

        public static void fsort(EdgeF[] a)
        {
            int i = 0;
            bool t = true;
            while (t)
            {
                t = false;
                for (int j = 0; j < a.Length - i - 1; j++)
                {
                    if (a[j].a.Y > a[j + 1].a.Y)
                    {
                        EdgeF buf = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = buf;
                        t = true;
                    }
                }
                i = i + 1;
            }
        }

        public static void fsortx(List<Point> a)
        {
            int i = 0;
            bool t = true;
            while (t)
            {
                t = false;
                for (int j = 0; j < a.Count() - i - 1; j++)
                {
                    if (a[j].X > a[j + 1].X)
                    {
                        Point buf = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = buf;
                        t = true;
                    }
                }
                i = i + 1;
            }
        }

        public static bool equalp1(Point a, Point b)
        {
            if (b.X - a.X < 2 && b.X - a.X > -2 || b.Y - a.Y < 2 && b.Y - a.Y > -2)
            {
                return true;
            }
            return false;
        }

        public static Edge[] getEdgesOfFObject(FObject fo)
        {
            Edge[] ed; 
            if (fo.getFType() != 6)
            {
                Point[] k = fo.getPoints();
                ed = new Edge[k.Length];
                for (int i = 0; i < k.Length - 1; i++)
                {
                    ed[i] = new Edge(k[i], k[i + 1]);
                }
                ed[ed.Length - 1] = new Edge(ed[ed.Length - 2].b, k[0]);
            }
            else
            {
                FTMObjectEdge fte = (FTMObjectEdge)fo;
                ed = fte.getEdges();
            }
            return ed;
        }

        public static EdgeF[] edgeToEdge2(Edge[] edges)
        {
            EdgeF[] edg = new EdgeF[edges.Length];
            for (int i = 0; i < edges.Length; i++)
            {
                edg[i] = new EdgeF(new PointF(edges[i].a.X, edges[i].a.Y), new PointF(edges[i].b.X, edges[i].b.Y));
            }
            return edg;
        }

        public static bool equalE(Edge e1, Edge e2)
        {
            if ((equalp(e1.a, e2.a) && equalp(e1.b, e2.b)) || (equalp(e1.a, e2.b) && equalp(e1.b, e2.a)))
            {
                return true;
            }
            return false;
        }

        public static bool equalp(Point a, Point b)
        {
            if (a.X == b.X && a.Y == b.Y)
            {
                return true;
            }
            return false;
        }

        public static List<Point> delScam(List<Point> bl, FObject fo, Edge edg, EdgeF[] eds, Edge[] edsp) //Протестировать!!
        {
            List<Point> res = new List<Point>(bl); //Рузультирующий список

            List<int> pl = new List<int>(); //Список повторяющихся точек
            List<int> idDel = new List<int>(); //Список точек на удаление

            Edge ed; //Исследуемое ребро
            if (edg.a.X > edg.b.X) ed = new Edge(edg.b, edg.a);
            else ed = new Edge(edg.a, edg.b);

            for (int i = 0; i < bl.Count(); i++) //Главный цикл
            {
                pl.Clear(); //Очищаем список повторяющихся точек
                for (int j = i + 1; j < bl.Count(); j++)
                {
                    if (equalp(bl[i], bl[j])) //Если точка повторилась
                    {
                        pl.Add(j); //То добавить её номер
                    }
                }

                if (pl.Count() != 0) //Если список не пустой
                {
                    PointF before = new PointF(); //Точка до
                    PointF after = new PointF(); //Точка после
                    if (equalp(bl[pl[0]], ed.a)) //Если повторяющаяся точка совпадает с началом ребра
                    {
                        before = ed.a; //То точка до - это точка начала ребра
                    }
                    else //Иначе
                    {
                        if (ed.dx > ed.dy) //Если угол наклона менее 45 градусов
                        {
                            float x = ed.a.X - 1; //То считаем по иксу
                            float y = ed.a.Y + ((ed.dy * (x - ed.a.X)) / ed.dx); //Расчитываем игрек
                            before = new PointF(x, y); //Создаем точку ДО
                        }
                        else //Иначе
                        {
                            float y; 
                            if (ed.a.Y > ed.b.Y) //Если отрезок направлен на убывание
                            {
                                y = ed.a.Y + 1;
                            }
                            else //Иначе
                            {
                                y = ed.a.Y - 1;
                            }
                            float x = ed.a.X + ed.tg * (y - ed.a.Y); //Расчитываем икс
                            before = new PointF(x, y); //Создаем точку ДО
                        }
                    }
                    if (equalp(bl[pl[0]], ed.b)) //Если точка после - это точка конца ребра
                    {
                        after = ed.b; //То точка после - это точка конца ребра
                    }
                    else //Иначе
                    {
                        if (ed.dx > ed.dy) //Если угол наклона менее 45 градусов
                        {
                            float x = ed.a.X + 1; //Считаем по иксу
                            float y = ed.a.Y + ((ed.dy * (x - ed.a.X)) / ed.dx); //Расчитываем игрек
                            after = new PointF(x, y); //Создаем точку ПОСЛЕ
                        } 
                        else //Иначе
                        {
                            float y;
                            if (ed.a.Y > ed.b.Y) //Если отрезок направлен на убывание
                            {
                                y = ed.a.Y - 1;
                            }
                            else //Иначе
                            {
                                y = ed.a.Y + 1;
                            }
                            float x = ed.a.X + ed.tg * (y - ed.a.Y); //Расчитываем икс
                            after = new PointF(x, y); //Создаем точку ПОСЛЕ
                        }
                    }

                    bool k1 = Geom2_0.isInside(fo, before); //Проверяем точку ДО на принадлежность к области
                    bool k2 = Geom2_0.isInside(fo, after); //Проверяем точку ПОСЛЕ на принадлежность к области

                    for (int k5 = 0; k5 < edsp.Length; k5++)
                    {
                        if (Func.abs(ed.tg) - Func.abs(edsp[k5].tg) <= EPS)
                        {
                            if (equalp(bl[pl[0]], edsp[k5].a) || equalp(bl[pl[0]], edsp[k5].b))
                            {
                                k1 = true;
                                k2 = false;
                                break;
                            }
                            if (equalp(bl[pl[0]], ed.a) || equalp(bl[pl[0]], ed.b))
                            {
                                k1 = true;
                                k2 = false;
                                break;
                            }
                        }
                    }

                    if ((k1 && k2) || (!k1 && !k2)) //Если обе точки не принадлежат области или обе точки принадлежат области, то такие точки нужно удалить.
                    {
                        idDel.Add(i);
                        for (int m = 0; m < pl.Count(); m++)
                        {
                            idDel.Add(pl[m]);
                        }
                    }
                    else //Иначе, нужно оставить по одной
                    {
                        for (int m = 0; m < pl.Count(); m++)
                        {
                            idDel.Add(pl[m]);
                        }
                    }
                }
            }
            IEnumerable<int> distinctAges = idDel.Distinct();
            List<int> asList = distinctAges.ToList();
            asList.Sort();

            for (int i = asList.Count() - 1; i >= 0; i--)
            {
                res.RemoveAt(asList[i]);
            }
            return res;
        }       

        public static int Round(double d)
        {
            return (int)Math.Round(d, MidpointRounding.AwayFromZero);
        }

        public static int Round(float f)
        {
            return (int)Math.Round(f, MidpointRounding.AwayFromZero);
        }
    }
}
