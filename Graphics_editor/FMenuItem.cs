using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_editor
{
    class FMenuItem : Label
    {
        public Color mainColor;     //Основной цвет
        public Color secondColor;   //Цвет выделения
        public FMenu fm;             //Ссылка на все меню

        public delegate void myFuncs(List<MyFPanelItem> data, MouseEventArgs e); //Делегат
        public event myFuncs onClick;                      //Событие

        public FMenuItem(FMenu fm)
        {
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.mainColor = Color.Snow;
            this.BackColor = mainColor;
            this.secondColor = Color.DeepSkyBlue;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);
            this.MouseClick += new MouseEventHandler(this.mouseClick);
            this.fm = fm;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        public FMenuItem(string text, FMenu fm)
        {
            this.Text = text;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.mainColor = Color.Snow;
            this.BackColor = mainColor;
            this.secondColor = Color.DeepSkyBlue;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);
            this.MouseClick += new MouseEventHandler(this.mouseClick);
            this.fm = fm;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void mouseEnter(object sender, EventArgs e)
        {
            this.BackColor = secondColor;
        }

        private void mouseLeave(object sender, EventArgs e)
        {
            this.BackColor = mainColor;
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                onClick(fm.nowObjs, e);
                fm.hide();
            }           
        }
    }
}
