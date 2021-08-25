using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Graphics_editor
{
    class TMOperations
    {
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static FObject tmo1(FObject fo1, FObject fo2, Color color) //Пересечение
        {
            Edge[] eds1 = Func.getEdgesOfFObject(fo1); //Массив ребер первого операнда
            Edge[] eds2 = Func.getEdgesOfFObject(fo2); //Массив ребер второго операнда

            EdgeF[] edsf1 = Func.edgeToEdge2(eds1);
            EdgeF[] edsf2 = Func.edgeToEdge2(eds2);

            List<Edge> tar = new List<Edge>(); //Список ребер результата ТМО
            FPoint l = new FPoint(); //Буферная точка пересечения ребер
            FPoint r = new FPoint();
            List<Point> bufList = new List<Point>(); //Буферный список точек пересечений

            //
            //Сканирование ребер первого примитива на предмет нахождения их точек внутри второго примитива
            //

            bool b1; //Флаг принадлежности вершины одного примитива другому
            bool b2; //Флаг принадлежности вершины одного примитива другому

            for (int i = 0; i < eds1.Length; i++) //Для каждого ребра первого операнда
            {
                b1 = Geom2_0.isInside(fo2, eds1[i].a); //Проверим точки ребра на принадлежность второму примитиву
                b2 = Geom2_0.isInside(fo2, eds1[i].b); //Проверим точки ребра на принадлежность второму примитиву

                if (b1 && b2) //Если обе точки ребра внутри примитива
                {
                    eds1[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds2.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds1[i], eds2[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    bufList.Add(eds1[i].a);
                    bufList.Add(eds1[i].b);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo2, eds1[i], edsf2, eds2);

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
                else if (b1) //Если только первая точка внутри примитива
                {
                    eds1[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds2.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds1[i], eds2[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    bufList.Add(eds1[i].a);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo2, eds1[i], edsf2, eds2);

                    if (bufList.Count() % 2 != 0)
                    {
                        for (int n = 0; n < bufList.Count(); n++)
                        {
                            if (Func.equalp(eds1[i].b, bufList[n]))
                            {
                                bufList.RemoveAt(n);
                                break;
                            }
                        }
                    }

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
                else if (b2) //Если только вторая точка внутри примитива
                {
                    eds1[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds2.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds1[i], eds2[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    bufList.Add(eds1[i].b);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo2, eds1[i], edsf2, eds2);

                    if (bufList.Count() % 2 != 0)
                    {
                        for (int n = 0; n < bufList.Count(); n++)
                        {
                            if (Func.equalp(eds1[i].a, bufList[n]))
                            {
                                bufList.RemoveAt(n);
                                break;
                            }
                        }
                    }

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
            }
            //
            //Конец сканирования ребер первого примитива на предмет нахождения их точек внутри второго примитива
            //

            //
            //Сканирование ребер второго примитива на предмет нахождения их точек внутри первого примитива
            //
            for (int i = 0; i < eds2.Length; i++) //Для каждого ребра второго операнда 
            {

                b1 = Geom2_0.isInside(fo1, eds2[i].a); //Проверим точки ребра на принадлежность второму примитиву
                b2 = Geom2_0.isInside(fo1, eds2[i].b); //Проверим точки ребра на принадлежность второму примитиву

                if (b1 && b2) //Если обе точки ребра внутри примитива
                {
                    eds2[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds1.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds2[i], eds1[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    bufList.Add(eds2[i].a);
                    bufList.Add(eds2[i].b);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo1, eds2[i], edsf1, eds1);

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
                else if (b1) //Если только первая точка внутри примитива
                {
                    eds2[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds1.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds2[i], eds1[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    bufList.Add(eds2[i].a);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo1, eds2[i], edsf1, eds1);

                    if (bufList.Count() % 2 != 0)
                    {
                        for (int n = 0; n < bufList.Count(); n++)
                        {
                            if (Func.equalp(eds2[i].b, bufList[n]))
                            {
                                bufList.RemoveAt(n);
                                break;
                            }
                        }
                    }

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
                else if (b2) //Если только вторая точка внутри примитива
                {
                    eds2[i].flag2 = 1; //Обработка такого ребра произойдет в этом блоке

                    bufList.Clear(); //Очистка списка пересечений

                    for (int j = 0; j < eds1.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds2[i], eds1[j], l, r)) //Если есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }                  

                    bufList.Add(eds2[i].b);

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo1, eds2[i], edsf1, eds1);

                    if (bufList.Count() % 2 != 0)
                    {
                        for (int n = 0; n < bufList.Count(); n++)
                        {
                            if (Func.equalp(eds2[i].a, bufList[n]))
                            {
                                bufList.RemoveAt(n);
                                break;
                            }
                        }
                    }

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х

                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
            }
            //
            //Конец сканирования ребер второго примитива на предмет нахождения их точек внутри первого примитива
            //

            //
            //Сканирование оставшихся ребер первой фигуры
            //
            for (int i = 0; i < eds1.Length; i++) //Для каждого ребра первого операнда
            {
                bufList.Clear(); //Очистка списка пересечений

                if (eds1[i].flag2 == 0)
                {
                    for (int j = 0; j < eds2.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds1[i], eds2[j], l, r)) //Если ребро не было обработано ранее и есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo2, eds1[i], edsf2, eds2);

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);     
                    }
                }
            }
            //
            //Конец сканирования оставшихся ребер первой фигуры
            //

            //
            //Сканирование оставшихся ребер второй фигуры
            //
            for (int i = 0; i < eds2.Length; i++) //Для каждого ребра второго операнда
            {
                bufList.Clear(); //Очистка списка пересечений

                if (eds2[i].flag2 == 0)
                {
                    for (int j = 0; j < eds1.Length; j++) //Сканируем на пересечения
                    {
                        if (Geom2_0.intersect(eds2[i], eds1[j], l, r)) //Если ребро не было обработано ранее и есть пересечение
                        {
                            Point a = new Point(Func.Round(l.X), Func.Round(l.Y));
                            Point b = new Point(Func.Round(r.X), Func.Round(r.Y));
                            bufList.Add(a);
                            if (!Func.equalp(a, b))
                            {
                                bufList.Add(b);
                            }
                        }
                    }

                    List<Point> bufer = new List<Point>(bufList);

                    bufList = Func.delScam(bufList, fo1, eds2[i], edsf1, eds1);

                    if (bufList.Count() % 2 != 0)
                    {
                        return null;
                    }

                    Func.fsortx(bufList); //Отсортируем по возрастанию Х
                    for (int j = 0; j < bufList.Count(); j += 2)
                    {
                        bool add = true;
                        Edge bu = new Edge(bufList[j], bufList[j + 1]);
                        for (int t = 0; t < tar.Count(); t++)
                        {
                            if (Func.equalE(bu, tar[t])) add = false;
                        }
                        if (add) tar.Add(bu);                       
                    }
                }
            }
            //
            //Конец сканирования оставшихся ребер второй фигуры
            //            

            if (tar.Count() != 0) //Если список ребер не пустой
            {
                if (tar.Count() != 1)
                {
                    return new FTMObjectEdge(tar, color); //Создаем новую фигуру (Результат ТМО)
                }

            }
            return null; //Иначе возвращаем null
        }
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        public static FObject tmo2f(FObject fo1, FObject fo2, Color color) //Симметричная разность
        {
            Edge[] eds1 = Func.getEdgesOfFObject(fo1); //Массив ребер первого операнда
            Edge[] eds2 = Func.getEdgesOfFObject(fo2); //Массив ребер второго операнда

            List<Edge> tar = new List<Edge>(); //Список ребер результата ТМО
            FPoint k = new FPoint(); //Буферная точка пересечения ребер
            List<Point> bufList = new List<Point>(); //Буферный список точек пересечений

            for (int i = 0; i < eds1.Length; i++) //Для каждого ребра первого операнда
            {
                bufList.Clear(); //Очистка списка пересечений

                for (int j = 0; j < eds2.Length; j++) //Сканируем на пересечения
                {
                    if (Geom2_0.intersection(eds1[i], eds2[j], k)) //Если есть пересечение
                    {
                        Point p = new Point((int)Math.Round(k.X, MidpointRounding.AwayFromZero), (int)Math.Round(k.Y, MidpointRounding.AwayFromZero));
                        if (!Func.equalp1(eds1[i].a, p) && !Func.equalp1(eds1[i].b, p)) //Если эта точка не является граничной точкой первого ребра
                        {
                            if (!Func.equalp1(eds2[j].a, p) && !Func.equalp1(eds2[j].b, p)) //Если эта точка не является граничной точкой второго ребра
                            {
                                bufList.Add(new Point((int)Math.Round(k.X, MidpointRounding.AwayFromZero), (int)Math.Round(k.Y, MidpointRounding.AwayFromZero))); //То добавляем точку пересечения в список
                            }
                        }
                    }
                }
                bufList.Add(eds1[i].a); //Добаляем оставшиеся точки
                bufList.Add(eds1[i].b); //Добаляем оставшиеся точки
                Func.fsortx(bufList); //Отсортируем по возрастанию Х
                for (int j = 0; j < bufList.Count() - 1; j++)
                {
                    tar.Add(new Edge(bufList[j], bufList[j + 1])); //Добавим грани в список
                }
            }

            for (int i = 0; i < eds2.Length; i++) //Для каждого ребра второго операнда
            {
                bufList.Clear(); //Очистка списка пересечений

                for (int j = 0; j < eds1.Length; j++) //Сканируем на пересечения
                {
                    if (Geom2_0.intersection(eds2[i], eds1[j], k)) //Если есть пересечение
                    {
                        Point p = new Point((int)Math.Round(k.X, MidpointRounding.AwayFromZero), (int)Math.Round(k.Y, MidpointRounding.AwayFromZero));
                        if (!Func.equalp1(eds2[i].a, p) && !Func.equalp1(eds2[i].b, p)) //Если эта точка не является граничной точкой первого ребра
                        {
                            if (!Func.equalp1(eds1[j].a, p) && !Func.equalp1(eds1[j].b, p)) //Если эта точка не является граничной точкой второго ребра
                            {
                                bufList.Add(new Point((int)Math.Round(k.X, MidpointRounding.AwayFromZero), (int)Math.Round(k.Y, MidpointRounding.AwayFromZero))); //То добавляем точку пересечения в список
                            }
                        }
                    }
                }
                bufList.Add(eds2[i].a); //Добаляем оставшиеся точки
                bufList.Add(eds2[i].b); //Добаляем оставшиеся точки                
                Func.fsortx(bufList); //Отсортируем по возрастанию Х
                for (int j = 0; j < bufList.Count() - 1; j++)
                {
                    tar.Add(new Edge(bufList[j], bufList[j + 1])); //Добавим грани в список
                }
            }
            if (tar.Count() != 0)
            {
                return new FTMObjectEdge(tar, color); //Создание объекта результата ТМО
            }
            return null;
        }
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
    }
}
