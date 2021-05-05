namespace DLN.UserControls
{
    partial class OxyDailyDln
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
            this.plotDln = new OxyPlot.WindowsForms.PlotView();
            this.SuspendLayout();
            // 
            // plotDln
            // 
            this.plotDln.Cursor = System.Windows.Forms.Cursors.Cross;
            this.plotDln.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotDln.Location = new System.Drawing.Point(0, 0);
            this.plotDln.Name = "plotDln";
            this.plotDln.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDln.Size = new System.Drawing.Size(842, 453);
            this.plotDln.TabIndex = 0;
            this.plotDln.Text = "DLN";
            this.plotDln.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDln.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDln.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // OxyDailyDln
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.plotDln);
            this.Name = "OxyDailyDln";
            this.Size = new System.Drawing.Size(842, 453);
            this.ResumeLayout(false);

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotDln;
    }
}
