using System;
using UnityEngine;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class Health_Controller : MonoBehaviour
    {
        private static bool _isToggled = false;

        internal static void HealthWindow(int windowID)
        {
            // Make window draggable
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
            // Begins auto aligned vertical contents
            GUILayout.BeginVertical();
            _isToggled = GUI.Toggle(new Rect(10, 20, 130, 20), _isToggled, "Invulnerability");
            if (_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = true;
                Refs.LocalPlayer.activeWeapon.ammo = 999999;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 999999;
                return;
            }
            else if (!_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = false;
                Refs.LocalPlayer.activeWeapon.ammo = 1;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 10;
                return;
            }
        }
    }
}
