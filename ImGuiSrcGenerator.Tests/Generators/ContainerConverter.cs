using ImGuiSrcGenerator.Generators;
using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class ContainerConverter
    {
        const string ElementName = "Container";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        
        Dictionary<string, string> containerAttributes = new Dictionary<string, string>()
        {
            {"className", "TestContainer"},
        };

        Dictionary<string, string> checkboxAttributes = new Dictionary<string, string>()
        {
            {"name", "Checkbox1"},
            {"text", "Check Me!" }
        };

        [Fact]
        public void GeneratesClassName()
        {
            var containerElement = xmlDoc.CreateElementWithAttributes(ElementName, containerAttributes);
            
            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var containerConverter = new ImGuiSrcGenerator.Generators.ContainerConverter(generator);
            containerConverter.ConvertNodeForRenderPreChildren(sb, containerElement, ref prefix);
            
            Assert.Contains(
@"public partial class TestContainer
{
	public void Render()
	{", sb.ToString());
        }

        [Fact]
        public void GeneratesEndOfClass()
        {
            var containerElement = xmlDoc.CreateElementWithAttributes(ElementName, containerAttributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var containerConverter = new ImGuiSrcGenerator.Generators.ContainerConverter(generator);
            containerConverter.ConvertNodeForRenderPostChildren(sb, containerElement, ref prefix);

            Assert.Contains(
@"}
}
", sb.ToString());
        }

        [Fact]
        public void ConvertsChildrenForRendering()
        {
            var containerElement = xmlDoc.CreateElementWithAttributes(ElementName, containerAttributes);
            var checkboxElement = xmlDoc.CreateElementWithAttributes("Checkbox", checkboxAttributes);
            containerElement.AppendChild(checkboxElement);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var containerConverter = new ImGuiSrcGenerator.Generators.ContainerConverter(generator);
            containerConverter.ConvertNode(Constants.ConvertMode.Render, sb, containerElement, ref prefix);

            Assert.Contains(
@"public partial class TestContainer
{
	public void Render()
	{
		ImGui.Checkbox(""Check Me!"", ref Checkbox1_Checked);
	}
}", sb.ToString());
        }

        [Fact]
        public void ConvertsChildrenForPropertiesAndActions()
        {
            var containerElement = xmlDoc.CreateElementWithAttributes(ElementName, containerAttributes);
            var checkboxElement = xmlDoc.CreateElementWithAttributes("Checkbox", checkboxAttributes);
            containerElement.AppendChild(checkboxElement);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var containerConverter = new ImGuiSrcGenerator.Generators.ContainerConverter(generator);
            containerConverter.ConvertNode(Constants.ConvertMode.Action, sb, containerElement, ref prefix);

            Assert.Contains(
@"public partial class TestContainer
{
	public bool Checkbox1_Checked;

}
", sb.ToString());
        }
    }
}
