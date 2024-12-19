﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class ArrowButtonConverter : Converter
    {
        public ArrowButtonConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var codeName = GetCodeUsableName(xmlNode);
            rb.AppendLine(string.Format("{0}if (ImGui.ArrowButton(\"{1}\", {2}))", prefix, codeName, GetDirection(xmlNode.Attributes["direction"].Value)));
            rb.AppendLine(string.Format("{0}{{", prefix));
            rb.AppendLine(string.Format("{0}{1}_OnClick.DynamicInvoke();", prefix + Prefix, codeName));
            rb.AppendLine(string.Format("{0}}}", prefix));
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            properties.Add(string.Format("public Delegate {0}_OnClick;", codeName));
        }

        private string GetDirection(string dir)
        {
            switch (dir)
            {
                case "left":
                    return "ImGuiDir.Left";
                case "right": 
                    return "ImGuiDir.Right";
                case "up": 
                    return "ImGuiDir.Up";
                case "down": 
                    return "ImGuiDir.Down";
            }
            return "ImGuiDir.None";
        }
    }
}