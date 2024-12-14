using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImGuiSrcGenerator.Generators
{
    internal abstract class Converter
    {
        protected Generator Generator;
        protected string PrefixCharacter { get { return Generator.PrefixCharacter; } }

        protected Converter(Generator generator) 
        {
            Generator = generator;
        }

        public virtual StringBuilder ConvertNodeForRenderPreChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            return rb;
        }

        public virtual StringBuilder ConvertNodeForRenderPostChildren(StringBuilder rb, XmlNode xmlNode, ref string prefix)
        {
            return rb;
        }

        public virtual StringBuilder ConvertNodeForActionPreChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            return ab;
        }
        public virtual StringBuilder ConvertNodeForActionPostChildren(StringBuilder ab, XmlNode xmlNode, ref string prefix)
        {
            return ab;
        }
    }
}
