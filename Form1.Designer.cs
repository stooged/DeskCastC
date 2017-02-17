namespace DeskCastC
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
            this.Label3 = new System.Windows.Forms.Label();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.Button10 = new System.Windows.Forms.Button();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.Button8 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadFileFromURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectFileFromComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.Timer2 = new System.Windows.Forms.Timer(this.components);
            this.Timer3 = new System.Windows.Forms.Timer(this.components);
            this.BackgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Timer4 = new System.Windows.Forms.Timer(this.components);
            this.TxtState = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(25, 102);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(285, 13);
            this.Label3.TabIndex = 55;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(268, 63);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(60, 17);
            this.RadioButton2.TabIndex = 54;
            this.RadioButton2.TabStop = true;
            this.RadioButton2.Text = "Rooted";
            this.RadioButton2.UseVisualStyleBackColor = true;
            this.RadioButton2.Click += new System.EventHandler(this.RadioButton2_Click);
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Location = new System.Drawing.Point(268, 40);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(83, 17);
            this.RadioButton1.TabIndex = 53;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Non Rooted";
            this.RadioButton1.UseVisualStyleBackColor = true;
            this.RadioButton1.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // TrackBar2
            // 
            this.TrackBar2.Location = new System.Drawing.Point(319, 102);
            this.TrackBar2.Name = "TrackBar2";
            this.TrackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrackBar2.Size = new System.Drawing.Size(45, 60);
            this.TrackBar2.TabIndex = 51;
            this.TrackBar2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackBar2_MouseDown);
            this.TrackBar2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBar2_MouseUp);
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(14, 429);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(66, 17);
            this.CheckBox1.TabIndex = 50;
            this.CheckBox1.Text = "App Log";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(316, 168);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(34, 13);
            this.Label2.TabIndex = 49;
            this.Label2.Text = "00:00";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 168);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(34, 13);
            this.Label1.TabIndex = 48;
            this.Label1.Text = "00:00";
            // 
            // TrackBar1
            // 
            this.TrackBar1.Location = new System.Drawing.Point(56, 168);
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(254, 45);
            this.TrackBar1.TabIndex = 47;
            this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            this.TrackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            this.TrackBar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TrackBar1_MouseDown);
            this.TrackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBar1_MouseUp);
            // 
            // Button10
            // 
            this.Button10.Location = new System.Drawing.Point(215, 40);
            this.Button10.Name = "Button10";
            this.Button10.Size = new System.Drawing.Size(25, 21);
            this.Button10.TabIndex = 46;
            this.Button10.Text = "...";
            this.Button10.UseVisualStyleBackColor = true;
            this.Button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(28, 40);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(181, 21);
            this.ComboBox1.TabIndex = 45;
            // 
            // Button8
            // 
            this.Button8.Location = new System.Drawing.Point(91, 129);
            this.Button8.Name = "Button8";
            this.Button8.Size = new System.Drawing.Size(57, 22);
            this.Button8.TabIndex = 44;
            this.Button8.Text = "Resume";
            this.Button8.UseVisualStyleBackColor = true;
            this.Button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(253, 129);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(57, 22);
            this.Button5.TabIndex = 43;
            this.Button5.Text = "Mute";
            this.Button5.UseVisualStyleBackColor = true;
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(28, 129);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(57, 22);
            this.Button4.TabIndex = 42;
            this.Button4.Text = "Pause";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(141, 67);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(99, 25);
            this.Button2.TabIndex = 41;
            this.Button2.Text = "Disconnect CC";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(28, 67);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(99, 25);
            this.Button1.TabIndex = 40;
            this.Button1.Text = "Connect CC";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(12, 219);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox1.Size = new System.Drawing.Size(414, 204);
            this.TextBox1.TabIndex = 39;
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(366, 24);
            this.MenuStrip1.TabIndex = 52;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadFileFromURLToolStripMenuItem,
            this.ToolStripSeparator1,
            this.SelectFileFromComputerToolStripMenuItem});
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.OptionsToolStripMenuItem.Text = "Options";
            // 
            // LoadFileFromURLToolStripMenuItem
            // 
            this.LoadFileFromURLToolStripMenuItem.Name = "LoadFileFromURLToolStripMenuItem";
            this.LoadFileFromURLToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.LoadFileFromURLToolStripMenuItem.Text = "Load File From URL";
            this.LoadFileFromURLToolStripMenuItem.Click += new System.EventHandler(this.LoadFileFromURLToolStripMenuItem_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(211, 6);
            // 
            // SelectFileFromComputerToolStripMenuItem
            // 
            this.SelectFileFromComputerToolStripMenuItem.Name = "SelectFileFromComputerToolStripMenuItem";
            this.SelectFileFromComputerToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.SelectFileFromComputerToolStripMenuItem.Text = "Select File From Computer";
            this.SelectFileFromComputerToolStripMenuItem.Click += new System.EventHandler(this.SelectFileFromComputerToolStripMenuItem_Click);
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Timer2
            // 
            this.Timer2.Interval = 1000;
            this.Timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // Timer3
            // 
            this.Timer3.Interval = 1000;
            this.Timer3.Tick += new System.EventHandler(this.Timer3_Tick);
            // 
            // BackgroundWorker1
            // 
            this.BackgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            // 
            // Timer4
            // 
            this.Timer4.Interval = 4000;
            this.Timer4.Tick += new System.EventHandler(this.Timer4_Tick);
            // 
            // TxtState
            // 
            this.TxtState.Location = new System.Drawing.Point(842, 514);
            this.TxtState.Name = "TxtState";
            this.TxtState.Size = new System.Drawing.Size(57, 20);
            this.TxtState.TabIndex = 56;
            this.TxtState.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(111, 429);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 17);
            this.checkBox2.TabIndex = 57;
            this.checkBox2.Text = "Transcode Log";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 205);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.TxtState);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.RadioButton2);
            this.Controls.Add(this.RadioButton1);
            this.Controls.Add(this.TrackBar2);
            this.Controls.Add(this.CheckBox1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.Button10);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.Button8);
            this.Controls.Add(this.Button5);
            this.Controls.Add(this.Button4);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.MenuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "DeskCast";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.RadioButton RadioButton2;
        internal System.Windows.Forms.RadioButton RadioButton1;
        internal System.Windows.Forms.TrackBar TrackBar2;
        internal System.Windows.Forms.CheckBox CheckBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TrackBar TrackBar1;
        internal System.Windows.Forms.Button Button10;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.Button Button8;
        internal System.Windows.Forms.Button Button5;
        internal System.Windows.Forms.Button Button4;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem LoadFileFromURLToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem SelectFileFromComputerToolStripMenuItem;
        public System.Windows.Forms.ToolTip ToolTipMain;
        internal System.Windows.Forms.Timer Timer1;
        internal System.Windows.Forms.Timer Timer2;
        internal System.Windows.Forms.Timer Timer3;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.Timer Timer4;
        private System.Windows.Forms.TextBox TxtState;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

