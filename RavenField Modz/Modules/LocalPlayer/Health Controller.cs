using UnityEngine;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class Health_Controller : MonoBehaviour
    {
        private static bool _isToggled = false;
        private static float initialValue;


        internal static void HealthWindow(int windowID)
        {
            GUIStyle style = new GUIStyle(GUI.skin.toggle);
            GUIStyle style2 = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.green;
            style.fontStyle = FontStyle.Bold;
            style2.normal.textColor = Color.green;
            style2.fontStyle = FontStyle.Bold;

            _isToggled = GUI.Toggle(new Rect(10, 20, 130, 20), _isToggled, "Invulnerability", style);

            if (GUI.Button(new Rect(10, 45, 130, 20), "Store Init. Value", style2))
            {
                GetInitialValues();
            }

            if (GUI.Button(new Rect(10, 70, 130, 20), "Restore Init. Value", style2))
            {
                RestoreInitialValues();
            }

            if (GUI.Button(new Rect(10, 95, 50, 20), "X", style2))
            {
                Main._showGui = true;
                if (Main._showGui2)
                {
                    Main._showGui2 = false;
                }
            }

            if (_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = true;
                Refs.LocalPlayer.activeWeapon.ammo = 999999;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 999999;
                Refs.LocalPlayer.speedMultiplier = 10;
            }

            else if (!_isToggled)
            {
                Refs.LocalPlayer.isInvulnerable = false;
                Refs.LocalPlayer.activeWeapon.ammo = 10;
                Refs.LocalPlayer.activeWeapon.spareAmmo = 85;
                Refs.LocalPlayer.speedMultiplier = 0;

            }

            GUI.DragWindow();
        }
        internal static void GetInitialValues()
        {
            if (PlayerPrefs.HasKey("initialValue"))
            {
                initialValue = PlayerPrefs.GetFloat("initialValue");
                Debug.LogWarning($"[GetInitialValues]:Found InitialValue: {initialValue}f");
            }
            else
            {
                initialValue = Refs.LocalPlayer.health;
                PlayerPrefs.SetFloat("initialValue", initialValue);
                Debug.LogWarning($"[GetInitialValues]:InitialValue Was Blank, Setting InitialValue Now To {initialValue}f");
            }
        }
        internal static void RestoreInitialValues()
        {
            if (Refs.LocalPlayer != null)
            {
                Refs.LocalPlayer.health = initialValue;
                Debug.LogWarning($"[RestoreInitialValues]:Found InitialValue: {initialValue}f, Restoring InitialValue Now...");
            }
            else
            {
                Debug.LogError("[RestoreInitialValues]: Could Not Find LocalPlayer!");
            }
        }
    }
}