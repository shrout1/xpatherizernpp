using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
//using NppPluginNET;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    public partial class XPathResultsForm : Form
    {
        public bool HasBeenShown;
        public ArrayList SelectedNodes;
        public TreeNode LastSelectedNode;
        public bool VerifyResults;
        private bool bitRestart;

        public XPathResultsForm()
        {
            InitializeComponent();
            HasBeenShown = false;
            SelectedNodes = new ArrayList();
            VerifyResults = false;
            HideStopButton();
        }

        public void ShowStopButton()
        {
            panel1.Visible = true;
        }

        public void HideStopButton()
        {
            panel1.Visible = false;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            resultsTree.ExpandAll();
        }

        private void btnHideAll_Click(object sender, EventArgs e)
        {
            resultsTree.CollapseAll();
        }

        private void resultsTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (ModifierKeys == Keys.Control && !VerifyResults)
            {
                if (SelectedNodes.Contains(e.Node))
                {
                    SelectedNodes.Remove(e.Node);
                    e.Node.BackColor = resultsTree.BackColor;
                    e.Node.ForeColor = resultsTree.ForeColor;
                    e.Cancel = true;
                }
            }
        }

        private void resultsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ModifierKeys == Keys.Control && !VerifyResults)
            {
                SelectedNodes.Add(e.Node);
                e.Node.BackColor = SystemColors.Highlight;
                e.Node.ForeColor = SystemColors.HighlightText;
            }
            if (ModifierKeys != Keys.Control && !VerifyResults)
            {
                foreach (TreeNode Node in SelectedNodes)
                {
                    Node.BackColor = resultsTree.BackColor;
                    Node.ForeColor = resultsTree.ForeColor;
                }
                SelectedNodes.Clear();
                SelectedNodes.Add(e.Node);
                e.Node.BackColor = SystemColors.Highlight;
                e.Node.ForeColor = SystemColors.HighlightText;
            }
            if (e.Node.Tag != null && e.Node.Tag.ToString() != "" && !(e.Node.Tag.ToString().StartsWith("X:")))
            {
                Main.Highlight(e.Node.Tag.ToString());
            }
        }

        private void exportResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyResults)
            {
                ArrayList XPaths = new ArrayList();
                foreach (TreeNode Node in SelectedNodes)
                {
                    if (Node.Tag != null && Node.Tag.ToString() != "")
                        XPaths.Add(Node.Tag.ToString());
                }
                if (XPaths.Count > 0)
                    Main.ExportXML(XPaths);
            }
        }

        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!VerifyResults)
            {
                ArrayList XPaths = new ArrayList();
                foreach (TreeNode Node in SelectedNodes)
                {
                    if (Node.Tag != null && Node.Tag.ToString() != "")
                        XPaths.Add(Node.Tag.ToString());
                }
                if (XPaths.Count > 0)
                    Main.RemoveNodes(XPaths);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        public delegate void workerProgress(ProgressReporter progress);

        public void ReportProgress(ProgressReporter progress)
        {
            if (progress.strNodeText != null)
            {
                if (progress.strParnetNodeText != null)
                    resultsTree.Nodes.Find(progress.strParnetNodeText, false)[0].Nodes.Add(progress.strNodeText);
                else
                {
                    progress.treeNode = new TreeNode(progress.strNodeText);
                    progress.treeNode.Name = progress.strNodeText;
                    resultsTree.Nodes.Add(progress.treeNode);
                }
            }
            else
            {
                if (progress.strParnetNodeText != null)
                    resultsTree.Nodes.Find(progress.strParnetNodeText, false)[0].Nodes.Add(progress.treeNode);
                else
                {
                    progress.treeNode.Name = progress.treeNode.Text;
                    resultsTree.Nodes.Add(progress.treeNode);
                }
            }
        }

        public void BeginSearch(string[] xpathStrings)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
                bitRestart = true;
            }
            else
            {
                resultsTree.Nodes.Clear();
                VerifyResults = false;
                ShowStopButton();
                worker.RunWorkerAsync(xpathStrings);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressReporter progress = e.UserState as ProgressReporter;
            resultsTree.Invoke(new workerProgress(ReportProgress), new object[] { progress });
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            resultsTree.Nodes.RemoveAt(0);
            HideStopButton();
            if (bitRestart)
            {
                bitRestart = false;
                Main.BeginSearch();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DoSearch((string[])e.Argument);
        }

        /// <summary>
        /// Perform an XPath search.
        /// </summary>
        /// <param name="xpathStrings">List of strings to search against.</param>
        public void DoSearch(string[] xpathStrings)
        {
            try
            {
                worker.ReportProgress(0, new ProgressReporter("Processing..."));
                XmlDocument XMLdoc = new XmlDocument();
                try
                {
                    IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                    int length = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_GETLENGTH, 0, 0) + 1;
                    StringBuilder sb = new StringBuilder(length);
                    Win32.SendMessage(curScintilla, SciMsg.SCI_GETTEXT, length, sb);
                    string doc = sb.ToString();

                    if (Main.settings.IgnoreDocType)
                        XMLdoc.XmlResolver = null;
                    XMLdoc.LoadXml(doc.Replace("&", "&amp;"));
                }
                catch (XmlException xex)
                {
                    worker.ReportProgress(0, new ProgressReporter("Document ERROR"));
                    worker.ReportProgress(0, new ProgressReporter(xex.Message, "Document ERROR"));
                    try
                    {
                        IntPtr curScintilla = PluginBase.GetCurrentScintilla();
                        int startPos = (int)Win32.SendMessage(curScintilla, SciMsg.SCI_POSITIONFROMLINE, xex.LineNumber - 1, 0) + xex.LinePosition - 1;
                        Win32.SendMessage(curScintilla, SciMsg.SCI_GOTOPOS, startPos, 0);
                    }
                    catch { }
                    return;
                }

                bool bitDefaultRemoved = false;

                if (Main.settings.defaultNamespace == "")
                {
                    int Before = XMLdoc.OuterXml.Length;
                    XMLdoc = Main.RemoveDefaultNamespaces(XMLdoc);
                    if (XMLdoc.OuterXml.Length != Before)
                        bitDefaultRemoved = true;
                }

                XPathNavigator navigator = XMLdoc.CreateNavigator();
                XmlNamespaceManager xnm = new XmlNamespaceManager(XMLdoc.NameTable);
                Main.DNSCount = 1;
                xnm = Main.GetNameSpaces(XMLdoc.SelectSingleNode("/*"), xnm);
                XPathExpression xpe;
                object result;
                string strres = "";
                System.Xml.XPath.XPathResultType xprt = System.Xml.XPath.XPathResultType.Any;

                int i = 1;

                foreach (string s in xpathStrings)
                {
                    if (worker.CancellationPending) return;
                    string xPath = s;
                    //xPath Comments support.
                    //I know xPath 1.0 does not support these, but it is nice to be able to store comments at least in NPP.
                    while (xPath.Contains("(:") && xPath.Contains(":)"))
                    {
                        int intStart = xPath.IndexOf("(:");
                        int intEnd = xPath.IndexOf(":)", i);

                        if (intEnd <= intStart)
                            intEnd = xPath.Length - 2;

                        xPath = xPath.Remove(intStart, intEnd - intStart + 2);
                    }
                    if (xPath != "")
                    {
                        try
                        {
                            xpe = XPathExpression.Compile(xPath, xnm);
                            result = navigator.Evaluate(xpe);
                            xprt = xpe.ReturnType;
                            strres = result.ToString();
                        }
                        catch (System.Xml.XPath.XPathException xpx)
                        {
                            worker.ReportProgress(0, new ProgressReporter(i + ": ERROR"));
                            worker.ReportProgress(0, new ProgressReporter(xpx.Message.Replace("'" + xPath + "'", "The xPath statement"), i + ": ERROR"));
                            i++;
                            continue;
                        }
                        if (xprt == System.Xml.XPath.XPathResultType.NodeSet)
                        {
                            XPathNodeIterator xpni = navigator.Select(xPath, xnm);
                            string ss = "s";
                            try
                            {
                                if (xpni.Count == 1) ss = "";
                            }
                            catch (Exception ex)
                            {
                                worker.ReportProgress(0, new ProgressReporter(i + ": ERROR"));
                                worker.ReportProgress(0, new ProgressReporter(ex.Message, i + ": ERROR"));
                                i++;
                                continue;
                            }
                            TreeNode tNode = new TreeNode(i + ": " + xpni.Count + " Hit" + ss);
                            tNode.Tag = "X:" + xPath;
                            worker.ReportProgress(0, new ProgressReporter(tNode));
                            while (xpni.MoveNext())
                            {
                                if (worker.CancellationPending) return;
                                if (xpni.Current is IHasXmlNode)
                                {
                                    XmlNode Node = ((IHasXmlNode)xpni.Current).GetNode();
                                    string pos = Main.FindXPath(Node, bitDefaultRemoved);
                                    string NodeText = Main.NodetoText(xpni.Current.OuterXml);
                                    if (NodeText.StartsWith("Text") || NodeText.StartsWith("CDATA"))
                                        tNode = new TreeNode(NodeText);
                                    else
                                    {
                                        TreeNode[] childNodes = GetChildren(Node, bitDefaultRemoved);
                                        if (childNodes != null)
                                            tNode = new TreeNode(NodeText, childNodes);
                                    }
                                    tNode.Tag = pos;
                                    worker.ReportProgress(0, new ProgressReporter(tNode, i + ": " + xpni.Count + " Hit" + ss));
                                }
                            }
                        }
                        else
                        {
                            worker.ReportProgress(0, new ProgressReporter(i + ": " + xprt + ": " + strres));
                        }
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(0, new ProgressReporter("An unexpected error has occured: " + ex.Message));
            }
        }

        /// <summary>
        /// Gets data for all direct children of Node.
        /// </summary>
        /// <param name="Node">The Parent Node</param>
        /// <returns>A collection of child nodes.</returns>
        internal TreeNode[] GetChildren(XmlNode Node, bool bitDefaultRemoved)
        {
            TreeNode[] treeNodes;
            int j = 0;
            if (Node.Attributes != null)
            {
                treeNodes = new TreeNode[Node.ChildNodes.Count + Node.Attributes.Count];
                foreach (XmlNode att in Node.Attributes)
                {
                    TreeNode tNode = new TreeNode("@" + att.Name + ": " + att.Value);
                    tNode.Tag = Main.FindXPath(att, bitDefaultRemoved);
                    treeNodes[j] = tNode;
                    j++;
                    if (worker.CancellationPending) return new TreeNode[] { new TreeNode("Search has been Canceled.") };
                }
            }
            else
                treeNodes = new TreeNode[Node.ChildNodes.Count];
            foreach (XmlNode cNode in Node.ChildNodes)
            {
                TreeNode tNode = null;
                if (Main.NodetoText(cNode.OuterXml).StartsWith("Text") || Main.NodetoText(cNode.OuterXml).StartsWith("CDATA"))
                    tNode = new TreeNode(Main.NodetoText(cNode.OuterXml));
                else
                {
                    TreeNode[] childNodes = GetChildren(cNode, bitDefaultRemoved);
                    if (childNodes != null)
                        tNode = new TreeNode(Main.NodetoText(cNode.OuterXml), childNodes);
                }
                if (tNode != null)
                {
                    tNode.Tag = Main.FindXPath(cNode, bitDefaultRemoved);
                    treeNodes[j] = tNode;
                }
                j++;
                if (worker.CancellationPending) return new TreeNode[] { new TreeNode("Search has been Canceled.") };
            }
            return treeNodes;
        }

        private void resultsTree_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode tNode = resultsTree.GetNodeAt(e.Location);
            if (tNode != null && resultsTree.SelectedNode == tNode)
            {
                if (tNode.Tag != null && tNode.Tag.ToString() != "" && !(tNode.Tag.ToString().StartsWith("X:")))
                {
                    Main.Highlight(tNode.Tag.ToString());
                } 
            }
        }
    }

    public class ProgressReporter
    {
        public string strParnetNodeText;
        public string strNodeText;
        public TreeNode treeNode;

        public ProgressReporter(string strText)
        {
            strNodeText = strText;
        }

        public ProgressReporter(string strText, string strParentText)
        {
            strNodeText = strText;
            strParnetNodeText = strParentText;
        }

        public ProgressReporter(TreeNode tNode)
        {
            treeNode = tNode;
        }

        public ProgressReporter(TreeNode tNode, string strParentText)
        {
            treeNode = tNode;
            strParnetNodeText = strParentText;
        }
    }
}
