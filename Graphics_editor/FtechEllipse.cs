using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class FtechEllipse : FObject
    {
        public Point pt;

        public const int type = 9; //Код примитива

        public Pen pen;

        public FtechEllipse(Point pt, Color color)
        {
            this.pt = new Point(pt.X, pt.Y);
            this.pen = new Pen(color, 1);
        }

        public int getFType()
        {
            return type;
        }

        public void draw(Graphics e)
        {
            e.DrawEllipse(pen, pt.X - 2, pt.Y - 2, 5, 5);
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
