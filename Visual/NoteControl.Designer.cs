namespace NoteManager.Visual
{
    partial class NoteControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteControl));
            this.redtNote = new System.Windows.Forms.RichTextBox();
            this.lblDataSoruceDesc = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.LinkLabel();
            this.lblChooseDataSource = new System.Windows.Forms.LinkLabel();
            this.lblCleanDataSource = new System.Windows.Forms.LinkLabel();
            this.tsFunctions = new System.Windows.Forms.ToolStrip();
            this.btnSaveText = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.imgToolBar = new System.Windows.Forms.ImageList(this.components);
            this.tsTextFunctions = new System.Windows.Forms.ToolStrip();
            this.tsFunctions.SuspendLayout();
            this.SuspendLayout();
            // 
            // redtNote
            // 
            this.redtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redtNote.Location = new System.Drawing.Point(3, 60);
            this.redtNote.Name = "redtNote";
            this.redtNote.Size = new System.Drawing.Size(362, 377);
            this.redtNote.TabIndex = 0;
            this.redtNote.Text = "";
            // 
            // lblDataSoruceDesc
            // 
            this.lblDataSoruceDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSoruceDesc.AutoSize = true;
            this.lblDataSoruceDesc.Location = new System.Drawing.Point(3, 440);
            this.lblDataSoruceDesc.Name = "lblDataSoruceDesc";
            this.lblDataSoruceDesc.Size = new System.Drawing.Size(108, 15);
            this.lblDataSoruceDesc.TabIndex = 1;
            this.lblDataSoruceDesc.Text = "Источник данных:";
            // 
            // lblDataSource
            // 
            this.lblDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(117, 440);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(171, 15);
            this.lblDataSource.TabIndex = 2;
            this.lblDataSource.TabStop = true;
            this.lblDataSource.Text = "Источник данных отсутствует";
            // 
            // lblChooseDataSource
            // 
            this.lblChooseDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChooseDataSource.AutoSize = true;
            this.lblChooseDataSource.Location = new System.Drawing.Point(3, 464);
            this.lblChooseDataSource.Name = "lblChooseDataSource";
            this.lblChooseDataSource.Size = new System.Drawing.Size(153, 15);
            this.lblChooseDataSource.TabIndex = 3;
            this.lblChooseDataSource.TabStop = true;
            this.lblChooseDataSource.Text = "Выбрать источник данных";
            this.lblChooseDataSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblChooseDataSource_LinkClicked);
            // 
            // lblCleanDataSource
            // 
            this.lblCleanDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCleanDataSource.AutoSize = true;
            this.lblCleanDataSource.Location = new System.Drawing.Point(170, 464);
            this.lblCleanDataSource.Name = "lblCleanDataSource";
            this.lblCleanDataSource.Size = new System.Drawing.Size(158, 15);
            this.lblCleanDataSource.TabIndex = 4;
            this.lblCleanDataSource.TabStop = true;
            this.lblCleanDataSource.Text = "Очистить источник данных";
            // 
            // tsFunctions
            // 
            this.tsFunctions.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsFunctions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveText,
            this.btnOpenFile});
            this.tsFunctions.Location = new System.Drawing.Point(0, 0);
            this.tsFunctions.Name = "tsFunctions";
            this.tsFunctions.Size = new System.Drawing.Size(368, 25);
            this.tsFunctions.TabIndex = 5;
            // 
            // btnSaveText
            // 
            this.btnSaveText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveText.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveText.Image")));
            this.btnSaveText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveText.Name = "btnSaveText";
            this.btnSaveText.Size = new System.Drawing.Size(23, 22);
            this.btnSaveText.Text = "Сохранить";
            this.btnSaveText.Click += new System.EventHandler(this.SaveText);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(23, 22);
            this.btnOpenFile.Text = "Открыть текстовый файл";
            this.btnOpenFile.Click += new System.EventHandler(this.OpenTextFile);
            // 
            // imgToolBar
            // 
            this.imgToolBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgToolBar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolBar.ImageStream")));
            this.imgToolBar.TransparentColor = System.Drawing.Color.Transparent;
            this.imgToolBar.Images.SetKeyName(0, "save.png");
            // 
            // tsTextFunctions
            // 
            this.tsTextFunctions.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsTextFunctions.Location = new System.Drawing.Point(0, 25);
            this.tsTextFunctions.Name = "tsTextFunctions";
            this.tsTextFunctions.Size = new System.Drawing.Size(368, 25);
            this.tsTextFunctions.TabIndex = 6;
            this.tsTextFunctions.Visible = false;
            // 
            // NoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tsTextFunctions);
            this.Controls.Add(this.tsFunctions);
            this.Controls.Add(this.lblCleanDataSource);
            this.Controls.Add(this.lblChooseDataSource);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.lblDataSoruceDesc);
            this.Controls.Add(this.redtNote);
            this.Name = "NoteControl";
            this.Size = new System.Drawing.Size(368, 488);
            this.tsFunctions.ResumeLayout(false);
            this.tsFunctions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox redtNote;
        private Label lblDataSoruceDesc;
        private LinkLabel lblDataSource;
        private LinkLabel lblChooseDataSource;
        private LinkLabel lblCleanDataSource;
        private ToolStrip tsFunctions;
        private ToolStripButton btnSaveText;
        private ImageList imgToolBar;
        private ToolStripButton btnOpenFile;
        private ToolStrip tsTextFunctions;
    }
}
