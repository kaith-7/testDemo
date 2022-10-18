namespace winsorTest
{
    partial class Form6
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
            this.lblPrg = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.rdPolygon = new System.Windows.Forms.RadioButton();
            this.rdLine = new System.Windows.Forms.RadioButton();
            this.rdPoint = new System.Windows.Forms.RadioButton();
            this.cmbDelim = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.Button2 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbShapeID = new System.Windows.Forms.ComboBox();
            this.cmbPartID = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.chkCoordsIntoAttribs = new System.Windows.Forms.CheckBox();
            this.chkAttribs = new System.Windows.Forms.CheckBox();
            this.cmbM = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.cmbZ = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cmbY = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.cmbX = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPrg
            // 
            this.lblPrg.AutoSize = true;
            this.lblPrg.Location = new System.Drawing.Point(64, 597);
            this.lblPrg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrg.Name = "lblPrg";
            this.lblPrg.Size = new System.Drawing.Size(239, 15);
            this.lblPrg.TabIndex = 22;
            this.lblPrg.Text = "(progress will be shown here)";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.rdPolygon);
            this.GroupBox2.Controls.Add(this.rdLine);
            this.GroupBox2.Controls.Add(this.rdPoint);
            this.GroupBox2.Enabled = false;
            this.GroupBox2.Location = new System.Drawing.Point(64, 200);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox2.Size = new System.Drawing.Size(472, 103);
            this.GroupBox2.TabIndex = 21;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Data Type";
            // 
            // rdPolygon
            // 
            this.rdPolygon.AutoSize = true;
            this.rdPolygon.Location = new System.Drawing.Point(19, 75);
            this.rdPolygon.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdPolygon.Name = "rdPolygon";
            this.rdPolygon.Size = new System.Drawing.Size(92, 19);
            this.rdPolygon.TabIndex = 2;
            this.rdPolygon.Text = "Polygons";
            this.rdPolygon.UseVisualStyleBackColor = true;
            this.rdPolygon.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // rdLine
            // 
            this.rdLine.AutoSize = true;
            this.rdLine.Location = new System.Drawing.Point(19, 48);
            this.rdLine.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdLine.Name = "rdLine";
            this.rdLine.Size = new System.Drawing.Size(68, 19);
            this.rdLine.TabIndex = 1;
            this.rdLine.Text = "Lines";
            this.rdLine.UseVisualStyleBackColor = true;
            this.rdLine.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // rdPoint
            // 
            this.rdPoint.AutoSize = true;
            this.rdPoint.Checked = true;
            this.rdPoint.Location = new System.Drawing.Point(19, 22);
            this.rdPoint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdPoint.Name = "rdPoint";
            this.rdPoint.Size = new System.Drawing.Size(76, 19);
            this.rdPoint.TabIndex = 0;
            this.rdPoint.TabStop = true;
            this.rdPoint.Text = "Points";
            this.rdPoint.UseVisualStyleBackColor = true;
            this.rdPoint.CheckedChanged += new System.EventHandler(this.rdType_CheckedChanged);
            // 
            // cmbDelim
            // 
            this.cmbDelim.FormattingEnabled = true;
            this.cmbDelim.Items.AddRange(new object[] {
            ",",
            "|",
            ";",
            ":",
            "-",
            "=",
            "{",
            "}",
            "\'"});
            this.cmbDelim.Location = new System.Drawing.Point(180, 169);
            this.cmbDelim.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDelim.Name = "cmbDelim";
            this.cmbDelim.Size = new System.Drawing.Size(107, 23);
            this.cmbDelim.TabIndex = 15;
            this.cmbDelim.Text = ",";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(295, 592);
            this.btnConvert.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(119, 24);
            this.btnConvert.TabIndex = 19;
            this.btnConvert.Text = "&Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(70, 173);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(135, 15);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "Field Delimiter:";
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(316, 167);
            this.Button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(119, 25);
            this.Button2.TabIndex = 16;
            this.Button2.Text = "&Open File...";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(418, 591);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 25);
            this.btnClose.TabIndex = 18;
            this.btnClose.Text = "C&lose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cmbShapeID);
            this.GroupBox1.Controls.Add(this.cmbPartID);
            this.GroupBox1.Controls.Add(this.Label9);
            this.GroupBox1.Controls.Add(this.Label8);
            this.GroupBox1.Controls.Add(this.chkCoordsIntoAttribs);
            this.GroupBox1.Controls.Add(this.chkAttribs);
            this.GroupBox1.Controls.Add(this.cmbM);
            this.GroupBox1.Controls.Add(this.Label6);
            this.GroupBox1.Controls.Add(this.cmbZ);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.cmbY);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.cmbX);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Enabled = false;
            this.GroupBox1.Location = new System.Drawing.Point(64, 309);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Size = new System.Drawing.Size(472, 275);
            this.GroupBox1.TabIndex = 17;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Conversion Options";
            // 
            // cmbShapeID
            // 
            this.cmbShapeID.Enabled = false;
            this.cmbShapeID.FormattingEnabled = true;
            this.cmbShapeID.Location = new System.Drawing.Point(157, 22);
            this.cmbShapeID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbShapeID.Name = "cmbShapeID";
            this.cmbShapeID.Size = new System.Drawing.Size(228, 23);
            this.cmbShapeID.TabIndex = 10;
            this.cmbShapeID.Text = "(for Lines/Polygons Only)";
            // 
            // cmbPartID
            // 
            this.cmbPartID.Enabled = false;
            this.cmbPartID.FormattingEnabled = true;
            this.cmbPartID.Location = new System.Drawing.Point(157, 53);
            this.cmbPartID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPartID.Name = "cmbPartID";
            this.cmbPartID.Size = new System.Drawing.Size(228, 23);
            this.cmbPartID.TabIndex = 9;
            this.cmbPartID.Text = "(for Lines/Polygons Only)";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(15, 57);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(71, 15);
            this.Label9.TabIndex = 8;
            this.Label9.Text = "Part ID:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(15, 25);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(135, 15);
            this.Label8.TabIndex = 7;
            this.Label8.Text = "Polygon/Line ID:";
            // 
            // chkCoordsIntoAttribs
            // 
            this.chkCoordsIntoAttribs.AutoSize = true;
            this.chkCoordsIntoAttribs.Location = new System.Drawing.Point(24, 210);
            this.chkCoordsIntoAttribs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkCoordsIntoAttribs.Name = "chkCoordsIntoAttribs";
            this.chkCoordsIntoAttribs.Size = new System.Drawing.Size(349, 19);
            this.chkCoordsIntoAttribs.TabIndex = 4;
            this.chkCoordsIntoAttribs.Text = "Add Coordinates to Shapefile Attributes?";
            this.chkCoordsIntoAttribs.UseVisualStyleBackColor = true;
            // 
            // chkAttribs
            // 
            this.chkAttribs.AutoSize = true;
            this.chkAttribs.Checked = true;
            this.chkAttribs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttribs.Location = new System.Drawing.Point(24, 237);
            this.chkAttribs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAttribs.Name = "chkAttribs";
            this.chkAttribs.Size = new System.Drawing.Size(429, 19);
            this.chkAttribs.TabIndex = 5;
            this.chkAttribs.Text = "Convert All Other Fields into Shapefile Attributes";
            this.chkAttribs.UseVisualStyleBackColor = true;
            // 
            // cmbM
            // 
            this.cmbM.FormattingEnabled = true;
            this.cmbM.Location = new System.Drawing.Point(157, 178);
            this.cmbM.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbM.Name = "cmbM";
            this.cmbM.Size = new System.Drawing.Size(228, 23);
            this.cmbM.TabIndex = 3;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(15, 181);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(159, 15);
            this.Label6.TabIndex = 6;
            this.Label6.Text = "M Field: (optional)";
            // 
            // cmbZ
            // 
            this.cmbZ.FormattingEnabled = true;
            this.cmbZ.Location = new System.Drawing.Point(157, 147);
            this.cmbZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbZ.Name = "cmbZ";
            this.cmbZ.Size = new System.Drawing.Size(228, 23);
            this.cmbZ.TabIndex = 2;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(15, 150);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(159, 15);
            this.Label5.TabIndex = 4;
            this.Label5.Text = "Z Field: (optional)";
            // 
            // cmbY
            // 
            this.cmbY.FormattingEnabled = true;
            this.cmbY.Location = new System.Drawing.Point(157, 115);
            this.cmbY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbY.Name = "cmbY";
            this.cmbY.Size = new System.Drawing.Size(228, 23);
            this.cmbY.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(15, 119);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(71, 15);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Y Field:";
            // 
            // cmbX
            // 
            this.cmbX.FormattingEnabled = true;
            this.cmbX.Location = new System.Drawing.Point(157, 84);
            this.cmbX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbX.Name = "cmbX";
            this.cmbX.Size = new System.Drawing.Size(228, 23);
            this.cmbX.TabIndex = 0;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 88);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(71, 15);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "X Field:";
            // 
            // Button1
            // 
            this.Button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Button1.Location = new System.Drawing.Point(503, 134);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(33, 28);
            this.Button1.TabIndex = 13;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(173, 138);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(301, 25);
            this.txtInput.TabIndex = 11;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(70, 141);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(95, 15);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "Input File:";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(60, 68);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(491, 53);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "This tool will convert a comma-delimited text file into a shapefile. The file mus" +
    "t contain column titles as the first row, and each row must appear on a new line" +
    ".";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 636);
            this.Controls.Add(this.lblPrg);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.cmbDelim);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.Form6_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblPrg;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.RadioButton rdPolygon;
        internal System.Windows.Forms.RadioButton rdLine;
        internal System.Windows.Forms.RadioButton rdPoint;
        internal System.Windows.Forms.ComboBox cmbDelim;
        internal System.Windows.Forms.Button btnConvert;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox cmbShapeID;
        internal System.Windows.Forms.ComboBox cmbPartID;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.CheckBox chkCoordsIntoAttribs;
        internal System.Windows.Forms.CheckBox chkAttribs;
        internal System.Windows.Forms.ComboBox cmbM;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox cmbZ;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cmbY;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox cmbX;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox txtInput;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}