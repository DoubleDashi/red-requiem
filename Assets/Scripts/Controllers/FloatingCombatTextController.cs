using Configs.Events;
using Entities;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class FloatingCombatTextController : MonoBehaviour
    {
        public GameObject floatingCombatTextPrefab;
        
        private void OnEnable()
        {
            FloatingCombatTextEventConfig.OnHurt += HandleOnHurt;
        }
        
        private void OnDisable()
        {
            FloatingCombatTextEventConfig.OnHurt -= HandleOnHurt;
        }
        
        private void HandleOnHurt(EntityController controller, float damage)
        {
            GameObject instance = Instantiate(floatingCombatTextPrefab, controller.transform.position, Quaternion.identity);
            instance.GetComponent<TMP_Text>().text = $"{damage}";
        }
    }
}