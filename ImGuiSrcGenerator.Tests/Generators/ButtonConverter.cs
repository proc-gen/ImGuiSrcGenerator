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
    public class ButtonConverter
    {
        const string ElementName = "Button";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "Button1"},
            {"text", "Click Me!" }
        };

        [Fact]
        public void GeneratesRenderCodeForButton()
        {
            var buttonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var buttonConverter = new ImGuiSrcGenerator.Generators.ButtonConverter(generator);
            buttonConverter.ConvertNodeForRenderPreChildren(sb, buttonElement, ref prefix);
            
            Assert.Contains(
@"if (ImGui.Button(""Click Me!""))
{
	Button1_OnClick.DynamicInvoke();
}
", sb.ToString());
        }

        [Fact]
        public void GeneratesPropertyCodeForButton()
        {
            var buttonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            HashSet<string> properties = new HashSet<string>();
            var buttonConverter = new ImGuiSrcGenerator.Generators.ButtonConverter(generator);
            buttonConverter.ConvertNodeForProperties(properties, buttonElement);

            Assert.Contains("public Delegate Button1_OnClick;", properties);
        }
    }
}
