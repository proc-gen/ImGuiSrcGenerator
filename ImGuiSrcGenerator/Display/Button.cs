using ImGuiNET;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Display
{
    internal partial class Button : IDisplay
    {
        bool clicked = false;
        bool wasClicked = false;
        public void Render()
        {
            clicked = ImGui.Button("Click Me");
            if (clicked)
            {
                wasClicked = true;
            }
            if (wasClicked)
            {
                OnClick_Button();
            }
        }
    }

    internal partial class Button
    {
        public void OnClick_Button()
        {
            ImGui.Text("You clicked me!");
        }
    }
}
