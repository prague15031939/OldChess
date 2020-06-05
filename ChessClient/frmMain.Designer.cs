namespace OldChess
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBoard = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asWhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asBlackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinAGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitAGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelServer = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelPlayerBlack = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelPlayerWhite = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.BackColor = System.Drawing.SystemColors.GrayText;
            this.panelBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBoard.Location = new System.Drawing.Point(37, 124);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(402, 402);
            this.panelBoard.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(476, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.newGameToolStripMenuItem,
            this.cancelNewGameToolStripMenuItem,
            this.joinAGameToolStripMenuItem,
            this.quitAGameToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.connectToolStripMenuItem.Text = "Connect..";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asWhiteToolStripMenuItem,
            this.asBlackToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.newGameToolStripMenuItem.Text = "New game..";
            // 
            // asWhiteToolStripMenuItem
            // 
            this.asWhiteToolStripMenuItem.Name = "asWhiteToolStripMenuItem";
            this.asWhiteToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.asWhiteToolStripMenuItem.Text = "as white";
            this.asWhiteToolStripMenuItem.Click += new System.EventHandler(this.asWhiteToolStripMenuItem_Click);
            // 
            // asBlackToolStripMenuItem
            // 
            this.asBlackToolStripMenuItem.Name = "asBlackToolStripMenuItem";
            this.asBlackToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.asBlackToolStripMenuItem.Text = "as black";
            this.asBlackToolStripMenuItem.Click += new System.EventHandler(this.asBlackToolStripMenuItem_Click);
            // 
            // cancelNewGameToolStripMenuItem
            // 
            this.cancelNewGameToolStripMenuItem.Name = "cancelNewGameToolStripMenuItem";
            this.cancelNewGameToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.cancelNewGameToolStripMenuItem.Text = "Cancel new game";
            this.cancelNewGameToolStripMenuItem.Click += new System.EventHandler(this.cancelNewGameToolStripMenuItem_Click);
            // 
            // joinAGameToolStripMenuItem
            // 
            this.joinAGameToolStripMenuItem.Name = "joinAGameToolStripMenuItem";
            this.joinAGameToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.joinAGameToolStripMenuItem.Text = "Join a game..";
            this.joinAGameToolStripMenuItem.Click += new System.EventHandler(this.joinAGameToolStripMenuItem_Click);
            // 
            // quitAGameToolStripMenuItem
            // 
            this.quitAGameToolStripMenuItem.Name = "quitAGameToolStripMenuItem";
            this.quitAGameToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.quitAGameToolStripMenuItem.Text = "Quit a game";
            this.quitAGameToolStripMenuItem.Click += new System.EventHandler(this.quitAGameToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelServer);
            this.panel1.Location = new System.Drawing.Point(37, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 33);
            this.panel1.TabIndex = 5;
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelServer.Location = new System.Drawing.Point(3, 6);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(46, 18);
            this.labelServer.TabIndex = 0;
            this.labelServer.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelPlayerBlack);
            this.panel2.Location = new System.Drawing.Point(37, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 33);
            this.panel2.TabIndex = 6;
            // 
            // labelPlayerBlack
            // 
            this.labelPlayerBlack.AutoSize = true;
            this.labelPlayerBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlayerBlack.Location = new System.Drawing.Point(165, 6);
            this.labelPlayerBlack.Name = "labelPlayerBlack";
            this.labelPlayerBlack.Size = new System.Drawing.Size(46, 18);
            this.labelPlayerBlack.TabIndex = 0;
            this.labelPlayerBlack.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelPlayerWhite);
            this.panel3.Location = new System.Drawing.Point(37, 560);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(402, 33);
            this.panel3.TabIndex = 7;
            // 
            // labelPlayerWhite
            // 
            this.labelPlayerWhite.AutoSize = true;
            this.labelPlayerWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlayerWhite.Location = new System.Drawing.Point(165, 6);
            this.labelPlayerWhite.Name = "labelPlayerWhite";
            this.labelPlayerWhite.Size = new System.Drawing.Size(46, 18);
            this.labelPlayerWhite.TabIndex = 0;
            this.labelPlayerWhite.Text = "label1";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(476, 613);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelBoard);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "OldChess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinAGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asWhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asBlackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelPlayerBlack;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelPlayerWhite;
        private System.Windows.Forms.ToolStripMenuItem quitAGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelNewGameToolStripMenuItem;
    }
}

