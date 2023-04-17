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
            MODNAME = "RavenFieldGui",
            AUTHOR = "Mizar",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.2.0";

        internal readonly ManualLogSource log;
        internal readonly Harmony harmony;
        internal readonly Assembly assembly;
        public readonly string modFolder;

        public static bool showGui = false;
        public static bool showGui2 = false;
        public static bool isFlying = false;
        public static float initialValue;
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
            //// RavenGUI
            //if (showGui)
            //{
            //    GUI.color = Color.gray;
            //    windowRect = GUILayout.Window(0, windowRect, Modules.GuiClasses.RavenGui.MainWindow, "Debug Menu", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            //}

            // LocalPlayer Gui
            if (showGui2)
            {
                GUI.color = Color.gray;
                windowRect2 = GUILayout.Window(1, windowRect2, Modules.GuiClasses.LocalPlayerMenu.LocalPlayerWindow, "LocalPlayer Options", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            }
        }

        public void Update()
        {
            Modules.LocalPlayer.FlightModule.Flight();
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                showGui2 = !showGui2;
            }
            if(Input.GetKey(KeyCode.Space))
            {
                if (!Refs.FirstPersonController.canJump) 
                {
                    Refs.FirstPersonController.canJump = true;
                    Refs.ActorPlayer.parachuteDeployed = false;
                }
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
        /// Store values of your choosing (soon)
        /// </summary>
        internal static void GetInitialValues()
        {
            if (PlayerPrefs.HasKey("initialValue"))
            {
                initialValue = PlayerPrefs.GetFloat("initialValue");
                Debug.LogWarning($"[GetInitialValues]:Found InitialValue: {initialValue}f");
            }
            else
            {
                initialValue = Refs.ActorPlayer.health;
                PlayerPrefs.SetFloat("initialValue", initialValue);
                Debug.LogWarning($"[GetInitialValues]:InitialValue Was Blank, Setting InitialValue Now To {initialValue}f");
            }
        }

        /// <summary>
        /// Restore values of your choosing (soon)
        /// </summary>
        internal static void RestoreInitialValues()
        {
            if (Refs.ActorPlayer != null)
            {
                Refs.ActorPlayer.health = initialValue;
                Debug.LogWarning($"[RestoreInitialValues]:Found InitialValue: {initialValue}f, Restoring InitialValue Now...");
            }
            else
            {
                Debug.LogError("[RestoreInitialValues]: Could Not Find LocalPlayer!");
            }
        }
    }
}