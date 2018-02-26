using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace XPatherizerNPP
{
    public partial class XPathSearchForm : Form
    {
        public bool HasBeenShown;

        public XPathSearchForm()
        {
            InitializeComponent();
            HasBeenShown = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BeginSearch();
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXPath.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXPath.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtXPath.Paste();
        }

        private void txtXPath_TextChanged(object sender, EventArgs e)
        {
            if (Main.settings.AutoSearch)
            {
                BeginSearch();
            }
        }

        public void BeginSearch()
        {
            //Only search if the forms have been shown at least once.
            if (HasBeenShown && Main.frmXPathResults.HasBeenShown)
            {
                string[] xpathStrings;

                if (txtXPath.SelectedText.Length > 0)
                {
                    xpathStrings = txtXPath.SelectedText.Split('\n');
                }
                else
                    xpathStrings = txtXPath.Lines;

                Main.frmXPathResults.BeginSearch(xpathStrings);
            }
        }
    }
}
