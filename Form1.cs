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
        private async void btGardenaLogin_Click(object sender, EventArgs e)
        {

            tbResult.Text = await gl.GetToken();
        }

        private async void btRefreshToken_Click(object sender, EventArgs e)
        {
            tbResult.Text += await gl.RefrechToken();
        }

        private async void btGetLocationId_Click(object sender, EventArgs e)
        {
            tbResult.Text += await gl.GetLocationId();
        }

        private void btClearResultText_Click(object sender, EventArgs e)
        {
            tbResult.Text = String.Empty;
        }

        private async void btGetState_Click(object sender, EventArgs e)
        {
            dgvMowerStatus.DataSource = await gl.GetStatus();
            dgvMowerStatus.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private async void btGetWsUrl_Click(object sender, EventArgs e)
        {
            tbResult.Text += await gl.GetWebSocketUrl(); 
        }

        private void btStartWebSocket_Click(object sender, EventArgs e)
        {
            gl.StartWebSocket();
        }
    }
}
