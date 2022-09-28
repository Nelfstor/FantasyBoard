namespace GameAppBattleField
{
    public partial class Form1 : Form
    {
        int x;// Количество стобцов, всегда первый аргумент если передаются оба
        int y;// Количество строк
        bool tmrOn;

        internal GameCreator gameCreator;

        Graphics gr;
        public Form1()
        {
            InitializeComponent();
            btnGenerate.Focus();
            btnStartGame.Enabled = false;
        }

        private void txtBxColumns_TextChanged(object sender, EventArgs e)
        {
            CheckNumber(txtBxColumns.Text, 0); // 0 for x
        }
        private void CheckNumber(string s,int rows_or_columns)
        {
            int temp;
            if (!Int32.TryParse(s, out temp)) MessageBox.Show("Введите число");
            else if (rows_or_columns == 0) x = temp;
            else y = temp;
        }

        private void txtBxRows_TextChanged(object sender, EventArgs e)
        {
            CheckNumber(txtBxRows.Text, 1); // 1 for y
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            CheckNumber(txtBxColumns.Text, 0); // 0 for x
            CheckNumber(txtBxRows.Text, 1); // 1 for y
            if (x * y < 40) MessageBox.Show("Слишком маленькое поле, увеличьте количество строк или столбцов");
            else
            {
                gr = panelGame.CreateGraphics();
                gameCreator = new GameCreator(gr, panelGame.Height, panelGame.Width, x, y);
                btnStartGame.Enabled = true;
            }
        }

        private void panelGame_Paint(object sender, PaintEventArgs e)
        {
            this.Update();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (!tmrOn)
            {
                tmrGame.Interval = (int)nmrTimeInterval.Value;
               // tmrGame.Enabled = !tmrGame.Enabled;
                tmrGame.Start();
                tmrOn = true;
            }
            else
            {
                tmrGame.Stop();
                tmrOn = false;
            }

        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            if (tmrOn)
            {
                gameCreator.Step();
                if (gameCreator.Victory != null)
                {
                    tmrGame.Stop();
                    tmrOn = false;
                    btnGenerate.Text = "Новая игра";                              
                    btnStartGame.Enabled = false;
                    MessageBox.Show(gameCreator.Victory.ToString() + " победили!");
                }
            }
        }
    }
}