using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XPatherizerNPP
{
    public class XPatherizerSettings
    {
        public bool AutoLoad;
        public bool AutoSearch;
        public bool Indent;
        public string IndentChar;
        public int IndentCount;
        public bool IgnoreDocType;
        public string defaultNamespace;
        public ArrayList NodeTypes;
        public ArrayList AttributeNames;
        public ArrayList TextToShow;
        public bool Ignore;
        public bool NewFile;
        public ArrayList Extensions;
        public MenuItems nppMenuItems;

        public XPatherizerSettings()
        {
            AutoLoad = false;
            AutoSearch = false;
            Indent = true;
            IndentChar = " ";
            IndentCount = 2;
            IgnoreDocType = true;
            defaultNamespace = "";
            NodeTypes = new ArrayList();
            AttributeNames = new ArrayList();
            TextToShow = new ArrayList();
            Ignore = false;
            NewFile = true;
            Extensions = new ArrayList();
            nppMenuItems = new MenuItems();
        }
    }
}
