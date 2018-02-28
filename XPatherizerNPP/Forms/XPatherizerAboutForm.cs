using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    public partial class XPatherizerAboutForm : Form
    {
        public XPatherizerAboutForm()
        {
            InitializeComponent();

            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            version = version.Substring(0, version.IndexOf(".", version.IndexOf(".") + 1));

            label1.Text = "\r\nXPatherizer - NPP\r\n\r\nVersion " + version + "\r\n\r\nVisit the Project Homepage";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.HideAbout();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}
