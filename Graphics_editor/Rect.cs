using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics_editor
{
    class Rect
    {
        public Rectangle rct;
        public int id;

        public Rect()
        {

        }

        public Rect(Rectangle rct, int id)
        {
            this.rct = rct;
            this.id = id;
        }
    }
}
