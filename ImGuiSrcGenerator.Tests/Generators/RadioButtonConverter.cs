using ImGuiSrcGenerator.Generators;
using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class RadioButtonConverter
    {
        const string ElementName = "RadioButton";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "RadioGroup1"},
            {"text", "Radio 1" },
            {"value", "1" },
        };

        [Fact]
        public void GeneratesRenderCodeForRadioButton()
        {
            var radioButtonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var radioButtonConverter = new ImGuiSrcGenerator.Generators.RadioButtonConverter(generator);
            radioButtonConverter.ConvertNodeForRenderPreChildren(sb, radioButtonElement, ref prefix);

            Assert.Contains("ImGui.RadioButton(\"Radio 1\", ref RadioGroup1_Value, 1);", sb.ToString());
        }

        [Fact]
        public void GeneratesPropertyCodeForRadioButton()
        {
            var radioButtonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            HashSet<string> properties = new HashSet<string>();
            var radioButtonConverter = new ImGuiSrcGenerator.Generators.RadioButtonConverter(generator);
            radioButtonConverter.ConvertNodeForProperties(properties, radioButtonElement);

            Assert.Contains("public int RadioGroup1_Value;", properties);
        }
    }
}
