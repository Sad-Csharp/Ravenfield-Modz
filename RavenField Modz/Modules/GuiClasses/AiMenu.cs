using UnityEngine;

namespace RavenField_Modz.Modules.GuiClasses
{
    internal class AiMenu : MonoBehaviour
    {
        internal static bool redGod = false;
        internal static bool blueGod = false;
        internal static void AiWindow(int windowID)
        {

            if (Main.AutoSizeButton("Squad GodMode & Infinite Ammo"))
            {
                Refs.FPSactorcontroller.playerSquad.aiMembers.ForEach(member =>
                {
                    member.actor.isInvulnerable = true;
                    member.actor.activeWeapon.ammo = 9999;
                    member.actor.activeWeapon.spareAmmo = 9999;
                });
            }

            if (Main.AutoSizeButton($"Blue Team GodMode: {blueGod}"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();
                    if (allActors.team == 0)
                    {
                        blueGod = !blueGod;
                        allActors.isInvulnerable = blueGod;
                    }
                }
            }

            if (Main.AutoSizeButton($"Red Team GodMode: {redGod}"))
            {
                foreach (AiActorController aiController in Refs.AiActors)
                {
                    var allActors = aiController.GetComponent<Actor>();
                    if (allActors.team == 1)
                    {
                        redGod = !redGod;
                        allActors.isInvulnerable = redGod;
                    }
                }
            }

            if (Main.AutoSizeButton("Kill All Enemies"))
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
            
            if (Main.AutoSizeButton("Kill All Teammates"))
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

            if (Main.AutoSizeButton("Close Window"))
            {
                Main.aiWindow = false;
            }

            GUI.DragWindow();
        }
    }
}
