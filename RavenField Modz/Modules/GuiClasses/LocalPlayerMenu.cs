using UnityEngine;
using static DamageInfo;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class LocalPlayerMenu : MonoBehaviour
    {
        internal static bool isToggled = false;
        internal static float initValue;

        internal static void LocalPlayerWindow(int windowID)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle);
            GUIStyle style2 = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.yellow;
            style.fontStyle = FontStyle.Bold;
            style2.normal.textColor = Color.yellow;
            style2.fontStyle = FontStyle.Bold;
            if (GUILayout.Button("GodMode"))
            {
                Refs.ActorPlayer.isInvulnerable = true;
                Refs.ActorPlayer.activeWeapon.ammo = 999999;
                Refs.ActorPlayer.activeWeapon.spareAmmo = 999999;
                Refs.ActorPlayer.speedMultiplier = 10;
            }
            if (GUILayout.Button("Reset Gravity"))
            {
                Refs.FirstPersonController.m_GravityMultiplier = 1.2f;
            }
            if (GUILayout.Button("Set Team: Blue", GUILayout.Width(100)))
            {
                Refs.ActorPlayer.SetTeam(0);
            }
            if (GUILayout.Button("Set Team: Red", GUILayout.Width(100)))
            {
                Refs.ActorPlayer.SetTeam(1);
            }
            if (GUILayout.Button("Set Team GodMode"))
            {
                foreach(AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 0)
                    {
                        allActors.health = 9999f;
                        allActors.maxHealth = 9999f;
                    }
                }
            }
            if (GUILayout.Button("Set Enemy GodMode"))
            {
                foreach(AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 1)
                    {
                        allActors.health = 9999f;
                        allActors.maxHealth = 9999f;
                    }
                }
            }
            if (GUILayout.Button("Kill all enemies"))
            {
                foreach(AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 1)
                    {
                        allActors.Die(DamageInfo.Explosion, false);
                    }
                }
            }
            if (GUILayout.Button("Kill all teammates"))
            {
                foreach(AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();

                    if (allActors.team == 0)
                    {
                        allActors.Die(DamageInfo.Explosion, false);
                    }
                }
            }

            GUI.DragWindow();
        }
    }
}
