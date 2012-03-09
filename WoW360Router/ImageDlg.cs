using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DavidNikdel
{
    public partial class ImageDlg : Form
    {
        public ImageDlg(Image bg)
        {
            InitializeComponent();
            this.BackgroundImage = bg;
            this.ClientSize = bg.Size;
        }

        private void m_btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}