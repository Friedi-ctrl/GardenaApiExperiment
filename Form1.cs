using System;
using System.Windows.Forms;

namespace GardenaApi
{
    public partial class Form1 : Form
    {
        string debugText;
        private readonly Gardena.GardenaApi gApi = new();
        public Form1()
        {
            InitializeComponent();
            gApi.UpdateDebugText += GApi_UpdateDebugText;
        }
        private async void btGetToken_Click(object sender, EventArgs e)
        {
            await gApi.GetToken();
        }

        private async void btRefreshToken_Click(object sender, EventArgs e)
        {
            await gApi.RefrechToken();
        }

        private async void btGetLocationId_Click(object sender, EventArgs e)
        {
            await gApi.GetLocationId();
        }

        private async void btGetState_Click(object sender, EventArgs e)
        {
            dgvMowerStatus.DataSource = await gApi.GetStatus();
            dgvMowerStatus.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private async void btStartWebSocket_Click(object sender, EventArgs e)
        {
            await gApi.StartWebSocketClient();
        }

        private void btClearResultText_Click(object sender, EventArgs e)
        {
            tbResult.Text = debugText = string.Empty;
        }

        private void GApi_UpdateDebugText(object sender, DebugTextEventArgs e)
        {
            debugText += e.DebugText;
            AccessToTb();
        }

        void AccessToTb()
        {
            if (tbResult.InvokeRequired)
            {
                tbResult.Invoke(new MethodInvoker(AccessToTb));
                return;
            }
            tbResult.Text = debugText;
            tbResult.SelectionStart = tbResult.Text.Length;
            tbResult.ScrollToCaret();
        }
    }
}