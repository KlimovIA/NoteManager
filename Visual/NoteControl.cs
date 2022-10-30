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

        private void lblChooseDataSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
    }
}
