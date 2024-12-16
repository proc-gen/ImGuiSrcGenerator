using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class CheckboxConverter : Converter
    {
        public CheckboxConverter(Generator generator) : base(generator) { }
        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var codeName = GetCodeUsableName(xmlNode);
            rb.AppendLine(string.Format("{0}ImGui.Checkbox(\"{1}\", ref {2}_Checked));", prefix, xmlNode.Attributes["text"].Value, codeName));
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            properties.Add(string.Format("public bool {0}_Checked;", codeName));
        }
    }
}
