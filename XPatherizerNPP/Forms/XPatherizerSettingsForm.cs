using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace XPatherizerNPP
{
    public partial class XPatherizerSettingsForm : Form
    {
        public MenuItems SettingsMenuItems;

        public XPatherizerSettingsForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Main.GetSettingsFromDlg(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Main.HideSettings();
        }

        public void LoadData()
        {
            cbAutoLoad.Checked = Main.settings.AutoLoad;
            cbAutoSearch.Checked = Main.settings.AutoSearch;
            cbIndent.Checked = Main.settings.Indent;
            if (Main.settings.IndentChar == " ")
                ddlCharacter.Text = "Space";
            else
                ddlCharacter.Text = "Tab";
            nudAmount.Value = Main.settings.IndentCount;
            cbDTD.Checked = Main.settings.IgnoreDocType;
            txtDefaultNS.Text = Main.settings.defaultNamespace;
            dgvNodeSettings.Rows.Clear();
            for (int i = 0; i < Main.settings.NodeTypes.Count; i++)
            {
                dgvNodeSettings.Rows.Add();
                dgvNodeSettings.Rows[i].Cells[0].Value = Main.settings.NodeTypes[i];
                dgvNodeSettings.Rows[i].Cells[1].Value = Main.settings.AttributeNames[i];
                dgvNodeSettings.Rows[i].Cells[2].Value = Main.settings.TextToShow[i];
            }

            if (Main.settings.Ignore)
                rbtnIgnore.Checked = true;
            else
                rbtnAllow.Checked = true;

            if (Main.settings.NewFile)
                rbtnNewFile.Checked = true;
            else
                rbtnXPathResults.Checked = true;

            dgvExtensions.Rows.Clear();
            for (int i = 0; i < Main.settings.Extensions.Count; i++)
            {
                dgvExtensions.Rows.Add();
                dgvExtensions.Rows[i].Cells[0].Value = Main.settings.Extensions[i];
            }

            SettingsMenuItems = Main.settings.nppMenuItems;

            ddlKeys.DataSource = System.Enum.GetValues(typeof(Keys));
            ddlMenuOptions.Items.Clear();

            for (int i = 0; i < SettingsMenuItems.Count; i++)
            {
                MenuItem mi = SettingsMenuItems.Item(i);

                if (mi.SettingName != "")
                {
                    ddlMenuOptions.Items.Add(new ComboboxItem(mi.Text, mi.SettingName));
                }
            }
            if (ddlMenuOptions.Items.Count != 0)
            {
                ddlMenuOptions.SelectedIndex = -1;
                ddlMenuOptions.SelectedIndex = 0;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ddlMenuOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenuOptions.SelectedIndex > -1)
            {
                MenuItem mi = SettingsMenuItems.Item((ddlMenuOptions.SelectedItem as ComboboxItem).Value.ToString());
                ddlKeys.SelectedIndex = -1;
                ddlKeys.Text = ((Keys)mi.Shortcut._key).ToString();
                cbAlt.Checked = Convert.ToBoolean(mi.Shortcut._isAlt);
                cbCtrl.Checked = Convert.ToBoolean(mi.Shortcut._isCtrl);
                cbShift.Checked = Convert.ToBoolean(mi.Shortcut._isShift);
            }
        }

        private void ddlKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlKeys.SelectedIndex > -1 && ddlMenuOptions.SelectedIndex > -1)
            {
                MenuItem mi = SettingsMenuItems.Item((ddlMenuOptions.SelectedItem as ComboboxItem).Value.ToString());
                mi.Shortcut._key = Convert.ToByte(Enum.Parse(typeof(Keys), ddlKeys.SelectedValue.ToString(), true));
                if (ddlKeys.SelectedText == "None")
                {
                    cbAlt.Checked = false;
                    cbCtrl.Checked = false;
                    cbShift.Checked = false;
                }
            }
        }

        private void cbAlt_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlKeys.SelectedIndex > -1 && ddlMenuOptions.SelectedIndex > -1)
            {
                MenuItem mi = SettingsMenuItems.Item((ddlMenuOptions.SelectedItem as ComboboxItem).Value.ToString());
                mi.Shortcut._isAlt = Convert.ToByte(cbAlt.Checked);
            }
        }

        private void cbCtrl_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlKeys.SelectedIndex > -1 && ddlMenuOptions.SelectedIndex > -1)
            {
                MenuItem mi = SettingsMenuItems.Item((ddlMenuOptions.SelectedItem as ComboboxItem).Value.ToString());
                mi.Shortcut._isCtrl = Convert.ToByte(cbCtrl.Checked);
            }
        }

        private void cbShift_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlKeys.SelectedIndex > -1 && ddlMenuOptions.SelectedIndex > -1)
            {
                MenuItem mi = SettingsMenuItems.Item((ddlMenuOptions.SelectedItem as ComboboxItem).Value.ToString());
                mi.Shortcut._isShift = Convert.ToByte(cbShift.Checked);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Main.GetSettingsFromDlg(false);
        }
    }

    public class ComboboxItem
    {
        public string Text
        {
            get;
            set;
        }
        public object Value
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Text;
        }
        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
