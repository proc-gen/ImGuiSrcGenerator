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
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(1, 1, 0, 1));
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(1, 0, 1, 1));
            if (ImGui.Button("Click Me"))
            {
                ButtonOnClick.DynamicInvoke();
            }
            ImGui.PopStyleColor();
            ImGui.SetItemTooltip("Stop waiting and click me");
            ImGui.Checkbox("Check me off", ref CheckboxChecked);
            ImGui.RadioButton("Radio 1", ref RadioValue, 0);
            ImGui.SameLine();
            ImGui.RadioButton("Radio 2", ref RadioValue, 1);
            ImGui.SameLine(); 
            ImGui.RadioButton("Radio 3", ref RadioValue, 2);
            ImGui.SameLine(); 
            ImGui.RadioButton("Radio 4", ref RadioValue, 3);
            ImGui.InputText("##InputText", ref TextNormal, 100);
            ImGui.InputTextWithHint("##InputHint", "Hint Hint", ref TextHint, 100);
            ImGui.InputTextMultiline("##InputMulti", ref TextMulti, 1000, Vector2.One * 200);
            ImGui.InputInt("##InputInt", ref TextInt, 1);
            ImGui.InputFloat("##InputFloat", ref TextFloat, .1f, .5f);
            ImGui.InputDouble("##InputDouble", ref TextDouble, .1, .5);
            ImGui.InputInt3("##InputInt3", ref TextInt3[0]);
            ImGui.InputFloat3("##InputFloat3", ref TextFloat3);
            ImGui.ArrowButton("##LeftButton", ImGuiDir.Left);
            ImGui.SameLine(); 
            ImGui.ArrowButton("##RightButton", ImGuiDir.Right);
            ImGui.DragInt("##DragInt", ref DragInt, 1, 0, 100, "%d");
            ImGui.DragInt2("##DragInt2", ref DragInt2[0], 1, 0, 100, "%d%%");
            ImGui.DragFloat("##DragFloat", ref DragFloat, .1f, -1, 1);
            ImGui.DragFloat2("##DragFloat2", ref DragFloat2, .1f, -10, 10);
            
            ImGui.PopStyleColor();
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
        public int[] TextInt3 = [0, 0, 0];
        public float TextFloat;
        public Vector3 TextFloat3 = new Vector3();
        public double TextDouble;
        public int DragInt;
        public float DragFloat;
        public int[] DragInt2 = [0, 0];
        public Vector2 DragFloat2 = new Vector2();

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
