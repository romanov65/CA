using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CA.Forms
{
    public partial class MainWinUser : Form
    {
        public MainWinUser()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Добро пожаловать ";
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authorization backtoauth = new Authorization();
            backtoauth.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Учёт компьютерной техники\n" + "Разработчик: Москаленко Александр\n" + "2018 ©", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
