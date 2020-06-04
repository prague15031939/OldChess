using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OldChess
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
            SetServers();
        }

        private void SetServers()
        {
            comboServers.Items.Add("127.0.0.1:8008");
            comboServers.Items.Add("192.168.100.126:8008");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string PatternName = @"^([A-Za-z0-9_])+$";
            string PatternServer = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b:[0-9]{1,5}$";
            if (Regex.IsMatch(txtName.Text, PatternName) && Regex.IsMatch(comboServers.Text, PatternServer))
            {
                (Owner as frmMain).UserName = txtName.Text;
                (Owner as frmMain).ServerInfo = comboServers.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("invalid name or server" , "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
