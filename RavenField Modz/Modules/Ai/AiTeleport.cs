using UnityEngine;

namespace RavenField_Modz.Modules.Ai
{
    internal class AiTeleport : MonoBehaviour
    {
        // Need to figure out how to prevent tp back.
        internal static void AiTeleportToPlayer()
        {
            if (Input.GetKey(KeyCode.Alpha1) && Modules.GuiClasses.LocalPlayerMenu.raySphereToggle == true)
            {
                foreach (AiActorController actorController in Refs.AiActors)
                {
                    var AiObject = actorController.gameObject;
                    var AiActor = AiObject.GetComponent<Actor>();
                    var AiActorTransform = AiObject.GetComponent<Transform>();
                    if (AiActor.team == 1)
                    {
                        AiActorTransform.position = Refs.PlayerObj.transform.position + new Vector3(0, 0, 5);
                    }
                }
            }
        }
    }
}
