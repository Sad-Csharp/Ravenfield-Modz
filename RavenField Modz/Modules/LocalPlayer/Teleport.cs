using UnityEngine;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class Teleport
    {
        internal static void CrosshairTeleport()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Modules.GuiClasses.LocalPlayerMenu.rayCastToggle == true)
            {
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
                Ray ray = Camera.main.ScreenPointToRay(screenCenter);

                if (Physics.Raycast(ray, out RaycastHit hit, 100000f))
                {
                    Refs.PlayerObj.transform.position = hit.point;
                }
            }
        }

        // Need to figure out how to prevent tp back.
        internal static void AiTeleport()
        {
            if (Input.GetKey(KeyCode.Alpha1) && Modules.GuiClasses.LocalPlayerMenu.raySphereToggle == true)
            {
                foreach(AiActorController actorController in Refs.AiActors)
                {
                    var AiObj = actorController.gameObject;
                    var AiActor = AiObj.GetComponent<Actor>();
                    var AiActorTransform = AiObj.GetComponent<Transform>();
                    if (AiActor.team == 1)
                    {
                        AiActorTransform.position = Refs.PlayerObj.transform.position + new Vector3(0, 0, 5);
                    }
                }
            }
        }
    }
}
