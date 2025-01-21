using System;
using UnityEngine;

namespace Controllers
{
    [Serializable]
    public class ProjectileStats
    {
        public float speed;
        public float damage;
    }
    
    public abstract class ProjectileController : MonoBehaviour
    {
        [SerializeField] private ProjectileStats projectileStats;
        
        public ProjectileStats stats => projectileStats;
        
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