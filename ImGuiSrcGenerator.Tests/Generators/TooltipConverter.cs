using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class TooltipConverter
    {
        const string ElementName = "Tooltip";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "Tooltip1"},
            {"text", "Don't wait, click now!" }
        };

        [Fact]
        public void GeneratesRenderCodeForTooltip()
        {
            var tooltipElement = xmlDoc.CreateElementWithAttributes(ElementName, attributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var tooltipConverter = new ImGuiSrcGenerator.Generators.TooltipConverter(generator);
            tooltipConverter.ConvertNodeForRenderPreChildren(sb, tooltipElement, ref prefix);

            Assert.Contains("ImGui.SetItemTooltip(\"Don't wait, click now!\");", sb.ToString());
        }
    }
}
