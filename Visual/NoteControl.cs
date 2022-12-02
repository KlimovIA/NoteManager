using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Data.Debug;
using System.Text;

namespace NoteManager.Visual
{
    public partial class NoteControl : UserControl
    {
        private ObjectData? _objectData;
        private bool _textAutoSaveEnabled;

        public NoteControl()
        {
            InitializeComponent();
        }

        public void TerminateAutosaveThread()
        {
            SetAutosaveEnabled(false);
            // Найти возможность безопасно схлопнуть программу
        }

        private void SetAutosaveEnabled(bool enabled)
        {
            _textAutoSaveEnabled = enabled;
            if (_textAutoSaveEnabled)
                StartAutosaveThread();
        }

        private void StartAutosaveThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SaveText));           
        }

        private void NoteControlLoad(object sender, EventArgs e)
        {
            // Здесь формируем набор в комбобоксе шрифтов и размеров шрифтов
            InitFonts();
            InitFontSizes();
            SetAutosaveEnabled(false);
        }

        public void SetObjectData(ObjectData objectData)
        {
            if (objectData.ObjectType == CommonTypes.Enums.ENodeType.NoteNode)
            {
                // Стопим поток, чтобы переопределить объект, в который будет сохраняться текст.
                Visible = true;
                SetAutosaveEnabled(false);

                _objectData = objectData;
                UpdateNote();

                // Возвращаем поток к работе               
                SetAutosaveEnabled(true);
            }
            else
            {
                Visible = false;
                SetAutosaveEnabled(false);
            }
        }

        private void SaveText(object stateInfo)
        {
            const int AUTOSAVE_TIMEOUT = 1000;
            while (_textAutoSaveEnabled)
            {               
                Invoke(new Action(SaveTextInObjectData));
                Thread.Sleep(AUTOSAVE_TIMEOUT);
            }           
        }

        private void UpdateNote()
        {
            redtNote.Clear();
            if (_objectData?.Note?.Capacity > 0)
            {
                _objectData.Note.Position = 0;
                redtNote.LoadFile(_objectData.Note, RichTextBoxStreamType.RichText);
                lblDataSource.Text = _objectData?.DataSource?.SourceName ?? Constants.NoDataSource;
            }
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

        private void SaveTextInObjectData()
        {
            if (_objectData is not null)
            {
                if (_objectData.Note is not null)
                {
                    _objectData.Note.Dispose();
                    _objectData.Note = new MemoryStream();
                    // Сохраняем содержимое richEdit в память объекта данных
                    redtNote.SaveFile(_objectData.Note, RichTextBoxStreamType.RichText);

                    // Отмечаем, что данные обновились, и при сохранении в БД это нужно учитывать.          
                    // Но в случае, если узел только создан без сохранения в БД, то статус не меняем.
                    if (_objectData.DataStatus != CommonTypes.Enums.EDataStatus.DataAdd)
                        _objectData.DataStatus = CommonTypes.Enums.EDataStatus.DataUpdate;
                }
            }
        }

        private async void OpenTextFile(object? sender, EventArgs e)
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
                        redtNote.Text = await reader.ReadToEndAsync();
                    }
                }
            }
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

        private void FontSizeUpDown(object sender, EventArgs e)
        {
            // Универсальный метод для двух кнопок изменения размера шрифта на единицу (+1 или -1 в зависимости от задействованной кнопки)
            string fontName = cbbFontNames.SelectedItem.ToString() ?? "";
            int fontSize = sender == btnFontSizeUp ? 1 : -1;

            redtNote.SelectionFont = new Font(fontName,
                                              redtNote.SelectionFont.Size + fontSize,
                                              redtNote.SelectionFont.Style);
            UpdateTextSettings(null, new EventArgs());
        }

        private void UpdateTextSettings(object? sender, EventArgs e)
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

        private void SetAligmentOnSelectedText(object sender, EventArgs e)
        {
            if (sender == btnLeftTextAlign) redtNote.SelectionAlignment = HorizontalAlignment.Left;
            if (sender == btnCenterTextAlign) redtNote.SelectionAlignment = HorizontalAlignment.Center;
            if (sender == btnRightTextAlign) redtNote.SelectionAlignment = HorizontalAlignment.Right;

            UpdateTextSettings(null, new EventArgs());
        }

        private void SetFontStyle(object sender, EventArgs e)
        {
            string fontName = cbbFontNames.SelectedItem.ToString() ?? "";
            FontStyle fontStyle = FontStyle.Regular;

            if (sender == btnBoldFont) fontStyle = FontStyle.Bold;
            if (sender == btnItalicFont) fontStyle = FontStyle.Italic;
            if (sender == btnStrikeoutFont) fontStyle = FontStyle.Strikeout;
            if (sender == btnUnderlineFont) fontStyle = FontStyle.Underline;

            redtNote.SelectionFont = new Font(fontName,
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style ^ fontStyle);
            UpdateTextSettings(null, new EventArgs());
        }

        private void ChangeFontName(object sender, EventArgs e)
        {
            string fontName = cbbFontNames.SelectedItem.ToString() ?? "";
            redtNote.SelectionFont = new Font(fontName,
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style);
            redtNote.Focus();

        }

        private void ChangeFontSize(object sender, EventArgs e)
        {
            float fontSize;
            float.TryParse(cbbFontSizes.SelectedItem.ToString(), out fontSize);
            redtNote.SelectionFont = new Font(redtNote.SelectionFont.Name,
                                              fontSize,
                                              redtNote.SelectionFont.Style);
            redtNote.Focus();
        }

        private void OnNoteControlKeyDown(object sender, KeyEventArgs e)
        {
            // Пока мертво
            if (e.Control && e.KeyValue == (int)Keys.O)
                OpenTextFile(null, new EventArgs());
        }
    }
}
