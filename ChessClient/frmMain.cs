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
    public partial class frmMain : Form
    {
        private readonly int CellSize = 50;
        Panel[,] GameMap;

        public frmMain()
        {
            InitializeComponent();
            InitGameMap();
        }

        private void InitGameMap()
        {
            GameMap = new Panel[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    GameMap[i, j] = AddCell(i, j);
        }

        Panel AddCell(int x, int y)
        {
            var panel = new Panel();
            panel.BackColor = (x + y) % 2 == 0 ? Color.DarkGray : Color.White;
            panel.Location = GetLocation(x, y);
            panel.Name = $"p{x}{y}";
            panel.Size = new Size(CellSize, CellSize);
            panel.MouseClick += new MouseEventHandler(panel_MouseClick);
            panelBoard.Controls.Add(panel);
            return panel;
        }

        Point GetLocation(int x, int y)
        {
            return new Point(CellSize / 50 + x * CellSize, CellSize / 50 + (7 - y) * CellSize);
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}
