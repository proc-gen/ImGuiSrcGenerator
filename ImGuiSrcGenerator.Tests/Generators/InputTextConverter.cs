using ImGuiSrcGenerator.Generators;
using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class InputTextConverter
    {
        const string ElementName = "InputText";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> textAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputText"},
            {"maxLength", "100" },
        };

        Dictionary<string, string> hintAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputHint"},
            {"maxLength", "100" },
            {"hint", "My Hint" }
        };

        Dictionary<string, string> multiLineAttributes = new Dictionary<string, string>()
        {
            {"name", "##InputMulti"},
            {"maxLength", "100" },
            {"multiline", "true" },
            {"width", "200" },
            {"height", "200" },
        };

        [Fact]
        public void GeneratesRenderCodeForInputText()
        {
            var textElement = xmlDoc.CreateElementWithAttributes(ElementName, textAttributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var inputTextConverter = new ImGuiSrcGenerator.Generators.InputTextConverter(generator);
            inputTextConverter.ConvertNodeForRenderPreChildren(sb, textElement, ref prefix);

            Assert.Contains("ImGui.InputText(\"##InputText\", ref InputText_Value, 100);", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForInputHint()
        {
            var hintElement = xmlDoc.CreateElementWithAttributes(ElementName, hintAttributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var inputTextConverter = new ImGuiSrcGenerator.Generators.InputTextConverter(generator);
            inputTextConverter.ConvertNodeForRenderPreChildren(sb, hintElement, ref prefix);

            Assert.Contains("ImGui.InputTextWithHint(\"##InputHint\", \"My Hint\", ref InputHint_Value, 100);", sb.ToString());
        }

        [Fact]
        public void GeneratesRenderCodeForInputMultiline()
        {
            var multilineTextElement = xmlDoc.CreateElementWithAttributes(ElementName, multiLineAttributes);

            StringBuilder sb = new StringBuilder();
            string prefix = "";
            var inputTextConverter = new ImGuiSrcGenerator.Generators.InputTextConverter(generator);
            inputTextConverter.ConvertNodeForRenderPreChildren(sb, multilineTextElement, ref prefix);

            Assert.Contains("ImGui.InputTextMultiline(\"##InputMulti\", ref InputMulti_Value, 100, new System.Numerics.Vector2(200, 200));", sb.ToString());
        }

        [Fact]
        public void GeneratesPropertyCodeForInputText()
        {
            var textElement = xmlDoc.CreateElementWithAttributes(ElementName, textAttributes);

            HashSet<string> properties = new HashSet<string>();
            var inputTextConverter = new ImGuiSrcGenerator.Generators.InputTextConverter(generator);
            inputTextConverter.ConvertNodeForProperties(properties, textElement);

            Assert.Contains("public string InputText_Value = \"\";", properties);
        }
    }
}
