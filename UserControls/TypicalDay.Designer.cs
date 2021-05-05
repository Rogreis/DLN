namespace DLN.UserControls
{
    partial class TypicalDay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerTypicalDay = new System.Windows.Forms.SplitContainer();
            this.dateTimePickerInicial = new System.Windows.Forms.DateTimePicker();
            this.btCalculaDiaTipico = new System.Windows.Forms.Button();
            this.dateTimePickerFinal = new System.Windows.Forms.DateTimePicker();
            this.btTestNewGraph = new System.Windows.Forms.Button();
            this.oxyDailyDlnTypicalDay = new DLN.UserControls.OxyDailyDln();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTypicalDay)).BeginInit();
            this.splitContainerTypicalDay.Panel1.SuspendLayout();
            this.splitContainerTypicalDay.Panel2.SuspendLayout();
            this.splitContainerTypicalDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerTypicalDay
            // 
            this.splitContainerTypicalDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTypicalDay.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerTypicalDay.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTypicalDay.Name = "splitContainerTypicalDay";
            this.splitContainerTypicalDay.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTypicalDay.Panel1
            // 
            this.splitContainerTypicalDay.Panel1.Controls.Add(this.button1);
            this.splitContainerTypicalDay.Panel1.Controls.Add(this.btTestNewGraph);
            this.splitContainerTypicalDay.Panel1.Controls.Add(this.dateTimePickerInicial);
            this.splitContainerTypicalDay.Panel1.Controls.Add(this.btCalculaDiaTipico);
            this.splitContainerTypicalDay.Panel1.Controls.Add(this.dateTimePickerFinal);
            // 
            // splitContainerTypicalDay.Panel2
            // 
            this.splitContainerTypicalDay.Panel2.Controls.Add(this.oxyDailyDlnTypicalDay);
            this.splitContainerTypicalDay.Size = new System.Drawing.Size(909, 485);
            this.splitContainerTypicalDay.SplitterDistance = 52;
            this.splitContainerTypicalDay.TabIndex = 3;
            // 
            // dateTimePickerInicial
            // 
            this.dateTimePickerInicial.Location = new System.Drawing.Point(13, 17);
            this.dateTimePickerInicial.Name = "dateTimePickerInicial";
            this.dateTimePickerInicial.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerInicial.TabIndex = 1;
            // 
            // btCalculaDiaTipico
            // 
            this.btCalculaDiaTipico.Location = new System.Drawing.Point(449, 16);
            this.btCalculaDiaTipico.Name = "btCalculaDiaTipico";
            this.btCalculaDiaTipico.Size = new System.Drawing.Size(119, 23);
            this.btCalculaDiaTipico.TabIndex = 3;
            this.btCalculaDiaTipico.Text = "Calculate";
            this.btCalculaDiaTipico.UseVisualStyleBackColor = true;
            this.btCalculaDiaTipico.Click += new System.EventHandler(this.BtCalculaDiaTipico_Click);
            // 
            // dateTimePickerFinal
            // 
            this.dateTimePickerFinal.Location = new System.Drawing.Point(231, 17);
            this.dateTimePickerFinal.Name = "dateTimePickerFinal";
            this.dateTimePickerFinal.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFinal.TabIndex = 2;
            // 
            // btTestNewGraph
            // 
            this.btTestNewGraph.Location = new System.Drawing.Point(574, 16);
            this.btTestNewGraph.Name = "btTestNewGraph";
            this.btTestNewGraph.Size = new System.Drawing.Size(119, 23);
            this.btTestNewGraph.TabIndex = 4;
            this.btTestNewGraph.Text = "Histogram";
            this.btTestNewGraph.UseVisualStyleBackColor = true;
            this.btTestNewGraph.Click += new System.EventHandler(this.BtTestNewGraph_Click);
            // 
            // oxyDailyDlnTypicalDay
            // 
            this.oxyDailyDlnTypicalDay.BackColor = System.Drawing.Color.White;
            this.oxyDailyDlnTypicalDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.oxyDailyDlnTypicalDay.Location = new System.Drawing.Point(0, 0);
            this.oxyDailyDlnTypicalDay.Name = "oxyDailyDlnTypicalDay";
            this.oxyDailyDlnTypicalDay.Size = new System.Drawing.Size(909, 429);
            this.oxyDailyDlnTypicalDay.TabIndex = 0;
            this.oxyDailyDlnTypicalDay.Load += new System.EventHandler(this.OxyDailyDlnTypicalDay_Load);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(699, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Histogram";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TypicalDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerTypicalDay);
            this.Name = "TypicalDay";
            this.Size = new System.Drawing.Size(909, 485);
            this.Load += new System.EventHandler(this.TypicalDay_Load);
            this.splitContainerTypicalDay.Panel1.ResumeLayout(false);
            this.splitContainerTypicalDay.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTypicalDay)).EndInit();
            this.splitContainerTypicalDay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerTypicalDay;
        private System.Windows.Forms.DateTimePicker dateTimePickerInicial;
        private System.Windows.Forms.Button btCalculaDiaTipico;
        private System.Windows.Forms.DateTimePicker dateTimePickerFinal;
        private OxyDailyDln oxyDailyDlnTypicalDay;
        private System.Windows.Forms.Button btTestNewGraph;
        private System.Windows.Forms.Button button1;
    }
}
