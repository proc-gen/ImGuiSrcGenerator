using ImGuiNET;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImGuiSrcGenerator.Display
{
    public class Main : IDisplay
    {
        bool _toolActive = true;

        public string XmlCode;
        public string ConvertedCode;

        public void Render()
        {
            if (_toolActive)
            {
                ImGui.Begin("My First Tool", ref _toolActive, ImGuiWindowFlags.MenuBar);

                // Display contents in a scrolling region
                ImGui.TextColored(new Vector4(1, 1, 0, 1).ToNumerics(), "Important Stuff");
                ImGui.BeginGroup();
                ImGui.BeginChild("Scrolling", new System.Numerics.Vector2(0), ImGuiChildFlags.Border);
                ImGui.InputTextMultiline("XML", ref XmlCode, int.MaxValue, new Vector2(600, 600).ToNumerics());
                ImGui.SameLine();
                ImGui.InputTextMultiline("Converted", ref ConvertedCode, int.MaxValue, new Vector2(600, 600).ToNumerics());
                ImGui.EndChild();
                ImGui.EndGroup();
                ImGui.End();
            }
        }
    }
}
