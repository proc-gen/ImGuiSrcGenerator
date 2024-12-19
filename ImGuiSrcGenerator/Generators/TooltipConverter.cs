using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class TooltipConverter : Converter
    {
        public TooltipConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}ImGui.SetItemTooltip(\"{1}\");", prefix, xmlNode.Attributes["text"].Value));
        }
    }
}
