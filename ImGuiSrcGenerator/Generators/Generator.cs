using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    internal class Generator
    {
        string PrefixCharacter = "\t";
        public string ConvertFromString(string input)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            SetConfiguration(xmlDoc.FirstChild);
            StringBuilder sb = new StringBuilder();
            ConvertNodeForRender(sb, xmlDoc.FirstChild);
            sb.AppendLine();
            sb.AppendLine();
            ConvertNodeForAction(sb, xmlDoc.FirstChild);
            return sb.ToString();
        }

        public string ConvertFromFile(string filename)
        {
            return "";
        }

        private void SetConfiguration(XmlNode xmlNode)
        {
            if (xmlNode.Attributes.GetNamedItem("prefixCharacter") != null)
            {
                PrefixCharacter = xmlNode.Attributes["prefixCharacter"].Value;
            }
        }

        private StringBuilder ConvertNodeForRender(StringBuilder renderBuilder, XmlNode xmlNode, string prefix = "")
        {
            switch (xmlNode.Name)
            {
                case "Button":
                    renderBuilder.AppendLine(string.Format("{0}if (ImGui.Button(\"{1}\"))", prefix, xmlNode.Attributes["text"].Value));
                    renderBuilder.AppendLine(string.Format("{0}{{", prefix));
                    renderBuilder.AppendLine(string.Format("{0}{1}_OnClick();", prefix + PrefixCharacter, xmlNode.Attributes["name"].Value));
                    renderBuilder.AppendLine(string.Format("{0}}}", prefix));
                    break;
                case "Container":
                    renderBuilder.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
                    renderBuilder.AppendLine("{");
                    prefix += PrefixCharacter;
                    renderBuilder.AppendLine(string.Format("{0}public void Render()", prefix));
                    renderBuilder.AppendLine(string.Format("{0}{{", prefix));
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    ConvertNodeForRender(renderBuilder, childNode, prefix + PrefixCharacter);
                }
            }

            switch (xmlNode.Name)
            {
                case "Container":
                    renderBuilder.AppendLine(string.Format("{0}}}", prefix));
                    renderBuilder.AppendLine("}");
                    break;
            }
            return renderBuilder;
        }

        private StringBuilder ConvertNodeForAction(StringBuilder actionBuilder, XmlNode xmlNode, string prefix = "")
        {
            switch (xmlNode.Name)
            {
                case "Button":
                    actionBuilder.AppendLine(string.Format("{0}public void {1}_OnClick()", prefix, xmlNode.Attributes["name"].Value));
                    actionBuilder.AppendLine(string.Format("{0}{{", prefix));
                    actionBuilder.AppendLine();
                    actionBuilder.AppendLine(string.Format("{0}}}", prefix));
                    break;
                case "Container":
                    actionBuilder.AppendLine(string.Format("public partial class {0}", xmlNode.Attributes["className"].Value));
                    actionBuilder.AppendLine("{");
                    prefix += PrefixCharacter;
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    ConvertNodeForAction(actionBuilder, childNode, prefix + PrefixCharacter);
                }
            }

            switch (xmlNode.Name)
            {
                case "Container":
                    actionBuilder.AppendLine("}");
                    break;
            }

            return actionBuilder;
        }
    }
}
