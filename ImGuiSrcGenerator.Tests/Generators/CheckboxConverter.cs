using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class CheckboxConverter
    {
        string toConvertOneCheckbox =
                @"
<Container className=""TestContainer"" >
<Checkbox name=""Checkbox1"" text=""Check Me!"" />
</Container>
                ";
        string toConvertTwoCheckboxes =
                @"
<Container className=""TestContainer"" >
<Checkbox name=""Checkbox1"" text=""Check Me!"" />
<Checkbox name=""Checkbox2"" text=""Check Me!"" />
</Container>
                ";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        [Fact]
        public void GenerateFromString()
        {
            string converted = generator.ConvertFromString(toConvertOneCheckbox);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void HasRenderCodeForCheckbox()
        {
            string converted = generator.ConvertFromString(toConvertOneCheckbox);
            Assert.Contains("ImGui.Checkbox(\"Check Me!\", ref Checkbox1_Checked));", converted);
        }

        [Fact]
        public void HasBooleanPropertyForCheckboxValue()
        {
            string converted = generator.ConvertFromString(toConvertOneCheckbox);
            Assert.Contains("public bool Checkbox1_Checked;", converted);
        }

        [Fact]
        public void HasRenderCodeForAllCheckboxes()
        {
            string converted = generator.ConvertFromString(toConvertTwoCheckboxes);
            Assert.Contains("ImGui.Checkbox(\"Check Me!\", ref Checkbox1_Checked));", converted);
            Assert.Contains("ImGui.Checkbox(\"Check Me!\", ref Checkbox2_Checked));", converted);
        }

        [Fact]
        public void HasBooleanPropertiesForAllCheckboxValues()
        {
            string converted = generator.ConvertFromString(toConvertTwoCheckboxes);
            Assert.Contains("public bool Checkbox1_Checked;", converted);
            Assert.Contains("public bool Checkbox2_Checked;", converted);
        }
    }
}
