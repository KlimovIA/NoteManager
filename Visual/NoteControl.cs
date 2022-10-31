using NoteManager.CommonTypes.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            redtNote.Text = _objectData?.Note?.ToString() ?? "";
            lblDataSource.Text = _objectData?.DataSource?.SourceName ?? "Источник данных отсутсвует";
        }

        private void ChooseDataSource(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataSourceForm dataSourceForm = new DataSourceForm();
            if (dataSourceForm.ShowDialog() == DialogResult.OK)
            {
                lblDataSource.Text = dataSourceForm.DataSource?.SourceName ?? "Источник данных отсутствует";
            }
        }

        private void SaveText(object sender, EventArgs e)
        {
            // Сохраняем содержимое richEdit в память объекта данных
            _objectData?.Note?.Clear();
            _objectData?.Note?.Append(redtNote.Text);
        }

        private void OpenTextFile(object sender, EventArgs e)
        {
            // Открываем текстовый файл и кидаем его содержимое в richEdit
            using (OpenFileDialog OPD = new OpenFileDialog())
            {
                OPD.Filter = "Текстовый файл (*.txt)|*.txt";
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
        /// Заполняет комбобокс списком шрифтов
        /// </summary>
        private void InitFonts()
        {
            foreach (FontFamily item in FontFamily.Families)
                cbbFontNames.Items.Add(item.Name);
            cbbFontNames.SelectedItem = cbbFontNames.Items[0];
        }

        /// <summary>
        /// Заполняет комбобокс размеров шрифта
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

        }

        private void FontSizeDown(object sender, EventArgs e)
        {

        }
    }
}
