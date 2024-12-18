using ImGuiSrcGenerator.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TheConverter = ImGuiSrcGenerator.Generators.Converter;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class Converter
    {
        const string ElementName = "Button";
        XmlDocument xmlDoc = new XmlDocument();
        Dictionary<string, string> attributes = new Dictionary<string, string>()
        {
            {"name", "##My_Name-Is" },
            {"text", "Click Me" },
        };

        [Fact]
        public void GetsNameFromElement()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            Assert.Equal(attributes["name"], TheConverter.GetName(element));
        }

        [Fact]
        public void GetsCodeUsableName()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            Assert.Equal("My_Name-Is", TheConverter.GetCodeUsableName(element));
        }

        [Fact]
        public void GetsAttributeValueWhenItExists()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            Assert.Equal(attributes["text"], TheConverter.GetAttributeValueOrDefault(element, "text", "Default Text"));
        }

        [Fact]
        public void GetsDefaultValueWhenAttributeDoesNotExist()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            Assert.Equal("Default Text", TheConverter.GetAttributeValueOrDefault(element, "badAttribute", "Default Text"));
        }

        [Fact]
        public void TryGetsAttributeValueWhenItExists()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            bool found = TheConverter.TryGetAttributeValue(element, "text", out string value);
            Assert.True(found);
            Assert.Equal(attributes["text"], value);
        }

        [Fact]
        public void TryGetsReturnsFalseWhenAttributeDoesNotExist()
        {
            var element = xmlDoc.CreateElementWithAttributes(ElementName, attributes);
            bool found = TheConverter.TryGetAttributeValue(element, "badAttribute", out string value);
            Assert.False(found);
        }
    }
}
