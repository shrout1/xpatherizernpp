using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace XPatherizerNPP
{
    public class MenuItems
    {
        private ArrayList menuItems;

        public MenuItems()
        {
            menuItems = new ArrayList();

            menuItems.Add(new MenuItem("ShowWindows", "Show XPatherizer Windows", Main.ShowXPatherizer));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("ShowSettings", "Settings...", Main.ShowSettings));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("Beautify", "Beautify", Main.Beautify));
            menuItems.Add(new MenuItem("Search", "Begin Search", Main.BeginSearch));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("VerifySingle", "Verify XML (current document)", Main.VerifySingle));
            menuItems.Add(new MenuItem("VerifyAll", "Verify XML (all documents)", Main.VerifyAll));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("Transform", "Transform current document", Main.Transform));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("LoadXMPL", "Load XPML", Main.LoadXPML));
            menuItems.Add(new MenuItem("SaveXMPL", "Save XPML", Main.SaveXPML));
            menuItems.Add(new MenuItem("", "---", null));
            menuItems.Add(new MenuItem("ShowAbout", "About", Main.ShowAbout));
            menuItems.Add(new MenuItem("XPathHelp", "XPath Help (W3 Schools)", Main.XPathHelp));
        }

        public void LoadDefaults()
        {
            Item("ShowWindows").Shortcut = new ShortcutKey(true, false, true, Keys.X);
            Item("Search").Shortcut = new ShortcutKey(false, false, false, Keys.F6);
            Item("Beautify").Shortcut = new ShortcutKey(true, false, false, Keys.F6);
        }

        public int Count
        {
            get
            {
                return menuItems.Count;
            }
        }

        public int CountNotBlank
        {
            get
            {
                int i = 0;

                foreach (MenuItem mi in menuItems)
                {
                    if (mi.SettingName != "")
                        i++;
                }
                return i;
            }
        }

        public MenuItem Item(int index)
        {
            return menuItems[index] as MenuItem;
        }

        public MenuItem Item(string SettingName)
        {
            foreach (MenuItem mi in menuItems)
            {
                if (mi.SettingName == SettingName)
                    return mi;
            }
            return menuItems[0] as MenuItem;
        }
    }

    public class MenuItem
    {
        public string SettingName;
        public string Text;
        public NppFuncItemDelegate Function;
        public ShortcutKey Shortcut;

        public MenuItem(string settingname, string text, NppFuncItemDelegate function, ShortcutKey shortcut)
        {
            SettingName = settingname;
            Text = text;
            Function = function;
            Shortcut = shortcut;
        }

        public MenuItem(string settingname, string text, NppFuncItemDelegate function)
        {
            SettingName = settingname;
            Text = text;
            Function = function;
            Shortcut = new ShortcutKey();
        }

        public bool HasShortcut()
        {
            if (((Keys)Shortcut._key).ToString() == "None")
                return false;
            else
                return true;
        }
    }
}
