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
                var size = ImGui.GetIO().DisplaySize;
                ImGui.SetNextWindowPos(Vector2.Zero.ToNumerics());
                ImGui.SetNextWindowSize(size);
                ImGui.Begin("My First Tool", ref _toolActive, 
                    ImGuiWindowFlags.MenuBar | 
                    ImGuiWindowFlags.NoDecoration | 
                    ImGuiWindowFlags.NoTitleBar | 
                    ImGuiWindowFlags.NoCollapse | 
                    ImGuiWindowFlags.NoScrollbar);
                ImGui.InputTextMultiline("##xml", ref XmlCode, int.MaxValue, new Vector2(size.X *.5f - 8, size.Y).ToNumerics());
                ImGui.SameLine(size.X * .5f + 8);
                ImGui.InputTextMultiline("##cs", ref ConvertedCode, int.MaxValue, new Vector2(size.X * .5f - 8, size.Y).ToNumerics());
                ImGui.End();
            }
        }
    }
}