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
    public partial class DataSourceForm : Form
    {
        private DataSource _dataSource;
        public DataSourceForm()
        {
            InitializeComponent();          
        }

        public DataSource DataSource { get => _dataSource; }
    }
}
