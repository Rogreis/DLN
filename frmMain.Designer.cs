namespace DLN
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.lblData = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageDadosEntrada = new System.Windows.Forms.TabPage();
            this.splitContainerDadosEntrada = new System.Windows.Forms.SplitContainer();
            this.groupBoxTimeInterval = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownEndHour = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownStartHour = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPointP = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownPointPAzimuth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPointPAngle = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLatitude = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLongitude = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAzimuth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btDLN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelLatiLong = new System.Windows.Forms.LinkLabel();
            this.lblLat = new System.Windows.Forms.Label();
            this.btFind = new System.Windows.Forms.Button();
            this.textBoxLocal = new System.Windows.Forms.TextBox();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.ucGoogleMapCoordenadas = new DLN.ucMap();
            this.tabOxyGraph = new System.Windows.Forms.TabPage();
            this.splitContainerDaily = new System.Windows.Forms.SplitContainer();
            this.hScrollBarDailyLigth = new System.Windows.Forms.HScrollBar();
            this.oxyDailyDln1 = new DLN.UserControls.OxyDailyDln();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.dataGridViewDln = new System.Windows.Forms.DataGridView();
            this.tabPageTypicalDay = new System.Windows.Forms.TabPage();
            this.typicalDay1 = new DLN.UserControls.TypicalDay();
            this.tabPageGraphicCommands = new System.Windows.Forms.TabPage();
            this.pictureBoxGraphicsHelp = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMensagens = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageDadosEntrada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDadosEntrada)).BeginInit();
            this.splitContainerDadosEntrada.Panel1.SuspendLayout();
            this.splitContainerDadosEntrada.Panel2.SuspendLayout();
            this.splitContainerDadosEntrada.SuspendLayout();
            this.groupBoxTimeInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartHour)).BeginInit();
            this.groupBoxPointP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPointPAzimuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPointPAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLatitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLongitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAzimuth)).BeginInit();
            this.tabOxyGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDaily)).BeginInit();
            this.splitContainerDaily.Panel1.SuspendLayout();
            this.splitContainerDaily.Panel2.SuspendLayout();
            this.splitContainerDaily.SuspendLayout();
            this.tabPageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDln)).BeginInit();
            this.tabPageTypicalDay.SuspendLayout();
            this.tabPageGraphicCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraphicsHelp)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.lblData);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1044, 609);
            this.splitContainerMain.SplitterDistance = 32;
            this.splitContainerMain.TabIndex = 3;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(12, 9);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(123, 13);
            this.lblData.TabIndex = 8;
            this.lblData.Text = "<nothing calculated yet>";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageDadosEntrada);
            this.tabControlMain.Controls.Add(this.tabOxyGraph);
            this.tabControlMain.Controls.Add(this.tabPageData);
            this.tabControlMain.Controls.Add(this.tabPageTypicalDay);
            this.tabControlMain.Controls.Add(this.tabPageGraphicCommands);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1044, 573);
            this.tabControlMain.TabIndex = 1;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPageDadosEntrada
            // 
            this.tabPageDadosEntrada.Controls.Add(this.splitContainerDadosEntrada);
            this.tabPageDadosEntrada.Location = new System.Drawing.Point(4, 22);
            this.tabPageDadosEntrada.Name = "tabPageDadosEntrada";
            this.tabPageDadosEntrada.Size = new System.Drawing.Size(1036, 547);
            this.tabPageDadosEntrada.TabIndex = 3;
            this.tabPageDadosEntrada.Text = "Data Entry";
            this.tabPageDadosEntrada.UseVisualStyleBackColor = true;
            // 
            // splitContainerDadosEntrada
            // 
            this.splitContainerDadosEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDadosEntrada.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDadosEntrada.Name = "splitContainerDadosEntrada";
            // 
            // splitContainerDadosEntrada.Panel1
            // 
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.groupBoxTimeInterval);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.groupBoxPointP);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.numericUpDownLatitude);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.numericUpDownLongitude);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.numericUpDownAzimuth);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.label5);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.label3);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.btDLN);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.label1);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.linkLabelLatiLong);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.lblLat);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.btFind);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.textBoxLocal);
            this.splitContainerDadosEntrada.Panel1.Controls.Add(this.lblLongitude);
            // 
            // splitContainerDadosEntrada.Panel2
            // 
            this.splitContainerDadosEntrada.Panel2.Controls.Add(this.ucGoogleMapCoordenadas);
            this.splitContainerDadosEntrada.Size = new System.Drawing.Size(1036, 547);
            this.splitContainerDadosEntrada.SplitterDistance = 304;
            this.splitContainerDadosEntrada.TabIndex = 0;
            // 
            // groupBoxTimeInterval
            // 
            this.groupBoxTimeInterval.Controls.Add(this.label6);
            this.groupBoxTimeInterval.Controls.Add(this.label7);
            this.groupBoxTimeInterval.Controls.Add(this.numericUpDownEndHour);
            this.groupBoxTimeInterval.Controls.Add(this.numericUpDownStartHour);
            this.groupBoxTimeInterval.Location = new System.Drawing.Point(10, 253);
            this.groupBoxTimeInterval.Name = "groupBoxTimeInterval";
            this.groupBoxTimeInterval.Size = new System.Drawing.Size(276, 76);
            this.groupBoxTimeInterval.TabIndex = 36;
            this.groupBoxTimeInterval.TabStop = false;
            this.groupBoxTimeInterval.Text = "Hours interval";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Start:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "End:";
            // 
            // numericUpDownEndHour
            // 
            this.numericUpDownEndHour.Location = new System.Drawing.Point(144, 38);
            this.numericUpDownEndHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownEndHour.Name = "numericUpDownEndHour";
            this.numericUpDownEndHour.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownEndHour.TabIndex = 31;
            this.numericUpDownEndHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownEndHour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // numericUpDownStartHour
            // 
            this.numericUpDownStartHour.Location = new System.Drawing.Point(4, 38);
            this.numericUpDownStartHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownStartHour.Name = "numericUpDownStartHour";
            this.numericUpDownStartHour.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownStartHour.TabIndex = 32;
            this.numericUpDownStartHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownStartHour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // groupBoxPointP
            // 
            this.groupBoxPointP.Controls.Add(this.label2);
            this.groupBoxPointP.Controls.Add(this.label4);
            this.groupBoxPointP.Controls.Add(this.numericUpDownPointPAzimuth);
            this.groupBoxPointP.Controls.Add(this.numericUpDownPointPAngle);
            this.groupBoxPointP.Location = new System.Drawing.Point(11, 167);
            this.groupBoxPointP.Name = "groupBoxPointP";
            this.groupBoxPointP.Size = new System.Drawing.Size(276, 80);
            this.groupBoxPointP.TabIndex = 35;
            this.groupBoxPointP.TabStop = false;
            this.groupBoxPointP.Text = "Point P";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Angle:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Azimuth:";
            // 
            // numericUpDownPointPAzimuth
            // 
            this.numericUpDownPointPAzimuth.DecimalPlaces = 6;
            this.numericUpDownPointPAzimuth.Location = new System.Drawing.Point(144, 38);
            this.numericUpDownPointPAzimuth.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownPointPAzimuth.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDownPointPAzimuth.Name = "numericUpDownPointPAzimuth";
            this.numericUpDownPointPAzimuth.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownPointPAzimuth.TabIndex = 31;
            this.numericUpDownPointPAzimuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownPointPAngle
            // 
            this.numericUpDownPointPAngle.DecimalPlaces = 6;
            this.numericUpDownPointPAngle.Location = new System.Drawing.Point(4, 38);
            this.numericUpDownPointPAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownPointPAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDownPointPAngle.Name = "numericUpDownPointPAngle";
            this.numericUpDownPointPAngle.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownPointPAngle.TabIndex = 32;
            this.numericUpDownPointPAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownLatitude
            // 
            this.numericUpDownLatitude.DecimalPlaces = 6;
            this.numericUpDownLatitude.Location = new System.Drawing.Point(15, 95);
            this.numericUpDownLatitude.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownLatitude.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDownLatitude.Name = "numericUpDownLatitude";
            this.numericUpDownLatitude.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownLatitude.TabIndex = 34;
            this.numericUpDownLatitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownLongitude
            // 
            this.numericUpDownLongitude.DecimalPlaces = 6;
            this.numericUpDownLongitude.Location = new System.Drawing.Point(155, 95);
            this.numericUpDownLongitude.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownLongitude.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDownLongitude.Name = "numericUpDownLongitude";
            this.numericUpDownLongitude.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownLongitude.TabIndex = 33;
            this.numericUpDownLongitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numericUpDownAzimuth
            // 
            this.numericUpDownAzimuth.DecimalPlaces = 6;
            this.numericUpDownAzimuth.Location = new System.Drawing.Point(15, 141);
            this.numericUpDownAzimuth.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownAzimuth.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDownAzimuth.Name = "numericUpDownAzimuth";
            this.numericUpDownAzimuth.Size = new System.Drawing.Size(121, 20);
            this.numericUpDownAzimuth.TabIndex = 30;
            this.numericUpDownAzimuth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "all values are in decimal degrees";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Azimuth:";
            // 
            // btDLN
            // 
            this.btDLN.Location = new System.Drawing.Point(158, 335);
            this.btDLN.Name = "btDLN";
            this.btDLN.Size = new System.Drawing.Size(121, 21);
            this.btDLN.TabIndex = 22;
            this.btDLN.Text = "Update Graphics";
            this.btDLN.UseVisualStyleBackColor = true;
            this.btDLN.Click += new System.EventHandler(this.BtDLN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Longitude:";
            // 
            // linkLabelLatiLong
            // 
            this.linkLabelLatiLong.AutoSize = true;
            this.linkLabelLatiLong.Location = new System.Drawing.Point(12, 56);
            this.linkLabelLatiLong.Name = "linkLabelLatiLong";
            this.linkLabelLatiLong.Size = new System.Drawing.Size(124, 13);
            this.linkLabelLatiLong.TabIndex = 19;
            this.linkLabelLatiLong.TabStop = true;
            this.linkLabelLatiLong.Text = "https://www.latlong.net/";
            this.linkLabelLatiLong.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelLatiLong_LinkClicked);
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Location = new System.Drawing.Point(12, 79);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(48, 13);
            this.lblLat.TabIndex = 17;
            this.lblLat.Text = "Latitude:";
            // 
            // btFind
            // 
            this.btFind.Location = new System.Drawing.Point(216, 56);
            this.btFind.Name = "btFind";
            this.btFind.Size = new System.Drawing.Size(60, 20);
            this.btFind.TabIndex = 16;
            this.btFind.Text = "Find";
            this.btFind.UseVisualStyleBackColor = true;
            this.btFind.Click += new System.EventHandler(this.BtFind_Click);
            // 
            // textBoxLocal
            // 
            this.textBoxLocal.Location = new System.Drawing.Point(15, 29);
            this.textBoxLocal.Name = "textBoxLocal";
            this.textBoxLocal.Size = new System.Drawing.Size(264, 20);
            this.textBoxLocal.TabIndex = 11;
            this.textBoxLocal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxLocal_KeyUp);
            // 
            // lblLongitude
            // 
            this.lblLongitude.AutoSize = true;
            this.lblLongitude.Location = new System.Drawing.Point(12, 13);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(36, 13);
            this.lblLongitude.TabIndex = 9;
            this.lblLongitude.Text = "Local:";
            // 
            // ucGoogleMapCoordenadas
            // 
            this.ucGoogleMapCoordenadas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGoogleMapCoordenadas.Location = new System.Drawing.Point(0, 0);
            this.ucGoogleMapCoordenadas.Name = "ucGoogleMapCoordenadas";
            this.ucGoogleMapCoordenadas.Size = new System.Drawing.Size(728, 547);
            this.ucGoogleMapCoordenadas.TabIndex = 0;
            // 
            // tabOxyGraph
            // 
            this.tabOxyGraph.Controls.Add(this.splitContainerDaily);
            this.tabOxyGraph.Location = new System.Drawing.Point(4, 22);
            this.tabOxyGraph.Name = "tabOxyGraph";
            this.tabOxyGraph.Size = new System.Drawing.Size(1036, 547);
            this.tabOxyGraph.TabIndex = 4;
            this.tabOxyGraph.Text = "Daily Light";
            this.tabOxyGraph.UseVisualStyleBackColor = true;
            // 
            // splitContainerDaily
            // 
            this.splitContainerDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDaily.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerDaily.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDaily.Name = "splitContainerDaily";
            this.splitContainerDaily.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDaily.Panel1
            // 
            this.splitContainerDaily.Panel1.Controls.Add(this.hScrollBarDailyLigth);
            // 
            // splitContainerDaily.Panel2
            // 
            this.splitContainerDaily.Panel2.Controls.Add(this.oxyDailyDln1);
            this.splitContainerDaily.Size = new System.Drawing.Size(1036, 547);
            this.splitContainerDaily.SplitterDistance = 52;
            this.splitContainerDaily.TabIndex = 5;
            // 
            // hScrollBarDailyLigth
            // 
            this.hScrollBarDailyLigth.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBarDailyLigth.Location = new System.Drawing.Point(0, 24);
            this.hScrollBarDailyLigth.Maximum = 374;
            this.hScrollBarDailyLigth.Minimum = 1;
            this.hScrollBarDailyLigth.Name = "hScrollBarDailyLigth";
            this.hScrollBarDailyLigth.Size = new System.Drawing.Size(1036, 28);
            this.hScrollBarDailyLigth.TabIndex = 9;
            this.hScrollBarDailyLigth.Value = 1;
            this.hScrollBarDailyLigth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDailyLigth_Scroll);
            // 
            // oxyDailyDln1
            // 
            this.oxyDailyDln1.BackColor = System.Drawing.Color.White;
            this.oxyDailyDln1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oxyDailyDln1.Location = new System.Drawing.Point(0, 0);
            this.oxyDailyDln1.Name = "oxyDailyDln1";
            this.oxyDailyDln1.Size = new System.Drawing.Size(1036, 491);
            this.oxyDailyDln1.TabIndex = 0;
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.dataGridViewDln);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(1036, 547);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Text = "Daily Light Values";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDln
            // 
            this.dataGridViewDln.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDln.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDln.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewDln.Name = "dataGridViewDln";
            this.dataGridViewDln.Size = new System.Drawing.Size(1030, 541);
            this.dataGridViewDln.TabIndex = 0;
            // 
            // tabPageTypicalDay
            // 
            this.tabPageTypicalDay.Controls.Add(this.typicalDay1);
            this.tabPageTypicalDay.Location = new System.Drawing.Point(4, 22);
            this.tabPageTypicalDay.Name = "tabPageTypicalDay";
            this.tabPageTypicalDay.Size = new System.Drawing.Size(1036, 547);
            this.tabPageTypicalDay.TabIndex = 5;
            this.tabPageTypicalDay.Text = "Typical Day";
            this.tabPageTypicalDay.UseVisualStyleBackColor = true;
            // 
            // typicalDay1
            // 
            this.typicalDay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typicalDay1.EntryData = null;
            this.typicalDay1.Location = new System.Drawing.Point(0, 0);
            this.typicalDay1.Name = "typicalDay1";
            this.typicalDay1.Size = new System.Drawing.Size(1036, 547);
            this.typicalDay1.TabIndex = 0;
            // 
            // tabPageGraphicCommands
            // 
            this.tabPageGraphicCommands.Controls.Add(this.pictureBoxGraphicsHelp);
            this.tabPageGraphicCommands.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraphicCommands.Name = "tabPageGraphicCommands";
            this.tabPageGraphicCommands.Size = new System.Drawing.Size(1036, 547);
            this.tabPageGraphicCommands.TabIndex = 6;
            this.tabPageGraphicCommands.Text = "Graphics Commands";
            this.tabPageGraphicCommands.UseVisualStyleBackColor = true;
            // 
            // pictureBoxGraphicsHelp
            // 
            this.pictureBoxGraphicsHelp.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGraphicsHelp.Image")));
            this.pictureBoxGraphicsHelp.Location = new System.Drawing.Point(55, 60);
            this.pictureBoxGraphicsHelp.Name = "pictureBoxGraphicsHelp";
            this.pictureBoxGraphicsHelp.Size = new System.Drawing.Size(711, 328);
            this.pictureBoxGraphicsHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxGraphicsHelp.TabIndex = 0;
            this.pictureBoxGraphicsHelp.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMensagens});
            this.statusStrip1.Location = new System.Drawing.Point(0, 609);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1044, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMensagens
            // 
            this.toolStripStatusLabelMensagens.Name = "toolStripStatusLabelMensagens";
            this.toolStripStatusLabelMensagens.Size = new System.Drawing.Size(1029, 17);
            this.toolStripStatusLabelMensagens.Spring = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 631);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmMain";
            this.Text = "Natural light availability";
            this.Activated += new System.EventHandler(this.FrmMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageDadosEntrada.ResumeLayout(false);
            this.splitContainerDadosEntrada.Panel1.ResumeLayout(false);
            this.splitContainerDadosEntrada.Panel1.PerformLayout();
            this.splitContainerDadosEntrada.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDadosEntrada)).EndInit();
            this.splitContainerDadosEntrada.ResumeLayout(false);
            this.groupBoxTimeInterval.ResumeLayout(false);
            this.groupBoxTimeInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartHour)).EndInit();
            this.groupBoxPointP.ResumeLayout(false);
            this.groupBoxPointP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPointPAzimuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPointPAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLatitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLongitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAzimuth)).EndInit();
            this.tabOxyGraph.ResumeLayout(false);
            this.splitContainerDaily.Panel1.ResumeLayout(false);
            this.splitContainerDaily.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDaily)).EndInit();
            this.splitContainerDaily.ResumeLayout(false);
            this.tabPageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDln)).EndInit();
            this.tabPageTypicalDay.ResumeLayout(false);
            this.tabPageGraphicCommands.ResumeLayout(false);
            this.tabPageGraphicCommands.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraphicsHelp)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMensagens;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageData;
        private System.Windows.Forms.TabPage tabPageDadosEntrada;
        private System.Windows.Forms.SplitContainer splitContainerDadosEntrada;
        private ucMap ucGoogleMapCoordenadas;
        private System.Windows.Forms.Label lblLongitude;
        private System.Windows.Forms.TabPage tabOxyGraph;
        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Button btFind;
        private System.Windows.Forms.TextBox textBoxLocal;
        private System.Windows.Forms.LinkLabel linkLabelLatiLong;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Button btDLN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewDln;
        private System.Windows.Forms.NumericUpDown numericUpDownPointPAngle;
        private System.Windows.Forms.NumericUpDown numericUpDownPointPAzimuth;
        private System.Windows.Forms.NumericUpDown numericUpDownAzimuth;
        private System.Windows.Forms.NumericUpDown numericUpDownLatitude;
        private System.Windows.Forms.NumericUpDown numericUpDownLongitude;
        private System.Windows.Forms.TabPage tabPageTypicalDay;
        private System.Windows.Forms.SplitContainer splitContainerDaily;
        private System.Windows.Forms.HScrollBar hScrollBarDailyLigth;
        private UserControls.OxyDailyDln oxyDailyDln1;
        private UserControls.TypicalDay typicalDay1;
        private System.Windows.Forms.TabPage tabPageGraphicCommands;
        private System.Windows.Forms.PictureBox pictureBoxGraphicsHelp;
        private System.Windows.Forms.GroupBox groupBoxTimeInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownEndHour;
        private System.Windows.Forms.NumericUpDown numericUpDownStartHour;
        private System.Windows.Forms.GroupBox groupBoxPointP;
    }
}

