using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            comboServers.Items.Add("192.168.100.200:7013");
            comboServers.Items.Add("192.168.10.1:6666");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            (Owner as frmMain).UserName = txtName.Text;
            (Owner as frmMain).ServerInfo = comboServers.Text;
            this.Close();
        }
    }
}
