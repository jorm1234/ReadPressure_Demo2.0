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
            this.ComPort_cbx.Location = new System.Drawing.Point(101, 109);
            this.ComPort_cbx.Name = "ComPort_cbx";
            this.ComPort_cbx.Size = new System.Drawing.Size(121, 24);
            this.ComPort_cbx.TabIndex = 0;
            // 
            // Trigger_Timer
            // 
            this.Trigger_Timer.Interval = 200;
            this.Trigger_Timer.Tick += new System.EventHandler(this.Trigger_Timer_Tick_1);
            // 
            // GunP_label
            // 
            this.GunP_label.AutoSize = true;
            this.GunP_label.Location = new System.Drawing.Point(21, 225);
            this.GunP_label.Name = "GunP_label";
            this.GunP_label.Size = new System.Drawing.Size(31, 16);
            this.GunP_label.TabIndex = 1;
            this.GunP_label.Text = "0.00";
            this.GunP_label.UseMnemonic = false;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(71, 249);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(75, 23);
            this.send_btn.TabIndex = 2;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click_1);
            // 
            // stop_btn
            // 
            this.stop_btn.Location = new System.Drawing.Point(186, 249);
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
            this.PressurePlot.Location = new System.Drawing.Point(368, 0);
            this.PressurePlot.Name = "PressurePlot";
            this.PressurePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.PressurePlot.Size = new System.Drawing.Size(1032, 551);
            this.PressurePlot.TabIndex = 5;
            this.PressurePlot.Text = "plotView1";
            this.PressurePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.PressurePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.PressurePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
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
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 415);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Box";
            // 
            // DipoleP_label
            // 
            this.DipoleP_label.AutoSize = true;
            this.DipoleP_label.Location = new System.Drawing.Point(248, 225);
            this.DipoleP_label.Name = "DipoleP_label";
            this.DipoleP_label.Size = new System.Drawing.Size(31, 16);
            this.DipoleP_label.TabIndex = 14;
            this.DipoleP_label.Text = "0.00";
            this.DipoleP_label.UseMnemonic = false;
            // 
            // LinacP_label
            // 
            this.LinacP_label.AutoSize = true;
            this.LinacP_label.Location = new System.Drawing.Point(130, 225);
            this.LinacP_label.Name = "LinacP_label";
            this.LinacP_label.Size = new System.Drawing.Size(31, 16);
            this.LinacP_label.TabIndex = 13;
            this.LinacP_label.Text = "0.00";
            this.LinacP_label.UseMnemonic = false;
            // 
            // Dipole_label
            // 
            this.Dipole_label.AutoSize = true;
            this.Dipole_label.Location = new System.Drawing.Point(243, 198);
            this.Dipole_label.Name = "Dipole_label";
            this.Dipole_label.Size = new System.Drawing.Size(89, 16);
            this.Dipole_label.TabIndex = 12;
            this.Dipole_label.Text = "Dipole: [Torr.]";
            // 
            // Linac_label
            // 
            this.Linac_label.AutoSize = true;
            this.Linac_label.Location = new System.Drawing.Point(127, 198);
            this.Linac_label.Name = "Linac_label";
            this.Linac_label.Size = new System.Drawing.Size(81, 16);
            this.Linac_label.TabIndex = 11;
            this.Linac_label.Text = "Linac: [Torr.]";
            // 
            // Logging_btn
            // 
            this.Logging_btn.Location = new System.Drawing.Point(27, 313);
            this.Logging_btn.Name = "Logging_btn";
            this.Logging_btn.Size = new System.Drawing.Size(75, 31);
            this.Logging_btn.TabIndex = 7;
            this.Logging_btn.Text = "Logging";
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
            this.Date_label.Location = new System.Drawing.Point(95, 19);
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
            this.reCom_btn.Location = new System.Drawing.Point(110, 150);
            this.reCom_btn.Name = "reCom_btn";
            this.reCom_btn.Size = new System.Drawing.Size(106, 30);
            this.reCom_btn.TabIndex = 6;
            this.reCom_btn.Text = "Refresh Port";
            this.reCom_btn.UseVisualStyleBackColor = true;
            this.reCom_btn.Click += new System.EventHandler(this.reCom_btn_Click);
            // 
            // Gun_label
            // 
            this.Gun_label.AutoSize = true;
            this.Gun_label.Location = new System.Drawing.Point(20, 198);
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
            this.ClientSize = new System.Drawing.Size(1400, 551);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PressurePlot);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}

