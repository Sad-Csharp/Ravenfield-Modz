using UnityEngine;
using UnityEngine.SceneManagement;

namespace RavenField_Modz.Modules.LocalPlayer
{
    internal class Flight : MonoBehaviour
    {
        internal static bool isFlying = false;

        internal static void FlightMode()
        {
            if (isFlying)
            {
                float flySpeed = 95f;
                Refs.FirstPersonController.m_GravityMultiplier = 0f;
                Vector3 position = Refs.PlayerObj.transform.position;
                Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
                Vector3 right = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

                if (Input.GetKey(KeyCode.LeftShift)) { flySpeed = 125f; }
                if (Input.GetKey(KeyCode.LeftControl)) { flySpeed = 15f; }

                if (Input.GetKey(KeyCode.W)) { position += forward * Time.deltaTime * flySpeed; }
                if (Input.GetKey(KeyCode.S)) { position -= forward * Time.deltaTime * flySpeed; }
                if (Input.GetKey(KeyCode.A)) { position -= right * Time.deltaTime * flySpeed; }
                if (Input.GetKey(KeyCode.D)) { position += right * Time.deltaTime * flySpeed; }
                if (Input.GetKey(KeyCode.Q)) { position += Refs.PlayerObj.transform.up * Time.deltaTime * flySpeed; }
                if (Input.GetKey(KeyCode.E)) { position -= Refs.PlayerObj.transform.up * Time.deltaTime * flySpeed; }

                Refs.PlayerObj.transform.position = position;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                isFlying = !isFlying;
                Refs.FirstPersonController.m_GravityMultiplier = 1.2f;
                return;
            }
        }
    }
}
