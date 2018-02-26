using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XPatherizerNPP
{
    public partial class XMLFileSelect : Form
    {
        public XMLFileSelect()
        {
            InitializeComponent();
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            if (lbFiles.SelectedIndex == -1)
                this.DialogResult = DialogResult.Cancel;
            else
                this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public int GetResult()
        {
            if (this.ShowDialog() == DialogResult.OK)
                return lbFiles.SelectedIndex;
            return -1;
        }
    }
}
