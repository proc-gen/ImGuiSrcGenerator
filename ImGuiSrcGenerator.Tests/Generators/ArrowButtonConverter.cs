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
    public class ArrowButtonConverter
    {
        const string ElementName = "ArrowButton";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "##Arrow1"},
            {"direction", "right" }
        };

        [Fact]
        public void GeneratesRenderCodeForButton()
        {
            var buttonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var arrowButtonConverter = new ImGuiSrcGenerator.Generators.ArrowButtonConverter(generator);
            arrowButtonConverter.ConvertNodeForRenderPreChildren(sb, buttonElement, ref prefix);
            
            Assert.Contains(
@"if (ImGui.ArrowButton(""Arrow1"", ImGuiDir.Right))
{
	Arrow1_OnClick.DynamicInvoke();
}
", sb.ToString());
        }

        [Fact]
        public void GeneratesPropertyCodeForButton()
        {
            var arrowButtonElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            HashSet<string> properties = new HashSet<string>();
            var arrowButtonConverter = new ImGuiSrcGenerator.Generators.ArrowButtonConverter(generator);
            arrowButtonConverter.ConvertNodeForProperties(properties, arrowButtonElement);

            Assert.Contains("public Delegate Arrow1_OnClick;", properties);
        }
    }
}
