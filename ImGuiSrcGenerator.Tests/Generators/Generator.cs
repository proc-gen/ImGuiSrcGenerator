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

        string testFilePath = Path.Combine("TestFiles", "TestClass.igml");

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

        [Fact]
        public void GenerateFromFile()
        {
            string converted = generator.ConvertFromFile(testFilePath);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void ConvertsAllComponents()
        {
            string converted = generator.ConvertFromFile(testFilePath);
            Assert.Equal(
@"public partial class TestContainer
{
	public void Render()
	{
		if (ImGui.Button(""Click Me!""))
		{
			MyFirstButton_OnClick.DynamicInvoke();
		}
		ImGui.Checkbox(""Check Me!"", ref MyFirstCheckbox_Checked));
		ImGui.RadioButton(""Radio 1"", ref RadioGroup_Value, 0));
		ImGui.RadioButton(""Radio 2"", ref RadioGroup_Value, 1));
		ImGui.RadioButton(""Radio 3"", ref RadioGroup_Value, 2));
		ImGui.RadioButton(""Radio 4"", ref RadioGroup_Value, 3));
		ImGui.InputText(""##InputText"", ref InputText_Value, 100));
		ImGui.InputTextWithHint(""##InputHint"", ""Hint Hint"", ref InputHint_Value, 100));
		ImGui.InputTextMultiline(""##InputMulti"", ref InputMulti_Value, 100, new System.Numerics.Vector2(200, 200)));
	}
}


public partial class TestContainer
{
	public Delegate MyFirstButton_OnClick;
	public bool MyFirstCheckbox_Checked;
	public int RadioGroup_Value;
	public string InputText_Value = """";
	public string InputHint_Value = """";
	public string InputMulti_Value = """";

}
", converted);
        }
    }
}
