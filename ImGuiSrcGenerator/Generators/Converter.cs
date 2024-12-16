using ImGuiSrcGenerator.Constants;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public abstract class Converter
    {
        protected Generator Generator;
        protected string PrefixCharacter { get { return Generator.PrefixCharacter; } }

        protected Converter(Generator generator) 
        {
            Generator = generator;
        }

        public virtual void ConvertNode(ConvertMode mode, StringBuilder sb, XmlNode xmlNode, ref string prefix)
        {
            var converter = Generator.GetConverterByComponentName(xmlNode.Name);

            switch (mode)
            {
                case ConvertMode.Render:
                    converter.ConvertNodeForRenderPreChildren(sb, xmlNode, ref prefix);
                    break;
                case ConvertMode.Action:
                    converter.ConvertNodeForActionPreChildren(sb, xmlNode, ref prefix);
                    break;
            }

            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    var childConverter = Generator.GetConverterByComponentName(childNode.Name);
                    string newPrefix = mode != ConvertMode.Property ? prefix + PrefixCharacter : prefix;
                    childConverter.ConvertNode(mode, sb, childNode, ref newPrefix);
                }
            }

            switch (mode)
            {
                case ConvertMode.Render:
                    converter.ConvertNodeForRenderPostChildren(sb, xmlNode, ref prefix);
                    break;
                case ConvertMode.Action:
                    converter.ConvertNodeForActionPostChildren(sb, xmlNode, ref prefix);
                    break;
            }
        }

        public virtual void ConvertNodeProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            ConvertNodeForProperties(properties, xmlNode);
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    var childConverter = Generator.GetConverterByComponentName(childNode.Name);
                    childConverter.ConvertNodeProperties(properties, childNode);
                }
            }
        }

        public virtual void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
        }

        public virtual void ConvertNodeForRenderPostChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
        }

        public virtual void ConvertNodeForActionPreChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
        }
        public virtual void ConvertNodeForActionPostChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
        }

        public virtual void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
        }

        public static string GetName(XmlNode xmlNode)
        {
            return xmlNode.Attributes["name"].Value;
        }

        static char[] allowedNonAlphanumericCharacters = ['-', '_'];

        public static string GetCodeUsableName(XmlNode xmlNode)
        {
            var name = GetName(xmlNode);

            char[] arr = name.Where(c => (char.IsLetterOrDigit(c) || allowedNonAlphanumericCharacters.Contains(c))).ToArray();

            return new string(arr);
        }

        public static string GetAttributeValueOrDefault(XmlNode xmlNode, string key, string defaultValue)
        {
            if(xmlNode.Attributes.GetNamedItem(key) != null)
            {
                return xmlNode.Attributes[key].Value;
            }
            return defaultValue;
        }

        public static bool TryGetAttributeValue(XmlNode xmlNode, string key, out string value)
        {
            value = null;
            if (xmlNode.Attributes.GetNamedItem(key) != null)
            {
                value = xmlNode.Attributes[key].Value;
                return true;
            }
            return false;
        }
    }
}
