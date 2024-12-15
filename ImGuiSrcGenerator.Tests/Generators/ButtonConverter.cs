using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class ButtonConverter
    {
        string toConvertOneButton =
@"<Container className=""TestContainer"" >
<Button name=""Button1"" text=""Click Me!"" />
</Container>
";
        string toConvertTwoButtons =
@"<Container className=""TestContainer"" >
<Button name=""Button1"" text=""Click Me!"" />
<Button name=""Button2"" text=""Click Me!"" />
</Container>
";

        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        [Fact]
        public void GenerateFromString()
        {
            string converted = generator.ConvertFromString(toConvertOneButton);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void HasRenderCodeForButton()
        {
            string converted = generator.ConvertFromString(toConvertOneButton);
            Assert.Contains(
@"		if (ImGui.Button(""Click Me!""))
		{
			Button1_OnClick();
		}
", converted);
        }

        [Fact]
        public void HasOnClickFunctionForButton()
        {
            string converted = generator.ConvertFromString(toConvertOneButton);
            Assert.Contains(
@"	public void Button1_OnClick()
	{

	}
", converted);
        }

        [Fact]
        public void HasRenderCodeForAllButtons()
        {
            string converted = generator.ConvertFromString(toConvertTwoButtons);
            Assert.Contains(
@"		if (ImGui.Button(""Click Me!""))
		{
			Button1_OnClick();
		}
", converted);
            Assert.Contains(
@"		if (ImGui.Button(""Click Me!""))
		{
			Button2_OnClick();
		}
", converted);
        }

        [Fact]
        public void HasOnClickFunctionForAllButtons()
        {
            string converted = generator.ConvertFromString(toConvertTwoButtons);
            Assert.Contains(
@"	public void Button1_OnClick()
	{

	}
", converted);
            Assert.Contains(
@"	public void Button2_OnClick()
	{

	}
", converted);
        }
    }
}
