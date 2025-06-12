namespace ReadPressure_Demo2._0
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ComPort_cbx = new System.Windows.Forms.ComboBox();
            this.Trigger_Timer = new System.Windows.Forms.Timer(this.components);
            this.Pressure_label = new System.Windows.Forms.Label();
            this.send_btn = new System.Windows.Forms.Button();
            this.stop_btn = new System.Windows.Forms.Button();
            this.port_label = new System.Windows.Forms.Label();
            this.PressurePlot = new OxyPlot.WindowsForms.PlotView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gagueName_label = new System.Windows.Forms.Label();
            this.reCom_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComPort_cbx
            // 
            this.ComPort_cbx.FormattingEnabled = true;
            this.ComPort_cbx.Location = new System.Drawing.Point(84, 48);
            this.ComPort_cbx.Name = "ComPort_cbx";
            this.ComPort_cbx.Size = new System.Drawing.Size(121, 24);
            this.ComPort_cbx.TabIndex = 0;
            // 
            // Trigger_Timer
            // 
            this.Trigger_Timer.Interval = 200;
            this.Trigger_Timer.Tick += new System.EventHandler(this.Trigger_Timer_Tick_1);
            // 
            // Pressure_label
            // 
            this.Pressure_label.AutoSize = true;
            this.Pressure_label.Location = new System.Drawing.Point(109, 141);
            this.Pressure_label.Name = "Pressure_label";
            this.Pressure_label.Size = new System.Drawing.Size(31, 16);
            this.Pressure_label.TabIndex = 1;
            this.Pressure_label.Text = "0.00";
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(27, 186);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 2;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click_1);
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(140, 186);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(75, 23);
            this.stop_btn.TabIndex = 3;
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click_1);
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(6, 51);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(62, 16);
            this.port_label.TabIndex = 4;
            this.port_label.Text = "ComPort:";
            // 
            // PressurePlot
            // 
            this.PressurePlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PressurePlot.Dock = System.Windows.Forms.DockStyle.Right;
            this.PressurePlot.Location = new System.Drawing.Point(285, 0);
            this.PressurePlot.Name = "PressurePlot";
            this.PressurePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.PressurePlot.Size = new System.Drawing.Size(1017, 450);
            this.PressurePlot.TabIndex = 5;
            this.PressurePlot.Text = "plotView1";
            this.PressurePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.PressurePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.PressurePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.reCom_btn);
            this.groupBox1.Controls.Add(this.gagueName_label);
            this.groupBox1.Controls.Add(this.port_label);
            this.groupBox1.Controls.Add(this.ComPort_cbx);
            this.groupBox1.Controls.Add(this.stop_btn);
            this.groupBox1.Controls.Add(this.Pressure_label);
            this.groupBox1.Controls.Add(this.send_btn);
            this.groupBox1.Location = new System.Drawing.Point(20, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 230);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Box";
            // 
            // gagueName_label
            // 
            this.gagueName_label.AutoSize = true;
            this.gagueName_label.Location = new System.Drawing.Point(30, 140);
            this.gagueName_label.Name = "gagueName_label";
            this.gagueName_label.Size = new System.Drawing.Size(68, 16);
            this.gagueName_label.TabIndex = 5;
            this.gagueName_label.Text = "Undulator:";
            // 
            // reCom_btn
            // 
            this.reCom_btn.Location = new System.Drawing.Point(61, 90);
            this.reCom_btn.Name = "reCom_btn";
            this.reCom_btn.Size = new System.Drawing.Size(106, 30);
            this.reCom_btn.TabIndex = 6;
            this.reCom_btn.Text = "Refresh Port";
            this.reCom_btn.UseVisualStyleBackColor = true;
            this.reCom_btn.Click += new System.EventHandler(this.reCom_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1302, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PressurePlot);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Vacuum Pressure Logger";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComPort_cbx;
        private System.Windows.Forms.Timer Trigger_Timer;
        private System.Windows.Forms.Label Pressure_label;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Label port_label;
        private OxyPlot.WindowsForms.PlotView PressurePlot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label gagueName_label;
        private System.Windows.Forms.Button reCom_btn;
    }
}

