using NoteManager.CommonTypes.Data;

namespace NoteManager.Visual
{
    public partial class DataSourceForm : Form
    {
        private DataSource? _dataSource = null;
        public DataSourceForm()
        {
            InitializeComponent();          
        }

        public DataSource? DataSource { get => _dataSource; }
    }
}
