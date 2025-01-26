using TMPro;
using UnityEngine;

namespace Projectiles
{
    public class FloatingCombatTextProjectile : MonoBehaviour
    {
        public float speed;
        public float duration;
        public AnimationCurve sizeOverTime;
        public AnimationCurve alphaOverTime;

        private Vector2 _direction;
        private float _elapsedTime;
        private TMP_Text _text;

        private void Start()
        {
            Destroy(gameObject, duration);

            _direction = Random.insideUnitCircle.normalized;
            _elapsedTime = 0f;
            _text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
            transform.position += (Vector3)_direction * (speed * Time.deltaTime);
            
            float scale = sizeOverTime.Evaluate(_elapsedTime / duration);
            transform.localScale = new Vector3(scale, scale, scale);
            
            Color color = _text.color;
            color.a = alphaOverTime.Evaluate(_elapsedTime / duration);
            _text.color = color;
        }
    }
}