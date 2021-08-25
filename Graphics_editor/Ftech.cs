using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class Ftech : FObject
    {
        public Point pt;

        public const int type = 7; //Код примитива

        public Brush pen;

        public Ftech(Point pt, Color color)
        {
            this.pt = new Point(pt.X, pt.Y);
            this.pen = new SolidBrush(color);
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            e.FillRectangle(pen, pt.X, pt.Y, 1, 1);
        }

        public Point[] getMinMax()
        {
            throw new NotImplementedException();
        }

        public Point[] getPoints()
        {
            throw new NotImplementedException();
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
