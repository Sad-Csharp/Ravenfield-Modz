using System;
using UnityEngine;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class LocalPlayerMenu : MonoBehaviour
    {
        #region Declarations

        internal static bool isGodMode = false;
        internal static bool isGhost = false;
        internal static bool rayCastToggle = false;
        internal static bool raySphereToggle = false;
        internal static bool espToggle = false;

        internal static string ammo = "9999";
        internal static int ammoCount;

        internal static string health = "9999";
        internal static float healthCount;

        #endregion

        internal static void LocalPlayerWindow(int windowID)
        {
            rayCastToggle = GUILayout.Toggle(rayCastToggle, "Teleport To Crosshair");
            raySphereToggle = GUILayout.Toggle(raySphereToggle, "Teleport Enemies To You");
            espToggle = GUILayout.Toggle(espToggle, "ESP");

            GUILayout.BeginHorizontal();
            if (Main.AutoSizeButton("GodMode:"))
            {
                isGodMode = !isGodMode;
                Refs.Actor.isInvulnerable = isGodMode;
            }
            GUILayout.Label(Refs.Actor.isInvulnerable.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (Main.AutoSizeButton("Ghost Mode:"))
            {
                isGhost = !isGhost;
                Refs.Actor.isIgnored = isGhost;
            }

            GUILayout.Label(Refs.Actor.isIgnored.ToString());
            GUILayout.EndHorizontal();

            if (Main.AutoSizeButton("No Recoil"))
            {
                foreach (Weapon weapon in Refs.Actor.weapons)
                {
                    try
                    {
                        weapon.configuration.kickback = 0f;
                        weapon.configuration.kickbackProneMultiplier = 0f;
                        weapon.configuration.followupMaxSpreadAim = 0f;
                        weapon.configuration.followupMaxSpreadHip = 0f;
                        weapon.configuration.followupSpreadGain = 0f;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Edit Ammo:");
            ammo = GUILayout.TextField(ammo);
            int.TryParse(ammo, out int ammoVal);
            ammoCount = ammoVal;
            GUILayout.EndHorizontal();

            if (Main.AutoSizeButton("Set Ammo"))
            {
                Refs.Actor.activeWeapon.ammo = ammoVal;
                Refs.Actor.activeWeapon.spareAmmo = ammoVal;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Edit Health:");
            health = GUILayout.TextField(health);
            float.TryParse(health, out float healthVal);
            healthCount = healthVal;
            GUILayout.EndHorizontal();

            if (Main.AutoSizeButton("Set Health"))
            {
                Refs.Actor.health = healthVal;
                Refs.Actor.maxHealth = healthVal;
            }

            if (Main.AutoSizeButton("Set Team: Blue"))
            {
                Refs.Actor.SetTeam(0);
            }

            if (Main.AutoSizeButton("Set Team: Red"))
            {
                Refs.Actor.SetTeam(1);
            }

            if (Main.AutoSizeButton("AddLineRender"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var aiObjs = aiController.gameObject;
                    aiObjs.AddComponent<LineRenderer>();
                }
            }

            if (Main.AutoSizeButton("Disable Rendered Lines"))
            {
                Modules.LocalPlayer.ESP.lineRenderer.positionCount = 0;
                Modules.LocalPlayer.ESP.lineRenderer.enabled = false;
            }

            if (Main.AutoSizeButton("Close Window"))
            {
                Refs.ShowText("GodMode: ITworked :D", 5f);
                Main.localPlayerWindow = false;
            }

            GUI.DragWindow();
        }
    }
}
