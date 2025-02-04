﻿using ImGuiSrcGenerator.Generators.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    public class InputNumberConverter : Converter
    {
        public InputNumberConverter(Generator generator) : base(generator) { }

        public override void ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            var name = GetName(xmlNode);
            var codeName = GetCodeUsableName(xmlNode);
            var dataType = GetAttributeValueOrDefault(xmlNode, "type", "");
            var componentName = ComponentNameFromType(dataType);
            var step = GetAttributeValueOrDefault(xmlNode, "step", "0");
            var stepFast = GetAttributeValueOrDefault(xmlNode, "stepFast", "0");

            if(dataType.Length == 1)
            {
                if (dataType == "i")
                {
                    rb.AppendLine(string.Format("{0}ImGui.{1}(\"{2}\", ref {3}_Value, {4});", prefix, componentName, name, codeName, step));
                }
                else
                {
                    if(dataType == "f")
                    {
                        step += dataType;
                        stepFast += dataType;
                    }

                    rb.AppendLine(string.Format("{0}ImGui.{1}(\"{2}\", ref {3}_Value, {4}, {5});", prefix, componentName, name, codeName, step, stepFast));
                }
            }
            else
            {
                rb.AppendLine(string.Format("{0}ImGui.{1}(\"{2}\", ref {3}_Value{4});", prefix, componentName, name, codeName, dataType[0] == 'i' ? "[0]" : ""));
            }
        }

        private string ComponentNameFromType(string dataType)
        {
            return string.Concat("Input", ConverterUtils.ComponentNameTypeFromDataType(dataType));
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
