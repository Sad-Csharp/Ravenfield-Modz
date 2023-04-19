using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RavenField_Modz
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        #region[Declarations]

        public const string
            MODNAME = "RavenFieldGui",
            AUTHOR = "Mizar",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.2.1";

        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        public static bool showGui = false;
        public static bool showGui2 = false;
        public static bool isFlying = false;
        public static float initialValue;
        public static Rect windowRect = new Rect(0, 0, 0, 0);
        internal static string[] sceneBlackList = { "Loading", "Loaded", "Unloading", "Menu" };

        #endregion

        public Main()
        {
            log = Logger;
            harmony = new Harmony(GUID);
            assembly = Assembly.GetExecutingAssembly();
            modFolder = Path.GetDirectoryName(assembly.Location);
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (sceneBlackList.Contains(scene.name) == false)
            {

            }
        }

        public void Start()
        {
            harmony.PatchAll(assembly);
            windowRect = CenterWindow(windowRect);
        }

        public void OnGUI()
        {
            #region FINISHME
            //// RavenGUI
            //if (showGui)
            //{
            //    GUI.color = Color.gray;
            //    windowRect = GUILayout.Window(0, windowRect, Modules.GuiClasses.RavenGui.MainWindow, "Debug Menu", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            //} 
            #endregion

            // LocalPlayer Gui
            if (showGui2)
            {
                GUI.color = Color.gray;
                windowRect = GUILayout.Window(1, windowRect, Modules.GuiClasses.LocalPlayerMenu.LocalPlayerWindow, "LocalPlayer Options", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
        }

        public void Update()
        {
            Modules.LocalPlayer.Flight.FlightMode();
            Modules.LocalPlayer.Teleport.CrosshairTeleport();
            Modules.LocalPlayer.Teleport.AiTeleport();
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                showGui2 = !showGui2;
            }
        }

        /// <summary>
        /// Makes any Rect you pass through, auto center to your screen.
        /// </summary>
        /// <param name="windowRect"></param>
        /// <returns>windowRect</returns>
        public static Rect CenterWindow(Rect windowRect)
        {
            windowRect.x = (Screen.width - windowRect.width) / 2;
            windowRect.y = (Screen.height - windowRect.height) / 2;
            return windowRect;
        }
    }
}