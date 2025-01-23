using UnityEditor.Rendering;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        
        [SerializeField] private Transform player;

        // private Vector3 _velocity = Vector3.zero;
        // private Vector3 _offset = Vector3.forward * -10f;
        // private float _smoothTime = 0.25f;
        //
        // private void LateUpdate()
        // {
        //     Vector3 targetPosition = player.position + _offset;
        //     transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        // }

        [SerializeField] private Vector2 deadzoneSize = new(2f, 2f);
        [SerializeField] private float smoothSpeed = 15f;
        
        private Vector3 _offset;
        private Vector3 _targetPosition;

        private void Start()
        {
            _offset = transform.position - player.position;
        }
        
        private void FixedUpdate()
        {
            _targetPosition = player.position + _offset;
        }
        
        private void LateUpdate()
        {
            Vector3 cameraPosition = transform.position;
        
            if (Mathf.Abs(_targetPosition.x - cameraPosition.x) > deadzoneSize.x)
            {
                cameraPosition.x = _targetPosition.x - Mathf.Sign(_targetPosition.x - cameraPosition.x) * deadzoneSize.x;   
            }
        
            if (Mathf.Abs(_targetPosition.y - cameraPosition.y) > deadzoneSize.y)
            {
                cameraPosition.y = _targetPosition.y - Mathf.Sign(_targetPosition.y - cameraPosition.y) * deadzoneSize.y;
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
            Vector3 deadzoneMin = cameraPosition - (Vector3)deadzoneSize;
            Vector3 deadzoneMax = cameraPosition + (Vector3)deadzoneSize;
        
            // Draw the deadzone rectangle
            Gizmos.DrawLine(new Vector3(deadzoneMin.x, deadzoneMin.y, cameraPosition.z), new Vector3(deadzoneMax.x, deadzoneMin.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMax.x, deadzoneMin.y, cameraPosition.z), new Vector3(deadzoneMax.x, deadzoneMax.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMax.x, deadzoneMax.y, cameraPosition.z), new Vector3(deadzoneMin.x, deadzoneMax.y, cameraPosition.z));
            Gizmos.DrawLine(new Vector3(deadzoneMin.x, deadzoneMax.y, cameraPosition.z), new Vector3(deadzoneMin.x, deadzoneMin.y, cameraPosition.z));
        }
    }
}