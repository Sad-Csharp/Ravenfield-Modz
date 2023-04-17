using UnityEngine;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class RavenGui : MonoBehaviour
    {
        internal static void MainWindow(int windowID)
        {

            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.yellow;

            GUIStyle style2 = new GUIStyle(GUI.skin.box);
            style2.normal.textColor = Color.yellow;

            if (GUILayout.Button("LocalPlayer Options", style, GUILayout.Width(150)))
            {
                Main.showGui2 = true;
                if (Main.showGui2)
                {
                    Main.showGui = false;
                }
            }
            if (GUILayout.Button(" X ", style2, GUILayout.Width(30)))
            {
                Main.showGui = !Main.showGui;
            }
            GUI.DragWindow();
        }
    }
}
