using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class ContainerConverter
    {
        string toConvertContainerOnly =
                @"
<Container className=""TestContainer"" >
</Container>
                ";

        string toConvertWithComponent =
@"<Container className=""TestContainer"" >
<Checkbox name=""MyFirstCheckbox"" text=""Check Me!"" />
</Container>
";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();

        [Fact]
        public void GenerateFromString()
        {
            string converted = generator.ConvertFromString(toConvertContainerOnly);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void HasClassName()
        {
            string converted = generator.ConvertFromString(toConvertContainerOnly);
            Assert.Contains("public partial class TestContainer", converted);
        }

        [Fact]
        public void HasRenderFunction()
        {
            string converted = generator.ConvertFromString(toConvertContainerOnly);
            Assert.Contains("public void Render()", converted);
        }

        [Fact]
        public void HasClassProperties()
        {
            string converted = generator.ConvertFromString(toConvertWithComponent);
            Assert.Contains("public bool MyFirstCheckbox_Checked;", converted);
        }
    }
}
