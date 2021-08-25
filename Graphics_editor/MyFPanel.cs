using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_editor
{
    class MyFPanel : Panel
    {
        private List<MyFPanelItem> mlist; 

        public int itW; //Ширина содержащихся элементов
        public int itH; //Высота содержащихся элементов
        public int itGapX; //Расстояние от левого края панели до элемента
        public int itGapY; //Расстояние от верхнего края панели до элемента
                            //За одно вертикальное расстояние между элементами
        public Point nowXY; //Откуда начинаем рисовать новый элемент

        public List<MyFPanelItem> nowSelected;

        public delegate void myFuncs(MyFPanelItem sender, MouseEventArgs e);
        public event myFuncs onClick;

        public MyFPanel()
        {
            this.mlist = new List<MyFPanelItem>();
            this.itW = 110; 
            this.itH = 23;        
            this.itGapX = 13;
            this.itGapY = 13;
            this.nowXY = new Point(itGapX, itGapY);
            this.nowSelected = new List<MyFPanelItem>();
        }

        public int getCount()
        {
            return mlist.Count();
        }

        public void addMyItem(string text, FObject data) //Добавление элемента и данных
        {
            MyFPanelItem item = new MyFPanelItem(text, data); //Автоматическое создание дочернего элемента
            item.Location = nowXY; //Задаем локейшен дочернего элемента
            item.Size = new Size(itW, itH); //Задаем его размеры
            nowXY.Y = nowXY.Y + itH + itGapY; //Считаем локейшин следующего элемента
            mlist.Add(item);
            this.Controls.Add(item); //Добавляем элемент в MyFPanel
            item.MouseUp += new MouseEventHandler(this.itemClick);
        }

        private void itemClick(object sender, MouseEventArgs e) //Клик по дочернему элементу
        {
            onClick((MyFPanelItem)sender, e);
        }

        public MyFPanelItem getMyItem(int i) //Получение дочернего элемента из списка по индексу
        {
            return mlist[i];
        }

        public FObject getMyItemObject(int i) //Получение данных из дочернего элемента из списка по индексу
        {
            return mlist[i].getData();
        }

        public List<MyFPanelItem> getItems() //Получение всего списка
        {
            return this.mlist;
        }

        public void clear() //Очистить MyFPanel
        {
            Controls.Clear();
            mlist.Clear();
            nowXY.X = itGapX;
            nowXY.Y = itGapY;
        } 

        public void delItem(int ind) //Удалить какой-либо элемент по id
        {
            for (int i = 0; i < mlist.Count(); i++)
            {
                if (mlist[i].getId() == ind)
                {
                    mlist.RemoveAt(i);
                    this.Controls.RemoveAt(i);
                    refact();
                    break;
                }
            }
             
        }

        private void refact() //Обновить/перерисовать MyFPanel
        {
            this.nowXY.X = itGapX;
            this.nowXY.Y = itGapY;
            for (int i = 0; i < mlist.Count(); i++)
            {
                mlist[i].Location = nowXY;
                nowXY.Y = nowXY.Y + itH + itGapY;
            }
        }

        public void selectAtId(int id)
        {
            for (int i = 0; i < mlist.Count(); i++)
            {
                if (mlist[i].getId() == id)
                {
                    mlist[i].select();
                    nowSelected.Add(mlist[i]);
                    break;
                }
            }
        }

        public void deSelectAll()
        {
            for (int i = 0; i < nowSelected.Count(); i++)
            {
                nowSelected[i].deSelect();
            }
            nowSelected.Clear();
        }

        public void deSelectAtId(int id)
        {
            for (int i = 0; i < nowSelected.Count(); i++)
            {
                if (nowSelected[i].getId() == id)
                {
                    nowSelected[i].deSelect();
                    nowSelected.RemoveAt(i);
                    break;
                }
            }
        }

        public void deSelectAtIndex(int index)
        {
            nowSelected[index].deSelect();
            nowSelected.RemoveAt(index);
        }

    }
}
