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
            this.redtNote = new System.Windows.Forms.RichTextBox();
            this.lblDataSoruceDesc = new System.Windows.Forms.Label();
            this.lblDataSource = new System.Windows.Forms.LinkLabel();
            this.lblChooseDataSource = new System.Windows.Forms.LinkLabel();
            this.lblCleanDataSource = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // redtNote
            // 
            this.redtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.redtNote.Location = new System.Drawing.Point(3, 3);
            this.redtNote.Name = "redtNote";
            this.redtNote.Size = new System.Drawing.Size(362, 265);
            this.redtNote.TabIndex = 0;
            this.redtNote.Text = "";
            // 
            // lblDataSoruceDesc
            // 
            this.lblDataSoruceDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSoruceDesc.AutoSize = true;
            this.lblDataSoruceDesc.Location = new System.Drawing.Point(3, 284);
            this.lblDataSoruceDesc.Name = "lblDataSoruceDesc";
            this.lblDataSoruceDesc.Size = new System.Drawing.Size(108, 15);
            this.lblDataSoruceDesc.TabIndex = 1;
            this.lblDataSoruceDesc.Text = "Источник данных:";
            // 
            // lblDataSource
            // 
            this.lblDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataSource.AutoSize = true;
            this.lblDataSource.Location = new System.Drawing.Point(117, 284);
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
            this.lblChooseDataSource.Location = new System.Drawing.Point(3, 308);
            this.lblChooseDataSource.Name = "lblChooseDataSource";
            this.lblChooseDataSource.Size = new System.Drawing.Size(146, 15);
            this.lblChooseDataSource.TabIndex = 3;
            this.lblChooseDataSource.TabStop = true;
            this.lblChooseDataSource.Text = "Выбрать источнк данных";
            this.lblChooseDataSource.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblChooseDataSource_LinkClicked);
            // 
            // lblCleanDataSource
            // 
            this.lblCleanDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCleanDataSource.AutoSize = true;
            this.lblCleanDataSource.Location = new System.Drawing.Point(170, 308);
            this.lblCleanDataSource.Name = "lblCleanDataSource";
            this.lblCleanDataSource.Size = new System.Drawing.Size(164, 15);
            this.lblCleanDataSource.TabIndex = 4;
            this.lblCleanDataSource.TabStop = true;
            this.lblCleanDataSource.Text = "Очистить источникк данных";
            // 
            // NoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.lblCleanDataSource);
            this.Controls.Add(this.lblChooseDataSource);
            this.Controls.Add(this.lblDataSource);
            this.Controls.Add(this.lblDataSoruceDesc);
            this.Controls.Add(this.redtNote);
            this.Name = "NoteControl";
            this.Size = new System.Drawing.Size(368, 332);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox redtNote;
        private Label lblDataSoruceDesc;
        private LinkLabel lblDataSource;
        private LinkLabel lblChooseDataSource;
        private LinkLabel lblCleanDataSource;
    }
}
