using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Generators.Helpers
{
    public class ConverterUtils
    {
        public static string ComponentNameTypeFromDataType(string dataType)
        {
            switch (dataType)
            {
                case "i":
                    return "Int";
                case "i2":
                    return "Int2";
                case "i3":
                    return "Int3";
                case "i4":
                    return "Int4";
                case "f":
                    return "Float";
                case "f2":
                    return "Float2";
                case "f3":
                    return "Float3";
                case "f4":
                    return "Float4";
                case "d":
                    return "Double";
                case "a":
                    return "Angle";
                default:
                    return "";
            }
        }

        public static string ComponentNativeTypeFromType(string dataType)
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
                case "a":
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

        public static string ComponentDefaultValueFromType(string dataType)
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
                case "a":
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
