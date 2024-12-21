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
            switch (dataType)
            {
                case "i":
                    return "InputInt";
                case "i2":
                    return "InputInt2";
                case "i3":
                    return "InputInt3";
                case "i4":
                    return "InputInt4";
                case "f":
                    return "InputFloat";
                case "f2":
                    return "InputFloat2";
                case "f3":
                    return "InputFloat3";
                case "f4":
                    return "InputFloat4";
                case "d":
                    return "InputDouble";
                default:
                    return "";
            }
        }

        public override void ConvertNodeForProperties(HashSet<string> properties, XmlNode xmlNode)
        {
            var codeName = GetCodeUsableName(xmlNode);
            var dataType = GetAttributeValueOrDefault(xmlNode, "type", "");
            var nativeType = ComponentNativeTypeFromType(dataType);
            var defaultValue = ComponentDefaultValueFromType(dataType);

            properties.Add(string.Format("public {0} {1}_Value = {2};", nativeType, codeName, defaultValue));
        }

        private string ComponentNativeTypeFromType(string dataType)
        {
            switch (dataType)
            {
                case "i":
                    return "int";
                case "i2":
                    return "int[]";
                case "i3":
                    return "int[]";
                case "i4":
                    return "int[]";
                case "f":
                    return "float";
                case "f2":
                    return "System.Numerics.Vector2";
                case "f3":
                    return "System.Numerics.Vector3";
                case "f4":
                    return "System.Numerics.Vector4";
                case "d":
                    return "double";
                default:
                    return "";
            }
        }

        private string ComponentDefaultValueFromType(string dataType)
        {
            switch (dataType)
            {
                case "i":
                    return "0";
                case "i2":
                    return "[0, 0]";
                case "i3":
                    return "[0, 0, 0]";
                case "i4":
                    return "[0, 0, 0, 0]";
                case "f":
                    return "0f";
                case "f2":
                    return "new System.Numerics.Vector2()";
                case "f3":
                    return "new System.Numerics.Vector3()";
                case "f4":
                    return "new System.Numerics.Vector4()";
                case "d":
                    return "0";
                default:
                    return "";
            }
        }
    }
}
