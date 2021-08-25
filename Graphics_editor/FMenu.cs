using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_editor
{
    class FMenu : Form
    {
        private List<FMenuItem> flist;
        public int itW; //Ширина содержащихся элементов
        public int itH; //Высота содержащихся элементов
        public int itGapX; //Расстояние от левого края панели до элемента
        public int itGapY; //Расстояние от верхнего края панели до элемента
                            //За одно вертикальное расстояние между элементами
        public Point nowXY; //Откуда начинаем рисовать новый элемент

        public List<MyFPanelItem> nowObjs; //MyFPanelItems

        public FMenu()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(150, 200);
            this.Visible = false;
            this.BackColor = Color.Gainsboro;
            this.Opacity = 0.90;
            this.ShowInTaskbar = false;
            this.LostFocus += new System.EventHandler(this.FMenu_LostFocus);
            this.flist = new List<FMenuItem>();
            this.itW = 140;
            this.itH = 15;
            this.itGapX = 5;
            this.itGapY = 3;
            this.nowXY = new Point(itGapX, itGapY);
        }

        public void open(Point loc, List<MyFPanelItem> obj) //Открыть контекстное меню !Нужно дописать!
        {           
            this.Visible = true; //Порядок очень важен!!!!!!!
            this.Location = loc; //Порядок очень важен!!!!!!!
            this.BringToFront();
            this.nowObjs = obj;
        }

        public void hide() //Скрыть контекстное меню
        {
            this.Visible = false;
        }

        private void FMenu_LostFocus(object sender, EventArgs e) //Потеря фокуса
        {
            hide();
        }

        public List<FMenuItem> getItems() //Получение всего списка
        {
            return this.flist;
        }

        public FMenuItem getMyItem(int i) //Получение дочернего элемента из списка по индексу
        {
            return flist[i];
        }

        public void add(FMenuItem fmi) //Добавление нового элемента
        {
            fmi.Location = nowXY;
            fmi.Size = new Size(itW, itH);
            nowXY.Y = nowXY.Y + itH + itGapY;
            flist.Add(fmi);
            this.Controls.Add(fmi);
        }

    }
}
