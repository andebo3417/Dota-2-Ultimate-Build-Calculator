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
                    hero = pos3[rnd.Next(0, pos3.Length)];
                }
                else do
                    {
                        hero = pos3[rnd.Next(0, pos3.Length)];
                    } while (hero == curr_hero);
            }
            if (pos == position_t.one)
            {
                if (btnHero.BackgroundImage == null)
                {
                    hero = pos1[rnd.Next(0, pos1.Length)];
                }
                else do
                    {
                        hero = pos1[rnd.Next(0, pos1.Length)];
                    } while (hero == curr_hero);
            }
            if (pos == position_t.four)
            {
                if (btnHero.BackgroundImage == null)
                {
                    hero = pos4[rnd.Next(0, pos4.Length)];
                }
                else do
                    {
                        hero = pos4[rnd.Next(0, pos4.Length)];
                    } while (hero == curr_hero);
            }
            curr_hero = hero;
            Hero tmp = new Hero(hero);
            btnHero.BackgroundImage = tmp.get_img();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pos = position_t.one;
            btnHero_Click(sender, e);
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            Items form = new Items();
            form.Owner = this;
            form.Show();
        }
    }
}