using Player.View;
using UnityEngine;
namespace CameraFollow
{
    public class CameraFollow : MonoBehaviour
    {
        [Header(" Camera Follow Settings ")]
        public Transform target;
        public Vector3 offset = new Vector3(0, 3, -6);
        public float smoothTime = 0.2f;
        private Vector3 velocity = Vector3.zero;


        public void SetupTarget(PlayerView player)
        {
            this.target = player.transform;
        }

        // Synchronized with Moved to avoid jittering (**)
        private void FixedUpdate()
        {
            if (target == null) return;
            Vector3 desiredPosition = target.position + offset;
            // Optional: transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

            // Optional: Look at the target.
        }
    }
}
