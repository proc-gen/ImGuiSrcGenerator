using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class InputTextConverter : Converter
    {
        public InputTextConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var name = GetName(xmlNode);
            var codeName = GetCodeUsableName(xmlNode);
            uint maxLength = 0;
            if (xmlNode.Attributes.GetNamedItem("maxLength") != null)
            {
                maxLength = uint.Parse(xmlNode.Attributes["maxLength"].Value);
            }

            if (xmlNode.Attributes.GetNamedItem("hint") != null)
            {
                rb.AppendLine(string.Format("{0}ImGui.InputTextWithHint(\"{1}\", \"{2}\", ref {3}_Value, {4}));", prefix, name, xmlNode.Attributes["hint"].Value, codeName, maxLength));
            }
            else
            {
                rb.AppendLine(string.Format("{0}ImGui.InputText(\"{1}\", ref {2}_Value, {3}));", prefix, name, codeName, maxLength));
            }
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            properties.Add(string.Format("public string {0}_Value = \"\";", codeName));
        }
    }
}
