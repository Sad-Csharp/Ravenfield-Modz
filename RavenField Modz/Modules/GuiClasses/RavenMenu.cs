using UnityEngine;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class RavenMenu : MonoBehaviour
    {
        internal static void MainWindow(int windowID)
        {
            GUILayout.Label("Flight Module: " + Modules.LocalPlayer.Flight.isFlying.ToString());
            GUILayout.Space(10);

            if (Main.AutoSizeButton("LocalPlayer Options"))
            {
                Main.localPlayerWindow = true;
                Main.ravenGuiWindow = false;
            }

            if (Main.AutoSizeButton("Ai Options"))
            {
                Main.aiWindow = true;
                Main.ravenGuiWindow = false;
            }

            if (Main.AutoSizeButton("Vehicles [Not Functional Yet!]"))
            {

            }

            GUI.DragWindow();
        }
    }
}
