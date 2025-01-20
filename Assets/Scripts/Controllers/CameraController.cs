using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        
        [SerializeField] private Transform player;
        [SerializeField] private Vector2 deadzoneSize = new(2f, 2f);
        [SerializeField] private float smoothSpeed = 15f;

        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - player.position;
        }

        private void LateUpdate()
        {
            Vector3 targetPosition = player.position + _offset;
            Vector3 cameraPosition = transform.position;

            if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadzoneSize.x)
            {
                cameraPosition.x = targetPosition.x - Mathf.Sign(targetPosition.x - cameraPosition.x) * deadzoneSize.x;   
            }
            
            if (Mathf.Abs(targetPosition.y - cameraPosition.y) > deadzoneSize.y)
            {
                cameraPosition.y = targetPosition.y - Mathf.Sign(targetPosition.y - cameraPosition.y) * deadzoneSize.y;
            }

            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
        }
        
        private void OnDrawGizmos()
        {
            if (player == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Vector3 cameraPosition = transform.position;
            Vector3 deadzoneMin = cameraPosition - (Vector3)deadzoneSize / 2;
            Vector3 deadzoneMax = cameraPosition + (Vector3)deadzoneSize / 2;

            // Draw the deadzone rectangle
            Gizmos.DrawLine(new Vector3(deadzoneMin.x, deadzoneMin.y, cameraPosition.z), new Vector3(deadzoneMax.x, deadzoneMin.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMax.x, deadzoneMin.y, cameraPosition.z), new Vector3(deadzoneMax.x, deadzoneMax.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMax.x, deadzoneMax.y, cameraPosition.z), new Vector3(deadzoneMin.x, deadzoneMax.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMin.x, deadzoneMax.y, cameraPosition.z), new Vector3(deadzoneMin.x, deadzoneMin.y, cameraPosition.z));
        }
    }
}