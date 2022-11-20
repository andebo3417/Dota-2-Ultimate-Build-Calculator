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
        bool flag = true;
        static int[] banned_items = new int[4] {-1, -1, -1, 32};
        static int[] picked_items = new int[3] {-1, -1, -1};
        string mode;
        int count = 0;
        float step = 0;
        Color currentColor = Color.DarkGreen;
        Color targetColor = Color.LightBlue;
        Random rnd = new Random();
        Button[] arr = new Button[63];
        public Items(string mode)
        {
            this.mode = mode;
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
                    if (banned_items.Contains(i * 7 + j))
                    {
                        Image source_img = Image.FromFile("resources\\cross.png");
                        Image bitmap = arr[i * 7 + j].BackgroundImage;
                        Graphics graphics = Graphics.FromImage(bitmap);

                        graphics.DrawImage(source_img, 0, 0);
                        arr[i * 7 + j].BackgroundImage = bitmap;
                        arr[i * 7 + j].Enabled = false;
                    }
                    arr[i * 7 + j].Name = Item.get_name(i * 7 + j);
                    arr[i * 7 + j].Click += new EventHandler(OnItemPress);
                }
            }
        }

        public void set_items()
        {
            int num = 0;
            Form1 main = this.Owner as Form1;
            int curr_hero = main.curr_hero_id;
            Item item = null;
            Image[] items = new Image[6];
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    item = new Item(picked_items[0]);
                    items[i] = item.get_img();
                }
                else if (i == 2)
                {
                    item = new Item(picked_items[1]);
                    items[i] = item.get_img();
                }
                else if (i == 4)
                {
                    item = new Item(picked_items[2]);
                    items[i] = item.get_img();
                }
                else if (i == 5)
                {
                    for (int j = 0; j < picked_items.Length; j++)
                    {
                        if (Item.boots.Contains(picked_items[j]))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        num = rnd.Next(1, 7);
                        item = new Item(num);
                        picked_items.Append(num);
                        items[i] = item.get_img();
                    }
                    else
                    {
                        num = rnd.Next(7, 64);
                        if (num == 63) num = 0;
                        item = new Item(num);
                        picked_items.Append(num);
                        items[i] = item.get_img();
                    }
                }
                else
                {
                    do
                    {
                        flag = true;
                        num = rnd.Next(0, 63);
                        if (Hero.melee.Contains(curr_hero) && Item.ranged.Contains(num)) flag = false;
                        if (!Hero.melee.Contains(curr_hero) && Item.melee.Contains(num)) flag = false;
                        if (Hero.magic.Contains(curr_hero) && Item.physical.Contains(num)) flag = false;
                        if (!Hero.magic.Contains(curr_hero) && Item.magic.Contains(num)) flag = false;
                        if (banned_items.Contains(num) || picked_items.Contains(num)) flag = false;
                        for (int j = 0; j < picked_items.Length; j++)
                        {
                            if (Item.boots.Contains(picked_items[j]))
                            {
                                flag = false;
                                break;
                            }
                        }
                    } while (flag == false);
                    item = new Item(num);
                    picked_items.Append(num);
                    items[i] = item.get_img();
                }
            }
            main.set_items(items);
        }

        public void OnItemPress(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            Button btn = sender as Button;
            Image source_img;
            Image bitmap;
            Graphics graphics;
            if (mode == "ban")
            {
                banned_items[count] = Item.get_num(btn.Name);
                count++;
                source_img = Image.FromFile("resources\\cross.png");
                bitmap = btn.BackgroundImage;
                graphics = Graphics.FromImage(bitmap);

                graphics.DrawImage(source_img, 0, 0);
                btn.BackgroundImage = bitmap;
                btn.Enabled = false;
                if (count == 3)
                {
                    this.Close();
                }
                return;
            }
            if (mode == "pick")
            {
                picked_items[count] = Item.get_num(btn.Name);
                btn.Enabled = false;
                count++;
                source_img = Image.FromFile("resources\\add.png");
                bitmap = btn.BackgroundImage;
                graphics = Graphics.FromImage(bitmap);

                graphics.DrawImage(source_img, 0, 0);
                btn.BackgroundImage = bitmap;
                btn.Enabled = false;
                if (count == 3)
                {
                    set_items();
                    banned_items[0] = -1;
                    banned_items[1] = -1;
                    banned_items[2] = -1;
                    picked_items[0] = -1;
                    picked_items[1] = -1;
                    this.Close();
                }
                return;
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
