namespace App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        int x1, x2, x3;
        int n, m = 0, p = 0, speed = 0, count = 0;

        System.Windows.Forms.Timer timer1;
        Graphics graphics;
        Bitmap bitmap;
        Pen pen;
        SolidBrush solid;

        int[,] box;
        char[,] move;

        private void hanoi(int h, char s, char i, char d)
        {
            if (h == 1)
            {
                listBox1.Items.Add(s + " -> " + d);
                move[0, m] = s;
                move[1, m] = d;
                m++;
                count = m;
                label5.Text = $"Count: {count}/{listBox1.Items.Count}";
            }
            else
            {
                hanoi(h - 1, s, d, i);
                hanoi(1, s, i, d);
                hanoi(h - 1, i, s, d);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //play/pause button
            if (timer1.Enabled)
                timer1.Stop();
            else
                timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pen = new Pen(Color.Black, 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            try
            {
                n = int.Parse(textBox1.Text);
                speed = int.Parse(textBox2.Text);
                Init();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input");
            }
        }
        private void Init()
        {
            p = 0; m = 0;
            timer1.Interval = speed;
            box = new int[3, n];
            move = new char[2, 33000];//experimental - 32767 - max moves for 15 plates
            for (int i = 0; i < n; i++)
                box[0, i] = n - i;
            hanoi(n, 'A', 'B', 'C');
            timer1.Start();
            _draw();
        }
        
        private void _drawS()
        {
            //deseneaza standurile 
            graphics.DrawLine(pen, 75, 350, 225, 350);
            graphics.DrawLine(pen, 325, 350, 475, 350);
            graphics.DrawLine(pen, 575, 350, 725, 350);

            graphics.DrawLine(pen, 150, 350, 150, 50);
            graphics.DrawString("A", new Font("Arial", 20), Brushes.Black, 140, 20);
            graphics.DrawLine(pen, 400, 350, 400, 50);
            graphics.DrawString("B", new Font("Arial", 20), Brushes.Black, 390, 20);
            graphics.DrawLine(pen, 650, 350, 650, 50);
            graphics.DrawString("C", new Font("Arial", 20), Brushes.Black, 640, 20);
        }

        private void _draw()
        {
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (box[i,j] != 0)
                    {
                        //selecteaza culoarea discului
                        switch (box[i,j])
                        {
                            case 1:
                                solid = new SolidBrush(Color.Red);
                                break;
                            case 2:
                                solid = new SolidBrush(Color.Blue);
                                break;
                            case 3:
                                solid = new SolidBrush(Color.Green);
                                break;
                            case 4:
                                solid = new SolidBrush(Color.Yellow);
                                break;
                            case 5:
                                solid = new SolidBrush(Color.Pink);
                                break;
                            case 6:
                                solid = new SolidBrush(Color.Orange);
                                break;
                            case 7:
                                solid = new SolidBrush(Color.Brown);
                                break;
                            case 8:
                                solid = new SolidBrush(Color.Purple);
                                break;
                            case 9:
                                solid = new SolidBrush(Color.Black);
                                break;
                            case 10:
                                solid = new SolidBrush(Color.Gray);
                                break;
                            case 11:
                                solid = new SolidBrush(Color.Cyan);
                                break;
                            case 12:
                                solid = new SolidBrush(Color.Lime);
                                break;
                            case 13:
                                solid = new SolidBrush(Color.Maroon);
                                break;
                            case 14:
                                solid = new SolidBrush(Color.Navy);
                                break;
                            case 15:
                                solid = new SolidBrush(Color.Olive);
                                break;
                            default:
                                throw new Exception("Too many disks");
                                break;
                        }
                        //deseneaza discul
                        int width = box[i, j] * 20;
                        graphics.FillRectangle(solid, (i + 1) * 150 + (100 * i) - width/2, 320 - j * 30 -2, width, 30);
                    }
                }
            }
        }

        private void refr()
        {
            pictureBox1.Image = bitmap;
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            //golim canvasul
            graphics.Clear(Color.White);

            x1 = -1;
            x2 = -1;
            x3 = -1;

            for (int i = 0; i < n; i++)
                if (box[0, i] == 0)
                { x1 = i; break; }
            for (int i = 0; i < n; i++)
                if (box[1, i] == 0)
                { x2 = i; break; }
            for (int i = 0; i < n; i++)
                if (box[2, i] == 0)
                { x3 = i; break; }

            if (x1 == -1)
                x1 = n;
            if (x2 == -1)
                x2 = n;
            if (x3 == -1)
                x3 = n;

            switch (move[0, p])
            {
                case 'A':
                    switch (move[1, p])
                    {
                        case 'B':
                            box[1, x2] = box[0, x1 - 1];
                            box[0, x1 - 1] = 0;
                            break;
                        case 'C':
                            box[2, x3] = box[0, x1 - 1];
                            box[0, x1 - 1] = 0;
                            break;
                    }
                    break;
                case 'B':
                    switch (move[1, p])
                    {
                        case 'A':
                            box[0, x1] = box[1, x2 - 1];
                            box[1, x2 - 1] = 0;
                            break;
                        case 'C':
                            box[2, x3] = box[1, x2 - 1];
                            box[1, x2 - 1] = 0;
                            break;
                    }
                    break;
                case 'C':
                    switch (move[1, p])
                    {
                        case 'A':
                            box[0, x1] = box[2, x3 - 1];
                            box[2, x3 - 1] = 0;
                            break;
                        case 'B':
                            box[1, x2] = box[2, x3 - 1];
                            box[2, x3 - 1] = 0;
                            break;
                    }
                    break;
            }
            _drawS();
            _draw();
            refr();
            p++;
            if (p == count + 1)
                timer1.Stop();
        }
    }
}
