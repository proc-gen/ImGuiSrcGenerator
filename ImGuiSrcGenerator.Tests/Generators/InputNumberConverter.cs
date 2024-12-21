using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class InputNumberConverter
    {
        const string ElementName = "InputNumber";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> inputIntAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputInt" },
            {"type", "i" },
            {"step", "1" },
        };

        Dictionary<string, string> inputFloatAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputFloat" },
            {"type", "f" },
            {"step", "0.1" },
            {"stepFast", "0.5" },
        };

        Dictionary<string, string> inputDoubleAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputDouble" },
            {"type", "d" },
            {"step", "0.1" },
            {"stepFast", "0.5" },
        };

        Dictionary<string, string> inputIntArrayAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputInt" },
            {"type", "i"},
        };

        Dictionary<string, string> inputFloatArrayAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputFloat" },
            {"type", "f"},
        };

        [Fact]
        public void GeneratesRenderCodeForInputInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputIntAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.InputInt(\"##InputInt\", ref InputInt_Value, 1);", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForInputFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputFloatAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.InputFloat(\"##InputFloat\", ref InputFloat_Value, 0.1f, 0.5f);", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForInputDouble()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputDoubleAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.InputDouble(\"##InputDouble\", ref InputDouble_Value, 0.1, 0.5);", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForInputIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", inputIntArrayAttributes["name"] + i.ToString() },
                    {"type", inputIntArrayAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);


                string expected = string.Format("ImGui.InputInt{0}(\"##InputInt{0}\", ref InputInt{0}_Value[0]);", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesRenderCodeForInputFloatArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", inputFloatArrayAttributes["name"] + i.ToString() },
                    {"type", inputFloatArrayAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);


                string expected = string.Format("ImGui.InputFloat{0}(\"##InputFloat{0}\", ref InputFloat{0}_Value);", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesPropertyCodeForInputInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputIntAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public int InputInt_Value = 0;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputFloatAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public float InputFloat_Value = 0f;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputDouble()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, inputDoubleAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public double InputDouble_Value = 0;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", inputIntArrayAttributes["name"] + i.ToString() },
                    {"type", inputIntArrayAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public int[] InputInt{0}_Value = [", i.ToString());
                for (int j = i; j > 0; j--)
                {
                    expected += "0";
                    if(j > 1)
                    {
                        expected += ", ";
                    }
                }
                expected += "];";
                
                Assert.Contains(expected, properties);
            }
        }

        [Fact]
        public void GeneratesPropertyCodeForInputFloatArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", inputFloatArrayAttributes["name"] + i.ToString() },
                    {"type", inputFloatArrayAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.InputNumberConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public System.Numerics.Vector{0} InputFloat{0}_Value = new System.Numerics.Vector{0}();", i.ToString());

                Assert.Contains(expected, properties);
            }
        }
    }
}
