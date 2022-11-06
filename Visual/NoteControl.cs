using NoteManager.CommonTypes.Data;
using System.Text;

namespace NoteManager.Visual
{
    public partial class NoteControl : UserControl
    {
        private ObjectData? _objectData;
        public NoteControl()
        {
            InitializeComponent();
        }

        public void SetObjectData(ObjectData objectData)
        { 
            _objectData = objectData;
            UpdateNote();
        }

        private void UpdateNote()
        {
            redtNote.Clear();
            redtNote.LoadFile(_objectData.Note, RichTextBoxStreamType.RichText);          
            lblDataSource.Text = _objectData?.DataSource?.SourceName ?? Constants.NoDataSource;
        }

        /// <summary>
        /// Выбор источника данных из базы данных.
        /// </summary>
        private void ChooseDataSource(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataSourceForm dataSourceForm = new DataSourceForm();
            if (dataSourceForm.ShowDialog() == DialogResult.OK)
            {
                lblDataSource.Text = dataSourceForm.DataSource?.SourceName ?? Constants.NoDataSource;
            }
        }

        private void SaveText(object sender, EventArgs e)
        {
            _objectData.Note.Dispose();
            _objectData.Note = new MemoryStream();
            // Сохраняем содержимое richEdit в память объекта данных
            redtNote.SaveFile(_objectData.Note, RichTextBoxStreamType.RichText);
            
            // Отмечаем, что данные обновились, и при сохранении в БД это нужно учитывать.          
            // Но в случае, если узел только создан без сохранения в БД, то статус не меняем.
            if (_objectData.DataStatus != CommonTypes.Enums.DataStatus.DataAdd)
                _objectData.DataStatus = CommonTypes.Enums.DataStatus.DataUpdate;
        }

        private void OpenTextFile(object sender, EventArgs e)
        {
            // Открываем текстовый файл и кидаем его содержимое в richEdit
            using (OpenFileDialog OPD = new OpenFileDialog())
            {
                OPD.Filter = Constants.TxtFileFilterDesc;
                OPD.RestoreDirectory = true;

                if (OPD.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader reader = new StreamReader(OPD.FileName, Encoding.UTF8))
                    {
                        // Предварительно чистим поле ввода
                        redtNote.Clear();
                        redtNote.Text = reader.ReadToEnd();
                    }    
                }
            }
        }

        private void NoteControlLoad(object sender, EventArgs e)
        {
            // Здесь формируем набор в комбобоксе шрифтов и размеров шрифтов
            InitFonts();
            InitFontSizes();
        }

        /// <summary>
        /// Заполняет комбобокс списком шрифтов.
        /// </summary>
        private void InitFonts()
        {
            foreach (FontFamily item in FontFamily.Families)
                cbbFontNames.Items.Add(item.Name);
            cbbFontNames.SelectedItem = cbbFontNames.Items[0];
        }

        /// <summary>
        /// Заполняет комбобокс размеров шрифта.
        /// </summary>
        private void InitFontSizes()
        {
            // Взял размеры шрифтов из Word'a
            int[] fontSizes = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

            foreach (var fontSize in fontSizes)
                cbbFontSizes.Items.Add(fontSize);
            cbbFontSizes.SelectedItem = cbbFontSizes.Items[0];
        }

        /// <summary>
        /// Увеличение размера шрифта.
        /// </summary>
        private void FontSizeUp(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size + 1,
                                              redtNote.SelectionFont.Style);
            UpdateTextSettings(null, new EventArgs());
        }

        /// <summary>
        /// Уменьшение размера шрифта.
        /// </summary>
        private void FontSizeDown(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size - 1, 
                                              redtNote.SelectionFont.Style);
            UpdateTextSettings(null, new EventArgs());
        }

        /// <summary>
        /// Устанавливает полужирный шрифт на выделенном тексте.
        /// </summary>
        private void SetBoldOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style ^ FontStyle.Bold);
            UpdateTextSettings(null, new EventArgs());
        }

        /// <summary>
        /// Устанавливает курсив на выделенном тексте. 
        /// </summary>
        private void SetItalicOnSelection(object sender, EventArgs e)
        {           
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style ^ FontStyle.Italic);
            UpdateTextSettings(null, new EventArgs());
        }

        /// <summary>
        /// Устанавливает подчеркивание на выделенном тексте.
        /// </summary>
        private void SetUnderlineOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style ^ FontStyle.Underline);
            UpdateTextSettings(null, new EventArgs());
        }

        /// <summary>
        /// Устанавливает перечеркивание на выделенном тексте.
        /// </summary>     
        private void SetStrikeoutOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style ^ FontStyle.Strikeout);
            UpdateTextSettings(null, new EventArgs());
        }

        private void UpdateTextSettings(object sender, EventArgs e)
        {
            // Обновляем состояние панели редактора текста

            // Проверяем выбранные режимы шрифтов
            btnBoldFont.Checked = redtNote.SelectionFont.Bold;
            btnItalicFont.Checked = redtNote.SelectionFont.Italic;
            btnUnderlineFont.Checked = redtNote.SelectionFont.Underline;
            btnStrikeoutFont.Checked = redtNote.SelectionFont.Strikeout;

            // Проверяем выбранный режим выравнивания текста
            btnLeftTextAlign.Checked = redtNote.SelectionAlignment == HorizontalAlignment.Left;
            btnCenterTextAlign.Checked = redtNote.SelectionAlignment == HorizontalAlignment.Center;
            btnRightTextAlign.Checked = redtNote.SelectionAlignment == HorizontalAlignment.Right;
             

            // Проверяем выбранный шрифт и размер шрифта
            cbbFontNames.SelectedItem = redtNote.SelectionFont.Name;
            cbbFontSizes.Text = redtNote.SelectionFont.Size.ToString();
        }

        private void SetLeftAlignmentOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionAlignment = HorizontalAlignment.Left;
            UpdateTextSettings(null, new EventArgs());
        }

        private void SetCenterAlignmentOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionAlignment = HorizontalAlignment.Center;
            UpdateTextSettings(null, new EventArgs());
        }

        private void SetRightAlignmentOnSelection(object sender, EventArgs e)
        {
            redtNote.SelectionAlignment = HorizontalAlignment.Right;
            UpdateTextSettings(null, new EventArgs());
        }
    }
}
