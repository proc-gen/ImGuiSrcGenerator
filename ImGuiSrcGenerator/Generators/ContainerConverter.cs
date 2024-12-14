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

        public override StringBuilder ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
            rb.AppendLine("{");
            prefix += PrefixCharacter;
            rb.AppendLine(string.Format("{0}public void Render()", prefix));
            rb.AppendLine(string.Format("{0}{{", prefix));
            return rb;
        }
        public override StringBuilder ConvertNodeForRenderPostChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            rb.AppendLine(string.Format("{0}}}", prefix));
            rb.AppendLine("}");
            return rb;
        }

        public override StringBuilder ConvertNodeForActionPreChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            ab.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
            ab.AppendLine("{");
            prefix += PrefixCharacter;
            return ab;
        }
        public override StringBuilder ConvertNodeForActionPostChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            ab.AppendLine("}");
            return ab;
        }
    }
}
