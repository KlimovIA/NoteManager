using EducationHelper.CommonTypes.Data;
using EducationHelper.CommonTypes.Enums;
using EducationHelper.Database;
using EducationHelper.Properties;
using EducationHelper.Visual;
using System.Linq;


namespace EducationHelper
{
    public partial class MainForm : Form
    {
        private ImageList? _imgTreeView;
        private readonly DatabaseManager _databaseManager;
        
        public MainForm()
        {
            InitializeComponent();
            InitImageList();
            InitMainRootNode();
                        
            _databaseManager = new DatabaseManager();
            _databaseManager.DatabaseActionEvent += ShowActionMessage;
            //tvObjectTree.Nodes.Clear();

            LoadObjectsFromDatabase();
        }

        /// <summary>
        /// Создаёт корневой узел, который является основной для хранения всего дерева.
        /// </summary>
        private void InitMainRootNode()
        {
            // Его не нужно добавлять в общий набор объектов. Он будет всегдда неизменным.
            TreeNode node = tvObjectTree.Nodes.Add("Корневой узел");
            node.SelectedImageIndex = (byte)ObjectType.RootNode;
            node.StateImageIndex = (byte)ObjectType.RootNode;
            node.ImageIndex = (byte)ObjectType.RootNode;
            node.Tag = new ObjectData(-1,
                                      -1,
                                      node.Text,
                                      ObjectType.RootNode
                                      );
        }      
       
        private void LoadObjectsFromDatabase()
        {
            _databaseManager.LoadObjectTreeFromDB();
            // Подгружаем дерево из БД и заполняем программу
            foreach (var objectData in ObjectDataManager.ObjectDataList)
            {
                AddNode(objectData);
            }
        }
        private void InitImageList()
        {
            // Для отображения в TreeView узлов в соответствии с выбранным типом
            // К сожалению, нельзя установить изображение напрямую из ресурсов
            _imgTreeView = new ImageList();
            _imgTreeView.Images.Add(Resources.FolderIcon);
            _imgTreeView.Images.Add(Resources.NoteIcon);
            _imgTreeView.Images.Add(Resources.RootNodeIcon);
            
            tvObjectTree.ImageList = _imgTreeView;
        }

        private void RemoveNode(TreeNode node)
        {
            // Если у узла нет детей, то шлёпаем этот узел и уходим
            if (node.Nodes.Count == 0)
            {
                ((ObjectData)node.Tag).DataStatus = DataStatus.DataDelete;
                node.Remove();
                return;
            }
            else
            {
                while (node.Nodes.Count != 0)
                {
                    ((ObjectData)node.Tag).DataStatus = DataStatus.DataDelete;
                    RemoveNode(node.Nodes[0]);
                }
                node.Remove();
            }
        }

        private void ShowActionMessage(string Message) => lbActionStatus.Text = Message;

        private void AddNode(ObjectData objectData)
        {
            TreeNode node = null;
            tvObjectTree.BeginUpdate();

            // Если объект не имеет родителя, то быстренько добавляем его в лист
            if (objectData.ParentID == -1)
            {
                node = tvObjectTree.Nodes[0];
                node = node.Nodes.Add(objectData.ObjectName);
                node.Tag = objectData;
                // Обозачаем выбраннный узел иконкой в зависимости от выбранного типа
                node.ImageIndex = (byte)objectData.ObjectType;
                node.SelectedImageIndex = (byte)objectData.ObjectType;
            }
            // Иначе ищем родительский узел и добавляем в него
            else
            {
                foreach (TreeNode treeNode in tvObjectTree.Nodes)
                    node = FindParentNode(treeNode, objectData.ParentID);

                TreeNode tmpNode;
                if (node != null)
                {
                    tmpNode = node.Nodes.Add(objectData.ObjectName);
                    tmpNode.Tag = objectData;
                }
                else
                {
                    tmpNode = tvObjectTree.Nodes.Add(objectData.ObjectName);
                    tmpNode.Tag = objectData;
                }
                // Обозачаем выбраннный узел иконкой в зависимости от выбранного типа
                tmpNode.ImageIndex = (byte)objectData.ObjectType;
                tmpNode.SelectedImageIndex = (byte)objectData.ObjectType;
            }
            tvObjectTree.EndUpdate();
        }

        private TreeNode FindParentNode(TreeNode node, int ParentID)
        {
            TreeNode parentNode = null;
            // Если в узле не нашли нужное значение, идём в дочерние узлы
            if (((ObjectData)node.Tag).ObjectID != ParentID)
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (((ObjectData)child.Tag).ObjectID != ParentID)
                    {
                        if (child.Nodes.Count != 0)
                            parentNode = FindParentNode(child, ParentID);
                        else parentNode = null;
                    }
                    else parentNode = child;
                }
            }
            else
            { 
                parentNode = node;
            }
            return parentNode;
        }
        private void AddNode(ObjectType nodeType, DataStatus dataStatus = DataStatus.DataAdd)
        {
            TreeNode? node;
            tvObjectTree.BeginUpdate();

            // Для регулирования вложенностей в дереве
            if (tvObjectTree.SelectedNode != null)
                node = tvObjectTree.SelectedNode.Nodes.Add(CommonTypes.Data.CommonTypes.NoteTypeDesc[(byte)nodeType]);
            else
                node = tvObjectTree.Nodes.Add(CommonTypes.Data.CommonTypes.NoteTypeDesc[(byte)nodeType]);

            // Пишем наш объект данных в узел
            if (nodeType == ObjectType.NoteNode)
            {
                node.Tag = new ObjectData(ItemIDManager.GetNewItemID(),
                                          node.Parent == null ? -1 : ((ObjectData)node.Parent.Tag).ObjectID,
                                          node.Text,
                                          nodeType,
                                          -1,
                                          new System.Text.StringBuilder()
                                         );
            }
            else
            {
                node.Tag = new ObjectData(ItemIDManager.GetNewItemID(),
                                          node.Parent == null ? -1 : ((ObjectData)node.Parent.Tag).ObjectID,
                                          node.Text,
                                          nodeType
                                         );
            }

            // Ставим отметку для БД. При загрузке из БД статус по умолчанию будет - DataNoneChange 
            ((ObjectData)node.Tag).DataStatus = dataStatus;

            // Сохраняем в список, чтобы сборщик мусора не сожрал объект данных,
            // чтобы я корректно убрал объект из базы данных
            ObjectDataManager.ObjectDataList.Add((ObjectData)node.Tag);

            // Обозачаем выбраннный узел иконкой в зависимости от выбранного типа
            node.ImageIndex = (byte)nodeType;
            node.SelectedImageIndex = (byte)nodeType;

            // Выбираем свежесозданный узел
            tvObjectTree.SelectedNode = node;

            tvObjectTree.EndUpdate();
        }

        private void tsAddFolder_Click(object sender, EventArgs e)
        {
            AddNode(ObjectType.FolderNode);
        }

        private void tsAddNote_Click(object sender, EventArgs e)
        {
            AddNode(ObjectType.NoteNode);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Если нет выбранного узла, то и удалять нечего
            if (tvObjectTree.SelectedNode == null) tsBtnRemoveNode.Enabled = false;
        }

        private void tvObjectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvObjectTree.SelectedNode != null)
            {
                /* Если выбрали узел, то:
                 * 1. Делаем доступной кнопку удаления узла
                 * 2.В кнопке добавления проверяем, к какому типу относится узел.
                 * Если узел - папка, то в ней можно создавать объекты до условной бесконечности
                 * Если узел - заметка, то у этого узла не может быть дочерних узлов
                 */
                tsBtnRemoveNode.Enabled = true;

                if (tvObjectTree.SelectedNode.Tag is not null)
                {
                    switch (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType)
                    {
                        case ObjectType.FolderNode:
                            // Здесь ничего не меняется
                            tsAddFolder.Enabled = true;
                            break;
                        case ObjectType.NoteNode:
                            // Здесь блокируется кнопка добавления объектов
                            tsAddFolder.Enabled = false;
                            break;
                    }
                }
            }
        }

        private void msTreeView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tvObjectTree.SelectedNode == null)
            {
                miDeleteNode.Enabled = false;
            }
            else
            {
                if (tvObjectTree.SelectedNode.Tag is not null)
                {
                    // Заметка - конечный узел, в него нельзя закладывать папки или другие заметки
                    if (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType == ObjectType.NoteNode)
                    {
                        miAddFolder.Enabled = false;
                        miAddNote.Enabled = false;
                        miDeleteNode.Enabled = true;
                    }
                    else
                    {
                        miAddFolder.Enabled = true;
                        miAddNote.Enabled = true;
                        miDeleteNode.Enabled = true;
                    }
                }
            }
        }

        private void tvObjectTree_DragEnter(object sender, DragEventArgs e)
        {
            // В sender'e не node, a treeview!!
            if (sender is not TreeNode)
            {
                // Не допускаем использование механизма Drag&Drop в случае перетаскивания не
                // узла дерева (TreeNode)
                // В этом методе будет меняться лишь курсор, остальная работа будет проводиться
                // в другом методе.

                //tvObjectTree.Cursor = Cursors.No;               
            }
            else
            {
                e.Effect = e.AllowedEffect;
                // Курсор доступа к перетаскиванию
                //tvObjectTree.Cursor = new Cursor(Resources.DragAndDropIcon.GetHicon());
            }
        }

        private void tsBtnRemoveNode_Click(object sender, EventArgs e)
        {
            if (tvObjectTree.SelectedNode != null)
                RemoveNode(tvObjectTree.SelectedNode);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjectDataManager.ObjectDataList.Clear();
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            btnApplyChanges.Enabled = !_databaseManager.UpdateObjectsInDatabase();
        }

        private void tvObjectTree_MouseDown(object sender, MouseEventArgs e)
        {
            // Активация узла дерева по нажатию правой кнопки мыши
            tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(new Point(e.X, e.Y));
        }

        private void tvObjectTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Возникает после изменения измения имени узла
            ((ObjectData)e.Node.Tag).ObjectName = e.Label;             
            
        }

        private void tvObjectTree_KeyDown(object sender, KeyEventArgs e)
        {
            // Добавляем функционал включения переименовывания узлов дерева
            if (e.KeyValue == (int)Keys.F2)
                tvObjectTree.SelectedNode?.BeginEdit();
        }
    }
}