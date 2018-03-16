using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
//using NppPluginNET;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    public class Main
    {
        #region " Fields "
        internal const string PluginName = "XPatherizerNPP";
        static string settingsFilePath = null;
        static string tmpFilePath = null;
        static string tmpFilesPath = null;
        static int idSearchDlg = 0;
        static int idResultsDlg = 1;
        public static int DNSCount = 1;
        static XPathSearchForm frmXPathSearch = null;
        public static XPathResultsForm frmXPathResults = null;
        static XPatherizerSettingsForm frmXPathSettings = null;
        static XPatherizerAboutForm frmXPathAbout = null;
        static OpenFileDialog openFileDialog1 = null;
        static SaveFileDialog saveFileDialog1 = null;
        static XPathTemp tempData = null;
        public static XPatherizerSettings settings = null;
        #endregion

        #region " StartUp/CleanUp "

//        public static void OnNotification(ScNotification notification)
//        {
//            //Borrowed from GUIDHelper
//            //selectWholeGuidIfStartOrEndIsSelected.Execute(notification);
//        }

        internal static void CommandMenuInit()
        {
            //Debug
            //System.Diagnostics.Debugger.Launch();
            StringBuilder sbFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbFilePath);
            settingsFilePath = sbFilePath.ToString() + "\\XPatherizer";
            if (!Directory.Exists(settingsFilePath)) Directory.CreateDirectory(settingsFilePath);
            tmpFilePath = settingsFilePath + "\\Temp.tmp";
            tmpFilesPath = settingsFilePath + "\\";
            settingsFilePath = settingsFilePath + "\\Settings.dat";

            LoadSettings();

            for (int i = 0; i < settings.nppMenuItems.Count; i++)
            {
                MenuItem mi = settings.nppMenuItems.Item(i);
                if (mi.HasShortcut())
                    PluginBase.SetCommand(i, mi.Text, mi.Function, mi.Shortcut);
                else
                    PluginBase.SetCommand(i, mi.Text, mi.Function);
            }
        }

        internal static void SetToolBarIcon()
        {
        }

        internal static void PluginCleanUp()
        {
            //Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }
        #endregion

        #region " Menu functions "
        /// <summary>
        /// Transforms the current XML file.
        /// </summary>
        internal static void Transform()
        {
            IntPtr curScintilla;
            ArrayList Files = new ArrayList();
            StringBuilder CurrentFile = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, CurrentFile);
            XMLFileSelect frmFilesForm = new XMLFileSelect();

            for (int window = 0; window < 2; window++)
            {
                int currentdoc = -1; int newdoc = -1;
                while (currentdoc == newdoc)
                {
                    newdoc++;
                    Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_ACTIVATEDOC, window, newdoc);
                    currentdoc = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETCURRENTDOCINDEX, 0, window);

                    if (currentdoc == newdoc)
                    {
                        curScintilla = PluginBase.GetCurrentScintilla();
                        StringBuilder path = new StringBuilder(Win32.MAX_PATH);
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);

                        bool bitNew = !Files.Contains(path);

                        if (bitNew)
                        {
                            Files.Add(path);
                            frmFilesForm.lbFiles.Items.Add(path);
                        }
                    }
                }
            }

            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SWITCHTOFILE, 0, CurrentFile.ToString());
            int i = frmFilesForm.GetResult();

            if (i > -1)
            {
                curScintilla = PluginBase.GetCurrentScintilla();
                int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);

                int j = 0;
                while (sb.ToString().Contains("<?xml-stylesheet"))
                {
                    j = sb.ToString().IndexOf("<?xml-stylesheet");
                    sb.Remove(j, sb.ToString().IndexOf("?>", j) - j + 2);
                }

                if (sb.ToString().Contains("<?xml "))
                    j = sb.ToString().IndexOf("?>", sb.ToString().IndexOf("<?xml ")) + 2;
                sb.Insert(j, "<?xml-stylesheet href=\"xslt.xslt\" type=\"text/xsl\"?>");
                File.WriteAllText(tmpFilesPath + "xml.xml", sb.ToString());

                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SWITCHTOFILE, 0, Files[i].ToString());

                curScintilla = PluginBase.GetCurrentScintilla();
                length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                sb = new StringBuilder(length);

                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                File.WriteAllText(tmpFilesPath + "xslt.xslt", sb.ToString());

                File.WriteAllText(tmpFilesPath + "html.html", "<!DOCTYPE html><html><frameset cols=\"*\"><frame src=\"xml.xml\" /></frameset></html>");

                System.Diagnostics.Process.Start(tmpFilesPath + "html.html");

                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SWITCHTOFILE, 0, CurrentFile.ToString());
            }
        }

        /// <summary>
        /// Verifies all open documents.
        /// </summary>
        internal static void VerifyAll()
        {
            //If Auto-Search is enabled, we have to disable it and reenable it after we're all done.
            bool bitAutoSearch = settings.AutoSearch;
            settings.AutoSearch = false;

            string outPut = "";
            IntPtr curScintilla;
            ArrayList FilePaths = new ArrayList();
            int index = 0;

            StringBuilder CurrentFile = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, CurrentFile);

            if (!settings.NewFile)
            {
                frmXPathResults.resultsTree.Nodes.Clear();
                frmXPathResults.VerifyResults = true;
            }

            for (int window = 0; window < 2; window++)
            {
                int currentdoc = -1; int newdoc = -1;
                while (currentdoc == newdoc)
                {
                    newdoc++;
                    Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_ACTIVATEDOC, window, newdoc);
                    currentdoc = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETCURRENTDOCINDEX, 0, window);

                    if (currentdoc == newdoc)
                    {
                        curScintilla = PluginBase.GetCurrentScintilla();
                        StringBuilder path = new StringBuilder(Win32.MAX_PATH);
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);

                        bool Verify = !FilePaths.Contains(path);

                        if (Verify)
                        {
                            if (!settings.Ignore)
                                Verify = false;

                            for (int i = 0; i < settings.Extensions.Count; i++)
                            {
                                if (settings.Ignore && path.ToString().EndsWith(settings.Extensions[i].ToString()))
                                    Verify = false;

                                if (!settings.Ignore && path.ToString().EndsWith(settings.Extensions[i].ToString()))
                                    Verify = true;
                            }

                            if (Verify)
                            {
                                string toOut = VerifyDocument(curScintilla);
                                outPut += path.ToString() + "\r\n" + toOut + "\r\n\r\n";

                                if (!settings.NewFile)
                                {
                                    TreeNode tNode = new TreeNode(index + ": " + Path.GetFileName(path.ToString()));
                                    tNode.Tag = path.ToString() + "|" + ((int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETCURRENTPOS, 0, 0)).ToString();
                                    frmXPathResults.resultsTree.Nodes.Add(tNode);
                                    tNode = new TreeNode(toOut);
                                    tNode.Tag = path.ToString() + "|" + ((int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETCURRENTPOS, 0, 0)).ToString();
                                    frmXPathResults.resultsTree.Nodes[frmXPathResults.resultsTree.Nodes.Count - 1].Nodes.Add(tNode);
                                }

                                FilePaths.Add(path);
                            }
                        }

                        index++;
                    }
                }
            }

            if (settings.NewFile)
            {
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_FILE_NEW);
                curScintilla = PluginBase.GetCurrentScintilla();
                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, outPut);
            }
            else
            {
                ShowXPatherizer(false, true);
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SWITCHTOFILE, 0, CurrentFile.ToString());
                frmXPathResults.resultsTree.ExpandAll();
            }

            //Re-enable Autosearch if it was on.
            settings.AutoSearch = bitAutoSearch;
        }

        /// <summary>
        /// Verifies the current document.
        /// </summary>
        internal static void VerifySingle()
        {
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();

            MessageBox.Show(VerifyDocument(curScintilla));
        }

        /// <summary>
        /// Opens a link to W3Schools for XPath Help.
        /// </summary>
        internal static void XPathHelp()
        {
            System.Diagnostics.Process.Start("https://www.w3schools.com/xml/xpath_intro.asp");
        }

        /// <summary>
        /// Auto indent and beutify the XML.
        /// </summary>
        /// <param name="ShowErrors">Do not show errors if false.</param>
        internal static void Beautify(bool ShowErrors)
        {
            XmlDocument XMLdoc = new XmlDocument();

            try
            {
                IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                string doc = sb.ToString();

                if (settings.IgnoreDocType)
                    XMLdoc.XmlResolver = null;
                XMLdoc.LoadXml(doc.Replace("&", "&amp;"));

                bool encoding = false;
                XmlDeclaration XMLdec = null;

                if (XMLdoc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    encoding = true;
                    // Get the encoding declaration.
                    XMLdec = (XmlDeclaration)XMLdoc.FirstChild;
                }

                sb = new StringBuilder();
                XmlWriterSettings xwsettings = new XmlWriterSettings();
                xwsettings.Indent = settings.Indent;
                xwsettings.IndentChars = "";
                for (int i = 0; i < settings.IndentCount; i++)
                    xwsettings.IndentChars += settings.IndentChar;
                xwsettings.NewLineChars = "\r\n";
                xwsettings.NewLineHandling = NewLineHandling.Replace;
                xwsettings.OmitXmlDeclaration = true;
                XmlWriter writer = XmlWriter.Create(sb, xwsettings);
                XMLdoc.Save(writer);
                writer.Close();

                if (encoding)
                {
                    string enc = "<?xml";
                    if (XMLdec.Version != "")
                        enc += " version=\"" + XMLdec.Version + "\"";
                    if (XMLdec.Encoding != "")
                        enc += " encoding=\"" + XMLdec.Encoding + "\"";
                    if (XMLdec.Standalone != "")
                        enc += " standalone=\"" + XMLdec.Standalone + "\"";

                    if (enc != "<?xml")
                    {
                        enc += "?>\r\n";
                        sb.Insert(0, enc);
                    }
                }

                Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONSTART, 0, 0);
                Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONEND, length, 0);
                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, sb.Replace("&amp;", "&"));
                Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, 0, 0);
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_LANG_XML);
            }
            catch (XmlException xex)
            {
                if (ShowErrors)
                {
                    MessageBox.Show(xex.Message);
                    try
                    {
                        IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                        int startPos = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_POSITIONFROMLINE, xex.LineNumber - 1, 0) + xex.LinePosition - 1;
                        Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, startPos, 0);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Call Beautify with ShowErrors = true.
        /// </summary>
        internal static void Beautify()
        {
            Beautify(true);
        }

        /// <summary>
        /// Begin search with the current XPath statement(s).
        /// If any text is selected in the XPath Search box, it will be used by itself.
        /// </summary>
        internal static void BeginSearch()
        {
            frmXPathSearch.BeginSearch();
        }

        /// <summary>
        /// Initialize the XPatherizer forms.
        /// Load the temporary XPath data.
        /// </summary>
        internal static void InitializeForms()
        {
            if (frmXPathSearch == null)
            {
                frmXPathSearch = new XPathSearchForm();
            }
            if (frmXPathResults == null)
            {
                frmXPathResults = new XPathResultsForm();
            }
            if (tempData == null)
            {
                tempData = new XPathTemp(tmpFilePath);
            }
        }

        /// <summary>
        /// Show both XPatherizer windows.
        /// </summary>
        internal static void ShowXPatherizer()
        {
            ShowXPatherizer(true, true);
        }

        /// <summary>
        /// Show XPatherizer Windows
        /// </summary>
        /// <param name="ShowSearch">Show the Serach window</param>
        /// <param name="ShowResults">Show the Results window</param>
        internal static void ShowXPatherizer(bool ShowSearch = true, bool ShowResults = true)
        {
            if (ShowSearch)
            {
                //Attempting to load through messaging system
                //Appears that forms were being initialized in a different way for older versions of NPP
                //InitializeForms();
                // {
                    if (!frmXPathSearch.HasBeenShown)
                    {
                        NppTbData _nppTbData = new NppTbData();
                        _nppTbData.hClient = frmXPathSearch.Handle;
                        _nppTbData.pszName = "XPath Search";
                        _nppTbData.dlgID = idSearchDlg;
                        _nppTbData.uMask = NppTbMsg.DWS_DF_CONT_TOP | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR;
                        _nppTbData.hIconTab = (uint)Icon.FromHandle(Properties.Resources.XPNPPIcon16.GetHicon()).Handle;
                        _nppTbData.pszModuleName = PluginName;
                        IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
                        Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);

                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
                        frmXPathSearch.HasBeenShown = true;
                    }
                    else
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMSHOW, 0, frmXPathSearch.Handle);
                }

            if (ShowResults)
            {
                //Can results be shown separately from search box??
                //Appears that forms were being initialized in a different way for older versions of NPP
                //InitializeForms();
                // if (!(frmXPathSearch == null))
                // {
                if (!frmXPathResults.HasBeenShown)
                    {
                        NppTbData _nppTbData = new NppTbData();
                        _nppTbData.hClient = frmXPathResults.Handle;
                        _nppTbData.pszName = "XPath Results";
                        _nppTbData.dlgID = idResultsDlg;
                        _nppTbData.uMask = NppTbMsg.DWS_DF_CONT_RIGHT | NppTbMsg.DWS_ICONTAB | NppTbMsg.DWS_ICONBAR;
                        _nppTbData.hIconTab = (uint)Icon.FromHandle(Properties.Resources.XPNPPIcon216.GetHicon()).Handle;
                        _nppTbData.pszModuleName = PluginName;
                        IntPtr _ptrNppTbData = Marshal.AllocHGlobal(Marshal.SizeOf(_nppTbData));
                        Marshal.StructureToPtr(_nppTbData, _ptrNppTbData, false);

                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMREGASDCKDLG, 0, _ptrNppTbData);
                        frmXPathResults.HasBeenShown = true;
                    }
                    else
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_DMMSHOW, 0, frmXPathResults.Handle);
                //}
                }

            if (ShowSearch)
                LoadXPath();
        }

        /// <summary>
        /// Load an XPML (XPath XML) file.
        /// </summary>
        internal static void LoadXPML()
        {
            if (openFileDialog1 == null)
                openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XPML Files|*.xpml|All Files|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.CheckFileExists)
                {
                    ShowXPatherizer();

                    Stream file = openFileDialog1.OpenFile();
                    BinaryReader br = new BinaryReader(file);
                    string strXPath = br.ReadString();
                    if (File.Exists(openFileDialog1.FileName + ".xml"))
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_DOOPEN, 0, openFileDialog1.FileName + ".xml");
                        IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                        int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                        StringBuilder sb = new StringBuilder(length);
                        Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                        string fromfile = br.ReadString();

                        if (fromfile != sb.ToString())
                        {
                            Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONSTART, 0, 0);
                            Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONEND, length, 0);
                            Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, fromfile);
                        }
                        Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, 0, 0);
                    }
                    else
                    {
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_FILE_NEW);
                        IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                        Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, br.ReadString());
                        Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_SAVECURRENTFILEAS, 0, openFileDialog1.FileName + ".xml");
                    }
                    frmXPathSearch.txtXPath.Text = strXPath;
                    br.Close();
                    file.Close();
                }
            }
        }

        /// <summary>
        /// Save an XMPL file with the current XPath and XML.
        /// </summary>
        internal static void SaveXPML()
        {
            if (saveFileDialog1 == null)
                saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XPML Files|*.xpml|All Files|*.*";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);

                Stream file = saveFileDialog1.OpenFile();
                BinaryWriter bw = new BinaryWriter(file);
                bw.Write(frmXPathSearch.txtXPath.Text);
                bw.Write(sb.ToString());
                bw.Flush();
                bw.Close();
                file.Close();
            }
        }

        /// <summary>
        /// Show the Settings Dialog.
        /// </summary>
        internal static void ShowSettings()
        {
            if (frmXPathSettings == null)
            {
                frmXPathSettings = new XPatherizerSettingsForm();

                frmXPathSettings.LoadData();
                frmXPathSettings.Show();
            }
            else
                frmXPathSettings.Visible = true;
        }

        /// <summary>
        /// Show the About Dialog.
        /// </summary>
        internal static void ShowAbout()
        {
            if (frmXPathAbout == null)
            {
                frmXPathAbout = new XPatherizerAboutForm();
                frmXPathAbout.Show();
            }
            else
                frmXPathAbout.Visible = true;
        }
        #endregion

        #region " Main Methods "
        /// <summary>
        /// Verify a single document and return the results.
        /// </summary>
        /// <param name="curScintilla">The Scintilla of the document to validate.</param>
        /// <returns>Validated if good, otherwise the error message.</returns>
        public static string VerifyDocument(IntPtr curScintilla)
        {
            XmlDocument XMLdoc = new XmlDocument();
            try
            {
                int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                string doc = sb.ToString();

                if (settings.IgnoreDocType)
                    XMLdoc.XmlResolver = null;
                XMLdoc.LoadXml(doc.Replace("&", "&amp;"));
            }
            catch (XmlException xex)
            {
                try
                {
                    int startPos = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_POSITIONFROMLINE, xex.LineNumber - 1, 0) + xex.LinePosition - 1;
                    Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, startPos, 0);
                }
                catch { }

                return xex.Message;
            }
            return "Validated.";
        }

        /// <summary>
        /// Hides the About Dialog.
        /// </summary>
        public static void HideAbout()
        {
            frmXPathAbout.Visible = false;
        }

        /// <summary>
        /// Removes all default namespaces from the document.
        /// </summary>
        /// <param name="XMLdoc">The document</param>
        /// <returns>The document without default namespaces.</returns>
        public static XmlDocument RemoveDefaultNamespaces(XmlDocument XMLdoc)
        {
            XmlNodeList Nodes = XMLdoc.SelectNodes("//namespace::*[name()='']");
            string xml = XMLdoc.OuterXml;

            foreach (XmlNode nod in Nodes)
            {
                string s = "xmlns=\"" + nod.InnerText + "\"";
                xml = xml.Replace(s, "");
            }

            XMLdoc.LoadXml(xml);
            return XMLdoc;
        }

        /// <summary>
        /// Dynamically builds an XmlNamespaceManager.
        /// </summary>
        /// <param name="Node">The Node to parse.</param>
        /// <param name="xnm">The XmlNameSpaceManager to add NameSpaces to.</param>
        /// <returns>The XmlNameSapceManager with all NameSpaces in it.</returns>
        internal static XmlNamespaceManager GetNameSpaces(XmlNode Node, XmlNamespaceManager xnm)
        {
            foreach (XmlAttribute attr in Node.Attributes)
            {
                if (attr.Name.StartsWith("xmlns:") || attr.Name == "xmlns")
                {
                    string prefix = attr.Name;
                    string uri = attr.Value;

                    if (prefix.Contains(":"))
                        prefix = prefix.Split(':')[1];
                    else if (settings.defaultNamespace != "")
                    {
                        if (DNSCount == 1)
                            prefix = settings.defaultNamespace;
                        else
                            prefix = settings.defaultNamespace + DNSCount;

                        DNSCount++;
                    }
                    else
                        prefix = "";

                    xnm.AddNamespace(prefix, uri);
                }
            }
            if (Node.HasChildNodes)
                foreach (XmlNode ChildNode in Node.ChildNodes)
                    if (ChildNode.NodeType == XmlNodeType.Element)
                        xnm = GetNameSpaces(ChildNode, xnm);
            return xnm;
        }

        /// <summary>
        /// Dynamically builds an XmlNamespaceManager.
        /// </summary>
        /// <param name="xpn">The Navigator used to find namespaces.</param>
        /// <param name="xnm">The XmlNameSpaceManager to add NameSpaces to.</param>
        /// <returns>The XmlNameSapceManager with all NameSpaces in it.</returns>
        internal static XmlNamespaceManager GetNameSpaces(XPathNavigator xpn, XmlNamespaceManager xnm)
        {
            foreach (XPathNavigator nod in xpn.Select("descendant::*"))
            {
                foreach (KeyValuePair<string, string> kvp in nod.GetNamespacesInScope(XmlNamespaceScope.Local))
                {
                    string prefix = kvp.Key;
                    if (prefix == "" && settings.defaultNamespace != "")
                    {
                        if (DNSCount == 1)
                            prefix = settings.defaultNamespace;
                        else
                            prefix = settings.defaultNamespace + DNSCount;
                        DNSCount++;
                    }

                    xnm.AddNamespace(prefix, kvp.Value);
                }
            }
            return xnm;
        }

        /// <summary>
        /// Return the text to display in the results tree.
        /// </summary>
        /// <param name="outerxml">OuterXML of the current node.</param>
        /// <returns>Text to display.</returns>
        internal static string NodetoText(string outerxml)
        {
            string toret = "";
            if (outerxml.StartsWith("<"))
            {
                if (outerxml.StartsWith("<![CDATA["))
                {
                    toret = "CDATA: " + outerxml.Substring(9, outerxml.IndexOf("]]>") - 9).Replace("&amp;", "&");
                }
                else if (outerxml.StartsWith("<!--"))
                {
                    toret = "Comment: " + outerxml.Substring(4, outerxml.IndexOf("-->") - 4).Replace("&amp;", "&");
                }
                else if (outerxml.StartsWith("<?xml"))
                {
                    toret = "XML Declaration";
                }
                else if (outerxml.StartsWith("<!DOCTYPE"))
                {
                    toret = "DOCTYPE";
                }
                else
                {
                    char[] chars = new char[2];
                    chars[0] = '>';
                    chars[1] = ' ';

                    int endIndex = outerxml.IndexOf(">");

                    toret = outerxml.Substring(1, outerxml.IndexOfAny(chars) - 1);
                    for (int i = 0; i < settings.NodeTypes.Count; i++)
                    {
                        string strAtt = settings.AttributeNames[i].ToString();
                        if ((toret.StartsWith(settings.NodeTypes[i].ToString()) || settings.NodeTypes[i].ToString() == "") &&
                            outerxml.IndexOf(strAtt + "=") != -1 && outerxml.IndexOf(strAtt + "=") < endIndex)
                        {
                            int startIndex = outerxml.IndexOf(strAtt + "=") + strAtt.Length + 2;
                            toret += " (" + settings.TextToShow[i].ToString() +
                                outerxml.Substring(startIndex, outerxml.IndexOf("\"", startIndex) - startIndex) + ")";
                        }
                    }
                }
            }
            else
                toret = "Text: " + outerxml.Replace("&amp;", "&");
            return toret;
        }

        /// <summary>
        /// Returns an XPath string that points directly to the node.
        /// </summary>
        /// <param name="node">The node to create an XPath string for.</param>
        /// <returns>The XPath string for the node.</returns>
        internal static string FindXPath(XmlNode node, bool DefaultRemoved)
        {
            StringBuilder builder = new StringBuilder();
            while (node != null)
            {
                int index = 1;
                try
                {
                    index = FindElementIndex(node);
                }
                catch { }
                switch (node.NodeType)
                {
                    case XmlNodeType.Attribute:
                        if (node.Name == "xmlns" || node.Name.StartsWith("xmlns:"))
                        {
                            if (((XmlAttribute)node).OwnerElement != null)
                            {
                                string name = node.Name.Remove(0, 5);
                                if (name.StartsWith(":"))
                                    name = name.Remove(0, 1);
                                builder.Insert(0, "/namespace::*[name()='" + name + "']");
                                node = ((XmlAttribute)node).OwnerElement;
                            }
                            else
                                return "/*";
                        }
                        else
                        {
                            builder.Insert(0, "/@" + node.Name);
                            node = ((XmlAttribute)node).OwnerElement;
                        }
                        break;
                    case XmlNodeType.Element:
                        if ((node.GetPrefixOfNamespace(node.NamespaceURI) == "" && node.NamespaceURI != "") ||
                            DefaultRemoved && node.NamespaceURI == "")
                            builder.Insert(0, "/*[local-name() = '" + node.Name + "']" + "[" + index + "]");
                        else
                            builder.Insert(0, "/" + node.Name + "[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Text:
                        builder.Insert(0, "/text()");
                        if (index > 1)
                            builder.Append("[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.CDATA:
                        builder.Insert(0, "/CDATA()");
                        if (index > 1)
                            builder.Append("[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Comment:
                        builder.Insert(0, "/comment()");
                        if (index > 1)
                            builder.Append("[" + index + "]");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        return builder.ToString();
                    case XmlNodeType.XmlDeclaration:
                        builder.Insert(0, "/XMLDeclaration()");
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.DocumentType:
                        builder.Insert(0, "/DocType()");
                        node = node.ParentNode;
                        break;
                    default:
                        MessageBox.Show("ERROR: Unhandled Node Type detected: " + node.NodeType);
                        node = node.ParentNode;
                        break;
                }
            }
            MessageBox.Show("An unknown error has occured in FindXPath (XpatherizerNPP plugin).");
            return "/*";
        }

        /// <summary>
        /// Returns the index of the element.
        /// </summary>
        /// <param name="element">The element to find the index for.</param>
        /// <returns>The index of the element.</returns>
        internal static int FindElementIndex(XmlNode element)
        {
            XmlNode parentNode = element.ParentNode;
            if (parentNode is XmlDocument)
            {
                return 1;
            }
            XmlElement parent = (XmlElement)parentNode;
            int index = 1;
            foreach (XmlNode candidate in parent.ChildNodes)
            {
                if (candidate.NodeType == element.NodeType && candidate.Name == element.Name)
                {
                    if (candidate == element)
                    {
                        return index;
                    }
                    index++;
                }
            }
            return 1;
        }

        /// <summary>
        /// Locates the data in the XML to highlight and highlights it.
        /// </summary>
        /// <param name="xPath">The XPath string that points to the specific data to highlight</param>
        public static void Highlight(string xPath)
        {
            if (frmXPathResults.VerifyResults)
            {
                string filename = xPath.Split('|')[0];
                int startPos = int.Parse(xPath.Split('|')[1]);

                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_SWITCHTOFILE, 0, filename);
                StringBuilder CurrentFile = new StringBuilder(Win32.MAX_PATH);
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, CurrentFile);

                if (CurrentFile.ToString() == filename)
                {
                    IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                    Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, startPos, 0);
                }
                else
                {
                    MessageBox.Show("The file is no longer open in Notepad++.");
                }
            }
            else
            {
                if (xPath.Contains("/CDATA()"))
                {
                    xPath = xPath.Replace("/CDATA()", "/text()");
                }
                else if (xPath.Contains("/XMLDeclaration()") || xPath.Contains("/DocType()"))
                {
                    return;
                }

                IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                string doc = sb.ToString();

                MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(doc.Replace("&", "&amp;")));
                XmlTextReader xtr = new XmlTextReader(ms);

                if (settings.IgnoreDocType)
                    xtr.XmlResolver = null;

                XPathDocument xpd = new XPathDocument(xtr);
                xtr.Close();
                ms.Close();

                XPathNavigator xpn = xpd.CreateNavigator();

                XmlNamespaceManager xnm = new XmlNamespaceManager(xpn.NameTable);
                DNSCount = 1;
                xnm = GetNameSpaces(xpn, xnm);

                XPathNodeIterator xpni = xpn.Select(xPath, xnm);
                xpni.MoveNext();
                IXmlLineInfo lineInfo = xpni.Current as IXmlLineInfo;

                int startPos = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_POSITIONFROMLINE, lineInfo.LineNumber - 1, 0) + lineInfo.LinePosition - 1;
                int endPos = 0;

                switch (xpni.Current.NodeType)
                {
                    case XPathNodeType.Attribute:
                        endPos = xpni.Current.Value.Length + 3 + xpni.Current.Name.Length;
                        break;
                    case XPathNodeType.Element:
                    case XPathNodeType.ProcessingInstruction:
                    case XPathNodeType.Root:
                        endPos = xpni.Current.Name.Length;
                        break;
                    case XPathNodeType.Comment:
                    case XPathNodeType.SignificantWhitespace:
                    case XPathNodeType.Text:
                    case XPathNodeType.Whitespace:
                        endPos = xpni.Current.Value.Length;
                        break;
                    case XPathNodeType.Namespace:
                        endPos = xpni.Current.Value.Length + 8;
                        if (xpni.Current.Name.Length > 0)
                            endPos += 1 + xpni.Current.Name.Length;
                        break;
                }

                //MessageBox.Show(xPath + "\r\n" + lineInfo.LineNumber + " - " + lineInfo.LinePosition + "\r\n" + xpni.Current.NodeType.ToString() + "\r\n" +
                //    xpni.Current.Value + " - " + xpni.Current.Value.Length + "\r\n" + xpni.Current.Name + " - " + xpni.Current.Name.Length + "\r\n" + startPos + " - " + endPos);

                ms.Close();

                endPos += startPos;

                if (endPos == startPos)
                {
                    MessageBox.Show("Node not found in document.  Please run Search again.\r\n" + xPath);
                }
                else
                {
                    Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, endPos, 0);
                    Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, startPos, 0);
                    Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONSTART, startPos, 0);
                    Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONEND, endPos, 0);
                }
            }
        }

        //This listener appears to be deprecated. Duplicating this code into BeNotified
        /// <summary>
        /// Listener for notifications from NPP.
        /// </summary>
        /// <param name="code">The parameter sent from NPP.</param>
        public static void OnNotification(ScNotification nc)
        {
            //This conversion may be duplicate from the 'beNotified' function in the UnmanagedExports.cs callback.
            //ScNotification nc = (ScNotification)Marshal.PtrToStructure(notifyCode, typeof(ScNotification));
            if (nc.Header.Code == (uint)NppMsg.NPPN_READY)
            {
                //NPP is done loading.

                //COMMENTED OUT FOR DEBUG - not sure how this is being invoked anymore.
                //Create the XPatherizer windows and load the data file.
                InitializeForms();

                //Show the XPatherizer windows.
                if (settings.AutoLoad)
                    ShowXPatherizer();

                int nbFile = (int)Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETNBOPENFILES, 0, 0);
                ClikeStringArray cStrArray = new ClikeStringArray(nbFile, Win32.MAX_PATH);
                if (Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETOPENFILENAMES, cStrArray.NativePointer, nbFile) != IntPtr.Zero)
                    tempData.Verify(new ArrayList(cStrArray.ManagedStringsUnicode));

                //Load the stored XPath for the current file.
                LoadXPath();
            }
            else if (nc.Header.Code == (uint)NppMsg.NPPN_BUFFERACTIVATED)
            {
                if (tempData != null)
                {
                    //Save the current XPath to the temp file.
                    SaveXPath();
                    //Load the stored XPath for the current file.
                    LoadXPath();
                }
            }
            else if (nc.Header.Code == (uint)NppMsg.NPPN_FILEOPENED)
            {
                if (tempData != null)
                {
                    //Save the current XPath to the temp file.
                    SaveXPath();
                    //Load the stored XPath for the current file.
                    LoadXPath();
                }
            }
            else if (nc.Header.Code == (uint)NppMsg.NPPN_FILECLOSED)
            {
                if (tempData != null)
                {
                    //Save the current XPath to the temp file.
                    SaveXPath();
                    //Load the stored XPath for the current file.
                    LoadXPath();
                }
            }
            else
            {
                /*
                long i;
                if (!long.TryParse(((NppMsg)nc.nmhdr.code).ToString(), out i))
                    System.Windows.Forms.MessageBox.Show(((NppMsg)nc.nmhdr.code).ToString());
                */
            }
        }

        /// <summary>
        /// Loads the XPath of the current file.
        /// </summary>
        internal static void LoadXPath()
        {
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();
            StringBuilder path = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_GETFULLCURRENTPATH, 0, path);

            frmXPathSearch.txtXPath.Text = tempData.Load(path.ToString());
        }

        /// <summary>
        /// Saves the XPath of the previous file.
        /// </summary>
        internal static void SaveXPath()
        {
            tempData.Save(frmXPathSearch.txtXPath.Text);
        }

        /// <summary>
        /// Exports the OuterXML of the selected items in the results tree to a new file.
        /// </summary>
        /// <param name="xPaths">An ArrayList of XPath strings that point to the data to export.</param>
        public static void ExportXML(ArrayList xPaths)
        {
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();
            int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
            StringBuilder sb = new StringBuilder(length);
            Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);

            XmlDocument XMLdoc = new XmlDocument();
            if (settings.IgnoreDocType)
                XMLdoc.XmlResolver = null;
            XMLdoc.LoadXml(sb.ToString());
            XmlNamespaceManager xnm = new XmlNamespaceManager(XMLdoc.NameTable);
            DNSCount = 1;
            xnm = GetNameSpaces(XMLdoc.SelectSingleNode("/*"), xnm);

            string toExport = "<root>";
            foreach (string xPathstr in xPaths)
            {
                string xPath = xPathstr.Replace("/CDATA()", "/text()");
                if (xPath.StartsWith("X:"))
                {
                    xPath = xPath.Substring(2, xPath.Length - 2);
                    XmlNodeList NodeList = XMLdoc.SelectNodes(xPath, xnm);
                    if (NodeList.Count > 0)
                    {
                        toExport += "<XPatherizerResult XPathstring=\"" + xPath + "\">";
                        foreach (XmlNode Node in NodeList)
                        {
                            if (Node.OuterXml != "")
                                toExport += Node.OuterXml + "\r\n";
                        }
                        toExport += "</XPatherizerResult>";
                    }
                }
                else
                {
                    XmlNode XMLNode = XMLdoc.SelectSingleNode(xPath, xnm);
                    if (XMLNode.OuterXml != null && XMLNode.OuterXml.Trim() != "")
                    {
                        toExport += "<XPatherizerResult XPathstring=\"" + xPath + "\">";
                        toExport += XMLNode.OuterXml + "</XPatherizerResult>\r\n";
                    }
                }
            }
            if (toExport != "<root>")
            {
                toExport += "</root>";
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_FILE_NEW);
                curScintilla = PluginBase.GetCurrentScintilla();
                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, toExport);
                Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_LANG_XML);
                Beautify(false);
            }
        }

        /// <summary>
        /// Removes Nodes that match the xPath expressions.
        /// </summary>
        /// <param name="xPaths">An ArrayList of xPath expressions</param>
        public static void RemoveNodes(ArrayList xPaths)
        {
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();
            int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
            StringBuilder sb = new StringBuilder(length);
            Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);

            XmlDocument XMLdoc = new XmlDocument();
            if (settings.IgnoreDocType)
                XMLdoc.XmlResolver = null;
            XMLdoc.LoadXml(sb.ToString());

            XmlNamespaceManager xnm = new XmlNamespaceManager(XMLdoc.NameTable);
            DNSCount = 1;
            xnm = GetNameSpaces(XMLdoc.SelectSingleNode("/*"), xnm);

            foreach (string xPathstr in xPaths)
            {
                string xPath = xPathstr.Replace("/CDATA()", "/text()");
                if (xPath.StartsWith("X:"))
                {
                    xPath = xPath.Substring(2, xPath.Length - 2);
                    XmlNodeList NodeList = XMLdoc.SelectNodes(xPath, xnm);

                    if (NodeList.Count == 0)
                        MessageBox.Show("Error: Node(s) not found in XML Document.");

                    foreach (XmlNode Node in NodeList)
                    {
                        RemoveNode(Node);
                    }
                }
                else
                {
                    XmlNode Node = XMLdoc.SelectSingleNode(xPath, xnm);
                    if (Node != null)
                        RemoveNode(Node);
                    else
                        MessageBox.Show("Error: Node not found in XML Document.");
                }
            }
            Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONSTART, 0, 0);
            Win32.SendMessage(curScintilla, SciMsg.SCI_SETSELECTIONEND, length, 0);
            Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, XMLdoc.OuterXml.Replace("&amp;", "&"));
            Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, 0, 0);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint) NppMsg.NPPM_MENUCOMMAND, 0, NppMenuCmd.IDM_LANG_XML);
            Beautify(false);
        }

        /// <summary>
        /// Removes a single Node.
        /// </summary>
        /// <param name="nod">The node to remove.</param>
        public static void RemoveNode(XmlNode nod)
        {
            if (nod != null)
            {
                switch (nod.NodeType)
                {
                    case XmlNodeType.Attribute:
                        ((XmlNode)((XmlAttribute)nod).OwnerElement).Attributes.Remove(((XmlAttribute)nod));
                        break;
                    default:
                        nod.ParentNode.RemoveChild(nod);
                        break;
                }
            }
        }

        /// <summary>
        /// Loads the Settings from the settings file.
        /// </summary>
        internal static void LoadSettings()
        {
            settings = new XPatherizerSettings();
            if (File.Exists(settingsFilePath))
            {
                FileStream file = File.OpenRead(settingsFilePath);
                BinaryReader reader = new BinaryReader(file);
                string version = "";
                int MajorVersion = 0;
                int MinorVersion = 0;
                try
                {
                    version = reader.ReadDouble().ToString();
                    MajorVersion = int.Parse(version.Split('.')[0]);
                    MinorVersion = int.Parse(version.Split('.')[1]);
                }
                catch
                {
                    reader.BaseStream.Position = 0;
                    version = reader.ReadString();
                    MajorVersion = int.Parse(version.Split('.')[0]);
                    MinorVersion = int.Parse(version.Split('.')[1]);
                }
                if (MajorVersion >= 2 && MinorVersion >= 8)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    settings.AutoSearch = reader.ReadBoolean();
                    settings.Indent = reader.ReadBoolean();
                    settings.IndentChar = reader.ReadString();
                    settings.IndentCount = reader.ReadInt32();
                    settings.IgnoreDocType = reader.ReadBoolean();
                    settings.defaultNamespace = reader.ReadString();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.Ignore = reader.ReadBoolean();
                    settings.NewFile = reader.ReadBoolean();
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.Extensions.Add(reader.ReadString());
                    }
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string MenuItemName = reader.ReadString();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._key = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isAlt = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isCtrl = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isShift = reader.ReadByte();
                    }
                }
                else if (MajorVersion >= 2 && MinorVersion >= 6)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    settings.Indent = reader.ReadBoolean();
                    settings.IndentChar = reader.ReadString();
                    settings.IndentCount = reader.ReadInt32();
                    settings.IgnoreDocType = reader.ReadBoolean();
                    settings.defaultNamespace = reader.ReadString();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.Ignore = reader.ReadBoolean();
                    settings.NewFile = reader.ReadBoolean();
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.Extensions.Add(reader.ReadString());
                    }
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string MenuItemName = reader.ReadString();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._key = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isAlt = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isCtrl = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isShift = reader.ReadByte();
                    }
                }
                else if (MajorVersion >= 2 && MinorVersion >= 5)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    settings.IgnoreDocType = reader.ReadBoolean();
                    settings.defaultNamespace = reader.ReadString();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.Ignore = reader.ReadBoolean();
                    settings.NewFile = reader.ReadBoolean();
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.Extensions.Add(reader.ReadString());
                    }
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        string MenuItemName = reader.ReadString();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._key = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isAlt = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isCtrl = reader.ReadByte();
                        settings.nppMenuItems.Item(MenuItemName).Shortcut._isShift = reader.ReadByte();
                    }
                }
                else if (MajorVersion >= 2 && MinorVersion >= 3)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    settings.IgnoreDocType = reader.ReadBoolean();
                    settings.defaultNamespace = reader.ReadString();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.nppMenuItems.LoadDefaults();
                }
                else if (MajorVersion >= 2 && MinorVersion >= 2)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    settings.defaultNamespace = reader.ReadString();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.IgnoreDocType = true;
                    settings.nppMenuItems.LoadDefaults();
                }
                else if (MajorVersion >= 2)
                {
                    settings.AutoLoad = reader.ReadBoolean();
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        settings.NodeTypes.Add(reader.ReadString());
                        settings.AttributeNames.Add(reader.ReadString());
                        settings.TextToShow.Add(reader.ReadString());
                    }
                    settings.IgnoreDocType = true;
                    settings.nppMenuItems.LoadDefaults();
                }
                reader.Close();
                file.Close();
            }
            else
            {
                settings.AutoLoad = false;
                settings.IgnoreDocType = true;
                settings.nppMenuItems.LoadDefaults();
            }
        }

        /// <summary>
        /// Save the settings to the settings file.
        /// </summary>
        internal static void SaveSettings()
        {
            FileStream file = File.Create(settingsFilePath);
            BinaryWriter writer = new BinaryWriter(file);
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            version = version.Substring(0, version.IndexOf(".", version.IndexOf(".") + 1));
            writer.Write(version);
            writer.Write(settings.AutoLoad);
            writer.Write(settings.AutoSearch);
            writer.Write(settings.Indent);
            writer.Write(settings.IndentChar);
            writer.Write(settings.IndentCount);
            writer.Write(settings.IgnoreDocType);
            writer.Write(settings.defaultNamespace);
            writer.Write(settings.NodeTypes.Count);
            for (int i = 0; i < settings.NodeTypes.Count; i++)
            {
                writer.Write(settings.NodeTypes[i].ToString());
                writer.Write(settings.AttributeNames[i].ToString());
                writer.Write(settings.TextToShow[i].ToString());
            }
            writer.Write(settings.Ignore);
            writer.Write(settings.NewFile);
            writer.Write(settings.Extensions.Count);
            for (int i = 0; i < settings.Extensions.Count; i++)
            {
                writer.Write(settings.Extensions[i].ToString());
            }
            writer.Write(settings.nppMenuItems.CountNotBlank);
            for (int i = 0; i < settings.nppMenuItems.Count; i++)
            {
                MenuItem mi = settings.nppMenuItems.Item(i);
                if (mi.SettingName != "")
                {
                    writer.Write(mi.SettingName);
                    writer.Write(mi.Shortcut._key);
                    writer.Write(mi.Shortcut._isAlt);
                    writer.Write(mi.Shortcut._isCtrl);
                    writer.Write(mi.Shortcut._isShift);
                }
            }
            writer.Close();
            file.Close();
        }

        /// <summary>
        /// Retrieves the settings from the XPatherizerSettingsForm.
        /// </summary>
        public static void GetSettingsFromDlg(bool Hide)
        {
            settings.AutoLoad = frmXPathSettings.cbAutoLoad.Checked;
            settings.AutoSearch = frmXPathSettings.cbAutoSearch.Checked;
            settings.Indent = frmXPathSettings.cbIndent.Checked;
            if (frmXPathSettings.ddlCharacter.Text == "Space")
                settings.IndentChar = " ";
            else
                settings.IndentChar = "\t";
            settings.IndentCount = (int)frmXPathSettings.nudAmount.Value;
            settings.IgnoreDocType = frmXPathSettings.cbDTD.Checked;
            settings.defaultNamespace = frmXPathSettings.txtDefaultNS.Text;
            settings.NodeTypes = new ArrayList();
            settings.AttributeNames = new ArrayList();
            settings.TextToShow = new ArrayList();
            for (int i = 0; i < frmXPathSettings.dgvNodeSettings.Rows.Count - 1; i++)
            {
                if (frmXPathSettings.dgvNodeSettings.Rows[i].Cells[1].Value != null)
                {
                    if (frmXPathSettings.dgvNodeSettings.Rows[i].Cells[0].Value != null)
                        settings.NodeTypes.Add(frmXPathSettings.dgvNodeSettings.Rows[i].Cells[0].Value.ToString());
                    else
                        settings.NodeTypes.Add("");
                    settings.AttributeNames.Add(frmXPathSettings.dgvNodeSettings.Rows[i].Cells[1].Value.ToString());
                    if (frmXPathSettings.dgvNodeSettings.Rows[i].Cells[2].Value != null)
                        settings.TextToShow.Add(frmXPathSettings.dgvNodeSettings.Rows[i].Cells[2].Value.ToString());
                    else
                        settings.TextToShow.Add("");
                }
            }
            settings.Extensions = new ArrayList();
            settings.Ignore = frmXPathSettings.rbtnIgnore.Checked;
            settings.NewFile = frmXPathSettings.rbtnNewFile.Checked;
            for (int i = 0; i < frmXPathSettings.dgvExtensions.Rows.Count - 1; i++)
            {
                if (frmXPathSettings.dgvExtensions.Rows[i].Cells[0].Value != null && frmXPathSettings.dgvExtensions.Rows[i].Cells[0].Value.ToString() != "'")
                {
                    settings.Extensions.Add(frmXPathSettings.dgvExtensions.Rows[i].Cells[0].Value.ToString());
                }
            }
            settings.nppMenuItems = frmXPathSettings.SettingsMenuItems;
            SaveSettings();
            if (Hide)
                HideSettings();
        }

        /// <summary>
        /// Hides the XPatherizerSettingsForm. 
        /// </summary>
        public static void HideSettings()
        {
            frmXPathSettings.Visible = false;
        }
        #endregion
    }
}