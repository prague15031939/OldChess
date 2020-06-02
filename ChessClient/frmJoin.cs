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
    public partial class frmJoin : Form
    {
        private int GamesAmount;
        public frmJoin(string GamesData)
        {
            InitializeComponent();
            InitListView(GamesData);
        }

        private void InitListView(string data)
        {
            string[] GameList = data.Split('\n');
            GamesAmount = GameList.Count();
            foreach (string line in GameList)
            {
                if (line == "") continue;
                string[] parts = line.Split(' ');
                var item = new ListViewItem();
                item.Text = parts[0];
                item.SubItems.Add(parts[1]);
                item.SubItems.Add(parts[2]);
                lvGames.Items.Add(item);
            }
        }

        private void lvGames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvGames.SelectedIndices[0] < GamesAmount)
                (Owner as frmMain).JoinedGameID = Convert.ToInt32(lvGames.SelectedItems[0].Text);
            this.Close();
        }

        private void lvGames_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvGames.FocusedItem != null)
                lvGames.FocusedItem.Focused = false;
        }
    }
}
