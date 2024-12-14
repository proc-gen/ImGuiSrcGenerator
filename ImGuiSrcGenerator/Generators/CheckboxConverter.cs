using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    internal class CheckboxConverter : Converter
    {
        public CheckboxConverter(Generator generator) : base(generator) { }
        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}ImGui.Checkbox(\"{1}\", ref {2}_Checked));", prefix, xmlNode.Attributes["text"].Value, xmlNode.Attributes["name"].Value));
        }

        public override void ConvertNodeForProperties(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}public bool {1}_Checked;", prefix, xmlNode.Attributes["name"].Value));
        }
    }
}
