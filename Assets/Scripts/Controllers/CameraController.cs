using System;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector2 deadzoneSize = new(2f, 2f);
        [SerializeField] private float smoothSpeed = 0.125f;

        private Vector3 _offset;
        private Vector3 _velocity;

        private void Start()
        {
            _offset = transform.position - target.position;
        }
        
        private void LateUpdate()
        {
            if (target == false)
            {
                return;
            }

            Vector3 targetPosition = target.position + _offset;
            Vector3 cameraPosition = transform.position;

            // Calculate the deadzone boundaries
            Vector3 deadzoneMin = cameraPosition - (Vector3)deadzoneSize / 2;
            Vector3 deadzoneMax = cameraPosition + (Vector3)deadzoneSize / 2;

            if (targetPosition.x < deadzoneMin.x || targetPosition.x > deadzoneMax.x)
            {
                cameraPosition.x = targetPosition.x;
            }
            
            if (targetPosition.y < deadzoneMin.y || targetPosition.y > deadzoneMax.y)
            {
                cameraPosition.y = targetPosition.y;
            }

            transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref _velocity, smoothSpeed);
        }

        private void OnDrawGizmos()
        {
            if (target == null)
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