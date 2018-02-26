namespace XPatherizerNPP
{
    partial class XPathResultsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHideAll = new System.Windows.Forms.Button();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.resultsTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHideAll);
            this.panel2.Controls.Add(this.btnShowAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.panel2.MinimumSize = new System.Drawing.Size(100, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(116, 21);
            this.panel2.TabIndex = 6;
            // 
            // btnHideAll
            // 
            this.btnHideAll.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHideAll.Location = new System.Drawing.Point(65, 0);
            this.btnHideAll.Name = "btnHideAll";
            this.btnHideAll.Size = new System.Drawing.Size(51, 21);
            this.btnHideAll.TabIndex = 1;
            this.btnHideAll.Text = "Hide All";
            this.btnHideAll.UseVisualStyleBackColor = true;
            this.btnHideAll.Click += new System.EventHandler(this.btnHideAll_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnShowAll.Location = new System.Drawing.Point(0, 0);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(56, 21);
            this.btnShowAll.TabIndex = 0;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // resultsTree
            // 
            this.resultsTree.ContextMenuStrip = this.contextMenuStrip1;
            this.resultsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTree.HideSelection = false;
            this.resultsTree.Location = new System.Drawing.Point(0, 42);
            this.resultsTree.MinimumSize = new System.Drawing.Size(100, 4);
            this.resultsTree.Name = "resultsTree";
            this.resultsTree.Size = new System.Drawing.Size(116, 220);
            this.resultsTree.TabIndex = 8;
            this.resultsTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.resultsTree_BeforeSelect);
            this.resultsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.resultsTree_AfterSelect);
            this.resultsTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.resultsTree_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportResultsToolStripMenuItem,
            this.removeSelectedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 48);
            // 
            // exportResultsToolStripMenuItem
            // 
            this.exportResultsToolStripMenuItem.Name = "exportResultsToolStripMenuItem";
            this.exportResultsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.exportResultsToolStripMenuItem.Text = "Export Selected";
            this.exportResultsToolStripMenuItem.Click += new System.EventHandler(this.exportResultsToolStripMenuItem_Click);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(116, 21);
            this.panel1.TabIndex = 7;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(0, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(116, 21);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Cancel";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // worker
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.worker_ProgressChanged);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // XPathResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(116, 262);
            this.Controls.Add(this.resultsTree);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "XPathResultsForm";
            this.Text = "XPathResultsForm";
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button btnHideAll;
        public System.Windows.Forms.Button btnShowAll;
        public System.Windows.Forms.TreeView resultsTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStop;
        public System.ComponentModel.BackgroundWorker worker;
    }
}