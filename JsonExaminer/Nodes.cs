using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonExaminer
{
    public class ValueNode : TreeNode
    {
        public ValueNode(string text)
            : base(text)
        {
            this.BackColor = Color.Yellow;
        }
    }

    public class ArrayNode : TreeNode
    {
        public ArrayNode(string text)
            : base(text)
        {
            this.BackColor = Color.LightGreen;
        }
    }

    public class ObjectNode : TreeNode
    {
        public ObjectNode(string text)
            : base(text)
        {
            this.BackColor = Color.LightBlue;
        }
    }

    public class PropertyNode : TreeNode
    {
        public PropertyNode(string text)
            : base(text)
        {
        }
    }
}
