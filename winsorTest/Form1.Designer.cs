namespace winsorTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo treeNodeAdvStyleInfo1 = new Syncfusion.Windows.Forms.Tools.TreeNodeAdvStyleInfo();
            this.button1 = new System.Windows.Forms.Button();
            this.treeViewAdv2 = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(250, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeViewAdv2
            // 
            treeNodeAdvStyleInfo1.CheckBoxTickThickness = 1;
            treeNodeAdvStyleInfo1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.EnsureDefaultOptionedChild = true;
            treeNodeAdvStyleInfo1.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            treeNodeAdvStyleInfo1.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.treeViewAdv2.BaseStylePairs.AddRange(new Syncfusion.Windows.Forms.Tools.StyleNamePair[] {
            new Syncfusion.Windows.Forms.Tools.StyleNamePair("Standard", treeNodeAdvStyleInfo1)});
            this.treeViewAdv2.BeforeTouchSize = new System.Drawing.Size(312, 124);
            // 
            // 
            // 
            this.treeViewAdv2.HelpTextControl.BaseThemeName = null;
            this.treeViewAdv2.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv2.HelpTextControl.Name = "";
            this.treeViewAdv2.HelpTextControl.Size = new System.Drawing.Size(392, 112);
            this.treeViewAdv2.HelpTextControl.TabIndex = 0;
            this.treeViewAdv2.HelpTextControl.Visible = true;
            this.treeViewAdv2.InactiveSelectedNodeForeColor = System.Drawing.SystemColors.ControlText;
            this.treeViewAdv2.Location = new System.Drawing.Point(71, 200);
            this.treeViewAdv2.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.treeViewAdv2.Name = "treeViewAdv2";
            this.treeViewAdv2.SelectedNodeForeColor = System.Drawing.SystemColors.HighlightText;
            this.treeViewAdv2.Size = new System.Drawing.Size(312, 124);
            this.treeViewAdv2.TabIndex = 1;
            this.treeViewAdv2.Text = "treeViewAdv2";
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.CheckBoxTickThickness = 1;
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.EnsureDefaultOptionedChild = true;
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.IntermediateCheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.OptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.treeViewAdv2.ThemeStyle.TreeNodeAdvStyle.SelectedOptionButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            // 
            // 
            // 
            this.treeViewAdv2.ToolTipControl.BaseThemeName = null;
            this.treeViewAdv2.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv2.ToolTipControl.Name = "";
            this.treeViewAdv2.ToolTipControl.Size = new System.Drawing.Size(392, 112);
            this.treeViewAdv2.ToolTipControl.TabIndex = 0;
            this.treeViewAdv2.ToolTipControl.Visible = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(496, 359);
            this.Controls.Add(this.treeViewAdv2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeViewAdv1;
        private System.Windows.Forms.Button button1;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeViewAdv2;
    }
}

