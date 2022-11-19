using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dota_2_Ultimate_Build_Calculator
{
    public partial class Items : Form
    {
        int[] banned_items = new int[3];
        string mode;
        int count = 0;
        float step = 0;
        Color currentColor = Color.DarkGreen;
        Color targetColor = Color.LightBlue;
        Random rnd = new Random();
        Button[] arr = new Button[63];
        public Items()
        {
            InitializeComponent();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            Item itm;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    itm = new Item(i * 7 + j);
                    arr[i * 7 + j] = new Button();
                    arr[i * 7 + j].Size = new Size(96, 70);
                    arr[i * 7 + j].Location = new Point(96 * j, 70 * i);
                    arr[i * 7 + j].BackgroundImageLayout = ImageLayout.Stretch;
                    arr[i * 7 + j].FlatStyle = FlatStyle.Flat;
                    arr[i * 7 + j].FlatAppearance.BorderSize = 1;
                    this.Controls.Add(arr[i * 7 + j]);
                    arr[i * 7 + j].BackgroundImage = itm.get_img();
                    arr[i * 7 + j].Name = Item.get_name(i * 7 + j);
                    arr[i * 7 + j].Click += new EventHandler(OnItemPress);
                }
            }
        }

        public void OnItemPress(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            Button btn = sender as Button;
            if (main != null)
            {
                main.banned_items[count] = Item.get_num(btn.Name);
                count++;
            }
            Image source_img = Image.FromFile("resources\\cross.png");
            Image bitmap = btn.BackgroundImage;
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.DrawImage(source_img, 0, 0);
            btn.BackgroundImage = bitmap;
            if (count == 3)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (step >= 1f)
            {
                step = 0;

                int R = rnd.Next(0, 255);
                int G = rnd.Next(0, 255);
                int B = rnd.Next(0, 255);
                currentColor = targetColor;
                targetColor = Color.FromArgb(R, G, B);
            }
            int mixR = (int)(currentColor.R * (1f - step) + targetColor.R * step);
            int mixG = (int)(currentColor.G * (1f - step) + targetColor.G * step);
            int mixB = (int)(currentColor.B * (1f - step) + targetColor.B * step);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    arr[i * 7 + j].FlatAppearance.BorderColor = Color.FromArgb(mixR, mixG, mixB);
                }
            }
            step += 0.03f;
        }
    }
}
