using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace RavenField_Modz
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]

        public const string
            MODNAME = "RavenField_Modz",
            AUTHOR = "",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.0.0";

        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        public static bool _showGui = false;
        public static bool _showGui2 = false;
        public static Rect windowRect = new Rect(0, 0, 150, 250);
        public static Rect windowRect2 = new Rect(0, 0, 150, 250);

        #endregion

        public Main()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
        }

        public void Start()
        {
            harmony.PatchAll(assembly);
            windowRect = CenterWindow(windowRect);
            windowRect2 = CenterWindow(windowRect2);
        }

        public void OnGUI()
        {
            if (_showGui)
            {
                GUI.color = Color.gray;
                windowRect = GUI.Window(0, windowRect, MakeMainWindow, "Debug Menu") ;
            }
            if (_showGui2)
            {
                GUI.color = Color.gray;
                windowRect2 = GUI.Window(1, windowRect2, Modules.LocalPlayer.Health_Controller.HealthWindow, "LocalPlayer Options");
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                _showGui = !_showGui;
            }
        }

        // Makes any Rect you pass through, auto center to your screen.
        public static Rect CenterWindow(Rect windowRect)
        {
            windowRect.x = (Screen.width - windowRect.width) / 2;
            windowRect.y = (Screen.height - windowRect.height) / 2;
            return windowRect;
        }

        public void MakeMainWindow(int windowID)
        {

            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.yellow;

            GUIStyle style2 = new GUIStyle(GUI.skin.box);
            style2.normal.textColor = Color.yellow;

            Vector2 size = style.CalcSize(new GUIContent("LocalPlayer Options"));
            Vector2 size2 = style2.CalcSize(new GUIContent(" X "));

            if (GUI.Button(new Rect(10, 20, size.x, size.y), "LocalPlayer Options", style))
            {
                _showGui2 = true;
                if (_showGui2)
                {
                    _showGui = false;
                }
            }
            if (GUI.Button(new Rect(123, 225, size2.x, size2.y), " X ", style2))
            {
                _showGui = !_showGui;
            }
            GUI.DragWindow();
        }
    }
}
