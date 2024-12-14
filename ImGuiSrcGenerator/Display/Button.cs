using ImGuiNET;
using System;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Display
{
    public partial class Button : IDisplay
    {
        public void Render()
        {
            if (ImGui.Button("Click Me"))
            {
                OnClick_Button();
            }
            ImGui.Checkbox("Check me off", ref CheckboxChecked);
            ImGui.RadioButton("Radio 1", ref RadioValue, 0);
            ImGui.RadioButton("Radio 2", ref RadioValue, 1);
            ImGui.RadioButton("Radio 3", ref RadioValue, 2);
            ImGui.RadioButton("Radio 4", ref RadioValue, 3);
        }
    }

    public partial class Button
    {
        public bool CheckboxChecked;
        public int RadioValue;
        public void OnClick_Button()
        {
            ImGui.Text("You clicked me!");
        }
    }
}
