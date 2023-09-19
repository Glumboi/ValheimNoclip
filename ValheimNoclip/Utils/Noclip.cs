using HarmonyLib;
using UnityEngine;

namespace ValheimNoclip.Utils
{
    public static class Noclip
    {
        public static bool isNoclip = false;
        private static uint additionalNoclipMoveSpeed = 1;
        private static Traverse<Rigidbody> rb = null;

        public static void DoNoclip(ref Player player, Traverse<Rigidbody> rb)
        {
            rb.Value.useGravity = !isNoclip;
            rb.Value.isKinematic = isNoclip;

            if (isNoclip)
            {
                // Access the GameCamera instance to get the camera's transform
                Transform cameraTransform = GameCamera.instance.transform;

                // Calculate the movement direction based on the camera's forward direction
                Vector3 moveDirection = cameraTransform.forward * Input.GetAxis("Vertical") + cameraTransform.right * Input.GetAxis("Horizontal");

                // Normalize the movement direction
                if (moveDirection.magnitude > 1f)
                {
                    moveDirection.Normalize();
                }

                // Calculate the new position based on movement direction and speed
                Vector3 newPosition = player.transform.position + moveDirection * player.m_runSpeed * additionalNoclipMoveSpeed * Time.fixedDeltaTime;
                // Handle height adjustment based on Space and Control keys
                if (Input.GetKey(KeyCode.Space))
                {
                    newPosition.y += player.m_runSpeed * additionalNoclipMoveSpeed * Time.fixedDeltaTime; // Move up
                }
                else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    newPosition.y -= player.m_runSpeed * additionalNoclipMoveSpeed * Time.fixedDeltaTime; // Move down
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    additionalNoclipMoveSpeed++;
                    ValheimNoclipPlugin.Log.LogInfo($"{nameof(additionalNoclipMoveSpeed)}: {additionalNoclipMoveSpeed}");
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    additionalNoclipMoveSpeed--;
                    if (additionalNoclipMoveSpeed == uint.MaxValue) additionalNoclipMoveSpeed = 0;
                    ValheimNoclipPlugin.Log.LogInfo($"{nameof(additionalNoclipMoveSpeed)}: {additionalNoclipMoveSpeed}");
                }

                // Apply the new position to the Rigidbody
                rb.Value.MovePosition(newPosition);
            }
        }
    }
}