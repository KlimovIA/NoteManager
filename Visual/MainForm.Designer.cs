namespace NoteManager
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlObjectTreeContainer = new System.Windows.Forms.Panel();
            this.tvObjectTree = new System.Windows.Forms.TreeView();
            this.msTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteNode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsFunctions = new System.Windows.Forms.ToolStrip();
            this.tsBtnAddNode = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsAddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBtnRemoveNode = new System.Windows.Forms.ToolStripButton();
            this.pnlMainContainer = new System.Windows.Forms.Panel();
            this.ncNote = new NoteManager.Visual.NoteControl();
            this.pnlApplyChanges = new System.Windows.Forms.Panel();
            this.lbActionStatus = new System.Windows.Forms.Label();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlToolStripContainer = new System.Windows.Forms.Panel();
            this.pnlObjectTreeContainer.SuspendLayout();
            this.msTreeView.SuspendLayout();
            this.tsFunctions.SuspendLayout();
            this.pnlMainContainer.SuspendLayout();
            this.pnlApplyChanges.SuspendLayout();
            this.pnlToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlObjectTreeContainer
            // 
            this.pnlObjectTreeContainer.Controls.Add(this.tvObjectTree);
            this.pnlObjectTreeContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlObjectTreeContainer.Location = new System.Drawing.Point(0, 27);
            this.pnlObjectTreeContainer.Name = "pnlObjectTreeContainer";
            this.pnlObjectTreeContainer.Size = new System.Drawing.Size(224, 522);
            this.pnlObjectTreeContainer.TabIndex = 1;
            // 
            // tvObjectTree
            // 
            this.tvObjectTree.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tvObjectTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvObjectTree.ContextMenuStrip = this.msTreeView;
            this.tvObjectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvObjectTree.LabelEdit = true;
            this.tvObjectTree.Location = new System.Drawing.Point(0, 0);
            this.tvObjectTree.Name = "tvObjectTree";
            this.tvObjectTree.Size = new System.Drawing.Size(224, 522);
            this.tvObjectTree.TabIndex = 0;
            this.tvObjectTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvObjectTree_AfterLabelEdit);
            this.tvObjectTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvObjectTree_AfterSelect);
            this.tvObjectTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvObjectTree_KeyDown);
            this.tvObjectTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvObjectTree_MouseDown);
            // 
            // msTreeView
            // 
            this.msTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddFolder,
            this.miAddNote,
            this.miDeleteNode});
            this.msTreeView.Name = "contextMenuStrip1";
            this.msTreeView.Size = new System.Drawing.Size(164, 70);
            this.msTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.msTreeView_Opening);
            // 
            // miAddFolder
            // 
            this.miAddFolder.Image = global::NoteManager.Properties.Resources.FolderIcon;
            this.miAddFolder.Name = "miAddFolder";
            this.miAddFolder.Size = new System.Drawing.Size(163, 22);
            this.miAddFolder.Text = "Создать папку";
            this.miAddFolder.Click += new System.EventHandler(this.tsAddFolder_Click);
            // 
            // miAddNote
            // 
            this.miAddNote.Image = global::NoteManager.Properties.Resources.NoteIcon;
            this.miAddNote.Name = "miAddNote";
            this.miAddNote.Size = new System.Drawing.Size(163, 22);
            this.miAddNote.Text = "Создать заметку";
            this.miAddNote.Click += new System.EventHandler(this.tsAddNote_Click);
            // 
            // miDeleteNode
            // 
            this.miDeleteNode.Image = global::NoteManager.Properties.Resources.RemoveIcon;
            this.miDeleteNode.Name = "miDeleteNode";
            this.miDeleteNode.Size = new System.Drawing.Size(163, 22);
            this.miDeleteNode.Text = "Удалить";
            this.miDeleteNode.Click += new System.EventHandler(this.tsBtnRemoveNode_Click);
            // 
            // tsFunctions
            // 
            this.tsFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAddNode,
            this.tsBtnRemoveNode});
            this.tsFunctions.Location = new System.Drawing.Point(0, 0);
            this.tsFunctions.Margin = new System.Windows.Forms.Padding(1);
            this.tsFunctions.Name = "tsFunctions";
            this.tsFunctions.Size = new System.Drawing.Size(844, 25);
            this.tsFunctions.TabIndex = 1;
            // 
            // tsBtnAddNode
            // 
            this.tsBtnAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAddNode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddFolder,
            this.tsAddNote});
            this.tsBtnAddNode.Image = global::NoteManager.Properties.Resources.AddIcon;
            this.tsBtnAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddNode.Name = "tsBtnAddNode";
            this.tsBtnAddNode.Size = new System.Drawing.Size(29, 22);
            this.tsBtnAddNode.DropDownOpened += new System.EventHandler(this.tsBtnAddNode_DropDownOpened);
            // 
            // tsAddFolder
            // 
            this.tsAddFolder.Image = global::NoteManager.Properties.Resources.FolderIcon;
            this.tsAddFolder.Name = "tsAddFolder";
            this.tsAddFolder.Size = new System.Drawing.Size(172, 22);
            this.tsAddFolder.Text = "Добавить папку";
            this.tsAddFolder.Click += new System.EventHandler(this.tsAddFolder_Click);
            // 
            // tsAddNote
            // 
            this.tsAddNote.Image = global::NoteManager.Properties.Resources.NoteIcon;
            this.tsAddNote.Name = "tsAddNote";
            this.tsAddNote.Size = new System.Drawing.Size(172, 22);
            this.tsAddNote.Text = "Добавить заметку";
            this.tsAddNote.Click += new System.EventHandler(this.tsAddNote_Click);
            // 
            // tsBtnRemoveNode
            // 
            this.tsBtnRemoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnRemoveNode.Image = global::NoteManager.Properties.Resources.RemoveIcon;
            this.tsBtnRemoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnRemoveNode.Name = "tsBtnRemoveNode";
            this.tsBtnRemoveNode.Size = new System.Drawing.Size(23, 22);
            this.tsBtnRemoveNode.Text = "Удалить выбраный узел";
            this.tsBtnRemoveNode.Click += new System.EventHandler(this.tsBtnRemoveNode_Click);
            // 
            // pnlMainContainer
            // 
            this.pnlMainContainer.Controls.Add(this.ncNote);
            this.pnlMainContainer.Controls.Add(this.pnlApplyChanges);
            this.pnlMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContainer.Location = new System.Drawing.Point(224, 27);
            this.pnlMainContainer.Name = "pnlMainContainer";
            this.pnlMainContainer.Size = new System.Drawing.Size(620, 522);
            this.pnlMainContainer.TabIndex = 2;
            // 
            // ncNote
            // 
            this.ncNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ncNote.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ncNote.Location = new System.Drawing.Point(3, 3);
            this.ncNote.Name = "ncNote";
            this.ncNote.Size = new System.Drawing.Size(610, 485);
            this.ncNote.TabIndex = 1;
            // 
            // pnlApplyChanges
            // 
            this.pnlApplyChanges.BackColor = System.Drawing.SystemColors.Control;
            this.pnlApplyChanges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlApplyChanges.Controls.Add(this.lbActionStatus);
            this.pnlApplyChanges.Controls.Add(this.btnApplyChanges);
            this.pnlApplyChanges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlApplyChanges.Location = new System.Drawing.Point(0, 492);
            this.pnlApplyChanges.Name = "pnlApplyChanges";
            this.pnlApplyChanges.Size = new System.Drawing.Size(620, 30);
            this.pnlApplyChanges.TabIndex = 0;
            // 
            // lbActionStatus
            // 
            this.lbActionStatus.AutoSize = true;
            this.lbActionStatus.Location = new System.Drawing.Point(5, 6);
            this.lbActionStatus.Name = "lbActionStatus";
            this.lbActionStatus.Size = new System.Drawing.Size(0, 15);
            this.lbActionStatus.TabIndex = 1;
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyChanges.Location = new System.Drawing.Point(533, 2);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(86, 23);
            this.btnApplyChanges.TabIndex = 0;
            this.btnApplyChanges.Text = "Применить";
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(224, 27);
            this.splitter1.MinExtra = 10;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 522);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // pnlToolStripContainer
            // 
            this.pnlToolStripContainer.Controls.Add(this.tsFunctions);
            this.pnlToolStripContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlToolStripContainer.Name = "pnlToolStripContainer";
            this.pnlToolStripContainer.Size = new System.Drawing.Size(844, 27);
            this.pnlToolStripContainer.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 549);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.pnlObjectTreeContainer);
            this.Controls.Add(this.pnlToolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(860, 588);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Менеждер заметок";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlObjectTreeContainer.ResumeLayout(false);
            this.msTreeView.ResumeLayout(false);
            this.tsFunctions.ResumeLayout(false);
            this.tsFunctions.PerformLayout();
            this.pnlMainContainer.ResumeLayout(false);
            this.pnlApplyChanges.ResumeLayout(false);
            this.pnlApplyChanges.PerformLayout();
            this.pnlToolStripContainer.ResumeLayout(false);
            this.pnlToolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlObjectTreeContainer;
        private Panel pnlMainContainer;
        private TreeView tvObjectTree;
        private ToolStrip tsFunctions;
        private ToolStripButton tsBtnRemoveNode;
        private ToolStripDropDownButton tsBtnAddNode;
        private ToolStripMenuItem tsAddFolder;
        private ToolStripMenuItem tsAddNote;
        private ContextMenuStrip msTreeView;
        private ToolStripMenuItem miAddFolder;
        private ToolStripMenuItem miAddNote;
        private ToolStripMenuItem miDeleteNode;
        private Panel pnlApplyChanges;
        private Button btnApplyChanges;
        private Label lbActionStatus;
        private Panel pnlToolStripContainer;
        private Visual.NoteControl ncNote;
        private Splitter splitter1;
    }
}