
namespace GardenaApi
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btGardenaLogin = new System.Windows.Forms.Button();
            this.btRefreshToken = new System.Windows.Forms.Button();
            this.btClearResultText = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.btGetLactionId = new System.Windows.Forms.Button();
            this.btGetState = new System.Windows.Forms.Button();
            this.dgvMowerStatus = new System.Windows.Forms.DataGridView();
            this.btGetWsUrl = new System.Windows.Forms.Button();
            this.btStartWebSocket = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMowerStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btGardenaLogin
            // 
            this.btGardenaLogin.Location = new System.Drawing.Point(18, 18);
            this.btGardenaLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btGardenaLogin.Name = "btGardenaLogin";
            this.btGardenaLogin.Size = new System.Drawing.Size(156, 66);
            this.btGardenaLogin.TabIndex = 0;
            this.btGardenaLogin.Text = "Gardena Login";
            this.btGardenaLogin.UseVisualStyleBackColor = true;
            this.btGardenaLogin.Click += new System.EventHandler(this.btGardenaLogin_Click);
            // 
            // btRefreshToken
            // 
            this.btRefreshToken.Location = new System.Drawing.Point(183, 18);
            this.btRefreshToken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btRefreshToken.Name = "btRefreshToken";
            this.btRefreshToken.Size = new System.Drawing.Size(156, 66);
            this.btRefreshToken.TabIndex = 1;
            this.btRefreshToken.Text = "Refresh Token";
            this.btRefreshToken.UseVisualStyleBackColor = true;
            this.btRefreshToken.Click += new System.EventHandler(this.btRefreshToken_Click);
            // 
            // btClearResultText
            // 
            this.btClearResultText.Location = new System.Drawing.Point(1024, 18);
            this.btClearResultText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btClearResultText.Name = "btClearResultText";
            this.btClearResultText.Size = new System.Drawing.Size(156, 66);
            this.btClearResultText.TabIndex = 3;
            this.btClearResultText.Text = "Clear Result Text";
            this.btClearResultText.UseVisualStyleBackColor = true;
            this.btClearResultText.Click += new System.EventHandler(this.btClearResultText_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(18, 112);
            this.tbResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(1162, 827);
            this.tbResult.TabIndex = 4;
            this.tbResult.WordWrap = false;
            // 
            // btGetLactionId
            // 
            this.btGetLactionId.Location = new System.Drawing.Point(348, 18);
            this.btGetLactionId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btGetLactionId.Name = "btGetLactionId";
            this.btGetLactionId.Size = new System.Drawing.Size(156, 66);
            this.btGetLactionId.TabIndex = 5;
            this.btGetLactionId.Text = "Get Location ID";
            this.btGetLactionId.UseVisualStyleBackColor = true;
            this.btGetLactionId.Click += new System.EventHandler(this.btGetLocationId_Click);
            // 
            // btGetState
            // 
            this.btGetState.Location = new System.Drawing.Point(513, 18);
            this.btGetState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btGetState.Name = "btGetState";
            this.btGetState.Size = new System.Drawing.Size(156, 66);
            this.btGetState.TabIndex = 6;
            this.btGetState.Text = "Get State";
            this.btGetState.UseVisualStyleBackColor = true;
            this.btGetState.Click += new System.EventHandler(this.btGetState_Click);
            // 
            // dgvMowerStatus
            // 
            this.dgvMowerStatus.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMowerStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMowerStatus.Location = new System.Drawing.Point(24, 972);
            this.dgvMowerStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMowerStatus.Name = "dgvMowerStatus";
            this.dgvMowerStatus.RowHeadersWidth = 62;
            this.dgvMowerStatus.Size = new System.Drawing.Size(1158, 328);
            this.dgvMowerStatus.TabIndex = 8;
            // 
            // btGetWsUrl
            // 
            this.btGetWsUrl.Location = new System.Drawing.Point(677, 18);
            this.btGetWsUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btGetWsUrl.Name = "btGetWsUrl";
            this.btGetWsUrl.Size = new System.Drawing.Size(156, 66);
            this.btGetWsUrl.TabIndex = 9;
            this.btGetWsUrl.Text = "Get WebSocket Url";
            this.btGetWsUrl.UseVisualStyleBackColor = true;
            this.btGetWsUrl.Click += new System.EventHandler(this.btGetWsUrl_Click);
            // 
            // btStartWebSocket
            // 
            this.btStartWebSocket.Location = new System.Drawing.Point(841, 18);
            this.btStartWebSocket.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btStartWebSocket.Name = "btStartWebSocket";
            this.btStartWebSocket.Size = new System.Drawing.Size(156, 66);
            this.btStartWebSocket.TabIndex = 10;
            this.btStartWebSocket.Text = "Start Web Sockel Client";
            this.btStartWebSocket.UseVisualStyleBackColor = true;
            this.btStartWebSocket.Click += new System.EventHandler(this.btStartWebSocket_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 1318);
            this.Controls.Add(this.btStartWebSocket);
            this.Controls.Add(this.btGetWsUrl);
            this.Controls.Add(this.dgvMowerStatus);
            this.Controls.Add(this.btGetState);
            this.Controls.Add(this.btGetLactionId);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btClearResultText);
            this.Controls.Add(this.btRefreshToken);
            this.Controls.Add(this.btGardenaLogin);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMowerStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btGardenaLogin;
        private System.Windows.Forms.Button btRefreshToken;
        private System.Windows.Forms.Button btClearResultText;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button btGetLactionId;
        private System.Windows.Forms.Button btGetState;
        private System.Windows.Forms.DataGridView dgvMowerStatus;
        private System.Windows.Forms.Button btGetWsUrl;
        private System.Windows.Forms.Button btStartWebSocket;
    }
}

