namespace JsonExaminer
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.mnuTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExport = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsContract = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeView.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(12, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(556, 369);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(93, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(475, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(12, 11);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Select";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // mnuTreeView
            // 
            this.mnuTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsExpand,
            this.tsContract,
            this.tsExport});
            this.mnuTreeView.Name = "mnuTreeView";
            this.mnuTreeView.ShowImageMargin = false;
            this.mnuTreeView.Size = new System.Drawing.Size(128, 92);
            this.mnuTreeView.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuTreeView_ItemClicked);
            // 
            // tsExpand
            // 
            this.tsExpand.Name = "tsExpand";
            this.tsExpand.Size = new System.Drawing.Size(127, 22);
            this.tsExpand.Text = "E&xpand All";
            // 
            // tsExport
            // 
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(127, 22);
            this.tsExport.Text = "&Export";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(580, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(70, 17);
            this.statusLabel.Text = "Hello World";
            // 
            // tsContract
            // 
            this.tsContract.Name = "tsContract";
            this.tsContract.Size = new System.Drawing.Size(127, 22);
            this.tsContract.Text = "&Contract All";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 446);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "JSON Examiner";
            this.mnuTreeView.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ContextMenuStrip mnuTreeView;
        private System.Windows.Forms.ToolStripMenuItem tsExpand;
        private System.Windows.Forms.ToolStripMenuItem tsExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem tsContract;
    }
}

