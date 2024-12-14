using ImGuiNET;
using System;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Display
{
    internal partial class Button : IDisplay
    {
        public void Render()
        {
            if (ImGui.Button("Click Me"))
            {
                OnClick_Button();
            }
            ImGui.Checkbox("Check me off", ref CheckboxChecked);
        }
    }

    internal partial class Button
    {
        public bool CheckboxChecked;
        public void OnClick_Button()
        {
            ImGui.Text("You clicked me!");
        }
    }
}
