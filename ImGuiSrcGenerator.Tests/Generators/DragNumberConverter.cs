using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class DragNumberConverter
    {
        const string ElementName = "DragNumber";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();

        Dictionary<string, string> dragIntAttributes = new Dictionary<string, string>()
        {
            {"name", "##DragInt" },
            {"type", "i" },
            {"speed", "1" },
            {"min", "0" },
            {"max", "100" },
            {"format", "%d%%" }
        };

        Dictionary<string, string> dragFloatAttributes = new Dictionary<string, string>()
        {
            {"name", "##DragFloat" },
            {"type", "f" },
            {"speed", ".1" },
            {"min", "-10" },
            {"max", "10" },
        };

        [Fact]
        public void GeneratesRenderCodeForDragInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, dragIntAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.DragInt(\"##DragInt\", ref DragInt_Value, 1, 0, 100, \"%d%%\");", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForDragFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, dragFloatAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.DragFloat(\"##DragFloat\", ref DragFloat_Value, .1f, -10f, 10f, \"%d\");", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForDragIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", dragIntAttributes["name"] + i.ToString() },
                    {"type", dragIntAttributes["type"] + i.ToString() },
                    {"speed", "1" },
                    {"min", "0" },
                    {"max", "100" },
                    {"format", "%d%%" }
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

                string expected = string.Format("ImGui.DragInt{0}(\"##DragInt{0}\", ref DragInt{0}_Value[0], 1, 0, 100, \"%d%%\");", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesRenderCodeForDragFloatArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", dragFloatAttributes["name"] + i.ToString() },
                    {"type", dragFloatAttributes["type"] + i.ToString() },
                    {"speed", ".1" },
                    {"min", "-10" },
                    {"max", "10" },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

                string expected = string.Format("ImGui.DragFloat{0}(\"##DragFloat{0}\", ref DragFloat{0}_Value[0], .1f, -10f, 10f, \"%d\");", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesPropertyCodeForInputInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, dragIntAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public int DragInt_Value = 0;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, dragFloatAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public float DragFloat_Value = 0f;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", dragIntAttributes["name"] + i.ToString() },
                    {"type", dragIntAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public int[] DragInt{0}_Value = [", i.ToString());
                for (int j = i; j > 0; j--)
                {
                    expected += "0";
                    if (j > 1)
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
                    {"name", dragFloatAttributes["name"] + i.ToString() },
                    {"type", dragFloatAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.DragNumberConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public System.Numerics.Vector{0} DragFloat{0}_Value = new System.Numerics.Vector{0}();", i.ToString());

                Assert.Contains(expected, properties);
            }
        }
    }
}
