using ImGuiSrcGenerator.Constants;
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
        public string PrefixCharacter { get; private set; } = "\t";
        Dictionary<string, Converter> Converters = new Dictionary<string, Converter>();

        public string ConvertFromString(string input)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            SetConfiguration(xmlDoc.FirstChild);
            StringBuilder sb = new StringBuilder();
            ConvertNode(ConvertMode.Render, sb, xmlDoc.FirstChild);
            sb.AppendLine();
            sb.AppendLine();
            ConvertNode(ConvertMode.Action, sb, xmlDoc.FirstChild);
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

        private StringBuilder ConvertNode(ConvertMode mode, StringBuilder sb, XmlNode xmlNode, string prefix = "")
        {
            if (!Converters.ContainsKey(xmlNode.Name)) 
            {
                Converters[xmlNode.Name] = (Converter)Activator.CreateInstance(Type.GetType(string.Format("ImGuiSrcGenerator.Generators.{0}Converter, ImGuiSrcGenerator", xmlNode.Name)), [this]);
            }

            switch (mode)
            {
                case ConvertMode.Render:
                    Converters[xmlNode.Name].ConvertNodeForRenderPreChildren(sb, xmlNode, ref prefix);
                    break;
                case ConvertMode.Action:
                    Converters[xmlNode.Name].ConvertNodeForActionPreChildren(sb, xmlNode, ref prefix);
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    ConvertNode(mode, sb, childNode, prefix + PrefixCharacter);
                }
            }

            switch (mode)
            {
                case ConvertMode.Render:
                    Converters[xmlNode.Name].ConvertNodeForRenderPostChildren(sb, xmlNode, ref prefix);
                    break;
                case ConvertMode.Action:
                    Converters[xmlNode.Name].ConvertNodeForActionPostChildren(sb, xmlNode, ref prefix);
                    break;
            }

            return sb;
        }
    }
}
