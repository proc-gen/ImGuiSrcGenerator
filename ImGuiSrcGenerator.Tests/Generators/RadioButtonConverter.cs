using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Tests.Generators
{
    public class RadioButtonConverter
    {
        string toConvertOneRadioButtonGroup =
        @"
<Container className=""TestContainer"" >
<RadioButton name=""RadioGroup1"" text=""Radio 1"" value=""0""/>
<RadioButton name=""RadioGroup1"" text=""Radio 2"" value=""1""/>
</Container>
                ";
        string toConvertTwoRadioButtonGroups =
                @"
<Container className=""TestContainer"" >
<RadioButton name=""RadioGroup1"" text=""Radio 1"" value=""0""/>
<RadioButton name=""RadioGroup1"" text=""Radio 2"" value=""1""/>
<RadioButton name=""RadioGroup2"" text=""Radio New 1"" value=""0""/>
<RadioButton name=""RadioGroup2"" text=""Radio New 2"" value=""1""/>
</Container>
                ";
        ImGuiSrcGenerator.Generators.Generator generator = new ImGuiSrcGenerator.Generators.Generator();
        [Fact]
        public void GenerateFromString()
        {
            string converted = generator.ConvertFromString(toConvertOneRadioButtonGroup);
            Assert.NotEmpty(converted);
        }

        [Fact]
        public void HasRenderCodeForRadioButtonGroup()
        {
            string converted = generator.ConvertFromString(toConvertOneRadioButtonGroup);
            Assert.Contains("ImGui.RadioButton(\"Radio 1\", ref RadioGroup1_Value, 0));", converted);
            Assert.Contains("ImGui.RadioButton(\"Radio 2\", ref RadioGroup1_Value, 1));", converted);
        }

        [Fact]
        public void HasIntPropertyForRadioButtonGroupValue()
        {
            string converted = generator.ConvertFromString(toConvertOneRadioButtonGroup);
            Assert.Contains("public int RadioGroup1_Value;", converted);
        }

        [Fact]
        public void HasRenderCodeForAllRadioButtonGroups()
        {
            string converted = generator.ConvertFromString(toConvertTwoRadioButtonGroups);
            Assert.Contains("ImGui.RadioButton(\"Radio 1\", ref RadioGroup1_Value, 0));", converted);
            Assert.Contains("ImGui.RadioButton(\"Radio 2\", ref RadioGroup1_Value, 1));", converted);
            Assert.Contains("ImGui.RadioButton(\"Radio New 1\", ref RadioGroup2_Value, 0));", converted);
            Assert.Contains("ImGui.RadioButton(\"Radio New 2\", ref RadioGroup2_Value, 1));", converted);
        }

        [Fact]
        public void HasIntPropertiesForAllRadioButtonGroupValues()
        {
            string converted = generator.ConvertFromString(toConvertTwoRadioButtonGroups);
            Assert.Contains("public int RadioGroup1_Value;", converted);
            Assert.Contains("public int RadioGroup2_Value;", converted);
        }
    }
}
