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
        public static string ConvertFromString(string input)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);

            StringBuilder sb = new StringBuilder();
            ConvertNodeForRender(sb, xmlDoc.FirstChild);
            sb.AppendLine();
            sb.AppendLine();
            ConvertNodeForAction(sb, xmlDoc.FirstChild);
            return sb.ToString();
        }

        public static string ConvertFromFile(string filename)
        {
            return "";
        }

        private static StringBuilder ConvertNodeForRender(StringBuilder renderBuilder, XmlNode xmlNode)
        {
            switch (xmlNode.Name)
            {
                case "Button":
                    renderBuilder.AppendLine(string.Format("if (ImGui.Button(\"{0}\"))", xmlNode.Attributes["text"].Value));
                    renderBuilder.AppendLine("{");
                    renderBuilder.AppendLine(string.Format("{0}_OnClick();", xmlNode.Attributes["name"].Value));
                    renderBuilder.AppendLine("}");
                    break;
                case "Container":
                    renderBuilder.AppendLine(string.Format("internal partial class {0}", xmlNode.Attributes["className"].Value));
                    renderBuilder.AppendLine("{");
                    renderBuilder.AppendLine("public void Render()");
                    renderBuilder.AppendLine("{");
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    ConvertNodeForRender(renderBuilder, childNode);
                }
            }

            switch (xmlNode.Name)
            {
                case "Container":
                    renderBuilder.AppendLine("}");
                    renderBuilder.AppendLine("}");
                    break;
            }
            return renderBuilder;
        }

        private static StringBuilder ConvertNodeForAction(StringBuilder actionBuilder, XmlNode xmlNode)
        {
            switch (xmlNode.Name)
            {
                case "Button":
                    actionBuilder.AppendLine(string.Format("public void {0}_OnClick()", xmlNode.Attributes["name"].Value));
                    actionBuilder.AppendLine("{");
                    actionBuilder.AppendLine();
                    actionBuilder.AppendLine("}");
                    break;
                case "Container":
                    actionBuilder.AppendLine(string.Format("internal partial class {0}", xmlNode.Attributes["className"].Value));
                    actionBuilder.AppendLine("{");
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    ConvertNodeForAction(actionBuilder, childNode);
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
