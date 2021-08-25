using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_editor
{
    class MyFPanelItem : Label
    {
        private FObject data; //Данные
        public Color mainColor; //Основной цвет
        public Color secondColor; //Цвет выделения
        private int id; //id
        public bool isSelected;                 

        public MyFPanelItem(string text, FObject data)
        {
            this.Text = text;
            this.data = data;
            this.id = data.getId();
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.mainColor = Color.Gainsboro;
            this.BackColor = mainColor;
            this.secondColor = Color.Cyan;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);
            this.isSelected = false;
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        public FObject getData() //Получить данные
        {
            return this.data;
        }

        private void mouseEnter(object sender, EventArgs e) //Курсор в пределах (выделение кнопки)
        {
            if (!isSelected)this.BackColor = secondColor;           
        }

        private void mouseLeave(object sender, EventArgs e) //Курсор вышел за пределы (снимаем выделение кнопки)
        {
            if (!isSelected)this.BackColor = mainColor;
        }

        public void setId(int id) //Установить id
        {
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }

        public void deSelect()
        {
            isSelected = false;
            BackColor = mainColor;
        }

        public void select()
        {
            isSelected = true;
            BackColor = secondColor;
        }
    }
}
