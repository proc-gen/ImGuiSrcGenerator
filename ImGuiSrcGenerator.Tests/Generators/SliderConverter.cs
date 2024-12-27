using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class SliderConverter
    {
        const string ElementName = "Slider";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();

        Dictionary<string, string> sliderIntAttributes = new Dictionary<string, string>()
        {
            {"name", "##SliderInt" },
            {"type", "i" },
            {"min", "0" },
            {"max", "100" },
            {"format", "%d%%" }
        };

        Dictionary<string, string> sliderFloatAttributes = new Dictionary<string, string>()
        {
            {"name", "##SliderFloat" },
            {"type", "f" },
            {"min", "-10" },
            {"max", "10" },
        };

        Dictionary<string, string> sliderAngleAttributes = new Dictionary<string, string>()
        {
            {"name", "##SliderAngle" },
            {"type", "a" },
            {"min", "0" },
            {"max", "180" },
        };

        [Fact]
        public void GeneratesRenderCodeForSliderInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderIntAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.SliderInt(\"##SliderInt\", ref SliderInt_Value, 0, 100, \"%d%%\");", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForSliderFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderFloatAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.SliderFloat(\"##SliderFloat\", ref SliderFloat_Value, -10f, 10f, \"%d\");", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForSliderAngle()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderAngleAttributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

            Assert.Contains("ImGui.SliderAngle(\"##SliderAngle\", ref SliderAngle_Value, 0f, 180f, \"%d\");", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForSliderIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", sliderIntAttributes["name"] + i.ToString() },
                    {"type", sliderIntAttributes["type"] + i.ToString() },
                    {"min", "0" },
                    {"max", "100" },
                    {"format", "%d%%" }
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

                string expected = string.Format("ImGui.SliderInt{0}(\"##SliderInt{0}\", ref SliderInt{0}_Value[0], 0, 100, \"%d%%\");", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesRenderCodeForSliderFloatArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", sliderFloatAttributes["name"] + i.ToString() },
                    {"type", sliderFloatAttributes["type"] + i.ToString() },
                    {"min", "-10" },
                    {"max", "10" },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                StringBuilder sb = new StringBuilder();
                string prefix = "";
                var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
                converter.ConvertNodeForRenderPreChildren(sb, element, ref prefix);

                string expected = string.Format("ImGui.SliderFloat{0}(\"##SliderFloat{0}\", ref SliderFloat{0}_Value[0], -10f, 10f, \"%d\");", i.ToString());
                Assert.Contains(expected, sb.ToString());
            }
        }

        [Fact]
        public void GeneratesPropertyCodeForInputInt()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderIntAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public int SliderInt_Value = 0;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputFloat()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderFloatAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public float SliderFloat_Value = 0f;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputAngle()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, sliderAngleAttributes);
            HashSet<string> properties = new HashSet<string>();
            var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
            converter.ConvertNodeForProperties(properties, element);

            Assert.Contains("public float SliderAngle_Value = 0f;", properties);
        }

        [Fact]
        public void GeneratesPropertyCodeForInputIntArray()
        {
            for (int i = 2; i <= 4; i++)
            {
                var attributes = new Dictionary<string, string>()
                {
                    {"name", sliderIntAttributes["name"] + i.ToString() },
                    {"type", sliderIntAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public int[] SliderInt{0}_Value = [", i.ToString());
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
                    {"name", sliderFloatAttributes["name"] + i.ToString() },
                    {"type", sliderFloatAttributes["type"] + i.ToString() },
                };

                var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
                HashSet<string> properties = new HashSet<string>();
                var converter = new ImGuiSrcGenerator.Generators.SliderConverter(generator);
                converter.ConvertNodeForProperties(properties, element);

                string expected = string.Format("public System.Numerics.Vector{0} SliderFloat{0}_Value = new System.Numerics.Vector{0}();", i.ToString());

                Assert.Contains(expected, properties);
            }
        }
    }
}
