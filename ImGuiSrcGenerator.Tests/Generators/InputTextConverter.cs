using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class InputTextConverter
    {
        string toConvertOneTextbox =
                @"
<Container className=""TestContainer"" >
<InputText name=""##InputText"" maxLength=""100"" />
</Container>
                ";
        string toConvertTwoTextboxes =
                @"
<Container className=""TestContainer"" >
<InputText name=""##InputText"" maxLength=""100"" />
<InputText name=""##InputHint"" hint=""My Hint"" maxLength=""100"" />
<InputText name=""##InputMulti"" maxLength=""100"" multiline=""true"" width=""200"" height=""200""/>
</Container>
                ";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        [Fact]
        public void GenerateFromString()
        {
            string converted = generator.ConvertFromString(toConvertOneTextbox);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void HasRenderCodeForTextbox()
        {
            string converted = generator.ConvertFromString(toConvertOneTextbox);
            Assert.Contains("ImGui.InputText(\"##InputText\", ref InputText_Value, 100));", converted);
        }

        [Fact]
        public void HasBooleanPropertyForTextboxValue()
        {
            string converted = generator.ConvertFromString(toConvertOneTextbox);
            Assert.Contains("public string InputText_Value = \"\";", converted);
        }

        [Fact]
        public void HasRenderCodeForAllTextboxes()
        {
            string converted = generator.ConvertFromString(toConvertTwoTextboxes);
            Assert.Contains("ImGui.InputText(\"##InputText\", ref InputText_Value, 100));", converted);
            Assert.Contains("ImGui.InputTextWithHint(\"##InputHint\", \"My Hint\", ref InputHint_Value, 100));", converted);
            Assert.Contains("ImGui.InputTextMultiline(\"##InputMulti\", ref InputMulti_Value, 100, new System.Numerics.Vector2(200, 200)));", converted);
        }

        [Fact]
        public void HasBooleanPropertiesForAllTextboxValues()
        {
            string converted = generator.ConvertFromString(toConvertTwoTextboxes);
            Assert.Contains("public string InputText_Value = \"\";", converted);
            Assert.Contains("public string InputHint_Value = \"\";", converted);
            Assert.Contains("public string InputMulti_Value = \"\";", converted);
        }
    }
}
