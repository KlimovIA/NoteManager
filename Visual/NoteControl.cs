using EducationHelper.CommonTypes.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationHelper.Visual
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
    }
}
