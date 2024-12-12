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

        public System.Numerics.Vector4 ColorV4 = Color.CornflowerBlue.ToVector4().ToNumerics();

        public void Render()
        {
            if (_toolActive)
            {
                ImGui.Begin("My First Tool", ref _toolActive, ImGuiWindowFlags.MenuBar);
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.BeginMenu("File"))
                    {
                        if (ImGui.MenuItem("Open..", "Ctrl+O")) { /* Do stuff */ }
                        if (ImGui.MenuItem("Save", "Ctrl+S")) { /* Do stuff */ }
                        if (ImGui.MenuItem("Close", "Ctrl+W")) { _toolActive = false; }
                        ImGui.EndMenu();
                    }
                    ImGui.EndMenuBar();
                }

                ImGui.LabelText("Stuff", "Hello");

                // Edit a color stored as 4 floats
                ImGui.ColorEdit4("Color", ref ColorV4);

                // Generate samples and plot them
                var samples = new float[100];
                for (var n = 0; n < samples.Length; n++)
                    samples[n] = (float)Math.Sin(n * 0.2f + ImGui.GetTime() * 1.5f);
                ImGui.PlotLines("Samples", ref samples[0], 100);

                // Display contents in a scrolling region
                ImGui.TextColored(new Vector4(1, 1, 0, 1).ToNumerics(), "Important Stuff");
                ImGui.BeginChild("Scrolling", new System.Numerics.Vector2(0), ImGuiChildFlags.Border);
                for (var n = 0; n < 50; n++)
                    ImGui.Text($"{n:0000}: Some text");
                ImGui.EndChild();
                ImGui.End();
            }
        }
    }
}
