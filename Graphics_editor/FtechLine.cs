using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FtechLine : FObject
    {
        private Point[] arr; //Координаты вершин треугольника

        public Pen pen;
        public Color color;

        public const int type = 8; //Код примитива

        public FtechLine(Point[] arr, Color color)
        {
            this.arr = arr;
            this.pen = new Pen(color, 1);
            this.color = color;
        }

        public int getFType()
        {
            return type;
        }

        public Point[] getMinMax()
        {
            Point[] mm = new Point[2];
            mm[0] = new Point(arr[0].X, arr[0].Y);
            mm[1] = new Point(arr[1].X, arr[1].Y);
            if (arr[0].X > arr[1].X)
            {
                mm[0].X = arr[1].X;
                mm[1].X = arr[0].X;
            }
            if (arr[0].Y > arr[1].Y)
            {
                mm[0].Y = arr[1].Y;
                mm[1].Y = arr[0].Y;
            }
            return mm;           
        }

        public Point[] getPoints()
        {
            return arr;
        }

        public void draw(Graphics e)
        {
            e.DrawLines(pen, arr);
        }

        public void setColor(Color color)
        {
            throw new NotImplementedException();
        }

        public Point getCenter()
        {
            throw new NotImplementedException();
        }
        
        public void setId(int id)
        {
            throw new NotImplementedException();
        }

        public int getId()
        {
            throw new NotImplementedException();
        }

        public void move(float dx, float dy)
        {
            throw new NotImplementedException();
        }

        public void rotate(Point p, float angle)
        {
            throw new NotImplementedException();
        }

        public void scale(Point p, float sx, float sy)
        {
            throw new NotImplementedException();
        }
    }
}
