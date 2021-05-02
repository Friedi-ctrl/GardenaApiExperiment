
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvMowerStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // btGardenaLogin
            // 
            this.btGardenaLogin.Location = new System.Drawing.Point(12, 12);
            this.btGardenaLogin.Name = "btGardenaLogin";
            this.btGardenaLogin.Size = new System.Drawing.Size(104, 43);
            this.btGardenaLogin.TabIndex = 0;
            this.btGardenaLogin.Text = "Gardena Login";
            this.btGardenaLogin.UseVisualStyleBackColor = true;
            this.btGardenaLogin.Click += new System.EventHandler(this.btGardenaLogin_Click);
            // 
            // btRefreshToken
            // 
            this.btRefreshToken.Location = new System.Drawing.Point(122, 12);
            this.btRefreshToken.Name = "btRefreshToken";
            this.btRefreshToken.Size = new System.Drawing.Size(104, 43);
            this.btRefreshToken.TabIndex = 1;
            this.btRefreshToken.Text = "Refresh Token";
            this.btRefreshToken.UseVisualStyleBackColor = true;
            this.btRefreshToken.Click += new System.EventHandler(this.btRefreshToken_Click);
            // 
            // btClearResultText
            // 
            this.btClearResultText.Location = new System.Drawing.Point(542, 12);
            this.btClearResultText.Name = "btClearResultText";
            this.btClearResultText.Size = new System.Drawing.Size(104, 43);
            this.btClearResultText.TabIndex = 3;
            this.btClearResultText.Text = "Clear Result Text";
            this.btClearResultText.UseVisualStyleBackColor = true;
            this.btClearResultText.Click += new System.EventHandler(this.btClearResultText_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(12, 73);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbResult.Size = new System.Drawing.Size(776, 539);
            this.tbResult.TabIndex = 4;
            this.tbResult.WordWrap = false;
            // 
            // btGetLactionId
            // 
            this.btGetLactionId.Location = new System.Drawing.Point(232, 12);
            this.btGetLactionId.Name = "btGetLactionId";
            this.btGetLactionId.Size = new System.Drawing.Size(104, 43);
            this.btGetLactionId.TabIndex = 5;
            this.btGetLactionId.Text = "Get Location ID";
            this.btGetLactionId.UseVisualStyleBackColor = true;
            this.btGetLactionId.Click += new System.EventHandler(this.btGetLactionId_Click);
            // 
            // btGetState
            // 
            this.btGetState.Location = new System.Drawing.Point(342, 12);
            this.btGetState.Name = "btGetState";
            this.btGetState.Size = new System.Drawing.Size(104, 43);
            this.btGetState.TabIndex = 6;
            this.btGetState.Text = "Get State";
            this.btGetState.UseVisualStyleBackColor = true;
            this.btGetState.Click += new System.EventHandler(this.btGetState_Click);
            // 
            // dgvMowerStatus
            // 
            this.dgvMowerStatus.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMowerStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMowerStatus.Location = new System.Drawing.Point(16, 632);
            this.dgvMowerStatus.Name = "dgvMowerStatus";
            this.dgvMowerStatus.Size = new System.Drawing.Size(772, 213);
            this.dgvMowerStatus.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 857);
            this.Controls.Add(this.dgvMowerStatus);
            this.Controls.Add(this.btGetState);
            this.Controls.Add(this.btGetLactionId);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.btClearResultText);
            this.Controls.Add(this.btRefreshToken);
            this.Controls.Add(this.btGardenaLogin);
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
    }
}

