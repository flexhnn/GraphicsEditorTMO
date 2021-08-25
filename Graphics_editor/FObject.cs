using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    interface FObject
    {
        int getFType();
        void draw(Graphics e);
        void move(float dx, float dy);
        void rotate(Point p, float angle);
        void scale(Point p, float sx, float sy);
        Point[] getMinMax();
        Point[] getPoints();
        Point getCenter();
        void setColor(Color color);
        void setId(int id);
        int getId();
    }
}
