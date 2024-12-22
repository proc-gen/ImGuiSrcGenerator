﻿using ImGuiSrcGenerator.Generators.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class DragNumberConverter : Converter
    {
        public DragNumberConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var name = GetName(xmlNode);
            var codeName = GetCodeUsableName(xmlNode);
            var dataType = GetAttributeValueOrDefault(xmlNode, "type", "");
            var componentName = ComponentNameFromType(dataType);
            var speed = GetAttributeValueOrDefault(xmlNode, "speed", "0");
            var min = GetAttributeValueOrDefault(xmlNode, "min", "0");
            var max = GetAttributeValueOrDefault(xmlNode, "max", "0");
            var format = GetAttributeValueOrDefault(xmlNode, "format", "%d");

            if (dataType[0] == 'f')
            {
                speed += 'f';
                min += 'f';
                max += "f";
            }

            rb.AppendLine(string.Format("{0}ImGui.{1}(\"{2}\", ref {3}_Value{4}, {5}, {6}, {7}, \"{8}\");", prefix, componentName, name, codeName, dataType.Length > 1 ? "[0]" : "", speed, min, max, format));
        }

        private string ComponentNameFromType(string dataType)
        {
            return string.Concat("Drag", ConverterUtils.ComponentNameTypeFromDataType(dataType));
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            var dataType = GetAttributeValueOrDefault(xmlNode, "type", "");
            var nativeType = ConverterUtils.ComponentNativeTypeFromType(dataType);
            var defaultValue = ConverterUtils.ComponentDefaultValueFromType(dataType);

            properties.Add(string.Format("public {0} {1}_Value = {2};", nativeType, codeName, defaultValue));
        }
    }
}
