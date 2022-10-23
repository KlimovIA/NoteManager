namespace EducationHelper.Visual
{
    partial class DataSourceForm
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
            this.tsFunctions = new System.Windows.Forms.ToolStrip();
            this.tsBtnAddNode = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBtnRemoveNode = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SourceID = new System.Windows.Forms.ColumnHeader();
            this.SourceName = new System.Windows.Forms.ColumnHeader();
            this.SourceType = new System.Windows.Forms.ColumnHeader();
            this.SourceDescription = new System.Windows.Forms.ColumnHeader();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.tsFunctions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsFunctions
            // 
            this.tsFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAddNode,
            this.tsBtnRemoveNode});
            this.tsFunctions.Location = new System.Drawing.Point(0, 0);
            this.tsFunctions.Margin = new System.Windows.Forms.Padding(1);
            this.tsFunctions.Name = "tsFunctions";
            this.tsFunctions.Size = new System.Drawing.Size(591, 25);
            this.tsFunctions.TabIndex = 2;
            // 
            // tsBtnAddNode
            // 
            this.tsBtnAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAddNode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddFolder,
            this.tsAddNote});
            this.tsBtnAddNode.Image = global::EducationHelper.Properties.Resources.AddIcon;
            this.tsBtnAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddNode.Name = "tsBtnAddNode";
            this.tsBtnAddNode.Size = new System.Drawing.Size(29, 22);
            // 
            // tsAddFolder
            // 
            this.tsAddFolder.Image = global::EducationHelper.Properties.Resources.YoutubeIcon;
            this.tsAddFolder.Name = "tsAddFolder";
            this.tsAddFolder.Size = new System.Drawing.Size(261, 22);
            this.tsAddFolder.Text = "Добавить ссылку на youtube";
            // 
            // tsAddNote
            // 
            this.tsAddNote.Image = global::EducationHelper.Properties.Resources.pdfIcon;
            this.tsAddNote.Name = "tsAddNote";
            this.tsAddNote.Size = new System.Drawing.Size(261, 22);
            this.tsAddNote.Text = "Добавить источник данных из pdf";
            // 
            // tsBtnRemoveNode
            // 
            this.tsBtnRemoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRemoveNode.Image = global::EducationHelper.Properties.Resources.RemoveIcon;
            this.tsBtnRemoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRemoveNode.Name = "tsBtnRemoveNode";
            this.tsBtnRemoveNode.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRemoveNode.Text = "Удалить выбраный узел";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.SourceID,
            this.SourceName,
            this.SourceType,
            this.SourceDescription});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(591, 461);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // SourceID
            // 
            this.SourceID.Text = "ID";
            // 
            // SourceName
            // 
            this.SourceName.Text = "Имя источника данных";
            // 
            // SourceType
            // 
            this.SourceType.Text = "Тип источника данных";
            // 
            // SourceDescription
            // 
            this.SourceDescription.Text = "Описание источника данных";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(513, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 461);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 457);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 29);
            this.panel2.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(432, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // DataSourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 486);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tsFunctions);
            this.Name = "DataSourceForm";
            this.Text = "DataSourceForm";
            this.tsFunctions.ResumeLayout(false);
            this.tsFunctions.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip tsFunctions;
        private ToolStripDropDownButton tsBtnAddNode;
        private ToolStripMenuItem tsAddFolder;
        private ToolStripMenuItem tsAddNote;
        private ToolStripButton tsBtnRemoveNode;
        private ListView listView1;
        private ColumnHeader SourceID;
        private ColumnHeader SourceName;
        private ColumnHeader SourceType;
        private ColumnHeader SourceDescription;
        private Button btnCancel;
        private Panel panel1;
        private Panel panel2;
        private Button btnOK;
    }
}