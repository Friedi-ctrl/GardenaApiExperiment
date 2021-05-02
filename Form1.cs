using System;
using System.Windows.Forms;
using GardenaApi.Gardena;


namespace GardenaApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly Gardena.GardenaApi gl = new Gardena.GardenaApi();
        private void btGardenaLogin_Click(object sender, EventArgs e)
        {
            
            tbResult.Text += gl.GetToken();           
        }

        private void btRefreshToken_Click(object sender, EventArgs e)
        {
            tbResult.Text += gl.RefreshToken();
        }

        private void btGetLactionId_Click(object sender, EventArgs e)
        {
            tbResult.Text += gl.GetLocation();
        }

        private void btClearResultText_Click(object sender, EventArgs e)
        {
            tbResult.Text = String.Empty;
        }

        private void btGetState_Click(object sender, EventArgs e)
        {
            dgvMowerStatus.DataSource = gl.GetStatus();
            dgvMowerStatus.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
    }
}
