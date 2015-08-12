using JsonExaminer.Utilities;
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
        const string VALUE_DELIMITER = "\t";
        const string LINE_DELIMITER = "\r\n";
        const string EXPORT_FILE = "export.txt";

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
            object data = JsonConvert.DeserializeObject(allJson);
            treeView1.Nodes.Clear(); //remove tree contents, in case we're reloading.
            
            PlaceElement(treeView1.Nodes, data);
        }

        TreeNode rightClickedNode;
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                rightClickedNode = e.Node;
                //manipulate right click menu options based on the state of the clicked node.
                tsExport.Enabled = (rightClickedNode is ArrayNode);
                tsContract.Visible = rightClickedNode.IsExpanded;
                tsExpand.Visible = !rightClickedNode.IsExpanded;

                mnuTreeView.Show(treeView1, e.Location);
            }
        }

        private void mnuTreeView_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                switch (e.ClickedItem.Name)
                {
                    case ("tsExport"):
                        ExportArray(rightClickedNode);
                        break;
                    case ("tsExpand"):
                        //we need to add some extra trickery to maintain scrolling position.
                        Point scrollPos = ScrollPosition.GetScrollPosition(treeView1);
                        rightClickedNode.ExpandAll();
                        rightClickedNode.EnsureVisible();
                        ScrollPosition.SetScrollPosition(treeView1, scrollPos);
                        break;
                    case ("tsContract"):
                        if (rightClickedNode.IsExpanded)
                            rightClickedNode.Toggle();
                        break;
                    default:
                        throw new Exception(string.Format("Right-click option {0} not found.", e.ClickedItem.Name));
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// for now, this export is pretty stupid. 
        /// It assumes 
        /// * properties are in the same order for each element
        /// * all objects have all properites. 
        /// * we are exporting an array of objects.  Only value properties will be reported, not arrays or other objects etc. 
        /// </summary>
        private void ExportArray(TreeNode arrayNode)
        {
            if (!(arrayNode is ArrayNode)) throw new Exception("Can't export a " + arrayNode.GetType().Name); //should never happen.
            if (arrayNode.Nodes.Count == 0) throw new Exception("Array has 0 elements.");
            if (!(arrayNode.Nodes[0] is ObjectNode)) throw new Exception("Export must be performed on an array of objects.");

            var exportCollection = GetStructuredExport(arrayNode);
            ExportToFile(exportCollection);
        }

        /// <summary>
        /// export to a format where properties are ROWS and each record is a COLUMN.
        /// </summary>
        private static void ExportToFile(Dictionary<string, List<string>> exportCollection)
        {
            List<string> exportLines = new List<string>();

            //loop through key/value pairs.
            foreach (var kv in exportCollection)
            {
                //note: the "value" in this case is the list with all the properties.
                string propLine = kv.Key + VALUE_DELIMITER + kv.Value.Aggregate((i, j) => i + VALUE_DELIMITER + j);
                exportLines.Add(propLine);
            }

            //always export to the default location, file called "export.txt"
            File.WriteAllText(
                EXPORT_FILE,
                exportLines.Aggregate((i, j) => i + LINE_DELIMITER + j)
            );
        }

        /// <summary>
        /// return export in the form of key -> multiple values;
        /// </summary>
        private Dictionary<string, List<string>> GetStructuredExport(TreeNode startNode)
        {
            Dictionary<string, List<string>> export = new Dictionary<string, List<string>>();

            //use the first element to determine the properties to be exported. 
            foreach (TreeNode pNode in startNode.Nodes[0].Nodes)
            {
                if (pNode is PropertyNode)
                {
                    export.Add(pNode.Text, new List<string>());
                }
            }

            //loop through all nodes and create the export.
            foreach (TreeNode oNode in startNode.Nodes)
            {
                if (oNode is ObjectNode)  //skip any non-objects in this array.
                {
                    foreach (TreeNode pNode in oNode.Nodes)
                    {
                        if (pNode is PropertyNode)  //skip any non-properties in this object.
                        {
                            if (export.ContainsKey(pNode.Text))
                            {
                                string value = pNode.Nodes.Count > 0 ? RemoveLineBreaks(pNode.Nodes[0].Text) : string.Empty;
                                export[pNode.Text].Add(value);
                            }
                        }
                    }
                }
            }

            return export;
        }

        /// <summary>
        /// prepare for export by removing special characters which would get in the way - \r\n\t
        /// </summary>
        private string RemoveLineBreaks(string p)
        {
            return p.Replace("\r", "").Replace("\n", "").Replace("\t", "");
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
                //only create a node if something is present.  May present issues for export later. 
                if (!string.IsNullOrEmpty(dat.ToString()))
                {
                    TreeNode vNode = new ValueNode(dat.ToString());
                    parentNode.Add(vNode);
                }
            }
            else
            {
                Debug.WriteLine("Unknown object: " + dat.GetType().Name);
            }
        }

        [Obsolete("Preliminary version of tree populator. Not sure if useful.")]
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
    }
}
