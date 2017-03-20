using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeViewDrag1
{
    public partial class Form1 : Form
    {

        // one treeview move
// http://stackoverflow.com/questions/20915260/c-sharp-winforms-dragdrop-within-the-same-treeviewcontrol

        // multiple nodes
        // https://social.msdn.microsoft.com/Forums/windows/en-US/4b6fb790-5ace-43c3-9a6e-0212f12ece2f/allow-drag-and-drop-of-multiple-nodes-in-tree-view?forum=winforms

        int i = 0;
        public Form1()
        {
            InitializeComponent();
            button1_Click(this, null);
            button1_Click(this, null);
            button1_Click(this, null);

            button2_Click(this, null); button2_Click(this, null);
            button2_Click(this, null);
            button2_Click(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;

            TreeNode newNode = new TreeNode("Text for new node " + i);
            if (null != treeView1.SelectedNode)
            {
                treeView1.SelectedNode.Nodes.Add(newNode);
                treeView1.SelectedNode.ExpandAll();
            }
            else
            {
                treeView1.Nodes.Add(newNode);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i++;

            TreeNode newNode = new TreeNode("Number " + i);
            if (null != treeView2.SelectedNode)
            {
                treeView2.SelectedNode.Nodes.Add(newNode);
                treeView2.SelectedNode.ExpandAll();
            }
            else
            {
                treeView2.Nodes.Add(newNode);

            }
            
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
           
                DoDragDrop(e.Item, DragDropEffects.Move);

        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (sender == treeView1)
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy;
                Console.WriteLine("e.Effect is {0}  allowed is {1}", e.Effect, e.AllowedEffect);
            }

            if (sender == treeView2)
            {
//                e.Effect = DragDropEffects.Copy;
            }
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {

            // Retrieve the client coordinates of the drop location.
            TreeView tv = (TreeView)sender;
            Point targetPoint = tv.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            TreeNode targetNode = tv.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            TreeNode parentNode = targetNode;

            if (draggedNode != null &&
                targetNode != null)
            {
                bool canDrop = true;
                while (canDrop && (parentNode != null))
                {
                    canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                    parentNode = parentNode.Parent;
                }

                if (canDrop)
                {
                    if (e.AllowedEffect == DragDropEffects.Move)
                    {
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                    }
                    else
                    {
                        TreeNode newNode = new TreeNode(draggedNode.Text + " Copy");
                        targetNode.Nodes.Add(newNode);
                    }
                    
                    targetNode.Expand();
                }
            }
        }

        private void treeView2_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void treeView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);
        }
    }
}
