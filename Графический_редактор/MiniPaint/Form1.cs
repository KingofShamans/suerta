using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            g = pnl_Draw.CreateGraphics();
            
        }
        bool startPaint = false;
        Graphics g;
        //хранение нулевого значения
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;
        int? initX = null;
        int? initY = null;
        

        public string Filter { get; private set; }
        public object m_filenames { get; private set; }
        public Bitmap m_Image { get; private set; }

        //Событие срабатывает при перемещении указателя мыши по панели
        private void pnl_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            string trbs = Convert.ToString(metroTrackBar1.Value);
            if (startPaint)
            {
                //Настройка заднего фона и толщины линии
                Pen p = new Pen(btn_PenColor.BackColor, float.Parse(trbs));
                //Drawing the line.
                g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
            }
        }
        //Событие срабатывает при наведении указателя на панель и нажатии кнопки мыши
        private void pnl_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            string trcv = Convert.ToString(metroTrackBar2.Value);
            startPaint = true;
            if (drawSquare)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);                
                g.FillRectangle(sb, e.X, e.Y, int.Parse(trcv), int.Parse(trcv));               
                startPaint = false;
                drawSquare = false;
            }

            if(drawCircle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                g.FillEllipse(sb, e.X, e.Y, int.Parse(trcv), int.Parse(trcv));
                startPaint = false;
                drawCircle = false;
            }

            if (drawRectangle)
            {
                SolidBrush sb = new SolidBrush(btn_PenColor.BackColor);
                g.FillRectangle(sb, e.X, e.Y, 2 * int.Parse(trcv), int.Parse(trcv));
                startPaint = false;
                drawRectangle = false;
            }


        }
        //Срабатывает, когда указатель мыши находится над карандашом и кнопка мыши отпущена
        private void pnl_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }
        //Цвет каранрдаша
        private void button1_Click(object sender, EventArgs e)
        {
            //Диалог цвета
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }


        private void btn_Ластик_Click(object sender, EventArgs e)
        {

            {
                btn_PenColor.BackColor = Color.White;

            }


        }

        private void btn_Карандаш_Click(object sender, EventArgs e)
        {

            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                btn_PenColor.BackColor = c.Color;
            }
        }

        //Новый холст 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

            g.Clear(pnl_Draw.BackColor);

            pnl_Draw.BackColor = Color.White;
            btn_CanvasColor.BackColor = Color.White;
        }
        //Настройка цвета холста
        private void btn_CanvasColor_Click_1(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                pnl_Draw.BackColor = c.Color;
                btn_CanvasColor.BackColor = c.Color;
            }
        }

        //Выход
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }


        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var m_OpenFileDialog = new OpenFileDialog();

            m_OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            m_OpenFileDialog.Filter = Filter;

            if (m_OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                

            }


            //О программе
        }
        private void О_программеStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа создана Можиным Дмитрий для демонстрации основныз функций MS Paint на базе Windows Form", "Справка", MessageBoxButtons.OK);

        }

        

        

        

            


        private void label1_Click(object sender, EventArgs e)
        {

        }

        





        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
            {



            //Получить размер бордера.
            Size BorderSize = new Size(pnl_Draw.Width, pnl_Draw.Height);
            //создаем изображение рамером в панель
            Bitmap screenshot = new Bitmap(pnl_Draw.Width, pnl_Draw.Height);
            //инициализируем графикс по это изображение.
            Graphics gr = Graphics.FromImage(screenshot);
            //копируем с экрана пиксели, не лучший костыль скажу сразу. попытался учесть размер бордера.
            gr.CopyFromScreen(Location.X+8, Location.Y+54, 0, 0, new Size(BorderSize.Width-5, BorderSize.Height-3));
            //сохраняем
            //saveFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            saveFileDialog1.Filter = "Image Files(*.png)|*.png|Image Files(*.jpg)|*.jpg|Image Files(*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog1.FileName)) File.Delete(saveFileDialog1.FileName);
                screenshot.Save(saveFileDialog1.FileName);
            }




          



        }

        private void sqr_button_Click(object sender, EventArgs e)
        {
          
             drawSquare = true;
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
           
            drawCircle = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            drawRectangle = true;

        }
      

       

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Получить размер бордера.
            Size BorderSize = new Size(pnl_Draw.Width, pnl_Draw.Height);
            //создаем изображение рамером в панель
            Bitmap screenshot = new Bitmap(pnl_Draw.Width, pnl_Draw.Height);
            //инициализируем графикс по это изображение.
            Graphics gr = Graphics.FromImage(screenshot);
            //копируем с экрана пиксели, не лучший костыль скажу сразу. попытался учесть размер бордера.
            gr.CopyFromScreen(Location.X, Location.Y + 41, 0, 0, new Size(BorderSize.Width - 5, BorderSize.Height - 3));
            //сохраняем
            //saveFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            saveFileDialog1.Filter = "Image Files(*.png)|*.png|Image Files(*.jpg)|*.jpg|Image Files(*.bmp)|*.bmp|All files (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog1.FileName)) File.Delete(saveFileDialog1.FileName);
                screenshot.Save(saveFileDialog1.FileName);
            }
        }

        private void metroTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            metroLabel1.Text = String.Format("{0}", metroTrackBar1.Value);
        }

        private void metroTrackBar2_Scroll(object sender, ScrollEventArgs e)
        {
            metroLabel2.Text = String.Format("{0}", metroTrackBar2.Value);
        }

        private void OpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files(*.png)|*.png| Image files(*.jpg)|*.jpg| Image files(*.bmp)|*.bmp";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                pnl_Draw.BackgroundImage = Image.FromFile(openFileDialog1.FileName);

            }
            
            
        }
    }
    } 
