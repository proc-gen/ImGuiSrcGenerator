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
    public class CheckboxConverter
    {
        const string ElementName = "Checkbox";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "Checkbox1"},
            {"text", "Check Me!" }
        };

        [Fact]
        public void GeneratesRenderCodeForCheckbox()
        {
            var checkboxElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var checkboxConverter = new ImGuiSrcGenerator.Generators.CheckboxConverter(generator);
            checkboxConverter.ConvertNodeForRenderPreChildren(sb, checkboxElement, ref prefix);

            Assert.Contains("ImGui.Checkbox(\"Check Me!\", ref Checkbox1_Checked));", sb.ToString());
        }

        [Fact]
        public void GeneratesPropertyCodeForCheckbox()
        {
            var checkboxElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            HashSet<string> properties = new HashSet<string>();
            var checkboxConverter = new ImGuiSrcGenerator.Generators.CheckboxConverter(generator);
            checkboxConverter.ConvertNodeForProperties(properties, checkboxElement);

            Assert.Contains("public bool Checkbox1_Checked;", properties);
        }
    }
}
