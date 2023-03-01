using UnityEngine;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class Health_Controller : MonoBehaviour
    {
        private static bool _isToggled = false;
        private static float? initialValue;


        internal static void HealthWindow(int windowID)
        {

            if (GUI.Button(new Rect(10, 45, 130, 20), "X"))
            {
                Main._showGui = true;
                if (Main._showGui2)
                {
                    Main._showGui2 = false;
                }
            }
            _isToggled = GUI.Toggle(new Rect(10, 20, 130, 20), _isToggled, "Invulnerability");
            if (_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = true;
                Refs.LocalPlayer.activeWeapon.ammo = 999999;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 999999;
            }
            else if (!_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = false;
                Refs.LocalPlayer.activeWeapon.ammo = 10;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 85;
            }
            GUI.DragWindow();
        }
         internal static void GetInitialValues()
        {
            if (Refs.LocalPlayer != null)
            {
                initialValue = Refs.LocalPlayer.health;
                Debug.Log($"[GetInitialValues]: {initialValue}");
                return;
            }
        }
        internal static void RestoreInitialValues()
        {
            if (PlayerPrefs.HasKey("initialValue"))
            {
                initialValue = PlayerPrefs.GetFloat("initialValue");
                Debug.Log($"");
            }
        }
    }
}