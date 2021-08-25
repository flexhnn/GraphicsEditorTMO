using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Graphics_editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        bool isTopPanelDragged = false;            //Изменение размера и позиции окна
        bool isLeftPanelDragged = false;           //Изменение размера и позиции окна
        bool isRightPanelDragged = false;          //Изменение размера и позиции окна
        bool isBottomPanelDragged = false;         //Изменение размера и позиции окна
        bool isTopBorderPanelDragged = false;      //Изменение размера и позиции окна
        bool isWindowMaximized = false;            //Изменение размера и позиции окна
        Point offset;                              //Изменение размера и позиции окна
        Size _normalWindowSize;                    //Изменение размера и позиции окна
        Point _normalWindowLocation = Point.Empty; //Изменение размера и позиции окна

        bool isRightDrawPanelDragged = false; //Изменение размера панели рисования
        bool isBottomDrawPanelDragged = false; //Изменение размера панели рисования

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelFile = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbSave = new System.Windows.Forms.Label();
            this.lbClear = new System.Windows.Forms.Label();
            this.lbFile2 = new System.Windows.Forms.Label();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.drawBottomPanel = new System.Windows.Forms.Panel();
            this.drawLeftPanel = new System.Windows.Forms.Panel();
            this.drawTopPanel = new System.Windows.Forms.Panel();
            this.drawRightPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.predTopPanel = new System.Windows.Forms.Panel();
            this.lbHelp = new System.Windows.Forms.Label();
            this.lbMain = new System.Windows.Forms.Label();
            this.lbFile = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbFtrg = new System.Windows.Forms.Label();
            this.nudStarf = new System.Windows.Forms.NumericUpDown();
            this.lbStarInfo = new System.Windows.Forms.Label();
            this.lbFstar = new System.Windows.Forms.Label();
            this.lbColor20 = new System.Windows.Forms.Label();
            this.lbColor19 = new System.Windows.Forms.Label();
            this.lbColor18 = new System.Windows.Forms.Label();
            this.lbColor17 = new System.Windows.Forms.Label();
            this.lbColor16 = new System.Windows.Forms.Label();
            this.lbColor15 = new System.Windows.Forms.Label();
            this.lbColor14 = new System.Windows.Forms.Label();
            this.lbColor13 = new System.Windows.Forms.Label();
            this.lbColor12 = new System.Windows.Forms.Label();
            this.lbColor11 = new System.Windows.Forms.Label();
            this.lbColor10 = new System.Windows.Forms.Label();
            this.lbColor9 = new System.Windows.Forms.Label();
            this.lbColor8 = new System.Windows.Forms.Label();
            this.lbColor7 = new System.Windows.Forms.Label();
            this.lbColor6 = new System.Windows.Forms.Label();
            this.lbColor5 = new System.Windows.Forms.Label();
            this.lbColor4 = new System.Windows.Forms.Label();
            this.lbColor3 = new System.Windows.Forms.Label();
            this.lbColor2 = new System.Windows.Forms.Label();
            this.lbColor1 = new System.Windows.Forms.Label();
            this.lbColorInfo = new System.Windows.Forms.Label();
            this.lbNowColor = new System.Windows.Forms.Label();
            this.lbFbezier = new System.Windows.Forms.Label();
            this.lbFline = new System.Windows.Forms.Label();
            //this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            //this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            //this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            //this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lbCursorXi = new System.Windows.Forms.Label();
            this.panelHelp = new System.Windows.Forms.Panel();
            this.topBorderPanel = new System.Windows.Forms.Panel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbExit = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.lbCursorX = new System.Windows.Forms.Label();
            this.lbCursorYi = new System.Windows.Forms.Label();
            this.lbCursorY = new System.Windows.Forms.Label();
            this.lbHelpMain = new System.Windows.Forms.Label();
            this.myFPanel = new Graphics_editor.MyFPanel();
            this.panelFile.SuspendLayout();
            this.drawPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.predTopPanel.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStarf)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelFile
            // 
            this.panelFile.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelFile.Controls.Add(this.label2);
            this.panelFile.Controls.Add(this.lbSave);
            this.panelFile.Controls.Add(this.lbClear);
            this.panelFile.Controls.Add(this.lbFile2);
            this.panelFile.Enabled = false;
            this.panelFile.Location = new System.Drawing.Point(1, 32);
            this.panelFile.Name = "panelFile";
            this.panelFile.Size = new System.Drawing.Size(250, 300);
            this.panelFile.TabIndex = 5;
            this.panelFile.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(220, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 30);
            this.label2.TabIndex = 3;
            this.label2.DoubleClick += new System.EventHandler(this.label2_DoubleClick);
            // 
            // lbSave
            // 
            this.lbSave.BackColor = System.Drawing.Color.LavenderBlush;
            this.lbSave.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSave.Location = new System.Drawing.Point(0, 26);
            this.lbSave.Name = "lbSave";
            this.lbSave.Size = new System.Drawing.Size(128, 26);
            this.lbSave.TabIndex = 2;
            this.lbSave.Text = "Сохранить как";
            this.lbSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSave.Click += new System.EventHandler(this.lbSave_Click);
            // 
            // lbClear
            // 
            this.lbClear.BackColor = System.Drawing.Color.Snow;
            this.lbClear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbClear.Location = new System.Drawing.Point(60, 0);
            this.lbClear.Name = "lbClear";
            this.lbClear.Size = new System.Drawing.Size(68, 26);
            this.lbClear.TabIndex = 1;
            this.lbClear.Text = "Очистить";
            this.lbClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbClear.Click += new System.EventHandler(this.lbClear_Click);
            // 
            // lbFile2
            // 
            this.lbFile2.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.lbFile2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbFile2.ForeColor = System.Drawing.Color.White;
            this.lbFile2.Location = new System.Drawing.Point(0, 0);
            this.lbFile2.Name = "lbFile2";
            this.lbFile2.Size = new System.Drawing.Size(60, 26);
            this.lbFile2.TabIndex = 0;
            this.lbFile2.Text = "Файл";
            this.lbFile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbFile2.Click += new System.EventHandler(this.lbFile2_Click);
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.drawPanel.Controls.Add(this.drawBottomPanel);
            this.drawPanel.Controls.Add(this.drawLeftPanel);
            this.drawPanel.Controls.Add(this.drawTopPanel);
            this.drawPanel.Controls.Add(this.drawRightPanel);
            this.drawPanel.Controls.Add(this.pictureBox1);
            this.drawPanel.Location = new System.Drawing.Point(10, 170);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(700, 450);
            this.drawPanel.TabIndex = 8;
            // 
            // drawBottomPanel
            // 
            this.drawBottomPanel.BackColor = System.Drawing.Color.Silver;
            this.drawBottomPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.drawBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.drawBottomPanel.Location = new System.Drawing.Point(1, 449);
            this.drawBottomPanel.Name = "drawBottomPanel";
            this.drawBottomPanel.Size = new System.Drawing.Size(698, 1);
            this.drawBottomPanel.TabIndex = 4;
            this.drawBottomPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBottomPanel_MouseDown);
            this.drawBottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBottomPanel_MouseMove);
            this.drawBottomPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBottomPanel_MouseUp);
            // 
            // drawLeftPanel
            // 
            this.drawLeftPanel.BackColor = System.Drawing.Color.Silver;
            this.drawLeftPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.drawLeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.drawLeftPanel.Location = new System.Drawing.Point(0, 1);
            this.drawLeftPanel.Name = "drawLeftPanel";
            this.drawLeftPanel.Size = new System.Drawing.Size(1, 449);
            this.drawLeftPanel.TabIndex = 3;
            // 
            // drawTopPanel
            // 
            this.drawTopPanel.BackColor = System.Drawing.Color.Silver;
            this.drawTopPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.drawTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.drawTopPanel.Location = new System.Drawing.Point(0, 0);
            this.drawTopPanel.Name = "drawTopPanel";
            this.drawTopPanel.Size = new System.Drawing.Size(699, 1);
            this.drawTopPanel.TabIndex = 2;
            // 
            // drawRightPanel
            // 
            this.drawRightPanel.BackColor = System.Drawing.Color.Silver;
            this.drawRightPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.drawRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.drawRightPanel.Location = new System.Drawing.Point(699, 0);
            this.drawRightPanel.Name = "drawRightPanel";
            this.drawRightPanel.Size = new System.Drawing.Size(1, 450);
            this.drawRightPanel.TabIndex = 1;
            this.drawRightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawRightPanel_MouseDown);
            this.drawRightPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawRightPanel_MouseMove);
            this.drawRightPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawRightPanel_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(700, 450);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // predTopPanel
            // 
            this.predTopPanel.Controls.Add(this.lbHelp);
            this.predTopPanel.Controls.Add(this.lbMain);
            this.predTopPanel.Controls.Add(this.lbFile);
            this.predTopPanel.Location = new System.Drawing.Point(1, 32);
            this.predTopPanel.Name = "predTopPanel";
            this.predTopPanel.Size = new System.Drawing.Size(1090, 26);
            this.predTopPanel.TabIndex = 1;
            // 
            // lbHelp
            // 
            this.lbHelp.BackColor = System.Drawing.Color.White;
            this.lbHelp.Enabled = false;
            this.lbHelp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHelp.Location = new System.Drawing.Point(124, 0);
            this.lbHelp.Name = "lbHelp";
            this.lbHelp.Size = new System.Drawing.Size(90, 26);
            this.lbHelp.TabIndex = 2;
            this.lbHelp.Text = "Поддержка";
            this.lbHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHelp.Click += new System.EventHandler(this.lbHelp_Click);
            // 
            // lbMain
            // 
            this.lbMain.BackColor = System.Drawing.Color.GhostWhite;
            this.lbMain.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMain.Location = new System.Drawing.Point(62, 0);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(60, 26);
            this.lbMain.TabIndex = 1;
            this.lbMain.Text = "Главная";
            this.lbMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbMain.Click += new System.EventHandler(this.lbMain_Click);
            // 
            // lbFile
            // 
            this.lbFile.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lbFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFile.ForeColor = System.Drawing.Color.White;
            this.lbFile.Location = new System.Drawing.Point(0, 0);
            this.lbFile.Name = "lbFile";
            this.lbFile.Size = new System.Drawing.Size(60, 26);
            this.lbFile.TabIndex = 0;
            this.lbFile.Text = "Файл";
            this.lbFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbFile.Click += new System.EventHandler(this.lbFile_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.Color.GhostWhite;
            this.panelMain.Controls.Add(this.button4);
            this.panelMain.Controls.Add(this.button3);
            this.panelMain.Controls.Add(this.button2);
            this.panelMain.Controls.Add(this.button1);
            this.panelMain.Controls.Add(this.lbFtrg);
            this.panelMain.Controls.Add(this.nudStarf);
            this.panelMain.Controls.Add(this.lbStarInfo);
            this.panelMain.Controls.Add(this.lbFstar);
            this.panelMain.Controls.Add(this.lbColor20);
            this.panelMain.Controls.Add(this.lbColor19);
            this.panelMain.Controls.Add(this.lbColor18);
            this.panelMain.Controls.Add(this.lbColor17);
            this.panelMain.Controls.Add(this.lbColor16);
            this.panelMain.Controls.Add(this.lbColor15);
            this.panelMain.Controls.Add(this.lbColor14);
            this.panelMain.Controls.Add(this.lbColor13);
            this.panelMain.Controls.Add(this.lbColor12);
            this.panelMain.Controls.Add(this.lbColor11);
            this.panelMain.Controls.Add(this.lbColor10);
            this.panelMain.Controls.Add(this.lbColor9);
            this.panelMain.Controls.Add(this.lbColor8);
            this.panelMain.Controls.Add(this.lbColor7);
            this.panelMain.Controls.Add(this.lbColor6);
            this.panelMain.Controls.Add(this.lbColor5);
            this.panelMain.Controls.Add(this.lbColor4);
            this.panelMain.Controls.Add(this.lbColor3);
            this.panelMain.Controls.Add(this.lbColor2);
            this.panelMain.Controls.Add(this.lbColor1);
            this.panelMain.Controls.Add(this.lbColorInfo);
            this.panelMain.Controls.Add(this.lbNowColor);
            this.panelMain.Controls.Add(this.lbFbezier);
            this.panelMain.Controls.Add(this.lbFline);
            //this.panelMain.Controls.Add(this.shapeContainer1);
            this.panelMain.Location = new System.Drawing.Point(1, 57);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(898, 100);
            this.panelMain.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(172, 68);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 24);
            this.button4.TabIndex = 33;
            this.button4.Text = "false";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(78, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 24);
            this.button3.TabIndex = 32;
            this.button3.Text = "copy/p";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(47, 68);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 24);
            this.button2.TabIndex = 31;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 68);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 24);
            this.button1.TabIndex = 30;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbFtrg
            // 
            this.lbFtrg.BackColor = System.Drawing.Color.LightSlateGray;
            this.lbFtrg.Image = global::Graphics_editor.Resource1.Ftrg;
            this.lbFtrg.Location = new System.Drawing.Point(328, 35);
            this.lbFtrg.Name = "lbFtrg";
            this.lbFtrg.Size = new System.Drawing.Size(21, 21);
            this.lbFtrg.TabIndex = 29;
            this.lbFtrg.Click += new System.EventHandler(this.lbFtrg_Click);
            this.lbFtrg.MouseEnter += new System.EventHandler(this.lbFtrg_MouseEnter);
            this.lbFtrg.MouseLeave += new System.EventHandler(this.lbFtrg_MouseLeave);
            // 
            // nudStarf
            // 
            this.nudStarf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudStarf.Location = new System.Drawing.Point(233, 9);
            this.nudStarf.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudStarf.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudStarf.Name = "nudStarf";
            this.nudStarf.Size = new System.Drawing.Size(64, 20);
            this.nudStarf.TabIndex = 28;
            this.nudStarf.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudStarf.Visible = false;
            this.nudStarf.ValueChanged += new System.EventHandler(this.nudStarf_ValueChanged);
            this.nudStarf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudStarf_KeyDown);
            this.nudStarf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudStarf_KeyUp);
            // 
            // lbStarInfo
            // 
            this.lbStarInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbStarInfo.Location = new System.Drawing.Point(127, 10);
            this.lbStarInfo.Name = "lbStarInfo";
            this.lbStarInfo.Size = new System.Drawing.Size(100, 19);
            this.lbStarInfo.TabIndex = 27;
            this.lbStarInfo.Text = "Вершин звезды:";
            this.lbStarInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbStarInfo.Visible = false;
            // 
            // lbFstar
            // 
            this.lbFstar.BackColor = System.Drawing.Color.LightSlateGray;
            this.lbFstar.Image = global::Graphics_editor.Resource1.Fstar;
            this.lbFstar.Location = new System.Drawing.Point(381, 8);
            this.lbFstar.Name = "lbFstar";
            this.lbFstar.Size = new System.Drawing.Size(21, 21);
            this.lbFstar.TabIndex = 25;
            this.lbFstar.Click += new System.EventHandler(this.lbFstar_Click);
            this.lbFstar.MouseEnter += new System.EventHandler(this.lbFstar_MouseEnter);
            this.lbFstar.MouseLeave += new System.EventHandler(this.lbFstar_MouseLeave);
            // 
            // lbColor20
            // 
            this.lbColor20.BackColor = System.Drawing.Color.Thistle;
            this.lbColor20.Location = new System.Drawing.Point(790, 29);
            this.lbColor20.Name = "lbColor20";
            this.lbColor20.Size = new System.Drawing.Size(15, 15);
            this.lbColor20.TabIndex = 24;
            this.lbColor20.Click += new System.EventHandler(this.lbColor20_Click);
            // 
            // lbColor19
            // 
            this.lbColor19.BackColor = System.Drawing.Color.SteelBlue;
            this.lbColor19.Location = new System.Drawing.Point(769, 29);
            this.lbColor19.Name = "lbColor19";
            this.lbColor19.Size = new System.Drawing.Size(15, 15);
            this.lbColor19.TabIndex = 23;
            this.lbColor19.Click += new System.EventHandler(this.lbColor19_Click);
            // 
            // lbColor18
            // 
            this.lbColor18.BackColor = System.Drawing.Color.PowderBlue;
            this.lbColor18.Location = new System.Drawing.Point(748, 29);
            this.lbColor18.Name = "lbColor18";
            this.lbColor18.Size = new System.Drawing.Size(15, 15);
            this.lbColor18.TabIndex = 22;
            this.lbColor18.Click += new System.EventHandler(this.lbColor18_Click);
            // 
            // lbColor17
            // 
            this.lbColor17.BackColor = System.Drawing.Color.LawnGreen;
            this.lbColor17.Location = new System.Drawing.Point(727, 29);
            this.lbColor17.Name = "lbColor17";
            this.lbColor17.Size = new System.Drawing.Size(15, 15);
            this.lbColor17.TabIndex = 21;
            this.lbColor17.Click += new System.EventHandler(this.lbColor17_Click);
            // 
            // lbColor16
            // 
            this.lbColor16.BackColor = System.Drawing.Color.Beige;
            this.lbColor16.Location = new System.Drawing.Point(706, 29);
            this.lbColor16.Name = "lbColor16";
            this.lbColor16.Size = new System.Drawing.Size(15, 15);
            this.lbColor16.TabIndex = 20;
            this.lbColor16.Click += new System.EventHandler(this.lbColor16_Click);
            // 
            // lbColor15
            // 
            this.lbColor15.BackColor = System.Drawing.Color.Orange;
            this.lbColor15.Location = new System.Drawing.Point(685, 29);
            this.lbColor15.Name = "lbColor15";
            this.lbColor15.Size = new System.Drawing.Size(15, 15);
            this.lbColor15.TabIndex = 19;
            this.lbColor15.Click += new System.EventHandler(this.lbColor15_Click);
            // 
            // lbColor14
            // 
            this.lbColor14.BackColor = System.Drawing.Color.Pink;
            this.lbColor14.Location = new System.Drawing.Point(664, 29);
            this.lbColor14.Name = "lbColor14";
            this.lbColor14.Size = new System.Drawing.Size(15, 15);
            this.lbColor14.TabIndex = 18;
            this.lbColor14.Click += new System.EventHandler(this.lbColor14_Click);
            // 
            // lbColor13
            // 
            this.lbColor13.BackColor = System.Drawing.Color.RosyBrown;
            this.lbColor13.Location = new System.Drawing.Point(643, 29);
            this.lbColor13.Name = "lbColor13";
            this.lbColor13.Size = new System.Drawing.Size(15, 15);
            this.lbColor13.TabIndex = 17;
            this.lbColor13.Click += new System.EventHandler(this.lbColor13_Click);
            // 
            // lbColor12
            // 
            this.lbColor12.BackColor = System.Drawing.Color.Silver;
            this.lbColor12.Location = new System.Drawing.Point(622, 29);
            this.lbColor12.Name = "lbColor12";
            this.lbColor12.Size = new System.Drawing.Size(15, 15);
            this.lbColor12.TabIndex = 16;
            this.lbColor12.Click += new System.EventHandler(this.lbColor12_Click);
            // 
            // lbColor11
            // 
            this.lbColor11.BackColor = System.Drawing.Color.White;
            this.lbColor11.Location = new System.Drawing.Point(601, 29);
            this.lbColor11.Name = "lbColor11";
            this.lbColor11.Size = new System.Drawing.Size(15, 15);
            this.lbColor11.TabIndex = 15;
            this.lbColor11.Click += new System.EventHandler(this.lbColor11_Click);
            // 
            // lbColor10
            // 
            this.lbColor10.BackColor = System.Drawing.Color.DarkOrchid;
            this.lbColor10.Location = new System.Drawing.Point(790, 8);
            this.lbColor10.Name = "lbColor10";
            this.lbColor10.Size = new System.Drawing.Size(15, 15);
            this.lbColor10.TabIndex = 14;
            this.lbColor10.Click += new System.EventHandler(this.lbColor10_Click);
            // 
            // lbColor9
            // 
            this.lbColor9.BackColor = System.Drawing.Color.Blue;
            this.lbColor9.Location = new System.Drawing.Point(769, 8);
            this.lbColor9.Name = "lbColor9";
            this.lbColor9.Size = new System.Drawing.Size(15, 15);
            this.lbColor9.TabIndex = 13;
            this.lbColor9.Click += new System.EventHandler(this.lbColor9_Click);
            // 
            // lbColor8
            // 
            this.lbColor8.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lbColor8.Location = new System.Drawing.Point(748, 8);
            this.lbColor8.Name = "lbColor8";
            this.lbColor8.Size = new System.Drawing.Size(15, 15);
            this.lbColor8.TabIndex = 12;
            this.lbColor8.Click += new System.EventHandler(this.lbColor8_Click);
            // 
            // lbColor7
            // 
            this.lbColor7.BackColor = System.Drawing.Color.OliveDrab;
            this.lbColor7.Location = new System.Drawing.Point(727, 8);
            this.lbColor7.Name = "lbColor7";
            this.lbColor7.Size = new System.Drawing.Size(15, 15);
            this.lbColor7.TabIndex = 11;
            this.lbColor7.Click += new System.EventHandler(this.lbColor7_Click);
            // 
            // lbColor6
            // 
            this.lbColor6.BackColor = System.Drawing.Color.Yellow;
            this.lbColor6.Location = new System.Drawing.Point(706, 8);
            this.lbColor6.Name = "lbColor6";
            this.lbColor6.Size = new System.Drawing.Size(15, 15);
            this.lbColor6.TabIndex = 10;
            this.lbColor6.Click += new System.EventHandler(this.lbColor6_Click);
            // 
            // lbColor5
            // 
            this.lbColor5.BackColor = System.Drawing.Color.Red;
            this.lbColor5.Location = new System.Drawing.Point(685, 8);
            this.lbColor5.Name = "lbColor5";
            this.lbColor5.Size = new System.Drawing.Size(15, 15);
            this.lbColor5.TabIndex = 9;
            this.lbColor5.Click += new System.EventHandler(this.lbColor5_Click);
            // 
            // lbColor4
            // 
            this.lbColor4.BackColor = System.Drawing.Color.Salmon;
            this.lbColor4.Location = new System.Drawing.Point(664, 8);
            this.lbColor4.Name = "lbColor4";
            this.lbColor4.Size = new System.Drawing.Size(15, 15);
            this.lbColor4.TabIndex = 8;
            this.lbColor4.Click += new System.EventHandler(this.lbColor4_Click);
            // 
            // lbColor3
            // 
            this.lbColor3.BackColor = System.Drawing.Color.Brown;
            this.lbColor3.Location = new System.Drawing.Point(643, 8);
            this.lbColor3.Name = "lbColor3";
            this.lbColor3.Size = new System.Drawing.Size(15, 15);
            this.lbColor3.TabIndex = 7;
            this.lbColor3.Click += new System.EventHandler(this.lbColor3_Click);
            // 
            // lbColor2
            // 
            this.lbColor2.BackColor = System.Drawing.Color.Gray;
            this.lbColor2.Location = new System.Drawing.Point(622, 8);
            this.lbColor2.Name = "lbColor2";
            this.lbColor2.Size = new System.Drawing.Size(15, 15);
            this.lbColor2.TabIndex = 6;
            this.lbColor2.Click += new System.EventHandler(this.lbColor2_Click);
            // 
            // lbColor1
            // 
            this.lbColor1.BackColor = System.Drawing.Color.Black;
            this.lbColor1.Location = new System.Drawing.Point(601, 8);
            this.lbColor1.Name = "lbColor1";
            this.lbColor1.Size = new System.Drawing.Size(15, 15);
            this.lbColor1.TabIndex = 5;
            this.lbColor1.Click += new System.EventHandler(this.lbColor1_Click);
            // 
            // lbColorInfo
            // 
            this.lbColorInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbColorInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lbColorInfo.Location = new System.Drawing.Point(535, 34);
            this.lbColorInfo.Name = "lbColorInfo";
            this.lbColorInfo.Size = new System.Drawing.Size(34, 16);
            this.lbColorInfo.TabIndex = 4;
            this.lbColorInfo.Text = "Цвет";
            // 
            // lbNowColor
            // 
            this.lbNowColor.BackColor = System.Drawing.Color.Black;
            this.lbNowColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbNowColor.Location = new System.Drawing.Point(539, 8);
            this.lbNowColor.Name = "lbNowColor";
            this.lbNowColor.Size = new System.Drawing.Size(25, 25);
            this.lbNowColor.TabIndex = 3;
            // 
            // lbFbezier
            // 
            this.lbFbezier.BackColor = System.Drawing.Color.LightSlateGray;
            this.lbFbezier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbFbezier.Image = global::Graphics_editor.Resource1.Fbezier;
            this.lbFbezier.Location = new System.Drawing.Point(355, 8);
            this.lbFbezier.Name = "lbFbezier";
            this.lbFbezier.Size = new System.Drawing.Size(21, 21);
            this.lbFbezier.TabIndex = 2;
            this.lbFbezier.Click += new System.EventHandler(this.lbFbezier_Click);
            this.lbFbezier.MouseEnter += new System.EventHandler(this.lbFbezier_MouseEnter);
            this.lbFbezier.MouseLeave += new System.EventHandler(this.lbFbezier_MouseLeave);
            // 
            // lbFline
            // 
            this.lbFline.BackColor = System.Drawing.Color.LightSlateGray;
            this.lbFline.Image = global::Graphics_editor.Resource1.Fline;
            this.lbFline.Location = new System.Drawing.Point(328, 8);
            this.lbFline.Name = "lbFline";
            this.lbFline.Size = new System.Drawing.Size(21, 21);
            this.lbFline.TabIndex = 1;
            this.lbFline.Click += new System.EventHandler(this.lbFline_Click);
            this.lbFline.MouseEnter += new System.EventHandler(this.lbFline_MouseEnter);
            this.lbFline.MouseLeave += new System.EventHandler(this.lbFline_MouseLeave);
            // 
            // shapeContainer1
            // 
            //this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            //this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            //this.shapeContainer1.Name = "shapeContainer1";
            //this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            //this.lineShape3,
            //this.lineShape2,
            //this.lineShape1});
            //this.shapeContainer1.Size = new System.Drawing.Size(898, 100);
            //this.shapeContainer1.TabIndex = 0;
            //this.shapeContainer1.TabStop = false;
            // 
            // lineShape3
            // 
            //this.lineShape3.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            //this.lineShape3.Name = "lineShape3";
            //this.lineShape3.X1 = 415;
            //this.lineShape3.X2 = 415;
            //this.lineShape3.Y1 = 5;
            //this.lineShape3.Y2 = 90;
            // 
            // lineShape2
            // 
            //this.lineShape2.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            //this.lineShape2.Name = "lineShape2";
            //this.lineShape2.X1 = 522;
            //this.lineShape2.X2 = 522;
            //this.lineShape2.Y1 = 5;
            //this.lineShape2.Y2 = 90;
            // 
            // lineShape1
            // 
            //this.lineShape1.BorderColor = System.Drawing.SystemColors.AppWorkspace;
            //this.lineShape1.Name = "lineShape1";
            //this.lineShape1.X1 = 315;
            //this.lineShape1.X2 = 315;
            //this.lineShape1.Y1 = 5;
            //this.lineShape1.Y2 = 90;
            // 
            // lbCursorXi
            // 
            this.lbCursorXi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCursorXi.Location = new System.Drawing.Point(12, 623);
            this.lbCursorXi.Name = "lbCursorXi";
            this.lbCursorXi.Size = new System.Drawing.Size(17, 14);
            this.lbCursorXi.TabIndex = 30;
            this.lbCursorXi.Text = "X: ";
            this.lbCursorXi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelHelp
            // 
            this.panelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHelp.BackColor = System.Drawing.Color.GhostWhite;
            this.panelHelp.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelHelp.Enabled = false;
            this.panelHelp.Location = new System.Drawing.Point(1, 57);
            this.panelHelp.Name = "panelHelp";
            this.panelHelp.Size = new System.Drawing.Size(898, 100);
            this.panelHelp.TabIndex = 4;
            this.panelHelp.Visible = false;
            // 
            // topBorderPanel
            // 
            this.topBorderPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.topBorderPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.topBorderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBorderPanel.Location = new System.Drawing.Point(0, 0);
            this.topBorderPanel.Name = "topBorderPanel";
            this.topBorderPanel.Size = new System.Drawing.Size(900, 1);
            this.topBorderPanel.TabIndex = 10;
            this.topBorderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topBorderPanel_MouseDown);
            this.topBorderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topBorderPanel_MouseMove);
            this.topBorderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topBorderPanel_MouseUp);
            // 
            // topPanel
            // 
            this.topPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.lbExit);
            this.topPanel.Location = new System.Drawing.Point(1, 1);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(898, 31);
            this.topPanel.TabIndex = 11;
            this.topPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseDown_1);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseMove_1);
            this.topPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.topPanel_MouseUp_1);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe Print", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Graphics editor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbExit
            // 
            this.lbExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbExit.BackColor = System.Drawing.Color.Snow;
            this.lbExit.Location = new System.Drawing.Point(865, 0);
            this.lbExit.Name = "lbExit";
            this.lbExit.Size = new System.Drawing.Size(33, 31);
            this.lbExit.TabIndex = 0;
            this.lbExit.Text = "X";
            this.lbExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbExit.MouseEnter += new System.EventHandler(this.lbExit_MouseEnter);
            this.lbExit.MouseLeave += new System.EventHandler(this.lbExit_MouseLeave);
            this.lbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbExit_MouseUp);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.bottomPanel.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 639);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(900, 1);
            this.bottomPanel.TabIndex = 12;
            this.bottomPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bottomPanel_MouseDown);
            this.bottomPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.bottomPanel_MouseMove);
            this.bottomPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bottomPanel_MouseUp);
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.leftPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 1);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(1, 638);
            this.leftPanel.TabIndex = 13;
            this.leftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.leftPanel_MouseDown);
            this.leftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.leftPanel_MouseMove);
            this.leftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leftPanel_MouseUp);
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.rightPanel.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(899, 1);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(1, 638);
            this.rightPanel.TabIndex = 14;
            this.rightPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightPanel_MouseDown);
            this.rightPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rightPanel_MouseMove);
            this.rightPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightPanel_MouseUp);
            // 
            // lbCursorX
            // 
            this.lbCursorX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCursorX.Location = new System.Drawing.Point(29, 623);
            this.lbCursorX.Name = "lbCursorX";
            this.lbCursorX.Size = new System.Drawing.Size(41, 14);
            this.lbCursorX.TabIndex = 31;
            this.lbCursorX.Text = "0";
            this.lbCursorX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCursorYi
            // 
            this.lbCursorYi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCursorYi.Location = new System.Drawing.Point(76, 623);
            this.lbCursorYi.Name = "lbCursorYi";
            this.lbCursorYi.Size = new System.Drawing.Size(17, 14);
            this.lbCursorYi.TabIndex = 32;
            this.lbCursorYi.Text = "Y: ";
            this.lbCursorYi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCursorY
            // 
            this.lbCursorY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCursorY.Location = new System.Drawing.Point(93, 623);
            this.lbCursorY.Name = "lbCursorY";
            this.lbCursorY.Size = new System.Drawing.Size(41, 14);
            this.lbCursorY.TabIndex = 33;
            this.lbCursorY.Text = "0";
            this.lbCursorY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbHelpMain
            // 
            this.lbHelpMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbHelpMain.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbHelpMain.Location = new System.Drawing.Point(125, 623);
            this.lbHelpMain.Name = "lbHelpMain";
            this.lbHelpMain.Size = new System.Drawing.Size(416, 14);
            this.lbHelpMain.TabIndex = 35;
            this.lbHelpMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myFPanel
            // 
            this.myFPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myFPanel.AutoScroll = true;
            this.myFPanel.BackColor = System.Drawing.Color.DarkGray;
            this.myFPanel.Location = new System.Drawing.Point(715, 170);
            this.myFPanel.Name = "myFPanel";
            this.myFPanel.Size = new System.Drawing.Size(173, 450);
            this.myFPanel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.lbHelpMain);
            this.Controls.Add(this.lbCursorY);
            this.Controls.Add(this.lbCursorYi);
            this.Controls.Add(this.lbCursorX);
            this.Controls.Add(this.lbCursorXi);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.topBorderPanel);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelFile);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.predTopPanel);
            this.Controls.Add(this.panelHelp);
            this.Controls.Add(this.myFPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Opacity = 0.99D;
            this.Text = "Векторный графический редактор";
            this.panelFile.ResumeLayout(false);
            this.drawPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.predTopPanel.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStarf)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void lbMain_Click(object sender, EventArgs e) //Вкладка Главная
        {
            panelFile.Enabled = false;
            panelFile.Visible = false;
            panelHelp.Enabled = false;
            panelHelp.Visible = false;
            lbHelp.BackColor = Color.White;

            lbMain.BackColor = Color.GhostWhite;
            panelMain.Enabled = true;
            panelMain.Visible = true;
            panelMain.BringToFront();
        }

        private void lbHelp_Click(object sender, EventArgs e) //Вкладка Поддержка
        {
            panelFile.Enabled = false;
            panelFile.Visible = false;
            panelMain.Enabled = false;
            panelMain.Visible = false;
            lbMain.BackColor = Color.White;

            lbHelp.BackColor = Color.GhostWhite;
            panelHelp.Enabled = true;
            panelHelp.Visible = true;
            panelHelp.BringToFront();
        }

        private void lbFile_Click(object sender, EventArgs e) //Вкладка Файл (открывает псевдо-контекстное меню)
        {
            panelMain.Enabled = false;
            panelMain.Visible = true;
            panelHelp.Enabled = false;
            panelHelp.Visible = false;

            panelFile.Enabled = true;
            panelFile.Visible = true;

            pictureBox1.Enabled = false;

            panelFile.BringToFront();
        }

        private void lbFile2_Click(object sender, EventArgs e) //Кнопка Файл псевдо-контекстного меню
        {
            panelFile.Enabled = false;
            panelFile.Visible = false;
            panelHelp.Enabled = false;
            panelHelp.Visible = false;
            lbHelp.BackColor = Color.White;

            lbMain.BackColor = Color.GhostWhite;
            panelMain.Enabled = true;
            panelMain.Visible = true;

            pictureBox1.Enabled = true;

            panelMain.BringToFront();
        }
        //
        //Перетаскивание и масштабирование окна
        //
        private void topBorderPanel_MouseDown(object sender, MouseEventArgs e) //Верхняя граница окна
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopBorderPanelDragged = true;
            }
            else
            {
                isTopBorderPanelDragged = false;
            }
        }

        private void topBorderPanel_MouseMove(object sender, MouseEventArgs e) //Верхняя граница окна
        {
            if (e.Y < this.Location.Y)
            {
                if (isTopBorderPanelDragged)
                {
                    if (this.Height < 640)
                    {
                        this.Height = 640;
                        isTopBorderPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y + e.Y);
                        this.Height = this.Height - e.Y;
                    }
                }
            }
        }

        private void topBorderPanel_MouseUp(object sender, MouseEventArgs e) //Верхняя граница окна
        {
            isTopBorderPanelDragged = false;
        }

        private void topPanel_MouseDown_1(object sender, MouseEventArgs e) //Строка заголовок формы
        {
            if (e.Button == MouseButtons.Left)
            {
                isTopPanelDragged = true;
                Point pointStartPosition = this.PointToScreen(new Point(e.X, e.Y));
                offset = new Point();
                offset.X = this.Location.X - pointStartPosition.X;
                offset.Y = this.Location.Y - pointStartPosition.Y;
            }
            else
            {
                isTopPanelDragged = false;
            }
            if (e.Clicks == 2)
            {
                isTopPanelDragged = false;
            }
        }

        private void topPanel_MouseMove_1(object sender, MouseEventArgs e) //Строка заголовок формы
        {
            if (isTopPanelDragged)
            {
                Point newPoint = topPanel.PointToScreen(new Point(e.X, e.Y));
                newPoint.Offset(offset);
                this.Location = newPoint;

                if (this.Location.X > 2 || this.Location.Y > 2)
                {
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.Location = _normalWindowLocation;
                        this.Size = _normalWindowSize;
                        isWindowMaximized = false;
                    }
                }
            }
        }

        private void topPanel_MouseUp_1(object sender, MouseEventArgs e) //Строка заголовок формы
        {
            isTopPanelDragged = false;
            if (this.Location.Y <= 5)
            {
                if (!isWindowMaximized)
                {
                    _normalWindowSize = this.Size;
                    _normalWindowLocation = this.Location;

                    Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                    this.Location = new Point(0, 0);
                    this.Size = new System.Drawing.Size(rect.Width, rect.Height);
                    isWindowMaximized = true;
                    drawPanel.MaximumSize = new Size(myFPanel.Location.X - 15, myFPanel.Height);
                    //^Размер окна был изменён, значит нужно определить максимальный размер панели рисования!
                }
            }
        }

        private void leftPanel_MouseDown(object sender, MouseEventArgs e) //Левая граница окна
        {
            if (this.Location.X <= 0 || e.X < 0)
            {
                isLeftPanelDragged = false;
                this.Location = new Point(10, this.Location.Y);
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    isLeftPanelDragged = true;
                }
                else
                {
                    isLeftPanelDragged = false;
                }
            }
        }

        private void leftPanel_MouseMove(object sender, MouseEventArgs e) //Левая граница окна
        {
            if (e.X < this.Location.X)
            {
                if (isLeftPanelDragged)
                {
                    if (this.Width < 900)
                    {
                        this.Width = 900;
                        isLeftPanelDragged = false;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X + e.X, this.Location.Y);
                        this.Width = this.Width - e.X;
                    }
                }
            }
            drawPanel.MaximumSize = new Size(myFPanel.Location.X - 15, myFPanel.Height);
            //^Размер окна был изменён, значит нужно определить максимальный размер панели рисования!
        }

        private void leftPanel_MouseUp(object sender, MouseEventArgs e) //Левая граница окна
        {
            isLeftPanelDragged = false;
        }

        private void rightPanel_MouseDown(object sender, MouseEventArgs e) //Правая граница окна
        {
            if (e.Button == MouseButtons.Left)
            {
                isRightPanelDragged = true;
            }
            else
            {
                isRightPanelDragged = false;
            }
        }

        private void rightPanel_MouseMove(object sender, MouseEventArgs e) //Правая граница окна
        {
            if (isRightPanelDragged)
            {
                if (this.Width < 900)
                {
                    this.Width = 900;
                    isRightPanelDragged = false;
                }
                else
                {
                    this.Width = this.Width + e.X;
                }
            }
            drawPanel.MaximumSize = new Size(myFPanel.Location.X - 15, myFPanel.Height);
            //^Размер окна был изменён, значит нужно определить максимальный размер панели рисования!
        }

        private void rightPanel_MouseUp(object sender, MouseEventArgs e) //Правая граница окна
        {
            isRightPanelDragged = false;
        }

        private void bottomPanel_MouseDown(object sender, MouseEventArgs e) //Нижняя граница окна
        {
            if (e.Button == MouseButtons.Left)
            {
                isBottomPanelDragged = true;
            }
            else
            {
                isBottomPanelDragged = false;
            }
        }

        private void bottomPanel_MouseMove(object sender, MouseEventArgs e) //Нижняя граница окна
        {
            if (isBottomPanelDragged)
            {
                if (this.Height < 640)
                {
                    this.Height = 640;
                    isBottomPanelDragged = false;
                }
                else
                {
                    this.Height = this.Height + e.Y;
                }
            }
            drawPanel.MaximumSize = new Size(myFPanel.Location.X - 15, myFPanel.Height);
            //^Размер окна был изменён, значит нужно определить максимальный размер панели рисования!
        }

        private void bottomPanel_MouseUp(object sender, MouseEventArgs e) //Нижняя граница окна
        {
            isBottomPanelDragged = false;
        }       
        //
        //Перетаскивание и масштабирование окна
        //

        //
        //Размеры панели рисования
        //
        private void drawBottomPanel_MouseDown(object sender, MouseEventArgs e) //Нижняя граница панели рисования
        {
            if (e.Button == MouseButtons.Left)
            {
                isBottomDrawPanelDragged = true;
            }
            else
            {
                isBottomDrawPanelDragged = false;
            }
        }
        private bool k = false;
        private void drawBottomPanel_MouseMove(object sender, MouseEventArgs e) //Нижняя граница панели рисования
        {
            if (isBottomDrawPanelDragged)
            {
                if (drawPanel.Height < 50)
                {
                    drawPanel.Height = 50;
                    isBottomDrawPanelDragged = false;
                }
                else
                {
                    drawPanel.Height = drawPanel.Height + e.Y; //Размер панели рисования был изменён,
                    k = true;                   
                }
            }
            else
            {
                if (k)
                {
                    this.bmp = new Bitmap(drawPanel.Width, drawPanel.Height); //Подгоняем новый битмап
                    this.pictureBox1.Image = bmp; //Присваиваем
                    this.g = Graphics.FromImage(bmp); //Присваиваем
                    pictureBox1.Invalidate(); //Перерисовываем
                    if (myFPanel.nowSelected.Count == 1)
                    {
                        select(myFPanel.nowSelected[0]);
                    }
                    k = false;
                }
            }
        }

        private void drawBottomPanel_MouseUp(object sender, MouseEventArgs e) //Нижняя граница панели рисования
        {
            isBottomDrawPanelDragged = false;
        }

        private void drawRightPanel_MouseDown(object sender, MouseEventArgs e) //Правая граница панели рисования
        {
            if (e.Button == MouseButtons.Left)
            {
                isRightDrawPanelDragged = true;
            }
            else
            {
                isRightDrawPanelDragged = false;
            }
        }

        private void drawRightPanel_MouseMove(object sender, MouseEventArgs e) //Правая граница панели рисования
        {
            if (isRightDrawPanelDragged)
            {
                if (drawPanel.Width < 50)
                {
                    drawPanel.Width = 50;
                    isRightDrawPanelDragged = false;
                }
                else
                {
                    drawPanel.Width = drawPanel.Width + e.X; //Размер панели рисования был изменён,
                    k = true;
                }          
            }
            else
            {
                if (k)
                {
                    this.bmp = new Bitmap(drawPanel.Width, drawPanel.Height); //Подгоняем новый битмап
                    this.pictureBox1.Image = bmp; //Присваиваем
                    this.g = Graphics.FromImage(bmp); //Присваиваем
                    pictureBox1.Invalidate(); //Перерисовываем
                    if (myFPanel.nowSelected.Count == 1)
                    {
                        select(myFPanel.nowSelected[0]);
                    }
                    k = false;
                }
            }
        }

        private void drawRightPanel_MouseUp(object sender, MouseEventArgs e) //Правая граница панели рисования
        {
            isRightDrawPanelDragged = false;
        }    
        //
        //Размеры панели рисования
        //

        //
        //Красота
        //
        private void lbFline_MouseEnter(object sender, EventArgs e) //Красота
        {
            this.lbFline.Image = Resource1.FlineD;
        }

        private void lbFline_MouseLeave(object sender, EventArgs e) //Красота
        {
            if (nowSelect != 1) this.lbFline.Image = Resource1.Fline;
            
        }

        private void lbFbezier_MouseEnter(object sender, EventArgs e) //Красота
        {
            this.lbFbezier.Image = Resource1.FbezierD;
        }

        private void lbFbezier_MouseLeave(object sender, EventArgs e) //Красота
        {
            if (nowSelect != 2) this.lbFbezier.Image = Resource1.Fbezier;
            
        }

        private void lbFstar_MouseEnter(object sender, EventArgs e)
        {
            this.lbFstar.Image = Resource1.FstarD;
        }

        private void lbFstar_MouseLeave(object sender, EventArgs e)
        {
            if (nowSelect != 3) this.lbFstar.Image = Resource1.Fstar;
        }

        private void lbFtrg_MouseEnter(object sender, EventArgs e)
        {
            this.lbFtrg.Image = Resource1.FtrgD;
        }

        private void lbFtrg_MouseLeave(object sender, EventArgs e)
        {
            if (nowSelect != 4) this.lbFtrg.Image = Resource1.Ftrg;
        }          

        private void lbExit_MouseEnter(object sender, EventArgs e) //Красота
        {
            lbExit.BackColor = Color.Cyan;
        }

        private void lbExit_MouseLeave(object sender, EventArgs e) //Красота
        {
            lbExit.BackColor = Color.Snow;
        }
        //
        //Красота
        //

        private void lbExit_MouseUp(object sender, MouseEventArgs e) //Кнопка завершения программы
        {
            if (e.Button == MouseButtons.Left) this.Close();
        }        

        //методы ниже нужно фиксить!!!!!
        private void Hook_LeftButtonDown(RamGecTools.MouseHook.MSLLHOOKSTRUCT mouseStruct) //Левый щелчок
        {
            if (panelFile.Visible)
            {
                if (!isInControl(panelFile, mouseStruct.pt))
                {
                    panelFile.Enabled = false;
                    panelFile.Visible = false;
                    
                    panelHelp.Enabled = false;
                    panelHelp.Visible = false;
                    lbHelp.BackColor = Color.White;

                    lbMain.BackColor = Color.GhostWhite;
                    panelMain.Enabled = true;
                    panelMain.Visible = true;
                    panelMain.BringToFront();
                    pictureBox1.Enabled = true;
                }              
            }
                   
        }

        private static bool isInControl(Control contrl,RamGecTools.MouseHook.POINT pt) //Была ли мышь в контроле
        {
            Point loc = contrl.PointToScreen(Point.Empty);
            if (pt.x >= loc.X && pt.x <= loc.X + contrl.Width)
            {
                if (pt.y >= loc.Y && pt.y <= loc.Y + contrl.Height)
                {
                    return true;
                }
            }
            return false;
        }

        //
        //Выбор цвета
        //
        private void lbColor1_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor2_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor3_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor4_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor5_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor6_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor7_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor8_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor9_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor10_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor11_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor12_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor13_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor14_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor15_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor16_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor17_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor18_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor19_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        private void lbColor20_Click(object sender, EventArgs e)
        {
            clickColor(sender);
        }

        //
        //Настройки звезды
        //
        private void nudStarf_ValueChanged(object sender, EventArgs e)
        {
            this.nCount = Decimal.ToInt32(nudStarf.Value);
            pictureBox1.Focus();
        }

        private void nudStarf_KeyDown(object sender, KeyEventArgs e)
        {
            this.nCount = Decimal.ToInt32(nudStarf.Value);
            pictureBox1.Focus();
        }

        private void nudStarf_KeyUp(object sender, KeyEventArgs e)
        {
            this.nCount = Decimal.ToInt32(nudStarf.Value);
            pictureBox1.Focus();
        }
        //
        //Настройки звезды
        //

        private void clickColor(object s)
        {
            nowColor = ((Label)s).BackColor;
            lbNowColor.BackColor = nowColor;
            drawPen.Color = nowColor;
            if (myFPanel.nowSelected != null)
            {
                List<MyFPanelItem> myfs = myFPanel.nowSelected;
                for (int i = 0; i < myFPanel.nowSelected.Count; i++)
                {
                    FObject fo = (FObject)myFPanel.nowSelected[i].getData();
                    fo.setColor(nowColor);
                }
                pictureBox1.Invalidate();
                if (myFPanel.nowSelected.Count == 1)
                {
                    select(myFPanel.nowSelected[0]);
                }
            }
            if (countPoints != 0)
            {
                countPoints = 0;
                buf = new Point[100];
                firstPoint = true;
                pictureBox1.Invalidate();

            }
        }

        //
        //Выбор цвета
        //

        #endregion

        private MyFPanel myFPanel;

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel predTopPanel;
        private System.Windows.Forms.Label lbFile;
        private System.Windows.Forms.Label lbMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lbHelp;
        private System.Windows.Forms.Panel panelHelp;
        private System.Windows.Forms.Panel panelFile;
        private System.Windows.Forms.Label lbFile2;
        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        //private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Label lbFbezier;
        private Label lbFline;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Label lbClear;
        private Label lbNowColor;
        private Label lbColorInfo;
        private Label lbColor6;
        private Label lbColor5;
        private Label lbColor4;
        private Label lbColor3;
        private Label lbColor2;
        private Label lbColor1;
        private Label lbColor20;
        private Label lbColor19;
        private Label lbColor18;
        private Label lbColor17;
        private Label lbColor16;
        private Label lbColor15;
        private Label lbColor14;
        private Label lbColor13;
        private Label lbColor12;
        private Label lbColor11;
        private Label lbColor10;
        private Label lbColor9;
        private Label lbColor8;
        private Label lbColor7;
        private Panel topBorderPanel;
        private Panel topPanel;
        private Label lbExit;
        private Panel bottomPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Label label1;
        private Panel drawBottomPanel;
        private Panel drawLeftPanel;
        private Panel drawTopPanel;
        private Panel drawRightPanel;
        private Label lbFstar;
        private Label lbStarInfo;
        //private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private NumericUpDown nudStarf;
        private Label lbFtrg;
        private Label lbCursorXi;
        private Label lbCursorX;
        private Label lbCursorYi;
        private Label lbCursorY;
        private Label lbHelpMain;
        private Button button2;
        private Button button1;
        private Button button3;
        private Button button4;
        private Label lbSave;
        private Label label2;
    }
    
}

