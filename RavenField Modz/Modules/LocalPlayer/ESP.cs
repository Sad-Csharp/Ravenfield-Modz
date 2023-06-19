using MoonSharp.VsCodeDebugger.SDK;
using UnityEngine;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class ESP : MonoBehaviour
    {
        internal static GameObject enemyGameObject;
        internal static LineRenderer lineRenderer;
        internal static void EnemyESP()
        {
            try
            {
                if (Modules.GuiClasses.LocalPlayerMenu.espToggle)
                {
                    foreach (AiActorController aiActor in Refs.AiActors)
                    {
                        var allActors = aiActor.GetComponent<Actor>();
                        enemyGameObject = aiActor.gameObject;
                        lineRenderer = enemyGameObject.GetComponent<LineRenderer>();

                        if (allActors.team == 1)
                        {
                            float distanceToPlayer = Vector3.Distance(enemyGameObject.transform.position, Refs.PlayerObj.transform.position);

                            if (distanceToPlayer <= 250f)
                            {
                                lineRenderer.startColor = Color.red;
                                lineRenderer.endColor = Color.green;
                                lineRenderer.enabled = true;
                                lineRenderer.startWidth = .02f;
                                lineRenderer.endWidth = .02f;
                                lineRenderer.SetPosition(0, enemyGameObject.transform.position);
                                lineRenderer.SetPosition(1, Refs.PlayerObj.transform.position);
                            }
                            else
                            {
                                lineRenderer.enabled = false;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}