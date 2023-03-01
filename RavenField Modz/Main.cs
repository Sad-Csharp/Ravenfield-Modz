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
        public static Rect windowRect2 = new Rect(0, 0, 120, 100);

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
        }

        public void MakeMainWindow(int windowID)
        {
            if (GUI.Button(new Rect(10, 20, 130, 20), "LocalPlayer Options"))
            {
                _showGui2 = true;
                if (_showGui2)
                {
                    _showGui = false;
                }
            }
            GUI.DragWindow();
        }

        public void OnGUI()
        {
            if (_showGui)
            {
                windowRect = GUI.Window(0, windowRect, MakeMainWindow, "Debug Menu");
            }
            if (_showGui2)
            {
                windowRect2 = GUI.Window(1, windowRect2, Modules.LocalPlayer.Health_Controller.HealthWindow, "LocalPlayer Options");
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                _showGui = !_showGui;
            }
        }
    }
}
