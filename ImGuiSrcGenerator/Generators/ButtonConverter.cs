using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class ButtonConverter : Converter
    {
        public ButtonConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}if (ImGui.Button(\"{1}\"))", prefix, xmlNode.Attributes["text"].Value));
            rb.AppendLine(string.Format("{0}{{", prefix));
            rb.AppendLine(string.Format("{0}{1}_OnClick();", prefix + PrefixCharacter, xmlNode.Attributes["name"].Value));
            rb.AppendLine(string.Format("{0}}}", prefix));
        }

        public override void ConvertNodeForActionPreChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            ab.AppendLine(string.Format("{0}public void {1}_OnClick()", prefix, xmlNode.Attributes["name"].Value));
            ab.AppendLine(string.Format("{0}{{", prefix));
            ab.AppendLine();
            ab.AppendLine(string.Format("{0}}}", prefix));
        }
    }
}
