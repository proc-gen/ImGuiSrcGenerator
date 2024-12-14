using ImGuiSrcGenerator.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    internal class ContainerConverter : Converter
    {
        public ContainerConverter(Generator generator) : base(generator)
        {
        }

        public override void ConvertNode(ConvertMode mode, StringBuilder sb, XmlNode xmlNode, ref string prefix)
        {
            if (mode == ConvertMode.Render)
            {
                base.ConvertNode(mode, sb, xmlNode, ref prefix);
            }
            else
            {
                ConvertNodeForActionPreChildren(sb, xmlNode, ref prefix);
                HashSet<string> properties = new HashSet<string>();
                base.ConvertNodeProperties(properties, xmlNode);
                foreach (string property in properties)
                {
                    sb.AppendLine(string.Format("{0}{1}", prefix, property));
                }
                sb.AppendLine();
                if (xmlNode.HasChildNodes)
                {
                    foreach (XmlNode childNode in xmlNode.ChildNodes)
                    {
                        var childConverter = Generator.GetConverterByComponentName(childNode.Name);
                        childConverter.ConvertNode(mode, sb, childNode, ref prefix);
                    }
                }

                ConvertNodeForActionPostChildren(sb, xmlNode, ref prefix);
            }
        }
        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
            rb.AppendLine("{");
            prefix += PrefixCharacter;
            rb.AppendLine(string.Format("{0}public void Render()", prefix));
            rb.AppendLine(string.Format("{0}{{", prefix));
        }
        public override void ConvertNodeForRenderPostChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}}}", prefix));
            rb.AppendLine("}");
        }

        public override void ConvertNodeForActionPreChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            ab.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
            ab.AppendLine("{");
            prefix += PrefixCharacter;
        }
        public override void ConvertNodeForActionPostChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            ab.AppendLine("}");
        }
    }
}
