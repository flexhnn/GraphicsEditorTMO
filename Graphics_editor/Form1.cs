using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics_editor
{
    public partial class Form1 : Form
    {
        private Bitmap bmp; //Картинка picturebox
        private Graphics g; //Полотно       

        private List<FObject> flist; //Список примитивов
        private List<int> idlist; //Список свободных id
        private List<FObject> techList; //Список побочных примитивов        
        private Point[] buf; //Буфер точек
        private Point old; //Переменная для перемещения объектов

        private Color nowColor; //Текущий цвет
        private Pen drawPen; //Объект для побочного рисования
        private Pen dashPen; //Пунктирное рисование

        private int nowSelect; //Текущий объект(фигура)
        private int countPoints; //Количество точек
        private int nCount; //Количество вершин звезды

        private bool rotateFlag; //Флаг поворота
        private bool rotateFlag2; //Флаг поворота
        private bool rotateFlag9;
        private bool otrFlag; //Флаг отражения
        private bool firstPoint; //Флаг первого нажатия
        private bool isSelectedDragged; //Флаг нажатия на выделенную фигуру
        private bool isCtrlPressed; //Флаг нажатия на Control

        private FMenu fmenu; //Объект контекстного меню

        private List<MyFPanelItem> bufItems; //Буферный список для отражения

        private List<Rect> selected; //Список прямоугольников для выделенных фигур

        private RamGecTools.MouseHook hook; //Хук на мышь
        private RamGecTools.KeyboardHook khookd; //Хук на клавиатуру      

        public int bufAngle; //Угол поворота
        private FMenuItem ipr; //Пункт меню: поворот на заданный угол              

        private FMenuItem tmoItem1; //Пункт меню пересечение
        private FMenuItem tmoItem2; //Пункт меню симметричная разность

        private Point rotatePoint;
        private PointF upp;

        public Form1()
        {
            InitializeComponent();

            this.bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.pictureBox1.Image = bmp;
            this.g = Graphics.FromImage(bmp);

            this.flist = new List<FObject>();
            this.idlist = new List<int>();
            this.techList = new List<FObject>();

            this.nowColor = Color.Black;
            this.drawPen = new Pen(Color.Black, 1);
            this.dashPen = new Pen(Color.Black, 1);
            this.dashPen.DashStyle = DashStyle.Dash;

            this.nowSelect = 0;
            this.countPoints = 0;
            this.nCount = 5;

            this.rotateFlag = false;
            this.rotateFlag2 = false;
            this.otrFlag = false;
            this.firstPoint = true;
            this.isSelectedDragged = false;
            this.isCtrlPressed = false;

            this.fmenu = new FMenu();

            this.bufItems = new List<MyFPanelItem>();

            this.selected = new List<Rect>();

            this.hook = new RamGecTools.MouseHook();
            this.hook.LeftButtonDown += Hook_LeftButtonDown;
            this.khookd = new RamGecTools.KeyboardHook();
            this.khookd.KeyDown += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyDown);
            this.khookd.KeyUp += new RamGecTools.KeyboardHook.KeyboardHookCallback(keyboardHook_KeyUp);
            this.khookd.Install();
            this.hook.Install();

            FMenuItem delItem = new FMenuItem("Удалить", fmenu); //Удаление
            delItem.onClick += this.delItemClick;
            this.fmenu.add(delItem);

            this.ipr = new FMenuItem("Поворот", fmenu);
            this.ipr.onClick += this.itemPovClickRand;
            this.fmenu.add(ipr);

            FMenuItem itemPov = new FMenuItem("Поворот +90", fmenu); //Поворот 90
            itemPov.onClick += this.itemPovClickf90;
            this.fmenu.add(itemPov);

            FMenuItem itemPov1 = new FMenuItem("Поворот -90", fmenu); //Поворот -90
            itemPov1.onClick += this.itemPovClickb90;
            this.fmenu.add(itemPov1);

            FMenuItem itemOtr = new FMenuItem("Отразить", fmenu); //Отразить
            itemOtr.onClick += this.itemOtrClick;
            this.fmenu.add(itemOtr);

            this.tmoItem1 = new FMenuItem("Пересечение", fmenu); //Пересечение
            this.tmoItem1.onClick += this.tmoItem1_click;
            this.tmoItem1.Enabled = false;
            this.fmenu.add(tmoItem1);

            this.tmoItem2 = new FMenuItem("Разность", fmenu); //Разность
            this.tmoItem2.onClick += this.tmoItem2_click;
            this.tmoItem2.Enabled = false;
            this.fmenu.add(tmoItem2);

            this.myFPanel.onClick += this.mfPanelClick;
            this.drawPanel.MaximumSize = new Size(drawPanel.Width, drawPanel.Height);

            this.pictureBox1.MouseWheel += new MouseEventHandler(this.mouseWheel);

            FMatrix.fa = FMatrix.getFactArray(100);

            this.rotatePoint = new Point();
            this.upp = new PointF();
        }

        private void tmoItem2_click(object data, MouseEventArgs e)
        {
            FObject buf0 = myFPanel.nowSelected[0].getData();
            FObject buf1 = myFPanel.nowSelected[1].getData();
            
            if (buf0.getFType() == 1 || buf0.getFType() == 2 || buf1.getFType() == 1 || buf1.getFType() == 2)
            {
                return;
            }
            
            FObject newFO = TMOperations.tmo2f(buf0, buf1, nowColor);
            if (newFO == null) return;
            
            delFObject(buf0.getId());
            delFObject(buf1.getId());
            unSelectAll();
            flist.Add(newFO);
            
            string text;
            if (idlist.Count() != 0)
            {
                text = (idlist[0]) + ": ТМО"; //Создаем строку для отображения в списке
                newFO.setId(idlist[0]);
                idlist.RemoveAt(0);
            }
            else
            {
                text = (flist.Count() - 1) + ": ТМО"; //Создаем строку для отображения в списке
                newFO.setId(flist.Count() - 1);
            }
            
            myFPanel.addMyItem(text, newFO); //Добавляем элемент списка
            pictureBox1.Invalidate();
        }

        private void tmoItem1_click(object data, MouseEventArgs e)
        {
            FObject buf0 = myFPanel.nowSelected[0].getData();
            FObject buf1 = myFPanel.nowSelected[1].getData();
            
            if (buf0.getFType() == 1 || buf0.getFType() == 2 || buf1.getFType() == 1 || buf1.getFType() == 2)
            {
                return;
            }
            
            FObject newFO = TMOperations.tmo1(buf0, buf1, nowColor);
            if (newFO == null) return;
            
            delFObject(buf0.getId());
            delFObject(buf1.getId());
            unSelectAll();
            flist.Add(newFO);
            
            string text;
            if (idlist.Count() != 0)
            {
                text = (idlist[0]) + ": ТМО"; //Создаем строку для отображения в списке
                newFO.setId(idlist[0]);
                idlist.RemoveAt(0);
            }
            else
            {
                text = (flist.Count() - 1) + ": ТМО"; //Создаем строку для отображения в списке
                newFO.setId(flist.Count() - 1);
            }
            
            myFPanel.addMyItem(text, newFO); //Добавляем элемент списка
            pictureBox1.Invalidate();
        }

        private void itemOtrClick(object data, MouseEventArgs e)
        {
            this.otrFlag = true;
            for (int i = 0; i < myFPanel.nowSelected.Count; i++)
            {
                bufItems.Add(myFPanel.nowSelected[i]);
            }
            unSelectAll();
        }

        private void mouseWheel(object sender, MouseEventArgs e) //Масштабирование по оси Х
        {
            if (isCtrlPressed)
            {
                if (myFPanel.nowSelected.Count() != 0)
                {
                    FObject fo = myFPanel.nowSelected[0].getData();
                    if (e.Delta > 0)
                    {
                        for (int i = 0; i < myFPanel.nowSelected.Count(); i++)
                        {
                            myFPanel.nowSelected[i].getData().scale(myFPanel.nowSelected[i].getData().getCenter(), 1.05f, 1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < myFPanel.nowSelected.Count(); i++)
                        {
                            myFPanel.nowSelected[i].getData().scale(myFPanel.nowSelected[i].getData().getCenter(), 0.95f, 1);
                        }
                    }
                    selectAll(myFPanel.nowSelected);
                }
            }
        }

        public void backrot() //Отмена вращения на заданный угол
        {
            this.rotateFlag = false;
            bufItems.Clear();
            unSelectAll();
        }

        private void mfPanelClick(MyFPanelItem sender, MouseEventArgs e) //Клик по элементу панели MyFPanel
        {
            if (countPoints != 0)
            {
                countPoints = 0;
                buf = new Point[100];
                firstPoint = true;
                pictureBox1.Invalidate();
            }

            if (!isCtrlPressed)
            {
                if (myFPanel.nowSelected.Count() == 0) //Если ничего не выделено
                {
                    if (e.Button == MouseButtons.Left) select(sender); //И выделяем
                    else if (e.Button == MouseButtons.Right)
                    {
                        select(sender); //И выделяем
                        Point screenPosition = sender.PointToScreen(new Point(e.X, e.Y));
                        if (myFPanel.nowSelected.Count() > 1)
                        {
                            tmoItem1.Enabled = true;
                            tmoItem2.Enabled = true;
                        }
                        fmenu.open(screenPosition, myFPanel.nowSelected);
                    }

                }
                else //Если же что то выделено
                {
                    if (myFPanel.nowSelected.Count() > 1)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            unSelectAll();
                            select(sender);
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            Point screenPosition = sender.PointToScreen(new Point(e.X, e.Y));
                            fmenu.open(screenPosition, myFPanel.nowSelected);
                        }
                    }
                    else
                    {
                        if (sender.isSelected) //Если выделен объект по которому нажали
                        {
                            if (e.Button == MouseButtons.Left) unSelectAll();
                            else if (e.Button == MouseButtons.Right)
                            {
                                Point screenPosition = sender.PointToScreen(new Point(e.X, e.Y));
                                fmenu.open(screenPosition, myFPanel.nowSelected);
                            }
                        }
                        else //Если выделен другой объект
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                unSelectAll(); //Снимаем выделение и загружаем старую картинку
                                select(sender); //И выделяем нужный
                            }
                            else if (e.Button == MouseButtons.Right)
                            {
                                unSelectAll(); //Снимаем старое выделение
                                select(sender); //И выделяем нужный
                                Point screenPosition = sender.PointToScreen(new Point(e.X, e.Y));
                                fmenu.open(screenPosition, myFPanel.nowSelected);
                            }

                        }
                    }

                }
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (sender.isSelected) unSelect(sender);
                    else select(sender);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (myFPanel.nowSelected.Count() != 0)
                    {
                        Point screenPosition = sender.PointToScreen(new Point(e.X, e.Y));
                        fmenu.open(screenPosition, myFPanel.nowSelected);
                    }
                }

            }
        }

        private void unSelect(MyFPanelItem item) //Снять выделение
        {
            for (int i = 0; i < selected.Count(); i++)
            {
                if (item.getId() == selected[i].id)
                {
                    selected.RemoveAt(i);
                    break;
                }
            }
            myFPanel.deSelectAtId(item.getId());
            for (int i = 0; i < myFPanel.nowSelected.Count; i++)
            {
                if (myFPanel.nowSelected[i].getId() == item.getId())
                {
                    myFPanel.nowSelected.RemoveAt(i);
                    break;
                }
            }
            if (myFPanel.nowSelected.Count() < 2)
            {
                tmoItem1.Enabled = false;
                tmoItem2.Enabled = false;
            }
            else
            {
                tmoItem1.Enabled = true;
                tmoItem2.Enabled = true;
            }
            pictureBox1.Invalidate();
            Cursor = Cursors.Default;
        }

        private void unSelectAll() //Снять выделение
        {
            selected.Clear();
            tmoItem1.Enabled = false;
            tmoItem2.Enabled = false;
            myFPanel.deSelectAll();
            pictureBox1.Invalidate();
            Cursor = Cursors.Default;
            tmoItem1.Enabled = false;
            tmoItem2.Enabled = false;
        }

        private void select(MyFPanelItem item) //Веделение объекта
        {
            if (!item.isSelected)
            {
                item.select();
                myFPanel.nowSelected.Add(item);
            }

            if (myFPanel.nowSelected.Count() > 1)
            {
                tmoItem1.Enabled = true;
                tmoItem2.Enabled = true;
            }

            FObject data = item.getData();
            int id = data.getId();

            for (int i = 0; i < flist.Count(); i++) //Процедура выноса фигуры на передний план
            {
                if (flist[i].getId() == id)
                {
                    flist.RemoveAt(i);
                    flist.Add(data);
                    break;
                }
            }
            for (int i = 0; i < selected.Count(); i++)
            {
                if (selected[i].id == item.getId()) selected.RemoveAt(i);
            }

            Point[] mm = data.getMinMax();
            Rectangle rct = new Rectangle(mm[0].X - 25, mm[0].Y - 25, mm[1].X - mm[0].X + 50, mm[1].Y - mm[0].Y + 50); //Создаем необходимые данные
            this.selected.Add(new Rect(rct, item.getId()));
            pictureBox1.Invalidate(); //Дооптимизировать!!
            pictureBox1.Focus();
        }

        private void selectAll(List<MyFPanelItem> list)
        {
            if (list.Count() > 0)
            {
                selected.Clear();
                for (int i = 0; i < list.Count(); i++)
                {
                    if (!list[i].isSelected)
                    {
                        list[i].select();
                        myFPanel.nowSelected.Add(list[i]);
                    }

                    Point[] mm = list[i].getData().getMinMax();
                    Rectangle rct = new Rectangle(mm[0].X - 25, mm[0].Y - 25, mm[1].X - mm[0].X + 50, mm[1].Y - mm[0].Y + 50); //Создаем необходимые данные
                    this.selected.Add(new Rect(rct, list[i].getId()));
                }
                pictureBox1.Invalidate(); //Дооптимизировать!!
                pictureBox1.Focus();
                if (myFPanel.nowSelected.Count() > 1)
                {
                    tmoItem1.Enabled = true;
                    tmoItem2.Enabled = true;
                }
            }
        }

        private void delItemClick(object data, MouseEventArgs e) //Отлдака//кнопка удаления в контекстном меню
        {
            List<MyFPanelItem> list = (List<MyFPanelItem>)data;
            if (list.Count() == 1)
            {
                delFObject(list[0].getId());
            }
            else
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    delFObject1(list[i].getId()); //Вызываем метод удаления фигуры и передаем туда номер фигуры
                }
                pictureBox1.Invalidate(); //Перерисовываем картинку уже без удаленной фигур
                unSelectAll();
            }

        }

        private void itemPovClickf90(object data, MouseEventArgs e) //Поворот по часовой стрелки на 90 градусов
        {
            rotateFlag9 = true;
            for (int i = 0; i < myFPanel.nowSelected.Count; i++)
            {
                bufItems.Add(myFPanel.nowSelected[i]);
            }
            bufAngle = 90;
            unSelectAll();
        }

        private void itemPovClickb90(object data, MouseEventArgs e) //Поворот против часовой стрелки на 90 градусов
        {
            rotateFlag9 = true;
            for (int i = 0; i < myFPanel.nowSelected.Count; i++)
            {
                bufItems.Add(myFPanel.nowSelected[i]);
            }
            bufAngle = -90;
            unSelectAll();
        }

        private void itemPovClickRand(object data, MouseEventArgs e) //Поворот на заданный угол
        {
            rotateFlag = true;
            for (int i = 0; i < myFPanel.nowSelected.Count; i++)
            {
                bufItems.Add(myFPanel.nowSelected[i]);
            }
            unSelectAll();
        }

        private void delFObject(int id) //Удалить фигуру
        {
            for (int i = 0; i < flist.Count(); i++)
            {
                if (flist[i].getId() == id)
                {
                    if (id < flist.Count() - 1)
                    {
                        idlist.Add(id);
                        idlist.Sort();
                    }
                    flist.RemoveAt(i);
                    break;
                }
            }
            myFPanel.delItem(id); //Удаляем фигуру из визуального списка справа и сдвигаем его        
            pictureBox1.Invalidate(); //Перерисовываем картинку уже без удаленной фигур
            unSelectAll();
        }

        private void delFObject1(int id) //Удалить фигуру
        {
            for (int i = 0; i < flist.Count(); i++)
            {
                if (flist[i].getId() == id)
                {
                    if (id < flist.Count() - 1)
                    {
                        idlist.Add(id);
                        idlist.Sort();
                    }
                    flist.RemoveAt(i);
                    break;
                }
            }
            myFPanel.delItem(id); //Удаляем фигуру из визуального списка справа и сдвигаем его        
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) //Обработка ввода точек
        {
            if (myFPanel.nowSelected.Count() == 1) //Если что-то выделено
            {
                FObject fo = myFPanel.nowSelected[0].getData(); //Получаем выделенный объект
                Point[] mm = fo.getMinMax();
                if (e.X > mm[0].X - 25 && e.X < mm[1].X + 25 && e.Y > mm[0].Y - 25 && e.Y < mm[1].Y + 25) //Если нажатие внутри выделения
                {
                    isSelectedDragged = true; //То устанавливаем флаг зажатия
                    old.X = e.X; //Точка для перемещения фигур
                    old.Y = e.Y; //Точка для перемещения фигур
                }
                else //Если нажатие за пределами выделения 
                {
                    unSelectAll(); //То снимаем выделение
                    analyzeDown(e); //Обрабатываем ввод
                }
            }
            else if (myFPanel.nowSelected.Count() > 1)
            {
                bool flag = false;
                for (int i = 0; i < myFPanel.nowSelected.Count(); i++)
                {
                    Point[] mm = myFPanel.nowSelected[i].getData().getMinMax();
                    if (e.X > mm[0].X - 25 && e.X < mm[1].X + 25 && e.Y > mm[0].Y - 25 && e.Y < mm[1].Y + 25) //Если нажатие внутри выделения
                    {
                        isSelectedDragged = true; //То устанавливаем флаг зажатия
                        old.X = e.X; //Точка для перемещения фигур
                        old.Y = e.Y; //Точка для перемещения фигур
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    unSelectAll();
                    analyzeDown(e);
                }
            }
            else
            {
                analyzeDown(e); //Если ничего не выделено, то обрабатываем ввод
            }

        }

        private void analyzeDown(MouseEventArgs e) //Анализ и обработка нажатия на панель рисования
        {
            if (rotateFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    FtechEllipse el = new FtechEllipse(new Point(e.X, e.Y), Color.Red);
                    techList.Add(el);
                    pictureBox1.Invalidate(new Rectangle(e.X - 2, e.Y - 2, 6, 6));
                    pictureBox1.Update();
                    rotateFlag2 = true;
                    rotatePoint.X = e.X;
                    rotatePoint.Y = e.Y;
                }
            }
            else if (rotateFlag9)
            {
                if (e.Button == MouseButtons.Left)
                {
                    FtechEllipse el = new FtechEllipse(new Point(e.X, e.Y), Color.Red);
                    techList.Add(el);
                    pictureBox1.Invalidate(new Rectangle(e.X - 2, e.Y - 2, 6, 6));
                    pictureBox1.Update();
                    rotatePoint.X = e.X;
                    rotatePoint.Y = e.Y;
                    techList.Clear();
                }
            }
            else if (otrFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    FtechLine fl = new FtechLine(new Point[] { new Point(0, e.Y), new Point(pictureBox1.Width, e.Y) }, Color.DarkCyan);
                    techList.Add(fl);
                    Point[] mm = fl.getMinMax();
                    pictureBox1.Invalidate(new Rectangle(mm[0].X - 1, mm[0].Y - 1, mm[1].X + 1, mm[1].Y + 1));
                    pictureBox1.Update();
                    techList.Clear();
                }
            }
            else
            {
                if (nowSelect == 0 || (e.Button == MouseButtons.Right && nowSelect != 2))
                {
                    Point k = new Point(e.X, e.Y);
                    for (int i = flist.Count() - 1; i > -1; i--)
                    {
                        if (Geom2_0.isInside(flist[i], k))
                        {
                            List<MyFPanelItem> li = myFPanel.getItems();
                            int id = flist[i].getId();
                            for (int j = 0; j < li.Count(); j++)
                            {
                                if (li[j].getId() == id)
                                {
                                    select(li[j]);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                else if (nowSelect == 1) //Если выбрана прямая
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ++countPoints; //Инкрементировать счетчик точек
                        if (firstPoint) //Если это первая точка
                        {
                            buf = new Point[2]; //Создать массив
                            buf[0].X = e.X; //Занести в массив первую точку
                            buf[0].Y = e.Y; //Занести в массив первую точку
                        }
                        else //Если вторая точка
                        {
                            if (isCtrlPressed)
                            {
                                if (Func.abs(e.X - buf[0].X) > Func.abs(e.Y - buf[0].Y))
                                {
                                    buf[1].X = e.X; //Занести в массив первую точку
                                    buf[1].Y = buf[0].Y; //Занести в массив первую точку
                                }
                                else
                                {
                                    buf[1].X = buf[0].X; //Занести в массив первую точку
                                    buf[1].Y = e.Y; //Занести в массив первую точку
                                }

                            }
                            else
                            {
                                buf[1].X = e.X;
                                buf[1].Y = e.Y; //То занести вторую точку в массив
                            }
                        }
                    }
                }
                else if (nowSelect == 2) //Если выбрана Безье
                {
                    if (e.Button != MouseButtons.Right) //Если нажата НЕ правая кнопка мыши
                    {
                        if (countPoints < 99)
                        {
                            if (firstPoint) //Если первая точка
                            {
                                buf = new Point[100]; //Выделяем место под точки
                                firstPoint = false; //Снимаем флаг первой точки
                            }
                            buf[countPoints].X = e.X; //Заносим точку в массив
                            buf[countPoints].Y = e.Y; //Заносим точку в массив
                            FtechEllipse el = new FtechEllipse(new Point(e.X, e.Y), nowColor);
                            techList.Add(el);
                            pictureBox1.Invalidate(new Rectangle(e.X - 2, e.Y - 2, 6, 6)); //Обновляем
                            pictureBox1.Update();
                            techList.Clear();
                            ++countPoints; //Инкрементируем счетчик
                        }
                    }
                    else //Если была нажата правая кнопка мыши
                    {
                        if (countPoints > 1) //Если имеется больше одной точки
                        {
                            FBezier br = new FBezier(buf, nowColor, countPoints); //То создаём фигуру 
                            flist.Add(br);
                            pictureBox1.Invalidate(); //Перерисовка с добавленной фигурой
                            string text;
                            if (idlist.Count() != 0)
                            {
                                text = (idlist[0]) + ": Кривая"; //Создаем строку для отображения в списке
                                br.setId(idlist[0]);
                                idlist.RemoveAt(0);
                            }
                            else
                            {
                                text = (flist.Count() - 1) + ": Кривая"; //Создаем строку для отображения в списке
                                br.setId(flist.Count() - 1);
                            }
                            myFPanel.addMyItem(text, br); //Добавляем элемент списка
                            countPoints = 0; //Обнуляем счетчик
                            firstPoint = true; //Устанавливаем флаг первой точки
                        }
                        else //Если имеем одну или меньше точек
                        {
                            firstPoint = true; //Устанавливаем флаг первой точки
                            countPoints = 0; //Обнуляем счетчик
                            pictureBox1.Invalidate();
                            return; //Завершаем функцию
                        }

                    }
                }
                else if (nowSelect == 3)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ++countPoints;
                        if (firstPoint) //Если это первая точка
                        {
                            FtechEllipse el = new FtechEllipse(new Point(e.X, e.Y), nowColor);
                            techList.Add(el);
                            pictureBox1.Invalidate(new Rectangle(e.X - 2, e.Y - 2, 6, 6)); //Обновляем
                            pictureBox1.Update();
                            techList.Clear();
                            buf = new Point[3]; //Создать массив
                            buf[0].X = e.X; //Занести в массив первую точку
                            buf[0].Y = e.Y; //Занести в массив первую точку
                        }
                        else
                        {
                            Ftech ft = new Ftech(new Point(e.X, e.Y), nowColor);
                            techList.Add(ft);
                            pictureBox1.Invalidate(new Rectangle(e.X, e.Y, 1, 1)); //Обновляем
                            pictureBox1.Update();
                            techList.Clear();
                            buf[countPoints - 1].X = e.X;
                            buf[countPoints - 1].Y = e.Y;
                        }
                    }
                }
                else if (nowSelect == 4)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (firstPoint)
                        {
                            buf = new Point[3];
                            buf[0].X = e.X; //Занести в массив первую точку
                            buf[0].Y = e.Y; //Занести в массив первую точку
                            countPoints = 1;
                            firstPoint = false;
                        }
                        else if (countPoints != 2)
                        {
                            countPoints = 2;
                            if (isCtrlPressed)
                            {
                                if (Func.abs(e.X - buf[0].X) > Func.abs(e.Y - buf[0].Y))
                                {
                                    buf[1].X = e.X; //Занести в массив первую точку
                                    buf[1].Y = buf[0].Y; //Занести в массив первую точку
                                }
                                else
                                {
                                    buf[1].X = buf[0].X; //Занести в массив первую точку
                                    buf[1].Y = e.Y; //Занести в массив первую точку
                                }

                            }
                            else
                            {
                                buf[1].X = e.X; //Занести в массив первую точку
                                buf[1].Y = e.Y; //Занести в массив первую точку
                            }
                        }
                        else
                        {
                            countPoints = 3;
                            buf[2].X = e.X; //Занести в массив первую точку
                            buf[2].Y = e.Y; //Занести в массив первую точку
                            Ftech ft = new Ftech(new Point(e.X, e.Y), nowColor);
                            techList.Add(ft);
                            pictureBox1.Invalidate(new Rectangle(e.X, e.Y, 1, 1));
                            pictureBox1.Update();
                            techList.Clear();
                        }
                    }
                }
            }
        }

        private void analyzeUp(MouseEventArgs e) //Анализ и обработка отпускания кнопки мыши на панели рисования
        {
            if (rotateFlag2)
            {
                if (e.Button == MouseButtons.Left)
                {                   
                    rotateFlag = false;
                    rotateFlag2 = false;
                    selectAll(bufItems);
                    select(bufItems[0]);
                    bufItems.Clear();
                    countPoints = 0;
                    techList.Clear();
                    pictureBox1.Invalidate();
                }
            }
            else if (rotateFlag9)
            {
                if (e.Button == MouseButtons.Left)
                {
                    rotateFlag9 = false;
                    for (int i = 0; i < bufItems.Count(); i++)
                    {
                        bufItems[i].getData().rotate(rotatePoint, bufAngle);
                    }
                    selectAll(bufItems);
                    select(bufItems[0]);
                    bufItems.Clear();
                    countPoints = 0;
                    techList.Clear();
                    pictureBox1.Invalidate();
                }
            }
            else if (otrFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < bufItems.Count; i++) bufItems[i].getData().scale(new Point(0, e.Y), 1, -1);
                    selectAll(bufItems);
                    bufItems.Clear();
                    otrFlag = false;
                    buf = new Point[100];
                    firstPoint = true;
                }
            }
            else if (nowSelect == 1) //Если выбрана прямая
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (!firstPoint) //Если не первая точка
                    {
                        FLine fl = new FLine(buf, nowColor); //Создаем объект прямой
                        flist.Add(fl);
                        pictureBox1.Invalidate();
                        string text;
                        if (idlist.Count() != 0)
                        {
                            text = (idlist[0]) + ": Прямая"; //Создаем строку для отображения в списке
                            fl.setId(idlist[0]);
                            idlist.RemoveAt(0);
                        }
                        else
                        {
                            text = (flist.Count() - 1) + ": Прямая"; //Создаем строку для отображения в списке
                            fl.setId(flist.Count() - 1);
                        }
                        myFPanel.addMyItem(text, fl); //Добавляем элемент списка
                        countPoints = 0; //Обнуляем счетчик
                        firstPoint = true; //Устанавливаем флаг первой точки                   
                    }
                    else firstPoint = false; //Если же первая точка, то снимаем флаг первой точки
                }
            }
            else if (nowSelect == 3)
            {
                if (e.Button == MouseButtons.Left)
                {
                    firstPoint = false;
                    if (countPoints == 3)
                    {
                        FStar fs = new FStar(buf, nCount, nowColor);
                        flist.Add(fs);
                        pictureBox1.Invalidate();
                        string text;
                        if (idlist.Count() != 0)
                        {
                            text = (idlist[0]) + ": Звезда"; //Создаем строку для отображения в списке
                            fs.setId(idlist[0]);
                            idlist.RemoveAt(0);
                        }
                        else
                        {
                            text = (flist.Count() - 1) + ": Звезда"; //Создаем строку для отображения в списке
                            fs.setId(flist.Count() - 1);
                        }
                        myFPanel.addMyItem(text, fs);
                        countPoints = 0;
                        firstPoint = true;
                    }
                }
            }
            else if (nowSelect == 4)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (countPoints == 3)
                    {
                        FTrg ft = new FTrg(buf, nowColor);
                        flist.Add(ft);
                        pictureBox1.Invalidate();
                        string text;
                        if (idlist.Count() != 0)
                        {
                            text = (idlist[0]) + ": Треугольник"; //Создаем строку для отображения в списке
                            ft.setId(idlist[0]);
                            idlist.RemoveAt(0);
                        }
                        else
                        {
                            text = (flist.Count() - 1) + ": Треугольник"; //Создаем строку для отображения в списке
                            ft.setId(flist.Count() - 1);
                        }
                        myFPanel.addMyItem(text, ft); //Добавляем элемент списка
                        countPoints = 0; //Обнуляем счетчик
                        firstPoint = true; //Устанавливаем флаг первой точки           
                    }
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) //Обработка ввода точек
        {
            if (myFPanel.nowSelected.Count() == 1) //Если что-то выделено
            {
                isSelectedDragged = false; //Снимаем флаг удержания  
                if (e.Button == MouseButtons.Right && myFPanel.nowSelected.Count == 1)
                {
                    FObject fo = myFPanel.nowSelected[0].getData();
                    Point[] mm = fo.getMinMax();
                    if (e.X > mm[0].X - 25 && e.X < mm[1].X + 25 && e.Y > mm[0].Y - 25 && e.Y < mm[1].Y + 25)
                    {
                        Point screenPosition = pictureBox1.PointToScreen(new Point(e.X, e.Y));
                        fmenu.open(screenPosition, myFPanel.nowSelected);
                    }
                }
            }
            else if (myFPanel.nowSelected.Count() > 1)
            {
                isSelectedDragged = false; //Снимаем флаг удержания  
                for (int i = 0; i < selected.Count(); i++)
                {
                    Point[] mm = myFPanel.nowSelected[i].getData().getMinMax();
                    if (e.X > mm[0].X - 25 && e.X < mm[1].X + 25 && e.Y > mm[0].Y - 25 && e.Y < mm[1].Y + 25)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            Point screenPosition = pictureBox1.PointToScreen(new Point(e.X, e.Y));
                            fmenu.open(screenPosition, myFPanel.nowSelected);
                            break;
                        }
                    }
                }
            }
            else analyzeUp(e); //То обрабатываем ввод
        }


        private void lbFline_Click(object sender, EventArgs e) //Выбор/снятие выбора отрезка прямой
        {
            if (nowSelect != 1)
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                nowSelect = 1;
                countPoints = 0;
                firstPoint = true;
                this.lbFline.Image = Resource1.FlineD;
                this.lbFbezier.Image = Resource1.Fbezier;
                this.lbFstar.Image = Resource1.Fstar;
                this.lbFtrg.Image = Resource1.Ftrg;
                this.nudStarf.Visible = false;
                this.lbStarInfo.Visible = false;
            }
            else
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                this.lbFline.Image = Resource1.Fline;
                nowSelect = 0;
            }

        }

        private void lbFbezier_Click(object sender, EventArgs e) //Выбор/снятие выбора кривой Безье
        {
            if (nowSelect != 2)
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                nowSelect = 2;
                countPoints = 0;
                firstPoint = true;
                this.lbFbezier.Image = Resource1.FbezierD;
                this.lbFline.Image = Resource1.Fline;
                this.lbFstar.Image = Resource1.Fstar;
                this.lbFtrg.Image = Resource1.Ftrg;
                this.nudStarf.Visible = false;
                this.lbStarInfo.Visible = false;
            }
            else
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                this.lbFbezier.Image = Resource1.Fbezier;
                nowSelect = 0;
            }

        }

        private void lbFstar_Click(object sender, EventArgs e) //Выбор/снятие выбора звезды
        {
            if (nowSelect != 3)
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                nowSelect = 3;
                countPoints = 0;
                firstPoint = true;
                this.lbFstar.Image = Resource1.FstarD;
                this.lbFline.Image = Resource1.Fline;
                this.lbFbezier.Image = Resource1.Fbezier;
                this.lbFtrg.Image = Resource1.Ftrg;
                this.nudStarf.Visible = true;
                this.lbStarInfo.Visible = true;
            }
            else
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                this.lbFstar.Image = Resource1.Fstar;
                nowSelect = 0;
                this.nudStarf.Visible = false;
                this.lbStarInfo.Visible = false;
            }
        }

        private void lbFtrg_Click(object sender, EventArgs e) //Выбор/снятие выбора треугольника
        {
            if (nowSelect != 4)
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                nowSelect = 4;
                countPoints = 0;
                firstPoint = true;
                this.lbFtrg.Image = Resource1.FtrgD;
                this.lbFline.Image = Resource1.Fline;
                this.lbFbezier.Image = Resource1.Fbezier;
                this.lbFstar.Image = Resource1.Fstar;
                this.nudStarf.Visible = false;
                this.lbStarInfo.Visible = false;
            }
            else
            {
                if (countPoints != 0) pictureBox1.Invalidate();
                this.lbFtrg.Image = Resource1.Ftrg;
                nowSelect = 0;
            }
        }

        private void lbClear_Click(object sender, EventArgs e) //Очистка
        {
            myFPanel.clear();
            flist.Clear();
            idlist.Clear();
            selected.Clear();
            techList.Clear();
            myFPanel.nowSelected.Clear();
            buf = new Point[100];

            countPoints = 0;
            firstPoint = true;

            panelFile.Enabled = false;
            panelFile.Visible = false;

            panelMain.Enabled = true;
            panelMain.Visible = true;

            pictureBox1.Enabled = true;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) //Движение мыши. Сдвиг и 'Масштабирование
        {
            lbCursorX.Text = "" + e.X;
            lbCursorY.Text = "" + e.Y;
            if (rotateFlag2)
            {
                float ux, uy;
                if (e.Y < rotatePoint.Y && e.X > rotatePoint.X) //Третья четверть
                {
                    ux = e.X - rotatePoint.X;
                    uy = rotatePoint.Y - e.Y;
                }
                else if (e.Y < rotatePoint.Y && e.X < rotatePoint.X) //Вторая четверть
                {
                    ux = e.X - rotatePoint.X;
                    uy = e.Y - rotatePoint.Y;
                }
                else if (e.Y > rotatePoint.Y && e.X > rotatePoint.X) //Четвертая четверть
                {
                    ux = rotatePoint.X - e.X;
                    uy = rotatePoint.Y - e.Y;
                }
                else //Первая четверть
                {
                    ux = rotatePoint.X - e.X;
                    uy = e.Y - rotatePoint.Y;
                }
                float dx = ux - upp.X;
                float dy = uy - upp.Y;
                if (uy > rotatePoint.Y)
                {
                    dx *= -1;
                }
                if (ux < rotatePoint.X)
                {
                    dy *= -1;
                }
                float flx = dx + dy;
                if (flx <= 30 && flx >= -30)
                {
                    for (int i = 0; i < bufItems.Count(); i++)
                    {
                        bufItems[i].getData().rotate(rotatePoint, flx * 0.35f);
                    }
                }
                upp.X = ux;
                upp.Y = uy;
                pictureBox1.Invalidate();
            }           
            else if (nowSelect == 1 && !firstPoint)
            {
                FtechLine fl;
                if (isCtrlPressed)
                {
                    if (Func.abs(e.X - buf[0].X) > Func.abs(e.Y - buf[0].Y))
                    {
                        fl = new FtechLine(new Point[] { buf[0], new Point(e.X, buf[0].Y) }, nowColor);
                    }
                    else
                    {
                        fl = new FtechLine(new Point[] { buf[0], new Point(buf[0].X, e.Y) }, nowColor);
                    }
                }
                else fl = new FtechLine(new Point[] { buf[0], new Point(e.X, e.Y) }, nowColor);
                techList.Add(fl);
                pictureBox1.Invalidate();
                pictureBox1.Update();
                techList.Clear();
            }
            else if (nowSelect == 4 && countPoints == 1)
            {
                FtechLine fl;
                if (isCtrlPressed)
                {
                    if (Func.abs(e.X - buf[0].X) > Func.abs(e.Y - buf[0].Y))
                    {
                        fl = new FtechLine(new Point[] { buf[0], new Point(e.X, buf[0].Y) }, nowColor);
                    }
                    else
                    {
                        fl = new FtechLine(new Point[] { buf[0], new Point(buf[0].X, e.Y) }, nowColor);
                    }
                }
                else fl = new FtechLine(new Point[] { buf[0], new Point(e.X, e.Y) }, nowColor);
                techList.Add(fl);
                pictureBox1.Invalidate();
                pictureBox1.Update();
                techList.Clear();
            }            
            else if (myFPanel.nowSelected.Count() == 1)
            {
                FObject fo = myFPanel.nowSelected[0].getData();
                Point[] mm = fo.getMinMax();
                if (e.X > mm[0].X - 25 && e.X < mm[1].X + 25 && e.Y > mm[0].Y - 25 && e.Y < mm[1].Y + 25)
                {
                    Cursor = Cursors.SizeAll;
                }
                else Cursor = Cursors.Default;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (isSelectedDragged)
                {
                    for (int i = 0; i < myFPanel.nowSelected.Count(); i++)
                    {
                        int dx = (e.X - old.X);
                        int dy = (e.Y - old.Y);
                        myFPanel.nowSelected[i].getData().move(dx, dy);
                    }
                    old.X = e.X;
                    old.Y = e.Y;
                    selectAll(myFPanel.nowSelected);
                }
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) //Перерисовка (обновление) панели рисования
        {
            for (int i = 0; i < flist.Count(); i++) flist[i].draw(e.Graphics);
            for (int i = 0; i < selected.Count(); i++) e.Graphics.DrawRectangle(dashPen, selected[i].rct);
            for (int i = 0; i < techList.Count(); i++) techList[i].draw(e.Graphics);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (myFPanel.nowSelected != null)
            {
                Cursor = Cursors.Default;
            }
        }

        private void keyboardHook_KeyDown(RamGecTools.KeyboardHook.VKeys key)
        {
            if (key.Equals(RamGecTools.KeyboardHook.VKeys.LCONTROL))
            {
                isCtrlPressed = true;
            }
        }

        private void keyboardHook_KeyUp(RamGecTools.KeyboardHook.VKeys key)
        {
            if (key.Equals(RamGecTools.KeyboardHook.VKeys.LCONTROL))
            {
                isCtrlPressed = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point[] pts = new Point[] { new Point(200, 60), new Point(200, 180), new Point(330, 120) };
            FTrg tr = new FTrg(pts, Color.Black);
            flist.Add(tr);
            string text;
            if (idlist.Count() != 0)
            {
                text = (idlist[0]) + ": Треугольник"; //Создаем строку для отображения в списке
                tr.setId(idlist[0]);
                idlist.RemoveAt(0);
            }
            else
            {
                text = (flist.Count() - 1) + ": Треугольник"; //Создаем строку для отображения в списке
                tr.setId(flist.Count() - 1);
            }
            myFPanel.addMyItem(text, tr);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Point[] pts = new Point[] { new Point(200, 60), new Point(200, 180), new Point(330, 120) };
            FTrg tr = new FTrg(pts, Color.Red);
            flist.Add(tr);
            string text;
            if (idlist.Count() != 0)
            {
                text = (idlist[0]) + ": Треугольник"; //Создаем строку для отображения в списке
                tr.setId(idlist[0]);
                idlist.RemoveAt(0);
            }
            else
            {
                text = (flist.Count() - 1) + ": Треугольник"; //Создаем строку для отображения в списке
                tr.setId(flist.Count() - 1);
            }
            myFPanel.addMyItem(text, tr);
            pictureBox1.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Point[] pts = new Point[] { new Point(200, 216), new Point(200, 157), new Point(200, 43) };
            Point[] pts2 = new Point[] { new Point(200, 216), new Point(200, 168), new Point(200, 86) };
            FStar fs1 = new FStar(pts, 5, Color.Black);
            FStar fs2 = new FStar(pts2, 5, Color.Black);
            FObject fte = TMOperations.tmo2f(fs1, fs2, Color.Black);
            flist.Add(fte);
            string text;
            if (idlist.Count() != 0)
            {
                text = (idlist[0]) + ": ТМО"; //Создаем строку для отображения в списке
                fte.setId(idlist[0]);
                idlist.RemoveAt(0);
            }
            else
            {
                text = (flist.Count() - 1) + ": ТМО"; //Создаем строку для отображения в списке
                fte.setId(flist.Count() - 1);
            }
            myFPanel.addMyItem(text, fte);
            pictureBox1.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings.debug1 = !Settings.debug1;
            button4.Text = "" + Settings.debug1;
        }

        private void lbSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить изображение как...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    pictureBox1.Image = bitmap;
                    g = Graphics.FromImage(bitmap);
                    for (int i = 0; i < flist.Count(); i++) flist[i].draw(g);
                    pictureBox1.Invalidate();
                    bitmap.Save(savedialog.FileName);
                    pictureBox1.Image = bmp;
                    panelFile.Visible = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureBox1.Image = bmp;
                }
            }
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            button4.Visible = !button4.Visible;
            button3.Visible = !button3.Visible;
            button2.Visible = !button2.Visible;
            button1.Visible = !button1.Visible;            
        }
    }
}
