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
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.imgToolBar = new System.Windows.Forms.ImageList(this.components);
            this.tsTextFunctions = new System.Windows.Forms.ToolStrip();
            this.cbbFontNames = new System.Windows.Forms.ToolStripComboBox();
            this.cbbFontSizes = new System.Windows.Forms.ToolStripComboBox();
            this.btnFontSizeUp = new System.Windows.Forms.ToolStripButton();
            this.btnFontSizeDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBoldFont = new System.Windows.Forms.ToolStripButton();
            this.btnItalicFont = new System.Windows.Forms.ToolStripButton();
            this.btnUnderlineFont = new System.Windows.Forms.ToolStripButton();
            this.btnStrikeoutFont = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLeftTextAlign = new System.Windows.Forms.ToolStripButton();
            this.btnCenterTextAlign = new System.Windows.Forms.ToolStripButton();
            this.btnRightTextAlign = new System.Windows.Forms.ToolStripButton();
            this.tsFunctions.SuspendLayout();
            this.tsTextFunctions.SuspendLayout();
            this.SuspendLayout();
            // 
            // redtNote
            // 
            this.redtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redtNote.Location = new System.Drawing.Point(3, 71);
            this.redtNote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.redtNote.Name = "redtNote";
            this.redtNote.Size = new System.Drawing.Size(601, 511);
            this.redtNote.TabIndex = 0;
            this.redtNote.Text = "";
            this.redtNote.SelectionChanged += new System.EventHandler(this.UpdateTextSettings);
            // 
            // lblDataSoruceDesc
            // 
            this.lblDataSoruceDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSoruceDesc.AutoSize = true;
            this.lblDataSoruceDesc.Location = new System.Drawing.Point(3, 587);
            this.lblDataSoruceDesc.Name = "lblDataSoruceDesc";
            this.lblDataSoruceDesc.Size = new System.Drawing.Size(134, 20);
            this.lblDataSoruceDesc.TabIndex = 1;
            this.lblDataSoruceDesc.Text = "Источник данных:";
            this.lblDataSoruceDesc.Visible = false;
            // 
            // lblDataSource
            // 
            this.lblDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(134, 587);
            this.lblDataSource.Name = "lblDataSource";
            this.lblDataSource.Size = new System.Drawing.Size(212, 20);
            this.lblDataSource.TabIndex = 2;
            this.lblDataSource.TabStop = true;
            this.lblDataSource.Text = "Источник данных отсутствует";
            this.lblDataSource.Visible = false;
            // 
            // lblChooseDataSource
            // 
            this.lblChooseDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChooseDataSource.AutoSize = true;
            this.lblChooseDataSource.Location = new System.Drawing.Point(3, 619);
            this.lblChooseDataSource.Name = "lblChooseDataSource";
            this.lblChooseDataSource.Size = new System.Drawing.Size(193, 20);
            this.lblChooseDataSource.TabIndex = 3;
            this.lblChooseDataSource.TabStop = true;
            this.lblChooseDataSource.Text = "Выбрать источник данных";
            this.lblChooseDataSource.Visible = false;
            this.lblChooseDataSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChooseDataSource);
            // 
            // lblCleanDataSource
            // 
            this.lblCleanDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCleanDataSource.AutoSize = true;
            this.lblCleanDataSource.Location = new System.Drawing.Point(194, 619);
            this.lblCleanDataSource.Name = "lblCleanDataSource";
            this.lblCleanDataSource.Size = new System.Drawing.Size(197, 20);
            this.lblCleanDataSource.TabIndex = 4;
            this.lblCleanDataSource.TabStop = true;
            this.lblCleanDataSource.Text = "Очистить источник данных";
            this.lblCleanDataSource.Visible = false;
            // 
            // tsFunctions
            // 
            this.tsFunctions.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tsFunctions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsFunctions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFile});
            this.tsFunctions.Location = new System.Drawing.Point(0, 0);
            this.tsFunctions.Name = "tsFunctions";
            this.tsFunctions.Size = new System.Drawing.Size(608, 27);
            this.tsFunctions.TabIndex = 5;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(29, 24);
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
            this.tsTextFunctions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTextFunctions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsTextFunctions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbFontNames,
            this.cbbFontSizes,
            this.btnFontSizeUp,
            this.btnFontSizeDown,
            this.toolStripSeparator1,
            this.btnBoldFont,
            this.btnItalicFont,
            this.btnUnderlineFont,
            this.btnStrikeoutFont,
            this.toolStripSeparator2,
            this.btnLeftTextAlign,
            this.btnCenterTextAlign,
            this.btnRightTextAlign});
            this.tsTextFunctions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsTextFunctions.Location = new System.Drawing.Point(0, 27);
            this.tsTextFunctions.Name = "tsTextFunctions";
            this.tsTextFunctions.Size = new System.Drawing.Size(608, 28);
            this.tsTextFunctions.TabIndex = 6;
            // 
            // cbbFontNames
            // 
            this.cbbFontNames.Name = "cbbFontNames";
            this.cbbFontNames.Size = new System.Drawing.Size(138, 28);
            this.cbbFontNames.SelectedIndexChanged += new System.EventHandler(this.ChangeFontName);
            // 
            // cbbFontSizes
            // 
            this.cbbFontSizes.Name = "cbbFontSizes";
            this.cbbFontSizes.Size = new System.Drawing.Size(85, 28);
            this.cbbFontSizes.SelectedIndexChanged += new System.EventHandler(this.ChangeFontSize);
            // 
            // btnFontSizeUp
            // 
            this.btnFontSizeUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontSizeUp.Image = ((System.Drawing.Image)(resources.GetObject("btnFontSizeUp.Image")));
            this.btnFontSizeUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontSizeUp.Name = "btnFontSizeUp";
            this.btnFontSizeUp.Size = new System.Drawing.Size(29, 25);
            this.btnFontSizeUp.Text = "Увеличить шрифт";
            this.btnFontSizeUp.Click += new System.EventHandler(this.FontSizeUpDown);
            // 
            // btnFontSizeDown
            // 
            this.btnFontSizeDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontSizeDown.Image = ((System.Drawing.Image)(resources.GetObject("btnFontSizeDown.Image")));
            this.btnFontSizeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontSizeDown.Name = "btnFontSizeDown";
            this.btnFontSizeDown.Size = new System.Drawing.Size(29, 25);
            this.btnFontSizeDown.Text = "Уменьшить шрифт";
            this.btnFontSizeDown.Click += new System.EventHandler(this.FontSizeUpDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // btnBoldFont
            // 
            this.btnBoldFont.CheckOnClick = true;
            this.btnBoldFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBoldFont.Image = ((System.Drawing.Image)(resources.GetObject("btnBoldFont.Image")));
            this.btnBoldFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBoldFont.Name = "btnBoldFont";
            this.btnBoldFont.Size = new System.Drawing.Size(29, 25);
            this.btnBoldFont.Text = "Полужирный текст";
            this.btnBoldFont.Click += new System.EventHandler(this.SetFontStyle);
            // 
            // btnItalicFont
            // 
            this.btnItalicFont.CheckOnClick = true;
            this.btnItalicFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItalicFont.Image = ((System.Drawing.Image)(resources.GetObject("btnItalicFont.Image")));
            this.btnItalicFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItalicFont.Name = "btnItalicFont";
            this.btnItalicFont.Size = new System.Drawing.Size(29, 25);
            this.btnItalicFont.Text = "Курсивный текст";
            this.btnItalicFont.Click += new System.EventHandler(this.SetFontStyle);
            // 
            // btnUnderlineFont
            // 
            this.btnUnderlineFont.CheckOnClick = true;
            this.btnUnderlineFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnderlineFont.Image = ((System.Drawing.Image)(resources.GetObject("btnUnderlineFont.Image")));
            this.btnUnderlineFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnderlineFont.Name = "btnUnderlineFont";
            this.btnUnderlineFont.Size = new System.Drawing.Size(29, 25);
            this.btnUnderlineFont.Text = "Подчёркнутый текст";
            this.btnUnderlineFont.Click += new System.EventHandler(this.SetFontStyle);
            // 
            // btnStrikeoutFont
            // 
            this.btnStrikeoutFont.CheckOnClick = true;
            this.btnStrikeoutFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStrikeoutFont.Image = ((System.Drawing.Image)(resources.GetObject("btnStrikeoutFont.Image")));
            this.btnStrikeoutFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStrikeoutFont.Name = "btnStrikeoutFont";
            this.btnStrikeoutFont.Size = new System.Drawing.Size(29, 25);
            this.btnStrikeoutFont.Text = "Перечёркнутый текст";
            this.btnStrikeoutFont.ToolTipText = "btn";
            this.btnStrikeoutFont.Click += new System.EventHandler(this.SetFontStyle);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // btnLeftTextAlign
            // 
            this.btnLeftTextAlign.CheckOnClick = true;
            this.btnLeftTextAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLeftTextAlign.Image = ((System.Drawing.Image)(resources.GetObject("btnLeftTextAlign.Image")));
            this.btnLeftTextAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLeftTextAlign.Name = "btnLeftTextAlign";
            this.btnLeftTextAlign.Size = new System.Drawing.Size(29, 25);
            this.btnLeftTextAlign.Text = "Выровнять по левому краю";
            this.btnLeftTextAlign.Click += new System.EventHandler(this.SetAligmentOnSelectedText);
            // 
            // btnCenterTextAlign
            // 
            this.btnCenterTextAlign.CheckOnClick = true;
            this.btnCenterTextAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCenterTextAlign.Image = ((System.Drawing.Image)(resources.GetObject("btnCenterTextAlign.Image")));
            this.btnCenterTextAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCenterTextAlign.Name = "btnCenterTextAlign";
            this.btnCenterTextAlign.Size = new System.Drawing.Size(29, 25);
            this.btnCenterTextAlign.Text = "Выровнять по центру";
            this.btnCenterTextAlign.Click += new System.EventHandler(this.SetAligmentOnSelectedText);
            // 
            // btnRightTextAlign
            // 
            this.btnRightTextAlign.CheckOnClick = true;
            this.btnRightTextAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRightTextAlign.Image = ((System.Drawing.Image)(resources.GetObject("btnRightTextAlign.Image")));
            this.btnRightTextAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRightTextAlign.Name = "btnRightTextAlign";
            this.btnRightTextAlign.Size = new System.Drawing.Size(29, 25);
            this.btnRightTextAlign.Text = "Выровнять по правому краю";
            this.btnRightTextAlign.Click += new System.EventHandler(this.SetAligmentOnSelectedText);
            // 
            // NoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tsTextFunctions);
            this.Controls.Add(this.tsFunctions);
            this.Controls.Add(this.lblCleanDataSource);
            this.Controls.Add(this.lblChooseDataSource);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.lblDataSoruceDesc);
            this.Controls.Add(this.redtNote);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NoteControl";
            this.Size = new System.Drawing.Size(608, 651);
            this.Load += new System.EventHandler(this.NoteControlLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnNoteControlKeyDown);
            this.tsFunctions.ResumeLayout(false);
            this.tsFunctions.PerformLayout();
            this.tsTextFunctions.ResumeLayout(false);
            this.tsTextFunctions.PerformLayout();
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
        private ImageList imgToolBar;
        private ToolStripButton btnOpenFile;
        private ToolStrip tsTextFunctions;
        private ToolStripComboBox cbbFontNames;
        private ToolStripComboBox cbbFontSizes;
        private ToolStripButton btnFontSizeUp;
        private ToolStripButton btnFontSizeDown;
        private ToolStripButton btnLeftTextAlign;
        private ToolStripButton btnCenterTextAlign;
        private ToolStripButton btnRightTextAlign;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnBoldFont;
        private ToolStripButton btnItalicFont;
        private ToolStripButton btnUnderlineFont;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnStrikeoutFont;
    }
}
