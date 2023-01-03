using NoteManager.CommonTypes.Data;
using NoteManager.CommonTypes.Enums;
using NoteManager.Database;
using NoteManager.Properties;
using NoteManager.CommonTypes.Data.Debug;
using Microsoft.Toolkit.Uwp.Notifications;
using NoteManager.CommonTypes.Extensions;
using NoteManager.Visual;

namespace NoteManager
{
    public partial class MainForm : Form
    {
        private ImageList? _imgTreeView;
        private readonly DatabaseManager _databaseManager;
        private bool _saveNotesOnCloseApplication = false;
        public MainForm()
        {
            InitializeComponent();
            InitImageList();
            CreateMainRootNode();

            _databaseManager = new DatabaseManager();

            LoadObjectsFromDatabase();
#if DEBUG
            // Вызов консоли для отладки.
            DebugConsole.AllocConsole();
#endif
        }

        private void CreateMainRootNode()
        {
            // Создаем корневой узел, который будет оснновой для всей иерархии.
            ENodeType nodeType = ENodeType.RootNode;

            // Его не нужно добавлять в общий набор объектов. Он будет всегдда неизменным.
            TreeNode node = tvObjectTree.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[(byte)nodeType]);
            node.SelectedImageIndex = ENodeType.RootNode.ToInt();
            node.StateImageIndex = ENodeType.RootNode.ToInt();
            node.ImageIndex = ENodeType.RootNode.ToInt();
            node.Tag = new ObjectData(-1,
                                      -1,
                                      node.Text,
                                      ENodeType.RootNode);
        }

        private void LoadObjectsFromDatabase()
        {
            _databaseManager.LoadObjectTreeFromDB();
            // Подгружаем дерево из БД и заполняем программу. Сортируем объекты, чтобы не возникало
            // проблемы, при которой дерево некорректно строится после перемещения узлов перетягиванием.
            // Объект несортированного списка может попасть в метод добавления в дерево объектов раньше,
            // чем родительский объект.
            ObjectDataManager.ObjectDataList.Sort(ObjectDataComparison.CompareObjectDataByParentID);
            foreach (var objectData in ObjectDataManager.ObjectDataList)
            {
                AddNode(objectData);
            }
        }

        /// <summary>
        /// Инициирует список изображений, содержащий в себе иконки узлов дерева.
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

        private void RemoveNode(TreeNode node)
        {
            // Если у узла нет детей, то шлёпаем этот узел и уходим
            if (node.Nodes.Count == 0)
            {
                ((ObjectData)node.Tag).DataStatus = EDataStatus.DataDelete;
                node.Remove();
                return;
            }
            else
            {
                while (node.Nodes.Count != 0)
                {
                    ((ObjectData)node.Tag).DataStatus = EDataStatus.DataDelete;
                    RemoveNode(node.Nodes[0]);
                }
                node.Remove();
            }
        }

        /// <summary>
        /// Добавление узла из базы данных
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
                node.ImageIndex = objectData.ObjectType.ToInt();
                node.SelectedImageIndex = objectData.ObjectType.ToInt();
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
                    tmpNode = tvObjectTree.Nodes[0];
                    tmpNode = tmpNode.Nodes.Add(objectData.ObjectName);
                    tmpNode.Tag = objectData;
                }
                // Обозачаем выбраннный узел иконкой в зависимости от выбранного типа
                tmpNode.ImageIndex = objectData.ObjectType.ToInt();
                tmpNode.SelectedImageIndex = objectData.ObjectType.ToInt();
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

            // Если в узле не нашли нужное значение, идём в дочерние узлы
            if (((ObjectData)node.Tag).ObjectID != ParentID)
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (((ObjectData)child.Tag).ObjectID != ParentID)
                    {
                        if (child.Nodes.Count != 0)
                            return FindParentNode(child, ParentID);
                    }
                    else
                        return child;
                }
            }
            else
                return node;

            return null;
        }

        /// <summary>
        /// Добавление нового узла.
        /// </summary>
        /// <param name="nodeType"> Тип узла</param>
        /// <param name="dataStatus">Статус данных. В данном случае статус по умолчанию DataAdd(добавление в БД)</param>
        private void AddNode(ENodeType nodeType, EDataStatus dataStatus = EDataStatus.DataAdd)
        {
            TreeNode? node;
            tvObjectTree.BeginUpdate();

            // Для регулирования вложенностей в дереве
            if (tvObjectTree.SelectedNode != null)
            {
                node = tvObjectTree.SelectedNode.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[nodeType.ToInt()]);
            }
            else
            {
                //  Если узел не выбран, то цепляемся к корневому узлу
                TreeNode root = tvObjectTree.Nodes[0];
                node = root.Nodes.Add(CommonTypes.Data.CommonTypes.NodeTypeDesc[nodeType.ToInt()]);
            }


            // Пишем наш объект данных в узел
            if (nodeType == ENodeType.NoteNode)
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
            node.ImageIndex = nodeType.ToInt();
            node.SelectedImageIndex = nodeType.ToInt();

            // Выбираем свежесозданный узел
            tvObjectTree.SelectedNode = node;

            tvObjectTree.EndUpdate();
        }

        private void tsAddFolder_Click(object sender, EventArgs e)
        {
            AddNode(ENodeType.FolderNode);
        }

        private void tsAddNote_Click(object sender, EventArgs e)
        {
            AddNode(ENodeType.NoteNode);
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // Если нет выбранного узла, то и удалять нечего
            if (tvObjectTree.SelectedNode == null) tsBtnRemoveNode.Enabled = false;
        }

        private void OnObjectTreeAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvObjectTree.SelectedNode is not null)
            {
                /* Если выбрали узел, то:
                 * 1. Делаем доступной кнопку удаления узла
                 * 2.В кнопке добавления проверяем, к какому типу относится узел.
                 * Если узел - папка, то в ней можно создавать объекты до условной бесконечности.
                 * Если узел - заметка, то у этого узла не может быть дочерних узлов.
                 * Если выбранный узел является корневым, то блокируем кнопку удаления.
                 */
                tsBtnRemoveNode.Enabled = true;

                if (tvObjectTree.SelectedNode?.Tag is not null)
                {
                    switch (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType)
                    {
                        case ENodeType.FolderNode:
                        case ENodeType.RootNode:
                            // Здесь убираем контрол заметки
                            tsAddFolder.Enabled = true;
                            break;
                        case ENodeType.NoteNode:
                            // Здесь блокируется кнопка добавления объектов и появляется контрол редактора текста
                            tsAddFolder.Enabled = false;
                            break;
                    }

                    // Прокидываем все типы узлов в контрол. Сделано для корректного контроля потока
                    // автосохранения текста в память.
                    ncNote.SetObjectData((ObjectData)tvObjectTree.SelectedNode.Tag);

                    // Доп. проверка: если выбран корневой узел, то его нельзя удалять. Не смог его в тот кейз поместить.
                    if (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType == ENodeType.RootNode)
                        tsBtnRemoveNode.Enabled = false;
                }
            }
        }

        private void msTreeView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tvObjectTree.SelectedNode == null || RootNodeSelected)
            {
                miDeleteNode.Enabled = false;
            }
            else
            {
                if (tvObjectTree.SelectedNode.Tag is not null)
                {
                    // Заметка - конечный узел, в него нельзя закладывать папки или другие заметки
                    if (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType == ENodeType.NoteNode)
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

        private async void SaveToDataBase(object sender, EventArgs e)
        {
            btnSaveToDB.Enabled = false;
            _databaseManager.SaveToDataBase();
            await Task.Delay(5000);
            btnSaveToDB.Enabled = true;
        }

        private void OnObjectTreeMouseDown(object sender, MouseEventArgs e)
        {
            // Активация узла дерева по нажатию любой кнопки мыши. Но дополнительно для вызова контекстного меню.
            tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(new Point(e.X, e.Y));
            if (tvObjectTree.SelectedNode is null)
            {
                ncNote.SetObjectData(null);
                tsBtnRemoveNode.Enabled = false;
            }
        }

        private void OnObjectTreeAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Возникает после изменения измения имени узла           
            ((ObjectData)e.Node.Tag).ObjectName = e.Label;
        }

        private void OnObjectTreeKeyDown(object sender, KeyEventArgs e)
        {
            // Добавляем функционал включения переименовывания узлов дерева
            if (e.KeyValue == (int)Keys.F2)
                tvObjectTree.SelectedNode?.BeginEdit();

            // Удаление по клавише Delete
            // Не должно работать с корневым узлом
            if (e.KeyValue == (int)Keys.Delete && !RootNodeSelected)
                if (tvObjectTree.SelectedNode is not null)
                    RemoveNode(tvObjectTree.SelectedNode);
        }

        /// <summary>
        /// Является ли выбранный узел корневым.
        /// Сделано для ограничения взаимодействий с корневым узлом (запрет на удаление и, возможно, что-то еще).
        /// </summary>
        private bool RootNodeSelected => tvObjectTree.SelectedNode == tvObjectTree.Nodes[0];

        private void OnBtnAddNodeDropDownOpened(object sender, EventArgs e)
        {
            if (tvObjectTree.SelectedNode is not null)
            {
                tsAddNote.Enabled = ((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType != ENodeType.NoteNode;
                tsAddFolder.Enabled = ((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType != ENodeType.NoteNode;
            }
        }

        private void OnObjectTreeItemDrag(object sender, ItemDragEventArgs e)
        {
            // Если пользователь пытается воткнуть что-то другое - выходим.
            if (e.Item is not TreeNode) return;

            // Порверяем, что объект данных узла не является корневым узлом, сразу пресекаем
            if ((e.Button == MouseButtons.Left) &&
               (((ObjectData)((TreeNode)e.Item).Tag).ObjectType != ENodeType.RootNode))
                DoDragDrop(e.Item, DragDropEffects.Move);

        }

        private void OnObjectTreeDragDrop(object sender, DragEventArgs e)
        {
            Point targetPoint = tvObjectTree.PointToClient(new Point(e.X, e.Y));
            TreeNode targetNode = tvObjectTree.GetNodeAt(targetPoint);
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            if (draggedNode is not null)
            {
                if (!draggedNode.Equals(targetNode) &&
                    !draggedNode.ContainsNode(targetNode))
                {
                    if (e.Effect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);

                        // Нужно поменять перемещенному узлу родителя
                        ((ObjectData)draggedNode.Tag).ParentID = ((ObjectData)targetNode.Tag).ObjectID;
                    }
                    targetNode.Expand();
                }
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

                if ((targetNode.Tag as ObjectData)?.ObjectType == ENodeType.NoteNode)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                else
                    e.Effect = DragDropEffects.Move;

                tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(targetPoint);
            }
        }

        private void OnFormShow(object sender, EventArgs e)
        {
            this.Text += $" версии {Application.ProductVersion}";
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            ncNote.SetObjectData(null);
            if (_saveNotesOnCloseApplication)
                new DatabaseManager().SaveToDataBase();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            ObjectDataManager.ObjectDataList.Clear();
            ToastNotificationManagerCompat.Uninstall();           
        }
    }
}