using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MapMagic;
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

        public static bool ravenGuiWindow = false;
        public static bool localPlayerWindow = false;
        public static bool isFlying = false;
        public static bool aiWindow = false;

        public static Rect windowRect = new Rect(0, 0, 0, 0);
        public static Rect windowRect1 = new Rect(0, 0, 0, 0);
        public static Rect windowRect2 = new Rect(0, 0, 0, 0);

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

        /// <summary>
        /// Makes any GUILayout button text you pass through, auto size the button based on text!
        /// </summary>
        /// <param name="buttonText"></param>
        /// <returns></returns>
        public static bool AutoSizeButton(string buttonText)
        {
            Vector2 buttonSize = GUI.skin.button.CalcSize(new GUIContent(buttonText));
            bool newButton = GUILayout.Button(buttonText, GUILayout.Width(buttonSize.x));
            return newButton;
        }

        public void Start()
        {
            windowRect = CenterWindow(windowRect);
            windowRect1 = CenterWindow(windowRect1);
            windowRect2 = CenterWindow(windowRect2);
        }

        public void OnGUI()
        {
            // Raven Gui
            if (ravenGuiWindow)
            {
                GUI.color = Color.gray;
                windowRect = GUILayout.Window(0, windowRect, Modules.GuiClasses.RavenMenu.MainWindow, "RavenGui", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }

            // LocalPlayer Gui
            if (localPlayerWindow)
            {
                GUI.color = Color.gray;
                windowRect1 = GUILayout.Window(1, windowRect1, Modules.GuiClasses.LocalPlayerMenu.LocalPlayerWindow, "LocalPlayer Options", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
            
            // Ai Gui
            if (aiWindow)
            {
                GUI.color = Color.gray;
                windowRect2 = GUILayout.Window(2, windowRect2, Modules.GuiClasses.AiMenu.AiWindow, "Ai Options", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                ravenGuiWindow = !ravenGuiWindow;
            }

            Modules.LocalPlayer.Flight.FlightMode();
            Modules.LocalPlayer.Teleport.CrosshairTeleport();
            Modules.Ai.AiTeleport.AiTeleportToPlayer();
            Modules.LocalPlayer.ESP.EnemyESP();
        }
    }
}