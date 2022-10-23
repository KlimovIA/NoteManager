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
        /// ������ �������� ����, ������� �������� �������� ��� �������� ����� ������.
        /// </summary>
        private void InitMainRootNode()
        {
            // ��� �� ����� ��������� � ����� ����� ��������. �� ����� ������� ����������.
            TreeNode node = tvObjectTree.Nodes.Add("�������� ����");
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
            // ���������� ������ �� �� � ��������� ���������
            foreach (var objectData in ObjectDataManager.ObjectDataList)
            {
                AddNode(objectData);
            }
        }
        private void InitImageList()
        {
            // ��� ����������� � TreeView ����� � ������������ � ��������� �����
            // � ���������, ������ ���������� ����������� �������� �� ��������
            _imgTreeView = new ImageList();
            _imgTreeView.Images.Add(Resources.FolderIcon);
            _imgTreeView.Images.Add(Resources.NoteIcon);
            _imgTreeView.Images.Add(Resources.RootNodeIcon);
            
            tvObjectTree.ImageList = _imgTreeView;
        }

        private void RemoveNode(TreeNode node)
        {
            // ���� � ���� ��� �����, �� ������ ���� ���� � ������
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

            // ���� ������ �� ����� ��������, �� ���������� ��������� ��� � ����
            if (objectData.ParentID == -1)
            {
                node = tvObjectTree.Nodes[0];
                node = node.Nodes.Add(objectData.ObjectName);
                node.Tag = objectData;
                // ��������� ���������� ���� ������� � ����������� �� ���������� ����
                node.ImageIndex = (byte)objectData.ObjectType;
                node.SelectedImageIndex = (byte)objectData.ObjectType;
            }
            // ����� ���� ������������ ���� � ��������� � ����
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
                // ��������� ���������� ���� ������� � ����������� �� ���������� ����
                tmpNode.ImageIndex = (byte)objectData.ObjectType;
                tmpNode.SelectedImageIndex = (byte)objectData.ObjectType;
            }
            tvObjectTree.EndUpdate();
        }

        private TreeNode FindParentNode(TreeNode node, int ParentID)
        {
            TreeNode parentNode = null;
            // ���� � ���� �� ����� ������ ��������, ��� � �������� ����
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

            // ��� ������������� ������������ � ������
            if (tvObjectTree.SelectedNode != null)
                node = tvObjectTree.SelectedNode.Nodes.Add(CommonTypes.Data.CommonTypes.NoteTypeDesc[(byte)nodeType]);
            else
                node = tvObjectTree.Nodes.Add(CommonTypes.Data.CommonTypes.NoteTypeDesc[(byte)nodeType]);

            // ����� ��� ������ ������ � ����
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

            // ������ ������� ��� ��. ��� �������� �� �� ������ �� ��������� ����� - DataNoneChange 
            ((ObjectData)node.Tag).DataStatus = dataStatus;

            // ��������� � ������, ����� ������� ������ �� ������ ������ ������,
            // ����� � ��������� ����� ������ �� ���� ������
            ObjectDataManager.ObjectDataList.Add((ObjectData)node.Tag);

            // ��������� ���������� ���� ������� � ����������� �� ���������� ����
            node.ImageIndex = (byte)nodeType;
            node.SelectedImageIndex = (byte)nodeType;

            // �������� �������������� ����
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
            // ���� ��� ���������� ����, �� � ������� ������
            if (tvObjectTree.SelectedNode == null) tsBtnRemoveNode.Enabled = false;
        }

        private void tvObjectTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvObjectTree.SelectedNode != null)
            {
                /* ���� ������� ����, ��:
                 * 1. ������ ��������� ������ �������� ����
                 * 2.� ������ ���������� ���������, � ������ ���� ��������� ����.
                 * ���� ���� - �����, �� � ��� ����� ��������� ������� �� �������� �������������
                 * ���� ���� - �������, �� � ����� ���� �� ����� ���� �������� �����
                 */
                tsBtnRemoveNode.Enabled = true;

                if (tvObjectTree.SelectedNode.Tag is not null)
                {
                    switch (((ObjectData)tvObjectTree.SelectedNode.Tag).ObjectType)
                    {
                        case ObjectType.FolderNode:
                            // ����� ������ �� ��������
                            tsAddFolder.Enabled = true;
                            break;
                        case ObjectType.NoteNode:
                            // ����� ����������� ������ ���������� ��������
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
                    // ������� - �������� ����, � ���� ������ ����������� ����� ��� ������ �������
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
            // � sender'e �� node, a treeview!!
            if (sender is not TreeNode)
            {
                // �� ��������� ������������� ��������� Drag&Drop � ������ �������������� ��
                // ���� ������ (TreeNode)
                // � ���� ������ ����� �������� ���� ������, ��������� ������ ����� �����������
                // � ������ ������.

                //tvObjectTree.Cursor = Cursors.No;               
            }
            else
            {
                e.Effect = e.AllowedEffect;
                // ������ ������� � ��������������
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
            // ��������� ���� ������ �� ������� ������ ������ ����
            tvObjectTree.SelectedNode = tvObjectTree.GetNodeAt(new Point(e.X, e.Y));
        }

        private void tvObjectTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // ��������� ����� ��������� ������� ����� ����
            ((ObjectData)e.Node.Tag).ObjectName = e.Label;             
            
        }

        private void tvObjectTree_KeyDown(object sender, KeyEventArgs e)
        {
            // ��������� ���������� ��������� ���������������� ����� ������
            if (e.KeyValue == (int)Keys.F2)
                tvObjectTree.SelectedNode?.BeginEdit();
        }
    }
}