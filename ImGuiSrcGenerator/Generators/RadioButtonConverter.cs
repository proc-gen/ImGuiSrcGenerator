using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class RadioButtonConverter : Converter
    {
        public RadioButtonConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var codeName = GetCodeUsableName(xmlNode);
            rb.AppendLine(string.Format("{0}ImGui.RadioButton(\"{1}\", ref {2}_Value, {3}));", prefix, xmlNode.Attributes["text"].Value, codeName, xmlNode.Attributes["value"].Value));
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            properties.Add(string.Format("public int {0}_Value;", codeName));
        }
    }
}
