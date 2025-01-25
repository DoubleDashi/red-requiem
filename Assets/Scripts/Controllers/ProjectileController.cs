using Entities;
using UnityEngine;
using Utility;

namespace Controllers
{
    public abstract class ProjectileController : MonoBehaviour
    {
        private Camera _camera;
        private UnityTag _targetTag;
        
        private void Awake()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            Move();
            OutboundDetection();
            CollisionDetection();
        }
        
        private void OutboundDetection()
        {
            Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
            if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                OnOutbound();
            }
        }
        
        protected abstract void Move();
        protected abstract void OnOutbound();
        protected abstract void CollisionDetection();
    }
}