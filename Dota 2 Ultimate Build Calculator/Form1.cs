using Dota_2_Ultimate_Build_Calculator.Properties;

namespace Dota_2_Ultimate_Build_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[] pos1 = new string[] { "Primal Beast", "Storm Spirit", "Zeus", "Pugna", "Void Spirit", "Ember Spirit", "Arc Warden", "Leshrac", "Lina", "Invoker", "Venomancer" };
        string[] pos3 = new string[] { "Silencer", "Skywrath Mage", "Techies", "Jakiro", "Phantom Assassin", "Sand King", "Death Prophet", "Dragon Knight", "Night Stalker"};
        string[] pos4 = new string[] { "Dark Willow", "Ancient Apparition", "Viper", "Windranger",  "Keeper of the Light", "Mirana", "Enchantres", "Hoodwink", "Rubick", "Io", "Nature`s Prophet" };
        public int[] banned_items = new int[3];
        string curr_hero;
        public int curr_hero_id;
        enum position_t { one, three, four };
        position_t pos;
        float step = 0;
        Color currentColor = Color.DarkGreen;
        Color targetColor = Color.LightBlue;
        Random rnd = new Random();
        private void btnPos_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string hero = "Primal Beast";
            switch (btn.Name) {
                case "btnPos1":
                    panelBackground.BackgroundImage = Image.FromFile("Resources\\primal_beast.png");
                    pos = position_t.one;
                    btnHero_Click(sender, e);
                    break;
                case "btnPos3":
                    panelBackground.BackgroundImage = Image.FromFile("Resources\\silencer.png");
                    pos = position_t.three;
                    btnHero_Click(sender, e);
                    break;
                case "btnPos4":
                    panelBackground.BackgroundImage = Image.FromFile("Resources\\dark_willow.png");
                    pos = position_t.four;
                    btnHero_Click(sender, e);
                    break;
            }
            btnBan.Enabled = true;
            btnPick.Enabled = false;
            btn1.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn2.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn3.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn4.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn5.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn6.BackgroundImage = Image.FromFile("Resources\\slot.png");
            lblAgh.Text = "??";
            lblShard.Text = "??";
        }

        public void set_items(Image[] arr)
        {
            btn1.BackgroundImage = arr[0];
            btn2.BackgroundImage = arr[1];
            btn3.BackgroundImage = arr[2];
            btn4.BackgroundImage = arr[3];
            btn5.BackgroundImage = arr[4];
            btn6.BackgroundImage = arr[5];
        }
        private void timerBorderColor_Tick(object sender, EventArgs e)
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

            btnHero.FlatAppearance.BorderColor = Color.FromArgb(mixR, mixG, mixB);
            step += 0.03f;
        }

        private void btnHero_Click(object sender, EventArgs e)
        {
            string hero = "Primal Beast";
            if (pos == position_t.three)
            {
                if (btnHero.BackgroundImage == null) {
                    curr_hero_id = rnd.Next(0, pos3.Length);
                    hero = pos3[curr_hero_id];
                }
                else do
                    {
                        curr_hero_id = rnd.Next(0, pos3.Length);
                        hero = pos3[curr_hero_id];
                    } while (hero == curr_hero);
            }
            if (pos == position_t.one)
            {
                if (btnHero.BackgroundImage == null)
                {
                    curr_hero_id = rnd.Next(0, pos1.Length);
                    hero = pos1[curr_hero_id];
                }
                else do
                    {
                        curr_hero_id = rnd.Next(0, pos1.Length);
                        hero = pos1[curr_hero_id];
                    } while (hero == curr_hero);
            }
            if (pos == position_t.four)
            {
                if (btnHero.BackgroundImage == null)
                {
                    curr_hero_id = rnd.Next(0, pos4.Length);
                    hero = pos4[curr_hero_id];
                }
                else do
                    {
                        curr_hero_id = rnd.Next(0, pos4.Length);
                        hero = pos4[curr_hero_id];
                    } while (hero == curr_hero);
            }
            curr_hero = hero;
            curr_hero_id = Hero.get_num(hero);
            Hero tmp = new Hero(hero);
            btnHero.BackgroundImage = tmp.get_img();
            btnBan.Enabled = true;
            btnPick.Enabled = false;
            btn1.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn2.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn3.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn4.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn5.BackgroundImage = Image.FromFile("Resources\\slot.png");
            btn6.BackgroundImage = Image.FromFile("Resources\\slot.png");
            lblAgh.Text = "??";
            lblShard.Text = "??";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pos = position_t.one;
            btnHero_Click(sender, e);
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            Items form = new Items("ban");
            form.Owner = this;
            form.Show();
            btnPick.Enabled = true;
            btnBan.Enabled = false;
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            Items form = new Items("pick");
            form.Owner = this;
            form.Show();
            btnPick.Enabled = false;
        }

        private void btnAgh_Click(object sender, EventArgs e)
        {
            lblAgh.Text = (10 * rnd.Next(1, 4)).ToString();
        }

        private void btnShard_Click(object sender, EventArgs e)
        {
            lblShard.Text = (10 + 5 * rnd.Next(0, 4)).ToString();
        }
    }
}