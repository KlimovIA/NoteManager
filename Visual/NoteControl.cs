using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Data.Debug;
using System.Text;

namespace NoteManager.Visual
{
    public partial class NoteControl : UserControl
    {
        private ObjectData? _objectData;
        private static ManualResetEvent _threadStopper = new ManualResetEvent(true);
        private Thread _textAutoSaveThread;
        public NoteControl()
        {
            InitializeComponent();
        }

        private void NoteControlLoad(object sender, EventArgs e)
        {
            // Здесь формируем набор в комбобоксе шрифтов и размеров шрифтов
            InitFonts();
            InitFontSizes();

            _textAutoSaveThread = new Thread(new ThreadStart(SaveText));
            _textAutoSaveThread.Start();           
        }

        public void SetObjectData(ObjectData objectData)
        {
            if (objectData.ObjectType == CommonTypes.Enums.ObjectType.NoteNode)
            {
                Visible = true;
                _threadStopper.Reset();
                DebugConsole.WriteLogMessage("_threadStopper.Reset()");


                _objectData = objectData;
                UpdateNote();

                DebugConsole.WriteLogMessage("_threadStopper.Set()");
                _threadStopper.Set();
            }
            else
            {
                Visible = false;
                DebugConsole.WriteLogMessage("_threadStopper.Reset()");
                _threadStopper.Reset();
            }
        }

        private void SaveText()
        {
            const int AUTOSAVE_TIMEOUT = 1000;
            while (true)
            {
                _threadStopper.WaitOne();
                DebugConsole.WriteLogMessage("SaveTextInObjectData");
                Invoke(new Action(SaveTextInObjectData));
                Thread.Sleep(AUTOSAVE_TIMEOUT);
            }           
        }

        private void UpdateNote()
        {
            redtNote.Clear();
            if (_objectData.Note.Capacity > 0)
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
                    DebugConsole.WriteLogMessage("Текст сохранен!");
                    _objectData?.Note?.Dispose();
                    _objectData.Note = new MemoryStream();
                    // Сохраняем содержимое richEdit в память объекта данных
                    redtNote.SaveFile(_objectData.Note, RichTextBoxStreamType.RichText);

                    // Отмечаем, что данные обновились, и при сохранении в БД это нужно учитывать.          
                    // Но в случае, если узел только создан без сохранения в БД, то статус не меняем.
                    if (_objectData.DataStatus != CommonTypes.Enums.DataStatus.DataAdd)
                        _objectData.DataStatus = CommonTypes.Enums.DataStatus.DataUpdate;
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

        private void FontSizeUp(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size + 1,
                                              redtNote.SelectionFont.Style);
            UpdateTextSettings(null, new EventArgs());
        }

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

        private void ChangeFontName(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(cbbFontNames.SelectedItem.ToString(),
                                              redtNote.SelectionFont.Size,
                                              redtNote.SelectionFont.Style);
            redtNote.Focus();

        }

        private void ChangeFontSize(object sender, EventArgs e)
        {
            redtNote.SelectionFont = new Font(redtNote.SelectionFont.Name,
                                              float.Parse(cbbFontSizes.SelectedItem.ToString()),
                                              redtNote.SelectionFont.Style);
            redtNote.Focus();
        }

        private void OnNoteControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == (int)Keys.O)
                OpenTextFile(null, new EventArgs());
        }
    }
}
