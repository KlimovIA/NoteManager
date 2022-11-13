using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Enums;
using NoteManager.Database;
using NoteManager.Properties;
using NoteManager.CommonTypes.Data.Debug;

namespace NoteManager
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
            InitNoteControl();

            _databaseManager = new DatabaseManager();
            _databaseManager.DatabaseActionEvent += ShowActionMessage;

            LoadObjectsFromDatabase();

#if DEBUG
            // Для отладки
            DebugConsole.AllocConsole(); 
#endif
        }                    
        
        /// <summary>
        /// Инициирует контрол заметки, подвязывает callback функции, если таковые имеются.
        /// </summary>
        private void InitNoteControl()
        {
            ncNote.OnChange += ActivateSaveDBButton;
        }

        /// <summary>
        /// Создаёт корневой узел, который является основной для хранения всего дерева.
        /// </summary>
        private void InitMainRootNode()
        {
            ObjectType nodeType = ObjectType.RootNode;
            // Его не нужно добавлять в общий набор объектов. Он будет всегдда неизменным.
            TreeNode node = tvObjectTree.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[(byte)nodeType]);
            node.SelectedImageIndex = (byte)ObjectType.RootNode;
            node.StateImageIndex = (byte)ObjectType.RootNode;
            node.ImageIndex = (byte)ObjectType.RootNode;
            node.Tag = new ObjectData(-1,
                                      -1,
                                      node.Text,
                                      ObjectType.RootNode
                                      );
        }      
       
        /// <summary>
        /// Загрузка дерева объектов из базы данных.
        /// </summary>
        private void LoadObjectsFromDatabase()
        {
            _databaseManager.LoadObjectTreeFromDB();
            // Подгружаем дерево из БД и заполняем программу
            foreach (var objectData in ObjectDataManager.ObjectDataList)
            {
                AddNode(objectData);
            }
        }
        /// <summary>
        /// Инициирует изображение иконок для узлов.
        /// </summary>
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

        /// <summary>
        /// Удаление узла.
        /// </summary>
        /// <param name="node"></param>
        private void RemoveNode(TreeNode node)
        {
            // Если у узла нет детей, то шлёпаем этот узел и уходим
            if (node.Nodes.Count == 0)
            {
                ((ObjectData)node.Tag).DataStatus = DataStatus.DataDelete;
                node.Remove();
                
                // Фиксируем, что произошли изменения, которые можно сохранить в БД
                ActivateSaveDBButton();
                
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

        /// <summary>
        /// Отображает на форме статус модификации данных из других потоков.
        /// </summary>
        /// <param name="Message">Текст статуса</param>
        private void ShowActionMessage(string Message) => lbActionStatus.Text = Message;

        /// <summary>
        /// Добавление узла из базыы данных
        /// </summary>
        /// <param name="objectData">Объект данных</param>
        private void AddNode(ObjectData objectData)
        {
            TreeNode? node = null;
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

        /// <summary>
        /// Поиск родительского узла по значению ParentID объекта данных (ObjectData)
        /// </summary>
        /// <param name="node">Узел для проверки</param>
        /// <param name="ParentID">Идентификационный номер родительского узла</param>
        /// <returns></returns>
        private TreeNode? FindParentNode(TreeNode node, int ParentID)
        {
            TreeNode? parentNode = null;
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
       
        /// <summary>
        /// Добавление нового узла.
        /// </summary>
        /// <param name="nodeType"> Тип узла</param>
        /// <param name="dataStatus">Статус данных. В данном случае статус по умолчанию DataAdd(добавление в БД)</param>
        private void AddNode(ObjectType nodeType, DataStatus dataStatus = DataStatus.DataAdd)
        {
            TreeNode? node;
            tvObjectTree.BeginUpdate();

            // Для регулирования вложенностей в дереве
            if (tvObjectTree.SelectedNode != null)
            {
                node = tvObjectTree.SelectedNode.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[(byte)nodeType]);
            }
            else
            {
                //  Если узел не выбран, то цепляемся к корневому узлу
                TreeNode root = tvObjectTree.Nodes[0];
                node = root.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[(byte)nodeType]);
            }
               

            // Пишем наш объект данных в узел
            if (nodeType == ObjectType.NoteNode)
            {
                node.Tag = new ObjectData(ItemIDManager.GetNewItemID(),
                                          node.Parent == null ? -1 : ((ObjectData)node.Parent.Tag).ObjectID,
                                          node.Text,
                                          nodeType,
                                          -1,
                                          new MemoryStream()
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

            // Фиксируем, что произошли изменения, которые можно сохранить в БД
            ActivateSaveDBButton();
        }

        private void tsAddFolder_Click(object sender, EventArgs e)
        {
            AddNode(ObjectType.FolderNode);
        }

        private void tsAddNote_Click(object sender, EventArgs e)
        {
            AddNode(ObjectType.NoteNode);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // Если нет выбранного узла, то и удалять нечего
            if (tvObjectTree.SelectedNode == null) tsBtnRemoveNode.Enabled = false;
        }

        private void OnObjectTreeAfterSelect(object sender, TreeViewEventArgs e)
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
                        case ObjectType.RootNode:
                            // Здесь убираем контрол заметки
                            tsAddFolder.Enabled = true;
                            ncNote.Visible = false;
                            break;
                        case ObjectType.NoteNode:
                            // Здесь блокируется кнопка добавления объектов и появляется контрол редактора текста
                            tsAddFolder.Enabled = false;
                            ncNote.Visible = true;
                            ncNote.SetObjectData((ObjectData)tvObjectTree.SelectedNode.Tag);
                            break;
                    }
                }
            }
        }

        private void msTreeView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tvObjectTree.SelectedNode == null || IsRootNode)
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
        private void tsBtnRemoveNode_Click(object sender, EventArgs e)
        {
            if (tvObjectTree.SelectedNode != null)
                RemoveNode(tvObjectTree.SelectedNode);           
        }

        private void OnFormCLosed(object sender, FormClosedEventArgs e)
        {
            ObjectDataManager.ObjectDataList.Clear();
        }

        private void SaveToDataBase(object sender, EventArgs e)
        {
            btnSaveToDB.Enabled = !_databaseManager.SaveToDataBase();
        }

        private void OnObjectTreeMouseDown(object sender, MouseEventArgs e)
        {
            // Активация узла дерева по нажатию правой кнопки мыши
            tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(new Point(e.X, e.Y));
        }

        private void OnObjectTreeAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Возникает после изменения измения имени узла
            if (e.Label is not null)
            {
                ((ObjectData)e.Node.Tag).ObjectName = e.Label;
                ActivateSaveDBButton();
            }
        }

        private void OnObjectTreeKeyDown(object sender, KeyEventArgs e)
        {
            // Добавляем функционал включения переименовывания узлов дерева
            if (e.KeyValue == (int)Keys.F2)
                tvObjectTree.SelectedNode?.BeginEdit();

            // Удаление по клавише Delete
            // Не должно работать с корневым узлом
            if (e.KeyValue == (int)Keys.Delete && !IsRootNode)
                if (tvObjectTree.SelectedNode is not null)
                    RemoveNode(tvObjectTree.SelectedNode);
        }

        /// <summary>
        /// Является ли выбранный узел корневым.
        /// Сделано для ограничения взаимодействий с корневым узлом (запрет на удаление и, возможно, что-то еще).
        /// </summary>
        private bool IsRootNode => tvObjectTree.SelectedNode == tvObjectTree.Nodes[0];
        
        private void OnBtnAddNodeDropDownOpened(object sender, EventArgs e)
        {
            if (tvObjectTree.SelectedNode is not null)
            {
                tsAddNote.Enabled = ((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType != ObjectType.NoteNode;
                tsAddFolder.Enabled = ((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType != ObjectType.NoteNode;
            }
        }

        private void ActivateSaveDBButton() => btnSaveToDB.Enabled = true;

        private void OnObjectTreeItemDrag(object sender, ItemDragEventArgs e)
        {
            // Если пользователь пытается воткнуть что-то другое - выходим.
            if (e.Item is not TreeNode) return;

            // Порверяем, что объект данных узла не является корневым узлом, сразу пресекаем
            if ((e.Button == MouseButtons.Left) && 
               (((ObjectData)((TreeNode)e.Item).Tag).ObjectType != ObjectType.RootNode))
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void OnObjectTreeDragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = tvObjectTree.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvObjectTree.GetNodeAt(targetPoint);
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (!draggedNode.Equals(targetNode) &&
                !draggedNode.ContainsNode(targetNode))
            {
                if (e.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    // Нужно поменять перемещенному узлу родителя
                    ((ObjectData)draggedNode.Tag).ParentID = ((ObjectData)draggedNode.Parent.Tag).ObjectID;
                    ActivateSaveDBButton();
                }
                targetNode.Expand();
            }
        }

        private void OnObjectTreeDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void OnObjectTreeDragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = tvObjectTree.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvObjectTree.GetNodeAt(targetPoint);
            
            // Заметка - конечный узел, в него нельзя вложить другой узел.
            if (targetNode is not null)
            {
                if (targetNode.Equals(tvObjectTree.SelectedNode)) return;
                
                if ((targetNode.Tag as ObjectData)?.ObjectType == ObjectType.NoteNode)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(targetPoint);
            }           
        }

        private void OnFormShow(object sender, EventArgs e)
        {
            this.Text +=$" версии {Application.ProductVersion}";
        }
    }
}