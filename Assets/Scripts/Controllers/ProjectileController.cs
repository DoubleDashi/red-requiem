using UnityEngine;

namespace Controllers
{
    public abstract class ProjectileController : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            Move();
            OutboundDetection();
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
    }
}