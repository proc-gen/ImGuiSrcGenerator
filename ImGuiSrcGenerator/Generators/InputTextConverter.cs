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
            uint maxLength = uint.Parse(GetAttributeValueOrDefault(xmlNode, "maxLength", "0"));

            if (bool.Parse(GetAttributeValueOrDefault(xmlNode, "multiline", "false")))
            {
                uint width, height;
                width = uint.Parse(GetAttributeValueOrDefault(xmlNode, "width", "0"));
                height = uint.Parse(GetAttributeValueOrDefault(xmlNode, "height", "0"));

                rb.AppendLine(string.Format("{0}ImGui.InputTextMultiline(\"{1}\", ref {2}_Value, {3}, new System.Numerics.Vector2({4}, {5})));", prefix, name, codeName, maxLength, width, height));
            }
            else if (TryGetAttributeValue(xmlNode, "hint", out var hint))
            {
                rb.AppendLine(string.Format("{0}ImGui.InputTextWithHint(\"{1}\", \"{2}\", ref {3}_Value, {4}));", prefix, name, hint, codeName, maxLength));
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
