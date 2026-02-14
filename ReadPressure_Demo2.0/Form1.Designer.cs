using System;

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
            this.GunP_label = new System.Windows.Forms.Label();
            this.send_btn = new System.Windows.Forms.Button();
            this.stop_btn = new System.Windows.Forms.Button();
            this.port_label = new System.Windows.Forms.Label();
            this.PressurePlot = new OxyPlot.WindowsForms.PlotView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SetLim_btn = new System.Windows.Forms.Button();
            this.DipLim_txb = new System.Windows.Forms.TextBox();
            this.LinLim_txb = new System.Windows.Forms.TextBox();
            this.GunLim_txb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.View_btn = new System.Windows.Forms.Button();
            this.DipoleP_label = new System.Windows.Forms.Label();
            this.LinacP_label = new System.Windows.Forms.Label();
            this.Dipole_label = new System.Windows.Forms.Label();
            this.Linac_label = new System.Windows.Forms.Label();
            this.Logging_btn = new System.Windows.Forms.Button();
            this.Time_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Date_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reCom_btn = new System.Windows.Forms.Button();
            this.Gun_label = new System.Windows.Forms.Label();
            this.DateTime_Timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComPort_cbx
            // 
            this.ComPort_cbx.FormattingEnabled = true;
            this.ComPort_cbx.Location = new System.Drawing.Point(101, 110);
            this.ComPort_cbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComPort_cbx.Name = "ComPort_cbx";
            this.ComPort_cbx.Size = new System.Drawing.Size(121, 24);
            this.ComPort_cbx.TabIndex = 0;
            // 
            // Trigger_Timer
            // 
            this.Trigger_Timer.Interval = 200;
            this.Trigger_Timer.Tick += new System.EventHandler(this.Trigger_Timer_Tick);
            // 
            // GunP_label
            // 
            this.GunP_label.AutoSize = true;
            this.GunP_label.Location = new System.Drawing.Point(25, 224);
            this.GunP_label.Name = "GunP_label";
            this.GunP_label.Size = new System.Drawing.Size(31, 16);
            this.GunP_label.TabIndex = 1;
            this.GunP_label.Text = "0.00";
            this.GunP_label.UseMnemonic = false;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(71, 249);
            this.send_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 2;
            this.send_btn.Text = "Start";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(187, 249);
            this.stop_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stop_btn.Name = "stop_btn";
            this.stop_btn.Size = new System.Drawing.Size(75, 23);
            this.stop_btn.TabIndex = 3;
            this.stop_btn.Text = "Stop";
            this.stop_btn.UseVisualStyleBackColor = true;
            this.stop_btn.Click += new System.EventHandler(this.stop_btn_Click);
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(24, 117);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(62, 16);
            this.port_label.TabIndex = 4;
            this.port_label.Text = "ComPort:";
            // 
            // PressurePlot
            // 
            this.PressurePlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PressurePlot.Dock = System.Windows.Forms.DockStyle.Right;
            this.PressurePlot.Location = new System.Drawing.Point(367, 0);
            this.PressurePlot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PressurePlot.Name = "PressurePlot";
            this.PressurePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.PressurePlot.Size = new System.Drawing.Size(892, 551);
            this.PressurePlot.TabIndex = 5;
            this.PressurePlot.Text = "plotView1";
            this.PressurePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.PressurePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.PressurePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.SetLim_btn);
            this.groupBox1.Controls.Add(this.DipLim_txb);
            this.groupBox1.Controls.Add(this.LinLim_txb);
            this.groupBox1.Controls.Add(this.GunLim_txb);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.View_btn);
            this.groupBox1.Controls.Add(this.DipoleP_label);
            this.groupBox1.Controls.Add(this.LinacP_label);
            this.groupBox1.Controls.Add(this.Dipole_label);
            this.groupBox1.Controls.Add(this.Linac_label);
            this.groupBox1.Controls.Add(this.Logging_btn);
            this.groupBox1.Controls.Add(this.Time_label);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Date_label);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.reCom_btn);
            this.groupBox1.Controls.Add(this.Gun_label);
            this.groupBox1.Controls.Add(this.port_label);
            this.groupBox1.Controls.Add(this.ComPort_cbx);
            this.groupBox1.Controls.Add(this.stop_btn);
            this.groupBox1.Controls.Add(this.GunP_label);
            this.groupBox1.Controls.Add(this.send_btn);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(349, 514);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Box";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 479);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 16);
            this.label8.TabIndex = 25;
            this.label8.Text = "Torr.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(225, 428);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Torr.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(225, 379);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 16);
            this.label6.TabIndex = 23;
            this.label6.Text = "Torr.";
            // 
            // SetLim_btn
            // 
            this.SetLim_btn.Location = new System.Drawing.Point(271, 393);
            this.SetLim_btn.Margin = new System.Windows.Forms.Padding(4);
            this.SetLim_btn.Name = "SetLim_btn";
            this.SetLim_btn.Size = new System.Drawing.Size(67, 86);
            this.SetLim_btn.TabIndex = 22;
            this.SetLim_btn.Text = "Set";
            this.SetLim_btn.UseVisualStyleBackColor = true;
            // 
            // DipLim_txb
            // 
            this.DipLim_txb.Location = new System.Drawing.Point(121, 470);
            this.DipLim_txb.Margin = new System.Windows.Forms.Padding(4);
            this.DipLim_txb.Name = "DipLim_txb";
            this.DipLim_txb.Size = new System.Drawing.Size(92, 22);
            this.DipLim_txb.TabIndex = 21;
            this.DipLim_txb.Text = "1e-6";
            // 
            // LinLim_txb
            // 
            this.LinLim_txb.Location = new System.Drawing.Point(121, 420);
            this.LinLim_txb.Margin = new System.Windows.Forms.Padding(4);
            this.LinLim_txb.Name = "LinLim_txb";
            this.LinLim_txb.Size = new System.Drawing.Size(93, 22);
            this.LinLim_txb.TabIndex = 20;
            this.LinLim_txb.Text = "1e-6";
            // 
            // GunLim_txb
            // 
            this.GunLim_txb.Location = new System.Drawing.Point(121, 370);
            this.GunLim_txb.Margin = new System.Windows.Forms.Padding(4);
            this.GunLim_txb.Name = "GunLim_txb";
            this.GunLim_txb.Size = new System.Drawing.Size(93, 22);
            this.GunLim_txb.TabIndex = 19;
            this.GunLim_txb.Text = "1e-8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 474);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Dipole Limit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 428);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Linac Limit:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 374);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Gun Limit:";
            // 
            // View_btn
            // 
            this.View_btn.Location = new System.Drawing.Point(187, 321);
            this.View_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.View_btn.Name = "View_btn";
            this.View_btn.Size = new System.Drawing.Size(75, 23);
            this.View_btn.TabIndex = 15;
            this.View_btn.Text = "Live";
            this.View_btn.UseVisualStyleBackColor = true;
            // 
            // DipoleP_label
            // 
            this.DipoleP_label.AutoSize = true;
            this.DipoleP_label.Location = new System.Drawing.Point(252, 224);
            this.DipoleP_label.Name = "DipoleP_label";
            this.DipoleP_label.Size = new System.Drawing.Size(31, 16);
            this.DipoleP_label.TabIndex = 14;
            this.DipoleP_label.Text = "0.00";
            this.DipoleP_label.UseMnemonic = false;
            // 
            // LinacP_label
            // 
            this.LinacP_label.AutoSize = true;
            this.LinacP_label.Location = new System.Drawing.Point(131, 225);
            this.LinacP_label.Name = "LinacP_label";
            this.LinacP_label.Size = new System.Drawing.Size(31, 16);
            this.LinacP_label.TabIndex = 13;
            this.LinacP_label.Text = "0.00";
            this.LinacP_label.UseMnemonic = false;
            // 
            // Dipole_label
            // 
            this.Dipole_label.AutoSize = true;
            this.Dipole_label.Location = new System.Drawing.Point(247, 197);
            this.Dipole_label.Name = "Dipole_label";
            this.Dipole_label.Size = new System.Drawing.Size(89, 16);
            this.Dipole_label.TabIndex = 12;
            this.Dipole_label.Text = "Dipole: [Torr.]";
            // 
            // Linac_label
            // 
            this.Linac_label.AutoSize = true;
            this.Linac_label.Location = new System.Drawing.Point(131, 197);
            this.Linac_label.Name = "Linac_label";
            this.Linac_label.Size = new System.Drawing.Size(81, 16);
            this.Linac_label.TabIndex = 11;
            this.Linac_label.Text = "Linac: [Torr.]";
            // 
            // Logging_btn
            // 
            this.Logging_btn.Location = new System.Drawing.Point(27, 313);
            this.Logging_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Logging_btn.Name = "Logging_btn";
            this.Logging_btn.Size = new System.Drawing.Size(133, 31);
            this.Logging_btn.TabIndex = 7;
            this.Logging_btn.Text = "Start Logging";
            this.Logging_btn.UseVisualStyleBackColor = true;
            this.Logging_btn.Click += new System.EventHandler(this.logging_btn_Click);
            // 
            // Time_label
            // 
            this.Time_label.AutoSize = true;
            this.Time_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Time_label.Location = new System.Drawing.Point(95, 55);
            this.Time_label.Name = "Time_label";
            this.Time_label.Size = new System.Drawing.Size(92, 32);
            this.Time_label.TabIndex = 10;
            this.Time_label.Text = "--:--:--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Time:";
            // 
            // Date_label
            // 
            this.Date_label.AutoSize = true;
            this.Date_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Date_label.Location = new System.Drawing.Point(95, 18);
            this.Date_label.Name = "Date_label";
            this.Date_label.Size = new System.Drawing.Size(110, 32);
            this.Date_label.TabIndex = 8;
            this.Date_label.Text = "-- -- ----";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Date:";
            // 
            // reCom_btn
            // 
            this.reCom_btn.Location = new System.Drawing.Point(109, 150);
            this.reCom_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reCom_btn.Name = "reCom_btn";
            this.reCom_btn.Size = new System.Drawing.Size(107, 30);
            this.reCom_btn.TabIndex = 6;
            this.reCom_btn.Text = "Refresh Port";
            this.reCom_btn.UseVisualStyleBackColor = true;
            this.reCom_btn.Click += new System.EventHandler(this.reCom_btn_Click);
            // 
            // Gun_label
            // 
            this.Gun_label.AutoSize = true;
            this.Gun_label.Location = new System.Drawing.Point(24, 197);
            this.Gun_label.Name = "Gun_label";
            this.Gun_label.Size = new System.Drawing.Size(94, 16);
            this.Gun_label.TabIndex = 5;
            this.Gun_label.Text = "RF Gun: [Torr.]";
            // 
            // DateTime_Timer
            // 
            this.DateTime_Timer.Tick += new System.EventHandler(this.DateTime_Timer_Tick);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1259, 551);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PressurePlot);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Vacuum Pressure Logger";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComPort_cbx;
        private System.Windows.Forms.Timer Trigger_Timer;
        private System.Windows.Forms.Label GunP_label;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.Button stop_btn;
        private System.Windows.Forms.Label port_label;
        private OxyPlot.WindowsForms.PlotView PressurePlot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Gun_label;
        private System.Windows.Forms.Button reCom_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Date_label;
        private System.Windows.Forms.Label Time_label;
        private System.Windows.Forms.Timer DateTime_Timer;
        private System.Windows.Forms.Button Logging_btn;
        private System.Windows.Forms.Label Dipole_label;
        private System.Windows.Forms.Label Linac_label;
        private System.Windows.Forms.Label DipoleP_label;
        private System.Windows.Forms.Label LinacP_label;
        private System.Windows.Forms.Button View_btn;
        private System.Windows.Forms.TextBox DipLim_txb;
        private System.Windows.Forms.TextBox LinLim_txb;
        private System.Windows.Forms.TextBox GunLim_txb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetLim_btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

