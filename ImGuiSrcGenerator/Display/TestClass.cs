using ImGuiNET;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Display
{
    public partial class TestClass : IDisplay
    {
        public void Render()
        {
            if (ImGui.Button("Click Me"))
            {
                ButtonOnClick.DynamicInvoke();
            }
            ImGui.SetItemTooltip("Stop waiting and click me");
            ImGui.Checkbox("Check me off", ref CheckboxChecked);
            ImGui.RadioButton("Radio 1", ref RadioValue, 0);
            ImGui.RadioButton("Radio 2", ref RadioValue, 1);
            ImGui.RadioButton("Radio 3", ref RadioValue, 2);
            ImGui.RadioButton("Radio 4", ref RadioValue, 3);
            ImGui.InputText("##InputText", ref TextNormal, 100);
            ImGui.InputTextWithHint("##InputHint", "Hint Hint", ref TextHint, 100);
            ImGui.InputTextMultiline("##InputMulti", ref TextMulti, 1000, Vector2.One * 200);
            ImGui.InputInt("##InputInt", ref TextInt, 1);
            ImGui.InputFloat("##InputFloat", ref TextFloat, .1f, .5f);
            ImGui.InputDouble("##InputDouble", ref TextDouble, .1, .5);
        }
    }

    public partial class TestClass
    {
        public Delegate ButtonOnClick;
        public bool CheckboxChecked;
        public int RadioValue;
        public string TextNormal = "";
        public string TextHint = "";
        public string TextMulti = "";
        public int TextInt;
        public float TextFloat;
        public double TextDouble;

        public TestClass()
        {
            ButtonOnClick = OnClick_Button;
        }
        public void OnClick_Button()
        {
            ImGui.Text("You clicked me!");
        }
    }
}
