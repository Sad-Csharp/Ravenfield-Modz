using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace RavenField_Modz
{
    internal class Refs : MonoBehaviour
    {
        #region[Managers]
        internal static GameManager GameManager { get => GameManager.instance; }
        internal static ActorManager ActorManager { get => ActorManager.instance; }
        internal static CoverManager CoverManager { get => CoverManager.instance; }
        internal static WeaponManager WeaponManager { get => WeaponManager.instance; }
        internal static DecalManager DecalManager { get => DecalManager.instance; }
        internal static OrderManager OrderManager { get => OrderManager.instance; }
        public static string Active_SceneName { get => SceneManager.GetActiveScene().name; }
        #endregion

        #region[LocalPlayer]
        internal static Actor ActorPlayer { get => ActorManager.player; }
        internal static GameObject PlayerObj { get => ActorPlayer.originalParent.gameObject; }
        internal static FpsActorController FPSactorcontroller { get => FpsActorController.instance; }
        internal static FirstPersonControllerInput FirstPersonControllerInput { get => FindObjectOfType<FirstPersonControllerInput>(); }
        internal static FirstPersonController FirstPersonController { get => PlayerObj.GetComponent<FirstPersonController>(); }
        #endregion

        #region[Blue AI = 0, Red AI = 1]
        internal static AiActorController[] AiActors { get => FindObjectsOfType<AiActorController>(); }

        #endregion
    }
}
