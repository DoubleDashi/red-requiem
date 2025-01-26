using System;
using System.Collections;
using Configs.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class ShakeController : MonoBehaviour
    {
        [SerializeField] public AnimationCurve curve;
        [SerializeField] public float duration = 1f;

        private float _consistentStrengthPercentage = 1f;
        private bool _isConsistentShake;
        private float _consistentShakeElapsedTime;

        private void OnEnable()
        {
            CameraEventConfig.OnShake += HandleOnShake;
            CameraEventConfig.OnConsistentShakeStart += HandleOnConsistentShakeStart;
            CameraEventConfig.OnConsistentShakeStop += HandleOnConsistentShakeStop;
        }

        private void OnDisable()
        {
            CameraEventConfig.OnShake -= HandleOnShake;
            CameraEventConfig.OnConsistentShakeStart -= HandleOnConsistentShakeStart;
            CameraEventConfig.OnConsistentShakeStop -= HandleOnConsistentShakeStop;
        }

        private void Update()
        {
            if (_isConsistentShake)
            {
                Vector3 startPosition = transform.position;
                _consistentShakeElapsedTime += Time.deltaTime;
                transform.position = startPosition + Random.insideUnitSphere * 0.025f * _consistentStrengthPercentage;
                
                if (_consistentShakeElapsedTime >= duration)
                {
                    _consistentShakeElapsedTime = 0f;
                }
            }
        }

        private IEnumerator ShakeRoutine(float strengthPercentage)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                Vector3 startPosition = transform.position;
                elapsedTime += Time.deltaTime;
                float strength = curve.Evaluate(elapsedTime / duration) * 1f;
                transform.position = startPosition + (Random.insideUnitSphere * (strength * strengthPercentage));
                yield return null;
            }
        }
        
        private void HandleOnShake(float strengthPercentage)
        {
            StartCoroutine(ShakeRoutine(strengthPercentage));
        }
        
        private void HandleOnConsistentShakeStart(float strengthPercentage)
        {
            _consistentStrengthPercentage = strengthPercentage;
            _isConsistentShake = true;
            _consistentShakeElapsedTime = 0f;
        }
        
        private void HandleOnConsistentShakeStop()
        {
            _consistentStrengthPercentage = 1f;
            _isConsistentShake = false;
        }
    }
}