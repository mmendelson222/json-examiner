using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonExaminer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;

                try
                {
                    LoadIntoTree(this.textBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadIntoTree(string p)
        {
            string allJson = File.ReadAllText(p);
            object dat = JsonConvert.DeserializeObject(allJson);

            //ParseElement(dat);
            PlaceElement(treeView1.Nodes, dat);
        }

        private void PlaceElement(TreeNodeCollection parentNode, object dat)
        {
            if (dat is JArray)
            {
                Debug.Indent();
                TreeNode aNode = new ArrayNode("ARRAY");
                parentNode.Add(aNode);
                foreach (var item in ((JArray)dat))
                {
                    PlaceElement(aNode.Nodes, item);
                }
                Debug.Unindent();
            }
            else if (dat is JObject)
            {
                TreeNode oNode = new ObjectNode("OBJECT");
                parentNode.Add(oNode);
                foreach (var prop in ((JObject)dat))
                {
                    TreeNode kNode = new PropertyNode(prop.Key);
                    oNode.Nodes.Add(kNode);
                    PlaceElement(kNode.Nodes, prop.Value);
                }
            }
            else if (dat is JValue)
            {
                TreeNode vNode = new ValueNode(dat.ToString());
                parentNode.Add(vNode);
            }
            else
            {
                Debug.WriteLine("Unknown object: " + dat.GetType().Name);
            }
        }

        private void PlaceElement2(TreeNodeCollection parentNode, object dat)
        {
            if (dat is JArray)
            {
                Debug.Indent();
                TreeNode aNode = new TreeNode("ARRAY");
                parentNode.Add(aNode);
                foreach (var item in ((JArray)dat))
                {
                    PlaceElement2(aNode.Nodes, item);
                }
                Debug.Unindent();
            }
            else if (dat is JObject)
            {
                TreeNode oNode = new TreeNode("OBJECT");
                parentNode.Add(oNode);
                foreach (var prop in ((JObject)dat))
                {
                    TreeNode kNode = new TreeNode(prop.Key);
                    oNode.Nodes.Add(kNode);
                    PlaceElement2(kNode.Nodes, prop.Value);
                }
            }
            else if (dat is JValue)
            {
                //only create a node if something is present.  May present issues for export later. 
                if (!string.IsNullOrEmpty(dat.ToString()))
                {
                    TreeNode vNode = new TreeNode();
                    parentNode.Add(vNode);
                }
            }
            else
            {
                Debug.WriteLine("Unknown object: " + dat.GetType().Name);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Node.IsExpanded)
                    e.Node.Toggle();
                else
                    e.Node.ExpandAll();
            }
        }

        /*
        private void ParseElement(object dat)
        {
            //gotta be an object or an array
            if (dat is JArray)
            {
                Debug.Indent();
                Debug.Write("\nARRAY ");
                foreach (var item in ((JArray)dat))
                {
                    ParseElement(item);
                }
                Debug.Unindent();
            }
            else if (dat is JObject)
            {
                Debug.Write("\nOBJECT ");
                foreach (var prop in ((JObject)dat))
                {
                    Debug.Write(string.Format("\nPROPERTY {0}:", prop.Key));
                    ParseElement(prop.Value);
                }
            }
            else if (dat is JValue)
            {
                Debug.Write(dat);
            }
            else
            {
                Debug.WriteLine("Unknown object: " + dat.GetType().Name);
            }
        }
         */
    }
}
