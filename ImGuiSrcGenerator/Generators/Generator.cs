﻿using ImGuiSrcGenerator.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ImGuiSrcGenerator.Generators
{
    public class Generator
    {
        public string Prefix { get; private set; } = "\t";
        Dictionary<string, Converter> Converters = new Dictionary<string, Converter>();

        public string ConvertFromString(string input)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);
            return Convert(xmlDoc);
        }

        public string ConvertFromFile(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            return Convert(xmlDoc);
        }

        private string Convert(XmlDocument xmlDoc)
        {
            SetConfiguration(xmlDoc.FirstChild);
            StringBuilder sb = new StringBuilder();
            ConvertDocument(ConvertMode.Render, sb, xmlDoc.FirstChild);
            sb.AppendLine();
            sb.AppendLine();
            ConvertDocument(ConvertMode.Action, sb, xmlDoc.FirstChild);
            return sb.ToString();
        }

        public Converter GetConverterByComponentName(string name)
        {
            if (!Converters.ContainsKey(name))
            {
                Converters[name] = (Converter)Activator.CreateInstance(Type.GetType(string.Format("ImGuiSrcGenerator.Generators.{0}Converter, ImGuiSrcGenerator", name)), [this]);
            }

            return Converters[name];
        }

        private void SetConfiguration(XmlNode xmlNode)
        {
            if (xmlNode.Attributes.GetNamedItem("prefix") != null)
            {
                Prefix = xmlNode.Attributes["prefix"].Value;
            }
        }

        private void ConvertDocument(ConvertMode mode, StringBuilder sb, XmlNode xmlNode, string prefix = "")
        {
            var converter = GetConverterByComponentName(xmlNode.Name);

            converter.ConvertNode(mode, sb, xmlNode, ref prefix);
        }
    }
}
