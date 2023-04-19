using System;
using UnityEngine;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class LocalPlayerMenu : MonoBehaviour
    {
        #region Declarations
        internal static bool isToggled = false;
        internal static bool rayCastToggle = false;
        internal static bool raySphereToggle = false;
        internal static float initValue;
        internal static string ammo = "9999";
        internal static int ammoCount;
        internal static string health = "9999";
        internal static float healthCount;
        internal static string aiGodMode = "false";
        #endregion

        internal static void LocalPlayerWindow(int windowID)
        {
            GUILayout.Label("Flight: " + Modules.LocalPlayer.Flight.isFlying.ToString());
            GUILayout.Space(5);

            rayCastToggle = (GUILayout.Toggle(rayCastToggle, "Teleport To Crosshair"));
            raySphereToggle = (GUILayout.Toggle(raySphereToggle, "Teleport Enemies To You"));
            GUILayout.Space(25);

            GUILayout.BeginHorizontal();
            if (AutoSizeButton("GodMode:"))
            {
                isToggled = !isToggled;
                Refs.Actor.isInvulnerable = isToggled;
            }
            GUILayout.Label(Refs.Actor.isInvulnerable.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (AutoSizeButton("Ghost Mode:"))
            {
                isToggled = !isToggled;
                Refs.Actor.isIgnored = isToggled;
            }
            GUILayout.Label(Refs.Actor.isIgnored.ToString());
            GUILayout.EndHorizontal();

            if (AutoSizeButton("No Recoil"))
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
            GUILayout.Space(25);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Edit Ammo:");
            ammo = GUILayout.TextField(ammo);
            int.TryParse(ammo, out int ammoVal);
            ammoCount = ammoVal;
            PlayerPrefs.SetInt("ammo", ammoVal);
            GUILayout.EndHorizontal();

            if (AutoSizeButton("Set Ammo"))
            {
                Refs.Actor.activeWeapon.ammo = ammoVal;
                Refs.Actor.activeWeapon.spareAmmo = ammoVal;
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Edit Health:");
            health = GUILayout.TextField(health);
            float.TryParse(health, out float healthVal);
            healthCount = healthVal;
            PlayerPrefs.SetFloat("health", healthVal);
            GUILayout.EndHorizontal();

            if (AutoSizeButton("Set Health"))
            {
                Refs.Actor.health = healthVal;
                Refs.Actor.maxHealth = healthVal;
            }
            GUILayout.Space(25);

            if (AutoSizeButton("Set Team: Blue"))
            {
                Refs.Actor.SetTeam(0);
            }
            if (AutoSizeButton("Set Team: Red"))
            {
                Refs.Actor.SetTeam(1);
            }
            GUILayout.Space(25);
            GUILayout.Label("Ai GodMode: " + aiGodMode.ToString());

            if (AutoSizeButton("Enable GodMode For Team/Enemy"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();
                    if (allActors.team == 0 || allActors.team == 1)
                    {
                        allActors.isInvulnerable = true;
                        aiGodMode = allActors.isInvulnerable.ToString();
                    }
                }
            }
            if (AutoSizeButton("Disable GodMode For Team/Enemy"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();
                    if (allActors.team == 1 || allActors.team == 0)
                    {
                        allActors.isInvulnerable = false;
                        aiGodMode = allActors.isInvulnerable.ToString();
                    }
                }
            }
            GUILayout.Space(25);

            if (AutoSizeButton("Kill All Enemies"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 1)
                    {
                        allActors.Die(DamageInfo.Explosion, false);
                    }
                }
            }
            if (AutoSizeButton("Kill All Teammates"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 0)
                    {
                        allActors.Die(DamageInfo.Explosion, false);
                    }
                }
            }
            if (AutoSizeButton("Set Squad: GodMode/Infinite Ammo"))
            {
                Refs.FPSactorcontroller.playerSquad.aiMembers.ForEach(member => 
                { 
                    member.actor.isInvulnerable = true; 
                    member.actor.activeWeapon.ammo = 9999; 
                    member.actor.activeWeapon.spareAmmo = 9999;
                });
            }
            if (AutoSizeButton("Unlock Hidden Weapons!"))
            {
                Refs.WeaponManager.secretWeapon.hidden = false;
                Refs.WeaponManager.secretMemeWeapon.hidden = false;
                Refs.WeaponManager.secretHalloweenWeapon.hidden = false;
            }

            if (AutoSizeButton("Restore default values!"))
            {
                RestoreInitialValues();
            }
            GUI.DragWindow();
        }
        public static bool AutoSizeButton(string buttonText)
        {
            // Calculate the size of the button based on the provided text
            Vector2 buttonSize = GUI.skin.button.CalcSize(new GUIContent(buttonText));

            // Draw the button with the auto-sized width
            bool newButton = GUILayout.Button(buttonText, GUILayout.Width(buttonSize.x));

            // Return whether the button was clicked
            return newButton;
        }

        /// <summary>
        /// Store values of your choosing (soon)
        /// </summary>
        internal static void GetInitialValues()
        {

            try
            {
                if (PlayerPrefs.HasKey("health") || PlayerPrefs.HasKey("ammo"))
                {
                    Modules.GuiClasses.LocalPlayerMenu.healthCount = PlayerPrefs.GetFloat("health");
                    Modules.GuiClasses.LocalPlayerMenu.ammoCount = PlayerPrefs.GetInt("ammo");
                }
            }
            catch(Exception)
            {

            }
        }

        /// <summary>
        /// Restore values of your choosing (soon)
        /// </summary>
        internal static void RestoreInitialValues()
        {
            try
            {
                if (Refs.Actor != null)
                {
                    Refs.Actor.health = Modules.GuiClasses.LocalPlayerMenu.healthCount;
                    Refs.Actor.activeWeapon.ammo = Modules.GuiClasses.LocalPlayerMenu.ammoCount;
                    Refs.Actor.activeWeapon.spareAmmo = Modules.GuiClasses.LocalPlayerMenu.ammoCount;
                }
            }
            catch(Exception e )
            {
                e.
            }
        }
    }
}
