using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Tests.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static XmlElement CreateElementWithAttributes(this XmlDocument document, string elementName, Dictionary<string, string> attributes)
        {
            var element = document.CreateElement(elementName);
            foreach (var set in attributes)
            {
                element.SetAttribute(set.Key, set.Value);
            }
            return element;
        }
    }
}
