using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class Generator
    {
        string toConvertContainerOnly =
                @"
<Container className=""TestContainer"" >
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
        public void HasProperClassStructure()
        {
            string converted = generator.ConvertFromString(toConvertContainerOnly);
            Assert.Equal(
@"public partial class TestContainer
{
	public void Render()
	{
	}
}


public partial class TestContainer
{

}
", converted);
        }
    }
}
