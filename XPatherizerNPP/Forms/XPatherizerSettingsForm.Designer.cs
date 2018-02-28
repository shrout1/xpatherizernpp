namespace Kbg.NppPluginNET
{
    public partial class XPatherizerSettingsForm
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
            this.cbAutoLoad = new System.Windows.Forms.CheckBox();
            this.dgvNodeSettings = new System.Windows.Forms.DataGridView();
            this.clmNodeNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAttributesNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDisplayTexts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDefaultNS = new System.Windows.Forms.TextBox();
            this.cbDTD = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbAutoSearch = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbIndent = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ddlCharacter = new System.Windows.Forms.ComboBox();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnXPathResults = new System.Windows.Forms.RadioButton();
            this.rbtnNewFile = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.rbtnAllow = new System.Windows.Forms.RadioButton();
            this.rbtnIgnore = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.dgvExtensions = new System.Windows.Forms.DataGridView();
            this.Extension = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbShift = new System.Windows.Forms.CheckBox();
            this.cbCtrl = new System.Windows.Forms.CheckBox();
            this.cbAlt = new System.Windows.Forms.CheckBox();
            this.ddlKeys = new System.Windows.Forms.ComboBox();
            this.ddlMenuOptions = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeSettings)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtensions)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbAutoLoad
            // 
            this.cbAutoLoad.AutoSize = true;
            this.cbAutoLoad.Location = new System.Drawing.Point(4, 6);
            this.cbAutoLoad.Name = "cbAutoLoad";
            this.cbAutoLoad.Size = new System.Drawing.Size(234, 17);
            this.cbAutoLoad.TabIndex = 0;
            this.cbAutoLoad.Text = "Auto-Show XPatherizer windows on Startup.";
            this.cbAutoLoad.UseVisualStyleBackColor = true;
            // 
            // dgvNodeSettings
            // 
            this.dgvNodeSettings.AllowUserToOrderColumns = true;
            this.dgvNodeSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNodeSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmNodeNames,
            this.clmAttributesNames,
            this.clmDisplayTexts});
            this.dgvNodeSettings.Location = new System.Drawing.Point(4, 119);
            this.dgvNodeSettings.Name = "dgvNodeSettings";
            this.dgvNodeSettings.Size = new System.Drawing.Size(360, 187);
            this.dgvNodeSettings.TabIndex = 1;
            // 
            // clmNodeNames
            // 
            this.clmNodeNames.HeaderText = "Node Name";
            this.clmNodeNames.Name = "clmNodeNames";
            this.clmNodeNames.Width = 105;
            // 
            // clmAttributesNames
            // 
            this.clmAttributesNames.HeaderText = "Attribute Name*";
            this.clmAttributesNames.Name = "clmAttributesNames";
            this.clmAttributesNames.Width = 105;
            // 
            // clmDisplayTexts
            // 
            this.clmDisplayTexts.HeaderText = "Text to Display";
            this.clmDisplayTexts.Name = "clmDisplayTexts";
            this.clmDisplayTexts.Width = 105;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(211, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(49, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "&Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Attribute info to display on parent results node:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Default Namespace:";
            // 
            // txtDefaultNS
            // 
            this.txtDefaultNS.Location = new System.Drawing.Point(111, 77);
            this.txtDefaultNS.Name = "txtDefaultNS";
            this.txtDefaultNS.Size = new System.Drawing.Size(252, 20);
            this.txtDefaultNS.TabIndex = 7;
            // 
            // cbDTD
            // 
            this.cbDTD.AutoSize = true;
            this.cbDTD.Location = new System.Drawing.Point(187, 3);
            this.cbDTD.Name = "cbDTD";
            this.cbDTD.Size = new System.Drawing.Size(169, 17);
            this.cbDTD.TabIndex = 8;
            this.cbDTD.Text = "Ignore HTML DOCTPYE tags.";
            this.cbDTD.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(377, 338);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbAutoSearch);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.cbAutoLoad);
            this.tabPage1.Controls.Add(this.dgvNodeSettings);
            this.tabPage1.Controls.Add(this.txtDefaultNS);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(369, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbAutoSearch
            // 
            this.cbAutoSearch.AutoSize = true;
            this.cbAutoSearch.Location = new System.Drawing.Point(282, 6);
            this.cbAutoSearch.Name = "cbAutoSearch";
            this.cbAutoSearch.Size = new System.Drawing.Size(85, 17);
            this.cbAutoSearch.TabIndex = 17;
            this.cbAutoSearch.Text = "Auto-Search";
            this.cbAutoSearch.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.cbDTD);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.cbIndent);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.ddlCharacter);
            this.panel3.Controls.Add(this.nudAmount);
            this.panel3.Location = new System.Drawing.Point(0, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(364, 47);
            this.panel3.TabIndex = 15;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(35, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(100, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "Beautify Options";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(229, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 13);
            this.label17.TabIndex = 13;
            this.label17.Text = "Amount:";
            // 
            // cbIndent
            // 
            this.cbIndent.AutoSize = true;
            this.cbIndent.Location = new System.Drawing.Point(-1, 24);
            this.cbIndent.Name = "cbIndent";
            this.cbIndent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbIndent.Size = new System.Drawing.Size(56, 17);
            this.cbIndent.TabIndex = 9;
            this.cbIndent.Text = "Indent";
            this.cbIndent.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(72, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Character:";
            // 
            // ddlCharacter
            // 
            this.ddlCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCharacter.FormattingEnabled = true;
            this.ddlCharacter.Items.AddRange(new object[] {
            "Space",
            "Tab"});
            this.ddlCharacter.Location = new System.Drawing.Point(133, 22);
            this.ddlCharacter.Name = "ddlCharacter";
            this.ddlCharacter.Size = new System.Drawing.Size(90, 21);
            this.ddlCharacter.TabIndex = 10;
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(281, 22);
            this.nudAmount.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(75, 20);
            this.nudAmount.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.rbtnAllow);
            this.tabPage3.Controls.Add(this.rbtnIgnore);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.dgvExtensions);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(367, 312);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Verification";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnXPathResults);
            this.panel2.Controls.Add(this.rbtnNewFile);
            this.panel2.Location = new System.Drawing.Point(9, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(112, 72);
            this.panel2.TabIndex = 5;
            // 
            // rbtnXPathResults
            // 
            this.rbtnXPathResults.AutoSize = true;
            this.rbtnXPathResults.Location = new System.Drawing.Point(3, 27);
            this.rbtnXPathResults.Name = "rbtnXPathResults";
            this.rbtnXPathResults.Size = new System.Drawing.Size(92, 17);
            this.rbtnXPathResults.TabIndex = 1;
            this.rbtnXPathResults.TabStop = true;
            this.rbtnXPathResults.Text = "XPath Results";
            this.rbtnXPathResults.UseVisualStyleBackColor = true;
            // 
            // rbtnNewFile
            // 
            this.rbtnNewFile.AutoSize = true;
            this.rbtnNewFile.Location = new System.Drawing.Point(3, 4);
            this.rbtnNewFile.Name = "rbtnNewFile";
            this.rbtnNewFile.Size = new System.Drawing.Size(63, 17);
            this.rbtnNewFile.TabIndex = 0;
            this.rbtnNewFile.TabStop = true;
            this.rbtnNewFile.Text = "New file";
            this.rbtnNewFile.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 155);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Display results to:";
            // 
            // rbtnAllow
            // 
            this.rbtnAllow.AutoSize = true;
            this.rbtnAllow.Location = new System.Drawing.Point(12, 114);
            this.rbtnAllow.Name = "rbtnAllow";
            this.rbtnAllow.Size = new System.Drawing.Size(103, 17);
            this.rbtnAllow.TabIndex = 3;
            this.rbtnAllow.TabStop = true;
            this.rbtnAllow.Text = "Allow extensions";
            this.rbtnAllow.UseVisualStyleBackColor = true;
            // 
            // rbtnIgnore
            // 
            this.rbtnIgnore.AutoSize = true;
            this.rbtnIgnore.Location = new System.Drawing.Point(12, 91);
            this.rbtnIgnore.Name = "rbtnIgnore";
            this.rbtnIgnore.Size = new System.Drawing.Size(108, 17);
            this.rbtnIgnore.TabIndex = 2;
            this.rbtnIgnore.TabStop = true;
            this.rbtnIgnore.Text = "Ignore extensions";
            this.rbtnIgnore.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 65);
            this.label14.TabIndex = 1;
            this.label14.Text = "For Verify XML\r\n(All Documents)\r\n\r\nEnter the Extensions\r\nto ignore or allow.";
            // 
            // dgvExtensions
            // 
            this.dgvExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtensions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Extension});
            this.dgvExtensions.Location = new System.Drawing.Point(127, 3);
            this.dgvExtensions.Name = "dgvExtensions";
            this.dgvExtensions.Size = new System.Drawing.Size(237, 306);
            this.dgvExtensions.TabIndex = 0;
            // 
            // Extension
            // 
            this.Extension.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Extension.HeaderText = "Extension";
            this.Extension.Name = "Extension";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cbShift);
            this.tabPage2.Controls.Add(this.cbCtrl);
            this.tabPage2.Controls.Add(this.cbAlt);
            this.tabPage2.Controls.Add(this.ddlKeys);
            this.tabPage2.Controls.Add(this.ddlMenuOptions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(367, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Shortcut Keys";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 249);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(341, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "For a list of the Keys and what they actually are, click the following link:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(0, 262);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(371, 35);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://msdn.microsoft.com/en-us/library/system.windows.forms.keys.aspx";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(34, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(274, 17);
            this.label12.TabIndex = 14;
            this.label12.Text = "*** CHANGE AT YOUR OWN RISK ***";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(107, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(222, 52);
            this.label11.TabIndex = 13;
            this.label11.Text = "Changing shortcut keys can have adverse\r\naffects on your ability to use Notepad++" +
                ".\r\nThe author of this plugin is not responsible\r\nfor any adverse affects this fe" +
                "ature may have.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(6, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "** WARNING **";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(55, 212);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(232, 26);
            this.label9.TabIndex = 11;
            this.label9.Text = "Changing Shortcut Keys will not take affect until\r\nNotepad++ is restarted.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(11, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Note:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Shift:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(58, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Control:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Alt:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Menu Option:";
            // 
            // cbShift
            // 
            this.cbShift.AutoSize = true;
            this.cbShift.Location = new System.Drawing.Point(107, 182);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(15, 14);
            this.cbShift.TabIndex = 4;
            this.cbShift.UseVisualStyleBackColor = true;
            this.cbShift.CheckedChanged += new System.EventHandler(this.cbShift_CheckedChanged);
            // 
            // cbCtrl
            // 
            this.cbCtrl.AutoSize = true;
            this.cbCtrl.Location = new System.Drawing.Point(107, 141);
            this.cbCtrl.Name = "cbCtrl";
            this.cbCtrl.Size = new System.Drawing.Size(15, 14);
            this.cbCtrl.TabIndex = 3;
            this.cbCtrl.UseVisualStyleBackColor = true;
            this.cbCtrl.CheckedChanged += new System.EventHandler(this.cbCtrl_CheckedChanged);
            // 
            // cbAlt
            // 
            this.cbAlt.AutoSize = true;
            this.cbAlt.Location = new System.Drawing.Point(107, 161);
            this.cbAlt.Name = "cbAlt";
            this.cbAlt.Size = new System.Drawing.Size(15, 14);
            this.cbAlt.TabIndex = 2;
            this.cbAlt.UseVisualStyleBackColor = true;
            this.cbAlt.CheckedChanged += new System.EventHandler(this.cbAlt_CheckedChanged);
            // 
            // ddlKeys
            // 
            this.ddlKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlKeys.FormattingEnabled = true;
            this.ddlKeys.Location = new System.Drawing.Point(107, 114);
            this.ddlKeys.Name = "ddlKeys";
            this.ddlKeys.Size = new System.Drawing.Size(121, 21);
            this.ddlKeys.TabIndex = 1;
            this.ddlKeys.SelectedIndexChanged += new System.EventHandler(this.ddlKeys_SelectedIndexChanged);
            // 
            // ddlMenuOptions
            // 
            this.ddlMenuOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlMenuOptions.FormattingEnabled = true;
            this.ddlMenuOptions.Location = new System.Drawing.Point(107, 87);
            this.ddlMenuOptions.Name = "ddlMenuOptions";
            this.ddlMenuOptions.Size = new System.Drawing.Size(201, 21);
            this.ddlMenuOptions.TabIndex = 0;
            this.ddlMenuOptions.SelectedIndexChanged += new System.EventHandler(this.ddlMenuOptions_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 32);
            this.panel1.TabIndex = 10;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(292, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // XPatherizerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 369);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XPatherizerSettingsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "XPatherizer Settings";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeSettings)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtensions)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckBox cbAutoLoad;
        public System.Windows.Forms.DataGridView dgvNodeSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNodeNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAttributesNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDisplayTexts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDefaultNS;
        public System.Windows.Forms.CheckBox cbDTD;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbShift;
        private System.Windows.Forms.CheckBox cbCtrl;
        private System.Windows.Forms.CheckBox cbAlt;
        private System.Windows.Forms.ComboBox ddlKeys;
        private System.Windows.Forms.ComboBox ddlMenuOptions;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        public System.Windows.Forms.RadioButton rbtnAllow;
        public System.Windows.Forms.RadioButton rbtnIgnore;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.DataGridView dgvExtensions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Extension;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.RadioButton rbtnXPathResults;
        public System.Windows.Forms.RadioButton rbtnNewFile;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.CheckBox cbIndent;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.ComboBox ddlCharacter;
        public System.Windows.Forms.NumericUpDown nudAmount;
        public System.Windows.Forms.CheckBox cbAutoSearch;
    }
}